// <copyright file="ContentHistoryService.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Api.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Marain.Cms.Api.Services.Internal;
    using Menes;
    using Menes.Hal;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Handles requests for content history.
    /// </summary>
    public class ContentHistoryService : IOpenApiService
    {
        /// <summary>
        /// The ID for the getContentHistory operation.
        /// </summary>
        public const string GetContentHistoryOperationId = "getContentHistory";

        private readonly ITenantedContentStoreFactory contentStoreFactory;
        private readonly ContentSummariesResponseMapper contentSummariesMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentService"/> class.
        /// </summary>
        /// <param name="contentStoreFactory">The content store for the service.</param>
        /// <param name="contentSummariesMapper">The mapper for <see cref="ContentSummaries"/> responses.</param>
        public ContentHistoryService(
            ITenantedContentStoreFactory contentStoreFactory,
            ContentSummariesResponseMapper contentSummariesMapper)
        {
            this.contentStoreFactory = contentStoreFactory;
            this.contentSummariesMapper = contentSummariesMapper;
        }

        /// <summary>
        /// Get the content at the slug with the given ID.
        /// </summary>
        /// <param name="tenantId">The ID of the tenant.</param>
        /// <param name="slug">The slug for the content.</param>
        /// <param name="limit">The maximum number of items to return in the batch.</param>
        /// <param name="continuationToken">The continuation token for the next batch.</param>
        /// <param name="ifNoneMatch">The ETag of the last known version.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [OperationId(GetContentHistoryOperationId)]
        public async Task<OpenApiResult> GetContentHistory(string tenantId, string slug, int? limit, string continuationToken, string ifNoneMatch)
        {
            IContentStore contentStore = await this.contentStoreFactory.GetContentStoreForTenantAsync(tenantId).ConfigureAwait(false);

            ContentSummaries result = await contentStore.GetContentSummariesAsync(slug, limit ?? 20, continuationToken).ConfigureAwait(false);

            string resultEtag = EtagHelper.BuildEtag(nameof(ContentSummary), result.Summaries.Select(x => x.ETag).ToArray());

            // If the etag in the result matches ifNoneMatch then we return 304 Not Modified
            if (EtagHelper.IsMatch(ifNoneMatch, resultEtag))
            {
                return this.NotModifiedResult();
            }

            var mappingContext = new ContentSummariesResponseMappingContext
            {
                ContinuationToken = continuationToken,
                Limit = limit,
                Slug = slug,
                TenantId = tenantId,
            };

            HalDocument resultDocument = this.contentSummariesMapper.Map(result, mappingContext);

            OpenApiResult response = this.OkResult(resultDocument);
            response.Results.Add(HeaderNames.ETag, resultEtag);

            return response;
        }
    }
}
