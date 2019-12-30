// <copyright file="ContentSummariesWithStateMappingContext.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Api.Services.Internal
{
    using Menes.Hal;

    /// <summary>
    /// Additional information needed to map a content summaries result to a <see cref="HalDocument"/> using
    /// this <see cref="ContentSummariesMapper"/>.
    /// </summary>
    public class ContentSummariesWithStateMappingContext : ResponseMappingContext
    {
        /// <summary>
        /// Gets or sets the operation that self/next links should refer to.
        /// </summary>
        public string TargetOperationId { get; set; }

        /// <summary>
        /// Gets or sets the slug for which history was being requested.
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of items that were requested.
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Gets or sets the continuation token for the current request.
        /// </summary>
        public string ContinuationToken { get; set; }

        /// <summary>
        /// Gets or sets the workflow Id for the current request.
        /// </summary>
        public string WorkflowId { get; set; }

        /// <summary>
        /// Gets or sets the state name for the current request.
        /// </summary>
        public string StateName { get; set; }
    }
}
