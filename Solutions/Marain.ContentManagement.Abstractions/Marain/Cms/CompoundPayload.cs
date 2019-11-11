// <copyright file="CompoundPayload.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A <see cref="IContentPayload"/> representing a composition of other content.
    /// </summary>
    public class CompoundPayload : IContentPayload
    {
        /// <summary>
        /// The registered content type for the content payload.
        /// </summary>
        public const string RegisteredContentType = "application/vnd.marain.cms.contentpayload.compound";
        private IList<IContentPayload> children;

        /// <inheritdoc/>
        public string ContentType
        {
            get
            {
                return RegisteredContentType;
            }
        }

        /// <summary>
        /// Gets or sets the child content payloads.
        /// </summary>
        public IList<IContentPayload> Children
        {
            get { return this.children ?? (this.children = new List<IContentPayload>()); }
            set { this.children = value; }
        }

        /// <inheritdoc/>
        public IContentPayload Copy(bool replaceId)
        {
            return new CompoundPayload() { Children = this.Children.Select(c => c.Copy(replaceId)).ToList() };
        }

        /// <inheritdoc/>
        public string GetFullTextSearchContent()
        {
            var sb = new StringBuilder();

            foreach (IContentPayload child in this.Children)
            {
                sb.AppendLine(child.GetFullTextSearchContent());
            }

            return sb.ToString();
        }
    }
}
