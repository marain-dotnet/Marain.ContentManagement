// <copyright file="WorkflowHistoryService.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Api.Services
{
    using System.Threading.Tasks;
    using Marain.Cms.Api.Services.Internal;
    using Menes;
    using Menes.Hal;

    /// <summary>
    /// Handles requests to create and retrieve content with state information.
    /// </summary>
    public class WorkflowHistoryService : IOpenApiService
    {
        /// <summary>
        /// The ID for the getWorkflowHistory operation.
        /// </summary>
        public const string GetWorkflowHistoryOperationId = "getWorkflowHistory";

        /// <summary>
        /// The ID for the getWorkflowStateHistory operation.
        /// </summary>
        public const string GetWorkflowStateHistoryOperationId = "getWorkflowStateHistory";

        private readonly ITenantedContentStoreFactory contentStoreFactory;
        private readonly ContentStatesResponseMapper contentStatesMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowHistoryService"/> class.
        /// </summary>
        /// <param name="contentStoreFactory">The content store for the service.</param>
        /// <param name="contentStatesMapper">Response mapper for content states.</param>
        public WorkflowHistoryService(
            ITenantedContentStoreFactory contentStoreFactory,
            ContentStatesResponseMapper contentStatesMapper)
        {
            this.contentStoreFactory = contentStoreFactory;
            this.contentStatesMapper = contentStatesMapper;
        }

        /// <summary>
        /// Get the content at the slug with the given ID, combined with state information from the
        /// workflow with the given ID.
        /// </summary>
        /// <param name="tenantId">The tenant Id for the request.</param>
        /// <param name="workflowId">The Id of the workflow the content is part of.</param>
        /// <param name="slug">The slug at which to create the content.</param>
        /// <param name="limit">The maximum number of items to return in the batch.</param>
        /// <param name="continuationToken">The continuation token for the next batch.</param>
        /// <param name="embed">The name of a link relation to embed in the response.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [OperationId(GetWorkflowHistoryOperationId)]
        public Task<OpenApiResult> GetWorkflowHistory(
            string tenantId,
            string workflowId,
            string slug,
            int? limit,
            string continuationToken,
            string embed) => this.GetWorkflowStateHistory(tenantId, workflowId, null, slug, limit, continuationToken, embed);

        /// <summary>
        /// Get the content at the slug with the given ID, combined with state information from the
        /// workflow with the given ID.
        /// </summary>
        /// <param name="tenantId">The tenant Id for the request.</param>
        /// <param name="workflowId">The Id of the workflow the content is part of.</param>
        /// <param name="stateName">The name of the state to retrieve history for.</param>
        /// <param name="slug">The slug at which to create the content.</param>
        /// <param name="limit">The maximum number of items to return in the batch.</param>
        /// <param name="continuationToken">The continuation token for the next batch.</param>
        /// <param name="embed">The name of a link relation to embed in the response.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [OperationId(GetWorkflowStateHistoryOperationId)]
        public async Task<OpenApiResult> GetWorkflowStateHistory(
            string tenantId,
            string workflowId,
            string stateName,
            string slug,
            int? limit,
            string continuationToken,
            string embed)
        {
            IContentStore contentStore = await this.contentStoreFactory.GetContentStoreForTenantAsync(tenantId).ConfigureAwait(false);

            ContentStates result = await contentStore.GetContentStatesForWorkflowAsync(slug, workflowId, stateName, limit ?? 20, continuationToken).ConfigureAwait(false);

            var mappingContext = new ContentStatesResponseMappingContext { TenantId = tenantId };

            if (embed == Constants.LinkRelations.ContentSummary)
            {
                mappingContext.ContentSummaries = await contentStore.GetContentSummariesForStatesAsync(result.States).ConfigureAwait(false);
            }

            HalDocument resultDocument = this.contentStatesMapper.Map(result, mappingContext);

            return this.OkResult(resultDocument);
        }
    }
}
