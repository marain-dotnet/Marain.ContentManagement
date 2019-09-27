// <copyright file="Content.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Corvus.Extensions;
    using Corvus.Extensions.Json;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// A unit of content in the CMS.
    /// </summary>
    public class Content : IFullTextSearchProvider
    {
        /// <summary>
        /// The registered content type for the content.
        /// </summary>
        public const string RegisteredContentType = "application/vnd.marain.content.contentdocument";

        /// <summary>
        /// The path to the partition key.
        /// </summary>
        /// <remarks>This is an encoded version of the slug, to keep related assets in the same partition.</remarks>
        public const string PartitionKeyPath = "/partitionKey";

        private IList<string> tags;
        private IList<string> categoryPaths;
        private Slug slug;

        /// <summary>
        /// Initializes a new instance of the <see cref="Content"/> class.
        /// </summary>
        public Content()
        {
        }

        /// <summary>
        /// Gets or sets the unique ID for the content.
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets the registered content type.
        /// </summary>
        public string ContentType => RegisteredContentType;

        /// <summary>
        /// Gets or sets the ETag for the content.
        /// </summary>
        [JsonProperty("_etag")]
        public string ETag { get; set; }

        /// <summary>
        /// Gets the category paths for the content.
        /// </summary>
        /// <remarks>
        /// This supports multiple, hierarchical taxonomies for the content.
        /// </remarks>
        /// <seealso cref="Tags"/>
        public IList<string> CategoryPaths
        {
            get
            {
                return this.categoryPaths ?? (this.categoryPaths = new List<string>());
            }

            private set
            {
                this.categoryPaths = value;
            }
        }

        /// <summary>
        /// Gets the tags for the content.
        /// </summary>
        /// <remarks>This support multiple, unstructured tags for the content.</remarks>
        /// <seealso cref="CategoryPaths"/>
        public IList<string> Tags
        {
            get
            {
                return this.tags ?? (this.tags = new List<string>());
            }

            private set
            {
                this.tags = value;
            }
        }

        /// <summary>
        /// Gets or sets a slug for the content.
        /// </summary>
        public string Slug
        {
            get
            {
                return this.slug ?? string.Empty;
            }

            set
            {
                this.slug = new Slug(value);
            }
        }

        /// <summary>
        /// Gets the partition key for the content.
        /// </summary>
        public string PartitionKey => GetPartitionKeyFromSlug(this.Slug);

        /// <summary>
        /// Gets an enumerable containing the full hierarchical tree for the slug.
        /// </summary>
        /// <remarks>
        /// <para>For example <c>foo/bar/baz/</c> would produce <c>["foo/", "foo/bar/", "foo/bar/baz/"]</c>.</para>
        /// </remarks>
        public IEnumerable<string> SlugTree
        {
            get
            {
                return Marain.Cms.Slug.GetTree(this.Slug);
            }
        }

        /// <summary>
        /// Gets the slug of the parent of this content.
        /// </summary>
        /// <remarks>
        /// <para>For example <c>foo/bar/baz/</c> would produce <c>foo/bar/</c>.</para>
        /// </remarks>
        public string ParentSlug
        {
            get
            {
                return Marain.Cms.Slug.GetParent(this.Slug);
            }
        }

        /// <summary>
        /// Gets or sets arbitrary JSON metadata for the content.
        /// </summary>
        public PropertyBag Metadata
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the author of the content fragment.
        /// </summary>
        public CmsIdentity Author { get; set; }

        /// <summary>
        /// Gets or sets the title for the content.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description for the content.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the culture for the content.
        /// </summary>
        public CultureInfo Culture { get; set; }

        /// <summary>
        /// Gets or sets a payload for the content.
        /// </summary>
        public IContentPayload ContentPayload { get; set; }

        /// <summary>
        /// Gets the parition key string from a slug.
        /// </summary>
        /// <param name="slug">The slug from which to get the partition key.</param>
        /// <returns>The partition key for the slug.</returns>
        public static string GetPartitionKeyFromSlug(string slug)
        {
            return slug.Base64UrlEncode();
        }

        /// <summary>
        /// Gets the full-text-search representation of the content.
        /// </summary>
        /// <returns>A string representing the search text for indexing.</returns>
        public string GetFullTextSearchContent()
        {
            return this.ContentPayload?.GetFullTextSearchContent();
        }

        /// <summary>
        /// Create a copy of the content, optionally replacing ids.
        /// </summary>
        /// <param name="replaceIds">Indicates whether Ids should be replaced.</param>
        /// <returns>A copy of the content.</returns>
        public Content Copy(bool replaceIds)
        {
            var copy = new Content
            {
                Slug = this.Slug,
                Culture = this.Culture,
                Description = this.Description,
                Metadata = this.Metadata,
                Title = this.Title,
                Author = this.Author,
            };

            if (!replaceIds)
            {
                copy.Id = this.Id;
                copy.ETag = this.ETag;
            }

            copy.ContentPayload = this.ContentPayload?.Copy(replaceIds);
            copy.CategoryPaths.AddRange(this.CategoryPaths);
            copy.Tags.AddRange(this.Tags);
            return copy;
        }
    }
}
