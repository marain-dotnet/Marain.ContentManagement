﻿// <copyright file="AbTestSet.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// A <see cref="IContentPayload"/> representing an AB test set of content.
    /// </summary>
    public class AbTestSet : IContentPayload
    {
        /// <summary>
        /// The registered content type for the content payload.
        /// </summary>
        public const string RegisteredContentType = "application/vnd.marain.cms.contentpayload.abtestset";
        private readonly IContentStore contentStore;
        private Dictionary<string, ContentSource> abTestContentMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbTestSet"/> class.
        /// </summary>
        /// <param name="contentStore">The content store.</param>
        public AbTestSet(IContentStore contentStore)
        {
            this.contentStore = contentStore ?? throw new System.ArgumentNullException(nameof(contentStore));
        }

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
        public Dictionary<string, ContentSource> AbTestContentMap
        {
            get { return this.abTestContentMap ?? (this.abTestContentMap = new Dictionary<string, ContentSource>()); }
            set { this.abTestContentMap = value; }
        }

        /// <inheritdoc/>
        public IContentPayload Copy(bool replaceId)
        {
            return new AbTestSet(this.contentStore) { AbTestContentMap = new Dictionary<string, ContentSource>(this.AbTestContentMap) };
        }

        /// <summary>
        /// Gets the content for the particular AB test group.
        /// </summary>
        /// <param name="abTestId">The AB test group id.</param>
        /// <returns>The <see cref="Content"/> for the given AB test group ID.</returns>
        public async Task<Content> GetContentForAbGroupAsync(string abTestId)
        {
            if (this.abTestContentMap.TryGetValue(abTestId, out ContentSource content))
            {
                return await this.contentStore.GetContentAsync(content.Id, content.Slug).ConfigureAwait(false);
            }

            throw new ContentNotFoundException();
        }

        /// <inheritdoc/>
        public string GetFullTextSearchContent()
        {
            // We do not offer full text search over this content type.
            return string.Empty;
        }
    }
}
