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
        /// The ID for the getContent operation.
        /// </summary>
        public const string GetWorkflowContentOperationId = "getWorkflowContent";

        private readonly ITenantedContentStoreFactory contentStoreFactory;
        private readonly ContentWithStateMapper contentWithStateMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowContentService"/> class.
        /// </summary>
        /// <param name="contentStoreFactory">The content store for the service.</param>
        /// <param name="contentWithStateMapper">The mapper for <see cref="ContentWithState"/> results.</param>
        public WorkflowContentService(
            ITenantedContentStoreFactory contentStoreFactory,
            ContentWithStateMapper contentWithStateMapper)
        {
            this.contentStoreFactory = contentStoreFactory;
            this.contentWithStateMapper = contentWithStateMapper;
        }

        /// <summary>
        /// Get the content at the slug with the given ID, combined with state information from the
        /// workflow with the given ID.
        /// </summary>
        /// <param name="context">The context for the request.</param>
        /// <param name="slug">The slug at which to create the content.</param>
        /// <param name="workflowId">The Id of the workflow the content is part of.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [OperationId(GetWorkflowContentOperationId)]
        public async Task<OpenApiResult> GetWorkflowContent(IOpenApiContext context, string slug, string workflowId)
        {
            IContentStore contentStore = await this.contentStoreFactory.GetContentStoreForTenantAsync(context.CurrentTenantId).ConfigureAwait(false);

            ContentWithState result = await contentStore.GetContentForWorkflowAsync(slug, workflowId).ConfigureAwait(false);

            HalDocument resultDocument = this.contentWithStateMapper.Map(result, context);

            return this.OkResult(resultDocument);
        }
    }
}
