// <copyright file="ContentStates.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System.Collections.Generic;

    /// <summary>
    /// A batch of <see cref="ContentSummary"/> instances returned from a query.
    /// </summary>
    public class ContentStates
    {
        private List<ContentState> states;

        /// <summary>
        /// Gets or sets the continuation token for the next batch of requests.
        /// </summary>
        public string ContinuationToken { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="ContentState"/> instances in this batch.
        /// </summary>
        public List<ContentState> States
        {
            get => this.states ?? (this.states = new List<ContentState>());
            set => this.states = value;
        }
    }
}
