// <copyright file="IContentStore.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface implemented by a CMS content store.
    /// </summary>
    public interface IContentStore
    {
        /// <summary>
        /// Gets <see cref="Content"/> given a particular content ID and slug.
        /// </summary>
        /// <param name="contentId">The content ID.</param>
        /// <param name="slug">The slug to which the content belongs.</param>
        /// <returns>The <see cref="Content"/> corresponding to the given slug.</returns>
        Task<Content> GetContentAsync(string contentId, string slug);

        /// <summary>
        /// Gets <see cref="ContentSummaries"/> given a particular content ID and slug.
        /// </summary>
        /// <param name="slug">The slug to which the content belongs.</param>
        /// <param name="limit">The maximum number of summaries to return in a batch. It may return fewer than this, even if more summaries are still available.</param>
        /// <param name="continuationToken">The continuation token for the next batch, or null if no batches are remaining.</param>
        /// <returns>The batch of <see cref="ContentSummaries"/> corresponding to the given slug.</returns>
        Task<ContentSummaries> GetContentSummariesAsync(string slug, int limit = 20, string continuationToken = null);

        /// <summary>
        /// Gets the latest version of the content in a particular workflow.
        /// </summary>
        /// <param name="slug">The slug for the content.</param>
        /// <param name="workflowId">The workflow for which to get the content.</param>
        /// <returns>The latest content in the given workflow.</returns>
        Task<ContentWithState> GetContentForWorkflowAsync(string slug, string workflowId);

        /// <summary>
        /// Updates the state for the content in a particular workflow.
        /// </summary>
        /// <param name="slug">The slug for the content.</param>
        /// <param name="contentId">The content ID for the content instance.</param>
        /// <param name="workflowId">The workflow ID in which to set the state.</param>
        /// <param name="stateName">The new state name.</param>
        /// <param name="stateChangedBy">The identity that made the change.</param>
        /// <returns>A <see cref="Task"/> which completes when the state has been set.</returns>
        Task SetContentWorkflowStateAsync(string slug, string contentId, string workflowId, string stateName, CmsIdentity stateChangedBy);

        /// <summary>
        /// Gets the state for the content in a particular workflow.
        /// </summary>
        /// <param name="slug">The slug for the content.</param>
        /// <param name="workflowId">The workflow ID in which to set the state.</param>
        /// <returns>A <see cref="Task"/> which completes when the state has been set.</returns>
        Task<ContentState> GetContentWorkflowStateAsync(string slug, string workflowId);

        /// <summary>
        /// Store a new content item.
        /// </summary>
        /// <param name="content">The content to store.</param>
        /// <returns>The stored content item.</returns>
        Task<Content> StoreContentAsync(Content content);

        /// <summary>
        /// Gets <see cref="ContentSummariesWithState"/> given a particular workflow ID and slug and an optional state name filter.
        /// </summary>
        /// <param name="slug">The slug to which the content belongs.</param>
        /// <param name="workflowId">The workflow ID to which the content belongs.</param>
        /// <param name="stateName">The name of the state, or null if no state filtering is required.</param>
        /// <param name="limit">The maximum number of summaries to return in a batch. It may return fewer than this, even if more summaries are still available.</param>
        /// <param name="continuationToken">The continuation token for the next batch, or null if no batches are remaining.</param>
        /// <returns>The batch of <see cref="ContentSummariesWithState"/> corresponding to the given slug and workflow ID.</returns>
        /// <remarks>
        /// This allows you to walk the state history of the content at a slug, optionally filtering by a particular state.
        /// </remarks>
        Task<ContentSummariesWithState> GetContentSummariesForWorkflowAsync(string slug, string workflowId, string stateName = null, int limit = 20, string continuationToken = null);
    }
}
