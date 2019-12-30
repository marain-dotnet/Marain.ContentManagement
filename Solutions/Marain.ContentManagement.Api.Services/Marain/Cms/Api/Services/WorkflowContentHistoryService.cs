// <copyright file="WorkflowContentHistoryService.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Api.Services
{
    using System.Threading.Tasks;
    using Marain.Cms.Api.Services.Internal;
    using Menes;
    using Menes.Hal;

    /// <summary>
    /// Handles requests to create and retrieve content state history information.
    /// </summary>
    public class WorkflowContentHistoryService : IOpenApiService
    {
        /// <summary>
        /// The Id for the getWorkflowHistory operation.
        /// </summary>
        public const string GetWorkflowHistoryOperationId = "getWorkflowHistory";

        /// <summary>
        /// The Id for the getWorkflowStateHistory operation.
        /// </summary>
        public const string GetWorkflowStateHistoryOperationId = "getWorkflowStateHistory";

        private readonly ITenantedContentStoreFactory contentStoreFactory;
        private readonly ContentSummariesWithStateMapper contentSummariesWithStateMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowContentHistoryService"/> class.
        /// </summary>
        /// <param name="contentStoreFactory">The content store for the service.</param>
        /// <param name="contentSummariesWithStateMapper">The mapper for <see cref="ContentSummariesWithState"/> results.</param>
        public WorkflowContentHistoryService(
            ITenantedContentStoreFactory contentStoreFactory,
            ContentSummariesWithStateMapper contentSummariesWithStateMapper)
        {
            this.contentStoreFactory = contentStoreFactory;
            this.contentSummariesWithStateMapper = contentSummariesWithStateMapper;
        }

        /// <summary>
        /// Get the content at the slug with the given ID.
        /// </summary>
        /// <param name="tenantId">The ID of the tenant.</param>
        /// <param name="slug">The slug for the content.</param>
        /// <param name="workflowId">The Id of the workflow the content is part of.</param>
        /// <param name="limit">The maximum number of items to return in the batch.</param>
        /// <param name="continuationToken">The continuation token for the next batch.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [OperationId(GetWorkflowHistoryOperationId)]
        public async Task<OpenApiResult> GetWorkflowHistory(string tenantId, string slug, string workflowId, int? limit, string continuationToken)
        {
            IContentStore contentStore = await this.contentStoreFactory.GetContentStoreForTenantAsync(tenantId).ConfigureAwait(false);

            ContentSummariesWithState result = await contentStore.GetContentSummariesForWorkflowAsync(slug, workflowId, null, limit ?? 20, continuationToken).ConfigureAwait(false);

            var mappingContext = new ContentSummariesWithStateMappingContext
            {
                TargetOperationId = GetWorkflowHistoryOperationId,
                ContinuationToken = continuationToken,
                Limit = limit,
                Slug = slug,
                TenantId = tenantId,
                WorkflowId = workflowId,
            };

            HalDocument resultDocument = this.contentSummariesWithStateMapper.Map(result, mappingContext);

            return this.OkResult(resultDocument);
        }

        /// <summary>
        /// Get the content at the slug with the given ID.
        /// </summary>
        /// <param name="tenantId">The ID of the tenant.</param>
        /// <param name="slug">The slug for the content.</param>
        /// <param name="workflowId">The Id of the workflow the content is part of.</param>
        /// <param name="stateName">The workflow state to limit the results to.</param>
        /// <param name="limit">The maximum number of items to return in the batch.</param>
        /// <param name="continuationToken">The continuation token for the next batch.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [OperationId(GetWorkflowStateHistoryOperationId)]
        public async Task<OpenApiResult> GetWorkflowStateHistory(string tenantId, string slug, string workflowId, string stateName, int? limit, string continuationToken)
        {
            IContentStore contentStore = await this.contentStoreFactory.GetContentStoreForTenantAsync(tenantId).ConfigureAwait(false);

            ContentSummariesWithState result = await contentStore.GetContentSummariesForWorkflowAsync(slug, workflowId, stateName, limit ?? 20, continuationToken).ConfigureAwait(false);

            var mappingContext = new ContentSummariesWithStateMappingContext
            {
                TargetOperationId = GetWorkflowStateHistoryOperationId,
                ContinuationToken = continuationToken,
                Limit = limit,
                Slug = slug,
                TenantId = tenantId,
                WorkflowId = workflowId,
                StateName = stateName,
            };

            HalDocument resultDocument = this.contentSummariesWithStateMapper.Map(result, mappingContext);

            return this.OkResult(resultDocument);
        }
    }
}
