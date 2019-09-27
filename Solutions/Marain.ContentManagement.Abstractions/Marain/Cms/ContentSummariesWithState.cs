// <copyright file="ContentSummariesWithState.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System.Collections.Generic;

    /// <summary>
    /// A batch of <see cref="ContentSummary"/> instances returned from a query.
    /// </summary>
    public class ContentSummariesWithState
    {
        private List<ContentSummaryWithState> summaries;

        /// <summary>
        /// Gets or sets the continuation token for the next batch of requests.
        /// </summary>
        public string ContinuationToken { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="ContentSummaryWithState"/> instances in this batch.
        /// </summary>
        public List<ContentSummaryWithState> Summaries
        {
            get => this.summaries ?? (this.summaries = new List<ContentSummaryWithState>());
            set => this.summaries = value;
        }
    }
}
