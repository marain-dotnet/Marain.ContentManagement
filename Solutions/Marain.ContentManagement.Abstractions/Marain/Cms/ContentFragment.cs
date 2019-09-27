// <copyright file="ContentFragment.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    /// <summary>
    /// A <see cref="IContentPayload"/> representing a simple string fragment of content.
    /// </summary>
    public class ContentFragment : IContentPayload
    {
        /// <summary>
        /// The registered content type for the content payload.
        /// </summary>
        public const string RegisteredContentType = "application/vnd.marain.cms.contentpayload.contentfragment";

        /// <inheritdoc/>
        public string ContentType
        {
            get
            {
                return RegisteredContentType;
            }
        }

        /// <summary>
        /// Gets or sets the string fragment of the content.
        /// </summary>
        public string Fragment { get; set; }

        /// <inheritdoc/>
        public IContentPayload Copy(bool replaceId)
        {
            return new ContentFragment { Fragment = this.Fragment };
        }

        /// <inheritdoc/>
        public string GetFullTextSearchContent()
        {
            return this.Fragment;
        }
    }
}
