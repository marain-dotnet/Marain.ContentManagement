// <copyright file="ContentSource.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    /// <summary>
    /// Represents the source content of some other content in the system.
    /// </summary>
    public struct ContentSource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentSource"/> struct.
        /// </summary>
        /// <param name="sourceSlug">The original slug of the source.</param>
        /// <param name="contentId">The original content ID of the source.</param>
        public ContentSource(string sourceSlug, string contentId)
            : this()
        {
            if (string.IsNullOrEmpty(contentId))
            {
                throw new System.ArgumentException("message", nameof(contentId));
            }

            this.SourceSlug = sourceSlug ?? throw new System.ArgumentNullException(nameof(sourceSlug));
            this.ContentId = contentId;
        }

        /// <summary>
        /// Gets the source content ID.
        /// </summary>
        public string ContentId { get; }

        /// <summary>
        /// Gets the source slug.
        /// </summary>
        public string SourceSlug { get; }
    }
}
