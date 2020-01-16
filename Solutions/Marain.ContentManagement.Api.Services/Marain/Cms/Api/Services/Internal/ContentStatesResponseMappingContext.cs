// <copyright file="ContentStatesResponseMappingContext.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Api.Services.Internal
{
    using System.Collections.Generic;
    using Menes.Hal;

    /// <summary>
    /// Additional information needed to map a content summaries result to a <see cref="HalDocument"/> using
    /// this <see cref="ContentSummariesResponseMapper"/>.
    /// </summary>
    public class ContentStatesResponseMappingContext : ResponseMappingContext
    {
        /// <summary>
        /// Gets or sets the slug for which history was being requested.
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Gets or sets the workflow Id for which history was being requested.
        /// </summary>
        public string WorkflowId { get; set; }

        /// <summary>
        /// Gets or sets the workflow state for which history was being requested.
        /// </summary>
        /// <remarks>
        /// Leave this as null if not specified in the request.
        /// </remarks>
        public string StateName { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of items that were requested.
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Gets or sets the continuation token for the current request.
        /// </summary>
        public string ContinuationToken { get; set; }

        /// <summary>
        /// Gets or sets the embedding instruction for the current request.
        /// </summary>
        public string Embed { get; set; }

        /// <summary>
        /// Gets or sets the content summaries that correspond to the states and should be embedded in the response.
        /// </summary>
        /// <remarks>
        /// Set this property to null if content summaries should not be embedded.
        /// </remarks>
        public IList<ContentSummary> ContentSummaries { get; set; }
    }
}
