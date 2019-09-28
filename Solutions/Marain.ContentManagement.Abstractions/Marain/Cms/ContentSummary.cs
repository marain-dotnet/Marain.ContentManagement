// <copyright file="ContentSummary.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// A summary representation of a piece of content.
    /// </summary>
    /// <remarks>
    /// This is a more wire-efficient view of the content which does not include
    /// the actual content itself.
    /// </remarks>
    public class ContentSummary
    {
        private IList<string> tags;
        private IList<string> categoryPaths;
        private string slug;

        /// <summary>
        /// Gets or sets the unique ID for the content.
        /// </summary>
        public string Id { get; set; }

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
                this.slug = new Slug(value).ToString();
            }
        }

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
    }
}
