// <copyright file="WorkflowStateService.cs" company="Endjin Limited">
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
    public class WorkflowStateService : IOpenApiService
    {
        /// <summary>
        /// The ID for the getWorkflowState operation.
        /// </summary>
        public const string GetWorkflowStateOperationId = "getWorkflowState";

        private readonly ITenantedContentStoreFactory contentStoreFactory;
        private readonly ContentStateResponseMapper contentStateMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowStateService"/> class.
        /// </summary>
        /// <param name="contentStoreFactory">The content store for the service.</param>
        /// <param name="contentStateMapper">The mapper for <see cref="ContentState"/> results.</param>
        public WorkflowStateService(
            ITenantedContentStoreFactory contentStoreFactory,
            ContentStateResponseMapper contentStateMapper)
        {
            this.contentStoreFactory = contentStoreFactory;
            this.contentStateMapper = contentStateMapper;
        }

        /// <summary>
        /// Get the content at the slug with the given ID, combined with state information from the
        /// workflow with the given ID.
        /// </summary>
        /// <param name="tenantId">The tenant Id for the request.</param>
        /// <param name="slug">The slug at which to create the content.</param>
        /// <param name="workflowId">The Id of the workflow the content is part of.</param>
        /// <param name="embed">The name of a link relation to embed in the response.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [OperationId(GetWorkflowStateOperationId)]
        public async Task<OpenApiResult> GetWorkflowState(string tenantId, string slug, string workflowId, string embed)
        {
            IContentStore contentStore = await this.contentStoreFactory.GetContentStoreForTenantAsync(tenantId).ConfigureAwait(false);

            ContentState result = await contentStore.GetContentStateForWorkflowAsync(slug, workflowId).ConfigureAwait(false);

            var mappingContext = new ContentStateResponseMappingContext { TenantId = tenantId, Embed = embed };

            if (embed == Constants.LinkRelations.Content)
            {
                mappingContext.Content = await contentStore.GetContentAsync(result.ContentId, result.Slug).ConfigureAwait(false);
            }
            else if (embed == Constants.LinkRelations.ContentSummary)
            {
                mappingContext.ContentSummary = await contentStore.GetContentSummaryAsync(result.ContentId, result.Slug).ConfigureAwait(false);
            }

            HalDocument resultDocument = await this.contentStateMapper.MapAsync(result, mappingContext);

            return this.OkResult(resultDocument);
        }
    }
}
