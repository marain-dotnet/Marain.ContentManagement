// <copyright file="ContentPublicationContentState.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    /// <summary>
    /// Well-known content states for the <see cref="WellKnownWorkflowId.ContentPublication"/>
    /// workflow.
    /// </summary>
    /// <remarks>
    /// Implementers of a content publication workflow should support at least these three
    /// states for the content.
    /// </remarks>
    public static class ContentPublicationContentState
    {
        /// <summary>
        /// The draft state.
        /// </summary>
        public const string Draft = "draft";

        /// <summary>
        /// The publication state.
        /// </summary>
        public const string Published = "published";

        /// <summary>
        /// The archived state.
        /// </summary>
        public const string Archived = "archived";
    }
}
