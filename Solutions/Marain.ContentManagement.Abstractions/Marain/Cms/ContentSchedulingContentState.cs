// <copyright file="ContentSchedulingContentState.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    /// <summary>
    /// Well-known content states for the <see cref="WellKnownWorkflowId.ContentScheduling"/> workflow.
    /// </summary>
    /// <remarks>Implementers of a content scheduling workflow should support at least these well-known states.</remarks>
    public static class ContentSchedulingContentState
    {
        /// <summary>
        /// The scheduled for publication state.
        /// </summary>
        public const string ScheduledForPublication = "scheduled-for-publication";

        /// <summary>
        /// The scheduled for archiving state.
        /// </summary>
        public const string ScheduledForArchiving = "scheduled-for-archiving";

        /// <summary>
        /// The published state.
        /// </summary>
        public const string Published = "published";

        /// <summary>
        /// The archived state.
        /// </summary>
        public const string Archived = "archived";
    }
}
