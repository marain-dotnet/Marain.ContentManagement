// <copyright file="WellKnownWorkflowId.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    /// <summary>
    /// Well-known workflow IDs.
    /// </summary>
    public static class WellKnownWorkflowId
    {
        /// <summary>
        /// The content publication workflow.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is a standard workflow that supports content passing through (at least) draft, publication, and archive states. (See <see cref="ContentPublicationContentState"/>).
        /// </para>
        /// <para>
        /// Extensions methods on <see cref="IContentStore"/> are provided which help support this workflow.
        /// </para>
        /// <para>
        /// The actual implementation of the workflow itself is the responsibility of the consuming application. <c>Marain.Worfklow</c>, or Microsoft Logic Apps could provide a suitable starting point.
        /// </para>
        /// </remarks>
        public const string ContentPublication = "content-publication";

        /// <summary>
        /// The content scheduling workflow.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is a standard worfklow that supports a scheduled publication process, where content can be scheduled for publication and archiving in the future. (See <see cref="ContentSchedulingContentState"/>.)
        /// </para>
        /// <para>
        /// Extensions methods on <see cref="IContentStore"/> are provided which help support this workflow.
        /// </para>
        /// <para>
        /// The actual implementation of the workflow itself is the responsibility of the consuming application. <c>Marain.Worfklow</c>, or Microsoft Logic Apps could provide a suitable starting point.
        /// </para>
        /// </remarks>
        public const string ContentScheduling = "content-scheduling";
    }
}
