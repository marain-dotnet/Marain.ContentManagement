// <copyright file="ContentWithState.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System;

    /// <summary>
    /// A representation of the content in a particular state.
    /// </summary>
    public class ContentWithState
    {
        /// <summary>
        /// The registered content type of the content for the <see cref="Corvus.ContentHandling.ContentFactory"/> pattern.
        /// </summary>
        public const string RegisteredContentType = "application/vnd.marain.content.contentwithstate";

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentWithState"/> class.
        /// </summary>
        /// <param name="content">The content from which to construct this instance.</param>
        /// <param name="stateName">The name of the new state.</param>
        /// <param name="timestamp">The timestamp for the change.</param>
        /// <param name="workflowId">The workflow ID in which the change is being made.</param>
        public ContentWithState(Content content, string stateName, DateTimeOffset timestamp, string workflowId)
        {
            this.Content = content ?? throw new ArgumentNullException(nameof(content));
            this.StateName = stateName ?? throw new ArgumentNullException(nameof(content));
            this.Timestamp = timestamp;
            this.WorkflowId = workflowId ?? throw new ArgumentNullException(nameof(content));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentWithState"/> class.
        /// </summary>
        /// <param name="content">The content from which to construct this instance.</param>
        /// <param name="state">The state with which to construct this instance.</param>
        public ContentWithState(Content content, ContentState state)
        {
            if (state is null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            this.Content = content ?? throw new ArgumentNullException(nameof(content));
            this.StateName = state.StateName ?? throw new ArgumentNullException(nameof(content));
            this.Timestamp = state.Timestamp;
            this.WorkflowId = state.WorkflowId ?? throw new ArgumentNullException(nameof(content));
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public Content Content { get; set; }

        /// <summary>
        /// Gets or sets the current state of the content.
        /// </summary>
        public string StateName { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the last state change.
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the ID of the content workflow to which this belongs.
        /// </summary>
        public string WorkflowId { get; set; }

        /// <summary>
        /// Gets the content type of the state for the <see cref="Corvus.ContentHandling.ContentFactory"/> pattern.
        /// </summary>
        public string ContentType => RegisteredContentType;
    }
}
