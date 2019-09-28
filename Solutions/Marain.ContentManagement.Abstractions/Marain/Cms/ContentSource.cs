// <copyright file="ContentSource.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the source content of some other content in the system.
    /// </summary>
    public struct ContentSource : IEquatable<ContentSource>
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
                throw new ArgumentException("message", nameof(contentId));
            }

            this.Slug = sourceSlug ?? throw new ArgumentNullException(nameof(sourceSlug));
            this.Id = contentId;
        }

        /// <summary>
        /// Gets or sets the source content ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the source slug.
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="left">The left hand side of the comparison.</param>
        /// <param name="right">The right hand side of the comparison.</param>
        /// <returns>True if they are equal.</returns>
        public static bool operator ==(ContentSource left, ContentSource right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="left">The left hand side of the comparison.</param>
        /// <param name="right">The right hand side of the comparison.</param>
        /// <returns>True if they are not equal.</returns>
        public static bool operator !=(ContentSource left, ContentSource right)
        {
            return !(left == right);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is ContentSource source && this.Equals(source);
        }

        /// <inheritdoc/>
        public bool Equals(ContentSource other)
        {
            return this.Id == other.Id &&
                   this.Slug == other.Slug;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = 1538571774;
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(this.Id);
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(this.Slug);
            return hashCode;
        }
    }
}
