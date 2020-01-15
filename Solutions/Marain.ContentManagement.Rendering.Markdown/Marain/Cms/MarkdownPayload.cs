// <copyright file="MarkdownPayload.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    /// <summary>
    /// A <see cref="IContentPayload"/> representing a simple string fragment of content.
    /// </summary>
    public class MarkdownPayload : IContentPayload
    {
        /// <summary>
        /// The registered content type for the content payload.
        /// </summary>
        public const string RegisteredContentType = "application/vnd.marain.cms.contentpayload.markdown";

        /// <inheritdoc/>
        public string ContentType => RegisteredContentType;

        /// <summary>
        /// Gets or sets the markdown for the content.
        /// </summary>
        public string Markdown { get; set; }

        /// <inheritdoc/>
        public IContentPayload Copy(bool replaceId) => new MarkdownPayload { Markdown = this.Markdown };

        /// <inheritdoc/>
        public string GetFullTextSearchContent() => this.Markdown;
    }
}
