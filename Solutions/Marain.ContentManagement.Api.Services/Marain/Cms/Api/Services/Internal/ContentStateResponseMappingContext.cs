// <copyright file="ContentStateResponseMappingContext.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Api.Services.Internal
{
    using Menes.Hal;

    /// <summary>
    /// Additional information needed to map a content summaries result to a <see cref="HalDocument"/> using
    /// this <see cref="ContentSummariesResponseMapper"/>.
    /// </summary>
    public class ContentStateResponseMappingContext : ResponseMappingContext
    {
        /// <summary>
        /// Gets or sets the embedding instruction for the current request.
        /// </summary>
        public string Embed { get; set; }

        /// <summary>
        /// Gets or sets the content corresponding to the state. If supplied, it will be added to the _embedded collection
        /// of the response.
        /// </summary>
        public Content Content { get; set; }

        /// <summary>
        /// Gets or sets the summary of the content corresponding to the state. If supplied, it will be added to the _embedded
        /// collection of the response.
        /// </summary>
        public ContentSummary ContentSummary { get; set; }
    }
}
