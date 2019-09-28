// <copyright file="ContentSummaryWithState.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System;

    /// <summary>
    /// A representation of the content in a particular state.
    /// </summary>
    public class ContentSummaryWithState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentSummaryWithState"/> class.
        /// </summary>
        /// <param name="contentSummary">The content summary from which to construct this instance.</param>
        /// <param name="state">The state with which to construct this instance.</param>
        public ContentSummaryWithState(ContentSummary contentSummary, ContentState state)
        {
            if (state is null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            this.ContentSummary = contentSummary ?? throw new ArgumentNullException(nameof(contentSummary));
            this.StateName = state.StateName;
            this.Timestamp = state.Timestamp;
            this.WorkflowId = state.WorkflowId;
        }

        /// <summary>
        /// Gets or sets the content summary.
        /// </summary>
        public ContentSummary ContentSummary { get; set; }

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
    }
}
