// <copyright file="CompoundContentReferencePayload.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System.Collections.Generic;

    /// <summary>
    /// A <see cref="IContentPayload"/> representing content composed from an ordered set of specific content references.
    /// </summary>
    public class CompoundContentReferencePayload : IContentPayload
    {
        /// <summary>
        /// The registered content type for the content payload.
        /// </summary>
        public const string RegisteredContentType = "application/vnd.marain.cms.contentpayload.compoundcontentreference";
        private List<ContentReference> children;

        /// <inheritdoc/>
        public string ContentType
        {
            get
            {
                return RegisteredContentType;
            }
        }

        /// <summary>
        /// Gets or sets the map of ABTest group ID to <see cref="Content.Id"/>.
        /// </summary>
        public List<ContentReference> Children
        {
            get { return this.children ??= new List<ContentReference>(); }
            set { this.children = value; }
        }

        /// <inheritdoc/>
        public IContentPayload Copy(bool replaceId)
        {
            // This is a shallow copy of the references i.e. we are copying the references, not cloning the things
            // to which they are referring and providing new references.
            return new CompoundContentReferencePayload() { Children = new List<ContentReference>(this.Children) };
        }

        /// <inheritdoc/>
        public string GetFullTextSearchContent()
        {
            // We do not offer full text search over this content type.
            return string.Empty;
        }
    }
}
