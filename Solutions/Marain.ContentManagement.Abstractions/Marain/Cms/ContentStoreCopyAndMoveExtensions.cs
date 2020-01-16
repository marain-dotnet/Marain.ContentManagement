// <copyright file="ContentStoreCopyAndMoveExtensions.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for the <see cref="IContentStore"/>.
    /// </summary>
    public static class ContentStoreCopyAndMoveExtensions
    {
        /// <summary>
        /// Creates a copy of the given content at the new slug, with traceability back to the original content.
        /// </summary>
        /// <param name="contentStore">The content store for which this is an extension.</param>
        /// <param name="targetSlug">The target slug for the content.</param>
        /// <param name="originalContentId">The original ID of the content to copy.</param>
        /// <param name="originalSlug">The original slug of the content to copy.</param>
        /// <returns>A <see cref="Task{Content}"/> which completes when the content is moved.</returns>
        public static async Task<Content> CopyContentAsync(this IContentStore contentStore, string targetSlug, string originalContentId, string originalSlug)
        {
            if (contentStore is null)
            {
                throw new System.ArgumentNullException(nameof(contentStore));
            }

            if (originalSlug is null)
            {
                throw new System.ArgumentNullException(nameof(originalSlug));
            }

            if (string.IsNullOrEmpty(originalContentId))
            {
                throw new System.ArgumentException("message", nameof(originalContentId));
            }

            if (targetSlug is null)
            {
                throw new System.ArgumentNullException(nameof(targetSlug));
            }

            Content originalContent = await contentStore.GetContentAsync(originalContentId, originalSlug).ConfigureAwait(false);
            Content newContent = originalContent.Copy(true);
            newContent.Slug = targetSlug;
            newContent.OriginalSource = new ContentReference(originalContent.Slug, originalContent.Id);

            return await contentStore.StoreContentAsync(newContent).ConfigureAwait(false);
        }
    }
}
