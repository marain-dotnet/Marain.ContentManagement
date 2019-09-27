// <copyright file="IFullTextSearchProvider.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    /// <summary>
    /// An interface implemented by content which support full text search.
    /// </summary>
    public interface IFullTextSearchProvider
    {
        /// <summary>
        /// Gets a string for use with a full text search provider.
        /// </summary>
        /// <returns>The text to use for full text search.</returns>
        string GetFullTextSearchContent();
    }
}
