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
            this.StateName = state.StateName;
            this.Timestamp = state.Timestamp;
            this.WorkflowId = state.WorkflowId;
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
    }
}
