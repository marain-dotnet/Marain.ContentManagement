// <copyright file="PartitionKeyHelper.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using Corvus.Extensions;

    /// <summary>
    /// Helper methods for working with partition keys for the content store.
    /// </summary>
    public static class PartitionKeyHelper
    {
        /// <summary>
        /// Gets a parition key string for <see cref="Content"/> and related items from a slug.
        /// </summary>
        /// <param name="slug">The slug from which to get the partition key.</param>
        /// <returns>The partition key for the slug.</returns>
        public static string GetPartitionKeyFromSlug(string slug)
        {
            string normalisedSlug = new Slug(slug).ToString();
            return normalisedSlug.Base64UrlEncode();
        }
    }
}
