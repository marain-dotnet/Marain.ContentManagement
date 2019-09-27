// <copyright file="ContentSummaries.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System.Collections.Generic;

    /// <summary>
    /// A batch of <see cref="ContentSummary"/> instances returned from a query.
    /// </summary>
    public class ContentSummaries
    {
        private List<ContentSummary> summaries;

        /// <summary>
        /// Gets or sets the continuation token for the next batch of requests.
        /// </summary>
        public string ContinuationToken { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="ContentSummary"/> instances in this batch.
        /// </summary>
        public List<ContentSummary> Summaries
        {
            get => this.summaries ?? (this.summaries = new List<ContentSummary>());
            set => this.summaries = value;
        }
    }
}
