// <copyright file="CosmosContentStore.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

            ItemResponse<Content> response = await this.container.CreateItemAsync(content, new PartitionKey(content.PartitionKey)).ConfigureAwait(false);
            return response.Resource;
        }

        /// <inheritdoc/>
        public Task SetContentWorkflowStateAsync(string slug, string contentId, string workflowId, string stateName, CmsIdentity stateChangedBy)
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

            return this.container.CreateItemAsync(new ContentState { Slug = slug, ContentId = contentId, StateName = stateName, WorkflowId = workflowId, ChangedBy = stateChangedBy }, new PartitionKey(Content.GetPartitionKeyFromSlug(slug)));
        }

        /// <inheritdoc/>
        public async Task<ContentState> GetContentWorkflowStateAsync(string slug, string workflowId)
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

            return null;
        }

        /// <inheritdoc/>
        public Task<Content> GetContentAsync(string contentId, string slug)
        {
            return this.GetContentAsync<Content>(contentId, slug);
        }

        /// <inheritdoc/>
        public async Task<ContentWithState> GetContentForWorkflowAsync(string slug, string workflowId)
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
                    Content content = await this.GetContentAsync<Content>(state.ContentId, state.Slug);
                    return new ContentWithState(content, state);
                }
            }

            return null;
        }

        /// <inheritdoc/>
        public async Task<ContentSummariesWithState> GetContentSummariesForWorkflowAsync(string slug, string workflowId, string stateName = null, int limit = 20, string continuationToken = null)
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

            var summaries = new ContentSummariesWithState();
            var states = new List<ContentState>();

            if (iterator.HasMoreResults)
            {
                FeedResponse<ContentState> results = await iterator.ReadNextAsync().ConfigureAwait(false);
                summaries.ContinuationToken = results.ContinuationToken;
                states.AddRange(results.Resource);
            }

            summaries.Summaries = await this.GetContentSummariesAsync(slug, states).ConfigureAwait(false);

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

            return await this.GetContentSummariesAsync(limit, continuationToken, queryDefinition);
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

        private async Task<List<ContentSummaryWithState>> GetContentSummariesAsync(string slug, IList<ContentState> states)
        {
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
                               .WithParameter("@slug", slug);

            contentIds.ForEachAtIndex((s, i) =>
            {
                queryDefinition = queryDefinition.WithParameter($"@id{i}", s);
            });

            ContentSummaries summaries = await this.GetContentSummariesAsync(states.Count, null, queryDefinition);
            var summaryDictionary = summaries.Summaries.ToDictionary(s => s.Id);

            return states.Select(s => new ContentSummaryWithState(summaryDictionary[s.ContentId], s)).ToList();
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
            ItemResponse<T> response = await this.container.ReadItemAsync<T>(contentId, new PartitionKey(Content.GetPartitionKeyFromSlug(slug))).ConfigureAwait(false);
            return response.Resource;
        }
    }
}
