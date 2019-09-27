// <copyright file="ContentStoreExtensions.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for the <see cref="IContentStore"/>.
    /// </summary>
    public static class ContentStoreExtensions
    {
        /// <summary>
        /// Publishes the given content in the <see cref="WellKnownWorkflowId.ContentPublication"/> workflow.
        /// </summary>
        /// <param name="contentStore">The content store for which this is an extension.</param>
        /// <param name="slug">The slug for which to publish the content.</param>
        /// <param name="contentId">The ID of the content to be published.</param>
        /// <returns>A <see cref="Task"/> which completes when the content is published.</returns>
        public static Task PublishContentAsync(this IContentStore contentStore, string slug, string contentId)
        {
            if (contentStore is null)
            {
                throw new System.ArgumentNullException(nameof(contentStore));
            }

            if (slug is null)
            {
                throw new System.ArgumentNullException(nameof(slug));
            }

            if (string.IsNullOrEmpty(contentId))
            {
                throw new System.ArgumentException("message", nameof(contentId));
            }

            return contentStore.SetContentWorkflowStateAsync(slug, contentId, WellKnownWorkflowId.ContentPublication, ContentPublicationContentState.Published);
        }

        /// <summary>
        /// Archives the given content in the <see cref="WellKnownWorkflowId.ContentPublication"/> workflow.
        /// </summary>
        /// <param name="contentStore">The content store for which this is an extension.</param>
        /// <param name="slug">The slug for which to publish the content.</param>
        /// <param name="contentId">The ID of the content to be published.</param>
        /// <returns>A <see cref="Task"/> which completes when the content is published.</returns>
        public static Task ArchiveContentAsync(this IContentStore contentStore, string slug, string contentId)
        {
            if (contentStore is null)
            {
                throw new System.ArgumentNullException(nameof(contentStore));
            }

            if (slug is null)
            {
                throw new System.ArgumentNullException(nameof(slug));
            }

            if (string.IsNullOrEmpty(contentId))
            {
                throw new System.ArgumentException("message", nameof(contentId));
            }

            return contentStore.SetContentWorkflowStateAsync(slug, contentId, WellKnownWorkflowId.ContentPublication, ContentPublicationContentState.Archived);
        }

        /// <summary>
        /// Gets the latest content in the <see cref="WellKnownWorkflowId.ContentPublication"/>
        /// workflow.
        /// </summary>
        /// <param name="contentStore">The content store for which this is an extension.</param>
        /// <param name="slug">The slug for which to retrieve the published content.</param>
        /// <returns>A <see cref="Task{Content}"/> which, when complete, returns the content.</returns>
        /// <remarks>
        /// <para>This will return the relevant content if the ContentPublication workflow for that slug is in the <see cref="ContentPublicationContentState.Published"/> state.</para>
        /// </remarks>
        public static async Task<Content> GetPublishedContentAsync(this IContentStore contentStore, string slug)
        {
            if (contentStore is null)
            {
                throw new System.ArgumentNullException(nameof(contentStore));
            }

            if (slug is null)
            {
                throw new System.ArgumentNullException(nameof(slug));
            }

            ContentWithState result = await contentStore.GetContentForWorkflowAsync(slug, WellKnownWorkflowId.ContentPublication).ConfigureAwait(false);
            if (result?.StateName != ContentPublicationContentState.Published)
            {
                throw new ContentNotFoundException();
            }

            return result?.Content ?? throw new ContentNotFoundException();
        }

        /// <summary>
        /// Gets the history of published content in the <see cref="WellKnownWorkflowId.ContentPublication"/>
        /// workflow.
        /// </summary>
        /// <param name="contentStore">The content store for which this is an extension.</param>
        /// <param name="slug">The slug for which to retrieve the published content.</param>
        /// <param name="limit">The maximum number of items to return in a batch.</param>
        /// <param name="continuationToken">The continuation token for the batch.</param>
        /// <returns>A <see cref="Task{ContentSummariesWithState}"/> which, when complete, returns the content.</returns>
        public static async Task<ContentSummariesWithState> GetPublicationHistoryAsync(this IContentStore contentStore, string slug, int limit = 20, string continuationToken = null)
        {
            if (contentStore is null)
            {
                throw new System.ArgumentNullException(nameof(contentStore));
            }

            if (slug is null)
            {
                throw new System.ArgumentNullException(nameof(slug));
            }

            ContentSummariesWithState result = await contentStore.GetContentSummariesForWorkflowAsync(slug, WellKnownWorkflowId.ContentPublication, ContentPublicationContentState.Published, limit, continuationToken).ConfigureAwait(false);
            return result ?? throw new ContentNotFoundException();
        }
    }
}
