﻿// <copyright file="LiquidWithMarkdownPayload.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    /// <summary>
    /// A <see cref="IContentPayload"/> representing a simple string fragment of content.
    /// </summary>
    public class LiquidWithMarkdownPayload : IContentPayload
    {
        /// <summary>
        /// The registered content type for the content payload.
        /// </summary>
        public const string RegisteredContentType = "application/vnd.marain.cms.contentpayload.liquidwithmarkdown";

        /// <inheritdoc/>
        public string ContentType => RegisteredContentType;

        /// <summary>
        /// Gets or sets the liquid with markdown template for the content.
        /// </summary>
        public string Template { get; set; }

        /// <inheritdoc/>
        public IContentPayload Copy(bool replaceId) => new LiquidPayload { Template = this.Template };

        /// <inheritdoc/>
        public string GetFullTextSearchContent() => this.Template;
    }
}
