// <copyright file="ContentService.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Api.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Marain.Cms.Api.Services.Dtos;
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

        private readonly ITenantedContentStoreFactory contentStoreFactory;
        private readonly ContentResponseMapper contentMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentService"/> class.
        /// </summary>
        /// <param name="contentStoreFactory">The content store for the service.</param>
        /// <param name="contentMapper">The mapper for <see cref="Content"/> responses.</param>
        public ContentService(
            ITenantedContentStoreFactory contentStoreFactory,
            ContentResponseMapper contentMapper)
        {
            this.contentStoreFactory = contentStoreFactory;
            this.contentMapper = contentMapper;
        }

        /// <summary>
        /// Create the given content at the slug.
        /// </summary>
        /// <param name="tenantId">The tenantId for the request.</param>
        /// <param name="slug">The slug at which to create the content.</param>
        /// <param name="body">The content.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [OperationId(CreateContentOperationId)]
        public async Task<OpenApiResult> CreateContent(string tenantId, string slug, CreateContentRequest body)
        {
            IContentStore contentStore = await this.contentStoreFactory.GetContentStoreForTenantAsync(tenantId).ConfigureAwait(false);

            Content request = body.AsContent(slug);

            Content result = await contentStore.StoreContentAsync(request).ConfigureAwait(false);

            string etag = EtagHelper.BuildEtag(nameof(Content), result.ETag);

            HalDocument resultDocument = this.contentMapper.Map(result, new ResponseMappingContext { TenantId = tenantId });

            WebLink location = resultDocument.GetLinksForRelation("self").First();

            OpenApiResult response = this.CreatedResult(location.Href, resultDocument);
            response.Results.Add(HeaderNames.ETag, etag);

            return response;
        }

        /// <summary>
        /// Get the content at the slug with the given ID.
        /// </summary>
        /// <param name="tenantId">The tenantId for the request.</param>
        /// <param name="slug">The slug at which to create the content.</param>
        /// <param name="contentId">The contentId for the content at the slug.</param>
        /// <param name="ifNoneMatch">The value from the If-None-Match header, if provided.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [OperationId(GetContentOperationId)]
        public async Task<OpenApiResult> GetContent(string tenantId, string slug, string contentId, string ifNoneMatch)
        {
            IContentStore contentStore = await this.contentStoreFactory.GetContentStoreForTenantAsync(tenantId).ConfigureAwait(false);

            Content result = await contentStore.GetContentAsync(contentId, slug).ConfigureAwait(false);

            string etag = EtagHelper.BuildEtag(nameof(Content), result.ETag);

            // If the etag in the result matches ifNoneMatch then we return 304 Not Modified
            if (EtagHelper.IsMatch(ifNoneMatch, etag))
            {
                return this.NotModifiedResult();
            }

            HalDocument resultDocument = this.contentMapper.Map(result, new ResponseMappingContext { TenantId = tenantId });

            OpenApiResult response = this.OkResult(resultDocument);
            response.Results.Add(HeaderNames.ETag, etag);

            // Since content is immutable we can allow clients to cache it indefinitely.
            response.Results.Add(HeaderNames.CacheControl, Constants.CacheControlHeaderOptions.NeverExpire);

            return response;
        }
    }
}
