// <copyright file="ContentManagementService.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.OpenApi
{
    using System;
    using System.Threading.Tasks;
    using Menes;
    using Menes.Hal;

    /// <summary>
    /// Handles content management requests.
    /// </summary>
    [EmbeddedOpenApiDefinition("Marain.ContentManagement.OpenApi.ContentManagementServices.yaml")]
    public class ContentManagementService : IOpenApiService
    {
        /// <summary>
        /// The ID for the createContent operation.
        /// </summary>
        public const string CreateContentOperationId = "createContent";

        /// <summary>
        /// Uri template passed to <c>OpenApiClaimsServiceCollectionExtensions.AddRoleBasedOpenApiAccessControlWithPreemptiveExemptions</c>
        /// to distinguish between rules defining access control policy for the ContentManagement service vs those for other services.
        /// </summary>
        public const string ContentManagementResourceTemplate = "{tenantId}/marain/content/";
        private readonly IContentStore contentStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentManagementService"/> class.
        /// </summary>
        /// <param name="contentStore">The content store for the service.</param>
        public ContentManagementService(IContentStore contentStore)
        {
            this.contentStore = contentStore;
        }

        /// <summary>
        /// Create the given content at the slug.
        /// </summary>
        /// <param name="tenantId">The ID of the tenant.</param>
        /// <param name="slug">The slug at which to create the content.</param>
        /// <param name="body">The content.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [OperationId("createContent")]
        public Task<OpenApiResult> CreateContent(string tenantId, string slug, HalDocument body)
        {
            throw new NotImplementedException();
        }
      }
}
