// <copyright file="ContentHistoryMappingContext.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Api.Services.Internal
{
    using Menes.Hal;

    /// <summary>
    /// Additional information needed to map a content summaries result to a <see cref="HalDocument"/> using
    /// this <see cref="ContentHistoryMapper"/>.
    /// </summary>
    public class ContentHistoryMappingContext : ResponseMappingContext
    {
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
    }
}
