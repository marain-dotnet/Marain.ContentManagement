// <copyright file="ContentService.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Api.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Marain.Cms.Api.Services.Internal;
    using Menes;
    using Menes.Hal;
    using Menes.Links;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Handles requests to create and retrieve content.
    /// </summary>
    public class ContentService : IOpenApiService
    {
        /// <summary>
        /// The ID for the createContent operation.
        /// </summary>
        public const string CreateContentOperationId = "createContent";

        /// <summary>
        /// The ID for the getContent operation.
        /// </summary>
        public const string GetContentOperationId = "getContent";

        /// <summary>
        /// Uri template passed to <c>OpenApiClaimsServiceCollectionExtensions.AddRoleBasedOpenApiAccessControlWithPreemptiveExemptions</c>
        /// to distinguish between rules defining access control policy for the ContentManagement service vs those for other services.
        /// </summary>
        public const string ContentManagementResourceTemplate = "{tenantId}/marain/content/";

        private readonly ITenantedContentStoreFactory contentStoreFactory;
        private readonly IHalDocumentFactory halDocumentFactory;
        private readonly ContentMapper contentMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentService"/> class.
        /// </summary>
        /// <param name="contentStoreFactory">The content store for the service.</param>
        /// <param name="halDocumentFactory">The HAL document factory for the service.</param>
        /// <param name="contentMapper">The mapper for <see cref="Content"/> responses.</param>
        public ContentService(
            ITenantedContentStoreFactory contentStoreFactory,
            IHalDocumentFactory halDocumentFactory,
            ContentMapper contentMapper)
        {
            this.contentStoreFactory = contentStoreFactory;
            this.halDocumentFactory = halDocumentFactory;
            this.contentMapper = contentMapper;
        }

        /// <summary>
        /// Create the given content at the slug.
        /// </summary>
        /// <param name="context">The context for the request.</param>
        /// <param name="slug">The slug at which to create the content.</param>
        /// <param name="body">The content.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [OperationId(CreateContentOperationId)]
        public async Task<OpenApiResult> CreateContent(IOpenApiContext context, string slug, Content body)
        {
            IContentStore contentStore = await this.contentStoreFactory.GetContentStoreForTenantAsync(context.CurrentTenantId).ConfigureAwait(false);

            body.Slug = slug;
            Content result = await contentStore.StoreContentAsync(body).ConfigureAwait(false);

            HalDocument response = this.contentMapper.Map(result, context);

            WebLink location = response.GetLinksForRelation("self").First();
            return this.CreatedResult(location.Href, response);
        }

        /// <summary>
        /// Get the content at the slug with the given ID.
        /// </summary>
        /// <param name="tenantId">The ID of the tenant.</param>
        /// <param name="slug">The slug at which to create the content.</param>
        /// <param name="contentId">The contentId for the content at the slug.</param>
        /// <param name="ifNoneMatch">The value from the If-None-Match header, if provided.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [OperationId(GetContentOperationId)]
        public async Task<OpenApiResult> GetContent(string tenantId, string slug, string contentId, string ifNoneMatch)
        {
            IContentStore contentStore = await this.contentStoreFactory.GetContentStoreForTenantAsync(tenantId).ConfigureAwait(false);

            Content result = await contentStore.GetContentAsync(contentId, slug).ConfigureAwait(false);

            // If the etag in the result matches ifNoneMatch then we return 304 Not Modified
            if (!string.IsNullOrEmpty(ifNoneMatch) && result.ETag == ifNoneMatch)
            {
                return this.NotModifiedResult();
            }

            HalDocument resultDocument = this.halDocumentFactory.CreateHalDocumentFrom(result);

            OpenApiResult response = this.OkResult(resultDocument);
            response.Results.Add(HeaderNames.ETag, result.ETag);

            return response;
        }
    }
}
