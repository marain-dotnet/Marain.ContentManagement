// <copyright file="ContentReference.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a reference to a particular instance of some content in the system.
    /// </summary>
    public struct ContentReference : IEquatable<ContentReference>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentReference"/> struct.
        /// </summary>
        /// <param name="slug">The slug of the referenced content.</param>
        /// <param name="contentId">The content ID of the referenced content.</param>
        public ContentReference(string slug, string contentId)
            : this()
        {
            if (string.IsNullOrEmpty(contentId))
            {
                throw new ArgumentException("message", nameof(contentId));
            }

            this.Slug = slug ?? throw new ArgumentNullException(nameof(slug));
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
        public static bool operator ==(ContentReference left, ContentReference right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="left">The left hand side of the comparison.</param>
        /// <param name="right">The right hand side of the comparison.</param>
        /// <returns>True if they are not equal.</returns>
        public static bool operator !=(ContentReference left, ContentReference right)
        {
            return !(left == right);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is ContentReference reference && this.Equals(reference);
        }

        /// <inheritdoc/>
        public bool Equals(ContentReference other)
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
