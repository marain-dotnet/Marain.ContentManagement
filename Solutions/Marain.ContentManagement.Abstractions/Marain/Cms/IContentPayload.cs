// <copyright file="IContentPayload.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    /// <summary>
    /// An interface implemented by entities that represent the payload of a <see cref="Content"/> instance in the CMS.
    /// </summary>
    public interface IContentPayload : IFullTextSearchProvider
    {
        /// <summary>
        /// Gets the content type of the payload.
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// Copies the content payload.
        /// </summary>
        /// <param name="replaceId">Indicates whether to replace the IDs within the content payload.</param>
        /// <returns>A copy of the content payload.</returns>
        IContentPayload Copy(bool replaceId);
    }
}