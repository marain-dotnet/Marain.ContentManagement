// <copyright file="ContentSummaryService.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Api.Services
{
    using System.Threading.Tasks;
    using Marain.Cms.Api.Services.Internal;
    using Menes;
    using Menes.Hal;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Handles request for content summary items.
    /// </summary>
    public class ContentSummaryService : IOpenApiService
    {
        /// <summary>
        /// The ID for the getContentSummary operation.
        /// </summary>
        public const string GetContentSummaryOperationId = "getContentSummary";

        private readonly ITenantedContentStoreFactory contentStoreFactory;
        private readonly ContentSummaryResponseMapper contentSummaryMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentService"/> class.
        /// </summary>
        /// <param name="contentStoreFactory">The content store for the service.</param>
        /// <param name="contentSummaryMapper">The mapper for <see cref="ContentSummary"/> responses.</param>
        public ContentSummaryService(
            ITenantedContentStoreFactory contentStoreFactory,
            ContentSummaryResponseMapper contentSummaryMapper)
        {
            this.contentStoreFactory = contentStoreFactory;
            this.contentSummaryMapper = contentSummaryMapper;
        }

        /// <summary>
        /// Get the content at the slug with the given ID.
        /// </summary>
        /// <param name="context">The context for the request.</param>
        /// <param name="slug">The slug at which to create the content.</param>
        /// <param name="contentId">The contentId for the content at the slug.</param>
        /// <param name="ifNoneMatch">The value from the If-None-Match header, if provided.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [OperationId(GetContentSummaryOperationId)]
        public async Task<OpenApiResult> GetContentSummary(IOpenApiContext context, string slug, string contentId, string ifNoneMatch)
        {
            IContentStore contentStore = await this.contentStoreFactory.GetContentStoreForTenantAsync(context.CurrentTenantId).ConfigureAwait(false);

            ContentSummary result = await contentStore.GetContentSummaryAsync(contentId, slug).ConfigureAwait(false);

            string etag = EtagHelper.BuildEtag(nameof(ContentSummary), result.ETag);

            // If the etag in the result matches ifNoneMatch then we return 304 Not Modified
            if (EtagHelper.IsMatch(ifNoneMatch, etag))
            {
                return this.NotModifiedResult();
            }

            HalDocument resultDocument = this.contentSummaryMapper.Map(result, new ResponseMappingContext { TenantId = context.CurrentTenantId });

            OpenApiResult response = this.OkResult(resultDocument);
            response.Results.Add(HeaderNames.ETag, etag);
            response.Results.Add(HeaderNames.CacheControl, "max-age=31536000");

            return response;
        }
    }
}
