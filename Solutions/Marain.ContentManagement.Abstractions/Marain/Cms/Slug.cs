// <copyright file="Slug.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Represents a slash-separated path fragment.
    /// </summary>
    internal readonly struct Slug
    {
        private readonly string slug;

        /// <summary>
        /// Initializes a new instance of the <see cref="Slug"/> class.
        /// </summary>
        /// <param name="slug">The string representation of the slug.</param>
        public Slug(string slug)
            : this()
        {
            this.slug = ToSlug(slug);
        }

        /// <summary>
        /// Trims leading and trailing forward-slashes from the slug, and converts to lowercase.
        /// </summary>
        /// <param name="slug">The slug to clean.</param>
        /// <returns>A clean slug.</returns>
        public static string Clean(string slug)
        {
            return slug.Trim(new[] { '/' }).ToLowerInvariant();
        }

        /// <summary>
        /// Gets the parent for the slug.
        /// </summary>
        /// <param name="slug">The slug for which to get the parent.</param>
        /// <returns>The parent slug.</returns>
        public static string GetParent(string slug)
        {
            string[] segments = Clean(slug).Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (segments.Length == 0)
            {
                return null;
            }

            return ToSlug(segments.Take(segments.Length - 1));
        }

        /// <summary>
        /// Get the tree for a slug.
        /// </summary>
        /// <param name="slug">The slug for which to get the tree.</param>
        /// <returns>The tree of parent.</returns>
        public static IEnumerable<string> GetTree(string slug)
        {
            var slugInstance = new Slug(slug);
            string[] segments = slugInstance.Split();
            if (segments.Length == 0)
            {
                yield break;
            }

            var sb = new StringBuilder();
            foreach (string segment in segments)
            {
                sb.Append(segment);
                if (sb.Length > 0)
                {
                    sb.Append("/");
                }

                yield return sb.ToString();
            }
        }

        /// <summary>
        /// Split the slug at slashes.
        /// </summary>
        /// <returns>The components of the slug.</returns>
        public string[] Split()
        {
            return this.ToString().Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.slug;
        }

        private static string ToSlug(IEnumerable<string> segments)
        {
            string fullPath = string.Join("/", segments.Select(Clean));
            return string.Concat(fullPath, '/');
        }

        private static string ToSlug(string uri)
        {
            return string.IsNullOrWhiteSpace(uri) ? "/" : string.Concat(Clean(uri), '/');
        }
    }
}