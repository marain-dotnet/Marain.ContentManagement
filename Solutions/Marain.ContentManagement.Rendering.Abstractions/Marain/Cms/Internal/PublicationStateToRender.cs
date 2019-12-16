// <copyright file="PublicationStateToRender.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Internal
{
    /// <summary>
    /// The publication state to render in the renderer.
    /// </summary>
    public enum PublicationStateToRender
    {
        /// <summary>
        /// Render only content in the published state.
        /// </summary>
        PublishedOnly,

        /// <summary>
        /// Render content in either the published or draft states.
        /// </summary>
        PublishedOrDraft,
    }
}