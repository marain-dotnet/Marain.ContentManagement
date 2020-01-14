// <copyright file="WorkflowContentService.cs" company="Endjin Limited">
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
    public class WorkflowContentService : IOpenApiService
    {
        /// <summary>
        /// The ID for the getWorkflowState operation.
        /// </summary>
        public const string GetWorkflowStateOperationId = "getWorkflowState";

        private readonly ITenantedContentStoreFactory contentStoreFactory;
        private readonly ContentStateResponseMapper contentStateMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowContentService"/> class.
        /// </summary>
        /// <param name="contentStoreFactory">The content store for the service.</param>
        /// <param name="contentStateMapper">The mapper for <see cref="ContentState"/> results.</param>
        public WorkflowContentService(
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
        /// <param name="context">The context for the request.</param>
        /// <param name="slug">The slug at which to create the content.</param>
        /// <param name="workflowId">The Id of the workflow the content is part of.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [OperationId(GetWorkflowStateOperationId)]
        public async Task<OpenApiResult> GetWorkflowState(IOpenApiContext context, string slug, string workflowId)
        {
            IContentStore contentStore = await this.contentStoreFactory.GetContentStoreForTenantAsync(context.CurrentTenantId).ConfigureAwait(false);

            ContentState result = await contentStore.GetContentStateForWorkflowAsync(slug, workflowId).ConfigureAwait(false);

            HalDocument resultDocument = this.contentStateMapper.Map(result, context);

            return this.OkResult(resultDocument);
        }
    }
}
