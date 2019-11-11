// <copyright file="LiquidPayload.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    /// <summary>
    /// A <see cref="IContentPayload"/> representing a simple string fragment of content.
    /// </summary>
    public class LiquidPayload : IContentPayload
    {
        /// <summary>
        /// The registered content type for the content payload.
        /// </summary>
        public const string RegisteredContentType = "application/vnd.marain.cms.contentpayload.liquid";

        /// <inheritdoc/>
        public string ContentType
        {
            get
            {
                return RegisteredContentType;
            }
        }

        /// <summary>
        /// Gets or sets the liquid template for the content.
        /// </summary>
        public string Template { get; set; }

        /// <inheritdoc/>
        public IContentPayload Copy(bool replaceId)
        {
            return new LiquidPayload { Template = this.Template };
        }

        /// <inheritdoc/>
        public string GetFullTextSearchContent()
        {
            return this.Template;
        }
    }
}
