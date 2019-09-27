// <copyright file="AbTestSet.cs" company="Endjin Limited">
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
        private Dictionary<string, string> abTestContentMap;

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
        public Dictionary<string, string> AbTestContentMap
        {
            get { return this.abTestContentMap ?? (this.abTestContentMap = new Dictionary<string, string>()); }
            set { this.abTestContentMap = value; }
        }

        /// <inheritdoc/>
        public IContentPayload Copy(bool replaceId)
        {
            return new AbTestSet(this.contentStore) { AbTestContentMap = new Dictionary<string, string>(this.AbTestContentMap) };
        }

        /// <summary>
        /// Gets the content for the particular AB test group.
        /// </summary>
        /// <param name="abTestId">The AB test group id.</param>
        /// <param name="slug">The slug for the content.</param>
        /// <returns>The <see cref="Content"/> for the given AB test group ID.</returns>
        public Task<Content> GetContentForAbGroupAsync(string abTestId, string slug)
        {
            if (this.abTestContentMap.TryGetValue(abTestId, out string contentId))
            {
                return this.contentStore.GetContentAsync(contentId, slug);
            }

            return Task.FromResult<Content>(null);
        }

        /// <inheritdoc/>
        public string GetFullTextSearchContent()
        {
            // We do not offer full text search over this content type.
            return string.Empty;
        }
    }
}
