// <copyright file="ContentStorePublicationWorkflowExtensions.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for the <see cref="IContentStore"/>.
    /// </summary>
    public static class ContentStorePublicationWorkflowExtensions
    {
        /// <summary>
        /// Publishes the given content in the <see cref="WellKnownWorkflowId.ContentPublication"/> workflow.
        /// </summary>
        /// <param name="contentStore">The content store for which this is an extension.</param>
        /// <param name="slug">The slug for which to publish the content.</param>
        /// <param name="contentId">The ID of the content to be published.</param>
        /// <param name="stateChangedBy">The identity that made the change.</param>
        /// <returns>A <see cref="Task"/> which completes when the content is published.</returns>
        public static Task PublishContentAsync(this IContentStore contentStore, string slug, string contentId, CmsIdentity stateChangedBy)
        {
            if (contentStore is null)
            {
                throw new ArgumentNullException(nameof(contentStore));
            }

            if (slug is null)
            {
                throw new ArgumentNullException(nameof(slug));
            }

            if (string.IsNullOrEmpty(contentId))
            {
                throw new ArgumentException("message", nameof(contentId));
            }

            return contentStore.SetContentWorkflowStateAsync(slug, contentId, WellKnownWorkflowId.ContentPublication, ContentPublicationContentState.Published, stateChangedBy);
        }

        /// <summary>
        /// Archives the given content in the <see cref="WellKnownWorkflowId.ContentPublication"/> workflow.
        /// </summary>
        /// <param name="contentStore">The content store for which this is an extension.</param>
        /// <param name="slug">The slug for which to publish the content.</param>
        /// <param name="stateChangedBy">The identity that made the change.</param>
        /// <returns>A <see cref="Task"/> which completes when the content is published.</returns>
        public static async Task ArchiveContentAsync(this IContentStore contentStore, string slug, CmsIdentity stateChangedBy)
        {
            if (contentStore is null)
            {
                throw new ArgumentNullException(nameof(contentStore));
            }

            if (slug is null)
            {
                throw new ArgumentNullException(nameof(slug));
            }

            ContentState contentState = await contentStore.GetContentWorkflowStateAsync(slug, WellKnownWorkflowId.ContentPublication).ConfigureAwait(false);

            if (contentState.StateName != ContentPublicationContentState.Archived)
            {
                await contentStore.SetContentWorkflowStateAsync(slug, contentState.ContentId, WellKnownWorkflowId.ContentPublication, ContentPublicationContentState.Archived, stateChangedBy).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Reverts the content to a draft state in the <see cref="WellKnownWorkflowId.ContentPublication"/> workflow.
        /// </summary>
        /// <param name="contentStore">The content store for which this is an extension.</param>
        /// <param name="slug">The slug for which to publish the content.</param>
        /// <param name="stateChangedBy">The identity that made the change.</param>
        /// <returns>A <see cref="Task"/> which completes when the content is published.</returns>
        public static async Task MakeDraftContentAsync(this IContentStore contentStore, string slug, CmsIdentity stateChangedBy)
        {
            if (contentStore is null)
            {
                throw new ArgumentNullException(nameof(contentStore));
            }

            if (slug is null)
            {
                throw new ArgumentNullException(nameof(slug));
            }

            ContentState contentState = await contentStore.GetContentWorkflowStateAsync(slug, WellKnownWorkflowId.ContentPublication).ConfigureAwait(false);
            await contentStore.SetContentWorkflowStateAsync(slug, contentState.ContentId, WellKnownWorkflowId.ContentPublication, ContentPublicationContentState.Archived, stateChangedBy).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the latest published content in the <see cref="WellKnownWorkflowId.ContentPublication"/>
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
                throw new ArgumentNullException(nameof(contentStore));
            }

            if (slug is null)
            {
                throw new ArgumentNullException(nameof(slug));
            }

            ContentWithState result = await contentStore.GetContentForWorkflowAsync(slug, WellKnownWorkflowId.ContentPublication).ConfigureAwait(false);
            if (result?.StateName != ContentPublicationContentState.Published)
            {
                throw new ContentNotFoundException();
            }

            return result?.Content ?? throw new ContentNotFoundException();
        }

        /// <summary>
        /// Gets the content for the current state in the <see cref="WellKnownWorkflowId.ContentPublication"/>
        /// workflow, with state information.
        /// </summary>
        /// <param name="contentStore">The content store for which this is an extension.</param>
        /// <param name="slug">The slug for which to retrieve the published content.</param>
        /// <returns>A <see cref="Task{Content}"/> which, when complete, returns the content.</returns>
        /// <remarks>
        /// <para>This will return the relevant content if the ContentPublication workflow for that slug is in the <see cref="ContentPublicationContentState.Published"/> state.</para>
        /// </remarks>
        public static async Task<ContentWithState> GetContentWithStateAsync(this IContentStore contentStore, string slug)
        {
            if (contentStore is null)
            {
                throw new ArgumentNullException(nameof(contentStore));
            }

            if (slug is null)
            {
                throw new ArgumentNullException(nameof(slug));
            }

            ContentWithState result = await contentStore.GetContentForWorkflowAsync(slug, WellKnownWorkflowId.ContentPublication).ConfigureAwait(false);
            return result ?? throw new ContentNotFoundException();
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
                throw new ArgumentNullException(nameof(contentStore));
            }

            if (slug is null)
            {
                throw new ArgumentNullException(nameof(slug));
            }

            ContentSummariesWithState result = await contentStore.GetContentSummariesForWorkflowAsync(slug, WellKnownWorkflowId.ContentPublication, ContentPublicationContentState.Published, limit, continuationToken).ConfigureAwait(false);
            return result ?? throw new ContentNotFoundException();
        }

        /// <summary>
        /// Moves the given content in the <see cref="WellKnownWorkflowId.ContentPublication"/> workflow.
        /// </summary>
        /// <param name="contentStore">The content store for which this is an extension.</param>
        /// <param name="targetSlug">The slug to which to move the content.</param>
        /// <param name="originalContentId">The ID of the content to be moved.</param>
        /// <param name="originalSlug">The slug from which the content is to be moved.</param>
        /// <param name="stateChangedBy">The identity that made the change.</param>
        /// <returns>A <see cref="Task"/> which completes when the content is published.</returns>
        public static async Task<Content> MoveContentForPublicationAsync(this IContentStore contentStore, string targetSlug, string originalContentId, string originalSlug, CmsIdentity stateChangedBy)
        {
            if (contentStore is null)
            {
                throw new ArgumentNullException(nameof(contentStore));
            }

            if (targetSlug is null)
            {
                throw new ArgumentNullException(nameof(targetSlug));
            }

            if (originalSlug is null)
            {
                throw new ArgumentNullException(nameof(originalSlug));
            }

            if (string.IsNullOrEmpty(originalContentId))
            {
                throw new ArgumentException("message", nameof(originalContentId));
            }

            ContentState state = await contentStore.GetContentWorkflowStateAsync(originalSlug, WellKnownWorkflowId.ContentPublication).ConfigureAwait(false);

            if (state.StateName == ContentPublicationContentState.Archived)
            {
                throw new InvalidOperationException("You cannot move archived content.");
            }

            Content copiedContent = await contentStore.CopyContentAsync(targetSlug, originalContentId, originalSlug).ConfigureAwait(false);

            Task t1 = contentStore.SetContentWorkflowStateAsync(originalSlug, originalContentId, WellKnownWorkflowId.ContentPublication, ContentPublicationContentState.Archived, stateChangedBy);
            Task t2 = contentStore.SetContentWorkflowStateAsync(targetSlug, copiedContent.Id, WellKnownWorkflowId.ContentPublication, state.StateName, stateChangedBy);

            await Task.WhenAll(t1, t2).ConfigureAwait(false);

            return copiedContent;
        }

        /// <summary>
        /// Copies the given content in the <see cref="WellKnownWorkflowId.ContentPublication"/> workflow.
        /// </summary>
        /// <param name="contentStore">The content store for which this is an extension.</param>
        /// <param name="targetSlug">The slug to which to move the content.</param>
        /// <param name="originalContentId">The ID of the content to be moved.</param>
        /// <param name="originalSlug">The slug from which the content is to be moved.</param>
        /// <param name="stateChangedBy">The identity that made the change.</param>
        /// <param name="targetState">The target state for the copy; the default is <see cref="ContentPublicationContentState.Draft"/>.</param>
        /// <returns>A <see cref="Task"/> which completes when the content is published.</returns>
        public static async Task<Content> CopyContentForPublicationAsync(this IContentStore contentStore, string targetSlug, string originalContentId, string originalSlug, CmsIdentity stateChangedBy, string targetState = ContentPublicationContentState.Draft)
        {
            if (contentStore is null)
            {
                throw new ArgumentNullException(nameof(contentStore));
            }

            if (targetSlug is null)
            {
                throw new ArgumentNullException(nameof(targetSlug));
            }

            if (originalSlug is null)
            {
                throw new ArgumentNullException(nameof(originalSlug));
            }

            if (string.IsNullOrEmpty(originalContentId))
            {
                throw new ArgumentException("message", nameof(originalContentId));
            }

            Content copiedContent = await contentStore.CopyContentAsync(targetSlug, originalContentId, originalSlug).ConfigureAwait(false);

            await contentStore.SetContentWorkflowStateAsync(targetSlug, copiedContent.Id, WellKnownWorkflowId.ContentPublication, targetState, stateChangedBy).ConfigureAwait(false);

            return copiedContent;
        }
    }
}
