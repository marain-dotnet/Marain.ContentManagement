// <copyright file="IContentRendererFactory.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    /// <summary>
    /// Defines a factory which can create renderers for particular content payloads.
    /// </summary>
    public interface IContentRendererFactory
    {
        /// <summary>
        /// Creates a <see cref="IContentRenderer"/> for a given payload.
        /// </summary>
        /// <param name="contentPayload">The payload for which to get a renderer.</param>
        /// <returns>The renderer for the given content payload.</returns>
        IContentRenderer GetRendererFor(IContentPayload contentPayload);
    }
}