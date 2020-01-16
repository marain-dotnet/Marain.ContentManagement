// <copyright file="PublicationWorkflowContentPayload.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    /// <summary>
    /// A <see cref="IContentPayload"/> representing content referenced from the publication workflow.
    /// </summary>
    public class PublicationWorkflowContentPayload : IContentPayload
    {
        /// <summary>
        /// The registered content type for the content payload.
        /// </summary>
        public const string RegisteredContentType = "application/vnd.marain.cms.contentpayload.publicationworkflowcontent";

        /// <inheritdoc/>
        public string ContentType
        {
            get
            {
                return RegisteredContentType;
            }
        }

        /// <summary>
        /// Gets or sets the slug for the content itself.
        /// </summary>
        public string Slug
        {
            get;
            set;
        }

        /// <inheritdoc/>
        public IContentPayload Copy(bool replaceId)
        {
            return new PublicationWorkflowContentPayload() { Slug = this.Slug };
        }

        /// <inheritdoc/>
        public string GetFullTextSearchContent()
        {
            // We do not offer full text search over this content type.
            return string.Empty;
        }
    }
}
