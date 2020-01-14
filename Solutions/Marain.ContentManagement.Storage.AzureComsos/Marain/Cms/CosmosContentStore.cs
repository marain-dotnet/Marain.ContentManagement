// <copyright file="CosmosContentStore.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Corvus.Extensions;
    using Microsoft.Azure.Cosmos;

    /// <summary>
    /// A Cosmos DB implementation of the content store.
    /// </summary>
    public class CosmosContentStore : IContentStore
    {
        private const string ContentSummaryProperties = "c.id, c._etag, c.categoryPaths, c.tags, c.slug, c.author, c.title, c.description, c.culture";

        private static readonly string ContentQuery =
            $"SELECT {ContentSummaryProperties} FROM c WHERE c.slug = @slug AND c.contentType = '{Content.RegisteredContentType}' ORDER BY c._ts DESC";

        private static readonly string ContentStateQuery =
            $"SELECT * FROM c WHERE c.contentType = '{ContentState.RegisteredContentType}' AND c.workflowId = @workflowId AND c.slug = @slug ORDER BY c._ts DESC";

        private static readonly string ContentStateQueryWithStateNameFilter =
            $"SELECT * FROM c WHERE c.contentType = '{ContentState.RegisteredContentType}' AND c.workflowId = @workflowId AND c.slug = @slug AND c.stateName = @stateName ORDER BY c._ts DESC";

        private static readonly string SingleContentStateQuery =
            $"SELECT TOP 1 * FROM c WHERE c.contentType = '{ContentState.RegisteredContentType}' AND c.workflowId = @workflowId AND c.slug = @slug ORDER BY c._ts DESC";

        private readonly Container container;

        /// <summary>
        /// Initializes a new instance of the <see cref="CosmosContentStore"/> class.
        /// </summary>
        /// <param name="container">The container for drafts.</param>
        public CosmosContentStore(Container container)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));
        }

        /// <inheritdoc/>
        public async Task<Content> StoreContentAsync(Content content)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            try
            {
                ItemResponse<Content> response = await this.container.CreateItemAsync(content, new PartitionKey(content.PartitionKey)).ConfigureAwait(false);
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
            {
                throw new ContentConflictException(content.Id, content.Slug, ex);
            }
        }

        /// <inheritdoc/>
        public async Task<ContentState> SetContentWorkflowStateAsync(
            string slug,
            string contentId,
            string workflowId,
            string stateName,
            CmsIdentity stateChangedBy)
        {
            if (slug is null)
            {
                throw new ArgumentNullException(nameof(slug));
            }

            if (string.IsNullOrEmpty(contentId))
            {
                throw new ArgumentException("message", nameof(contentId));
            }

            if (string.IsNullOrEmpty(workflowId))
            {
                throw new ArgumentException("message", nameof(workflowId));
            }

            if (string.IsNullOrEmpty(stateName))
            {
                throw new ArgumentException("message", nameof(stateName));
            }

            ItemResponse<ContentState> response = await this.container.CreateItemAsync(
                new ContentState { Slug = slug, ContentId = contentId, StateName = stateName, WorkflowId = workflowId, ChangedBy = stateChangedBy },
                new PartitionKey(PartitionKeyHelper.GetPartitionKeyFromSlug(slug))).ConfigureAwait(false);

            return response.Resource;
        }

        /// <inheritdoc/>
        public async Task<ContentState> GetContentStateForWorkflowAsync(string slug, string workflowId)
        {
            if (slug is null)
            {
                throw new ArgumentNullException(nameof(slug));
            }

            if (string.IsNullOrEmpty(workflowId))
            {
                throw new ArgumentException("message", nameof(workflowId));
            }

            QueryDefinition queryDefinition =
                new QueryDefinition(SingleContentStateQuery)
                    .WithParameter("@slug", slug)
                    .WithParameter("@workflowId", workflowId);

            FeedIterator<ContentState> iterator = this.container.GetItemQueryIterator<ContentState>(queryDefinition, null, new QueryRequestOptions { MaxItemCount = 1 });

            if (iterator.HasMoreResults)
            {
                FeedResponse<ContentState> results = await iterator.ReadNextAsync().ConfigureAwait(false);
                ContentState state = results.Resource.FirstOrDefault();
                if (!(state is null))
                {
                    return state;
                }
            }

            throw new ContentNotFoundException();
        }

        /// <inheritdoc/>
        public Task<Content> GetContentAsync(string contentId, string slug)
        {
            return this.GetContentAsync<Content>(contentId, slug);
        }

        /// <inheritdoc/>
        public async Task<ContentSummary> GetContentSummaryAsync(string contentId, string slug)
        {
            try
            {
                ItemResponse<ContentSummary> response = await this.container.ReadItemAsync<ContentSummary>(
                    contentId,
                    new PartitionKey(PartitionKeyHelper.GetPartitionKeyFromSlug(slug))).ConfigureAwait(false);

                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ContentNotFoundException("Content not found.", ex);
            }
        }

        /// <inheritdoc/>
        public async Task<ContentStates> GetContentStatesForWorkflowAsync(string slug, string workflowId, string stateName = null, int limit = 20, string continuationToken = null)
        {
            if (slug is null)
            {
                throw new ArgumentNullException(nameof(slug));
            }

            if (string.IsNullOrEmpty(workflowId))
            {
                throw new ArgumentException("message", nameof(workflowId));
            }

            QueryDefinition queryDefinition =
                           GetWorkflowStateQueryDefinition(slug, workflowId, stateName);

            FeedIterator<ContentState> iterator = this.container.GetItemQueryIterator<ContentState>(queryDefinition, continuationToken, new QueryRequestOptions { MaxItemCount = limit });

            var summaries = new ContentStates();

            if (iterator.HasMoreResults)
            {
                FeedResponse<ContentState> results = await iterator.ReadNextAsync().ConfigureAwait(false);
                summaries.ContinuationToken = results.ContinuationToken;
                summaries.States.AddRange(results.Resource);
            }

            return summaries;
        }

        /// <inheritdoc/>
        public async Task<ContentSummaries> GetContentSummariesAsync(string slug, int limit = 20, string continuationToken = null)
        {
            if (slug is null)
            {
                throw new ArgumentNullException(nameof(slug));
            }

            QueryDefinition queryDefinition =
                           new QueryDefinition(ContentQuery)
                               .WithParameter("@slug", slug);

            return await this.GetContentSummariesAsync(limit, continuationToken, queryDefinition).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<List<ContentSummary>> GetContentSummariesForStatesAsync(IList<ContentState> states)
        {
            // All of the states should have the same slug.
            if (states.Distinct(x => x.Slug).Count() != 1)
            {
                throw new ArgumentException("All of the supplied states should be for the same content slug.", nameof(states));
            }

            var queryBuilder = new StringBuilder("SELECT ");
            queryBuilder.Append(ContentSummaryProperties);
            queryBuilder.Append(" FROM c WHERE c.slug = @slug AND c.contentType = '");
            queryBuilder.Append(Content.RegisteredContentType);
            queryBuilder.Append("' AND ARRAY_CONTAINS([");
            IList<string> contentIds = states.Select(s => s.ContentId).Distinct().ToList();
            Enumerable.Range(0, contentIds.Count).ForEach(i =>
            {
                queryBuilder.Append("@id");
                queryBuilder.Append(i);
                if (i < contentIds.Count - 1)
                {
                    queryBuilder.Append(", ");
                }
            });
            queryBuilder.Append("], c.id)");

            QueryDefinition queryDefinition =
                           new QueryDefinition(queryBuilder.ToString())
                               .WithParameter("@slug", states[0].Slug);

            contentIds.ForEachAtIndex((s, i) => queryDefinition = queryDefinition.WithParameter($"@id{i}", s));

            ContentSummaries summaries = await this.GetContentSummariesAsync(states.Count, null, queryDefinition).ConfigureAwait(false);

            return summaries.Summaries;
        }

        private static QueryDefinition GetWorkflowStateQueryDefinition(string slug, string workflowId, string stateName)
        {
            if (string.IsNullOrEmpty(stateName))
            {
                return new QueryDefinition(ContentStateQuery)
                                               .WithParameter("@slug", slug)
                                               .WithParameter("@workflowId", workflowId);
            }
            else
            {
                return new QueryDefinition(ContentStateQueryWithStateNameFilter)
                                               .WithParameter("@slug", slug)
                                               .WithParameter("@workflowId", workflowId)
                                               .WithParameter("@stateName", stateName);
            }
        }

        private async Task<ContentSummaries> GetContentSummariesAsync(int limit, string continuationToken, QueryDefinition queryDefinition)
        {
            FeedIterator<ContentSummary> iterator = this.container.GetItemQueryIterator<ContentSummary>(queryDefinition, continuationToken, new QueryRequestOptions { MaxItemCount = limit });

            var summaries = new ContentSummaries();

            if (iterator.HasMoreResults)
            {
                FeedResponse<ContentSummary> results = await iterator.ReadNextAsync().ConfigureAwait(false);
                summaries.ContinuationToken = results.ContinuationToken;
                summaries.Summaries.AddRange(results.Resource);
            }

            return summaries;
        }

        private async Task<T> GetContentAsync<T>(string contentId, string slug)
            where T : Content
        {
            try
            {
                ItemResponse<T> response = await this.container.ReadItemAsync<T>(
                    contentId,
                    new PartitionKey(PartitionKeyHelper.GetPartitionKeyFromSlug(slug))).ConfigureAwait(false);

                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ContentNotFoundException("Content not found.", ex);
            }
        }
    }
}
