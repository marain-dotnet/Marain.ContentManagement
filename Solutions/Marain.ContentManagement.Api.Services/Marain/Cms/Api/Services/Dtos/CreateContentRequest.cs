// <copyright file="CreateContentRequest.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Api.Services.Dtos
{
    using System.Collections.Generic;
    using System.Globalization;
    using Corvus.Extensions.Json;

    /// <summary>
    /// A request to create a new content item in the CMS.
    /// </summary>
    public class CreateContentRequest
    {
        /// <summary>
        /// Gets or sets the unique ID for the content.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the category paths for the content.
        /// </summary>
        /// <remarks>
        /// This supports multiple, hierarchical taxonomies for the content.
        /// </remarks>
        /// <seealso cref="Tags"/>
        public IList<string> CategoryPaths { get; set; }

        /// <summary>
        /// Gets or sets the tags for the content.
        /// </summary>
        /// <remarks>This support multiple, unstructured tags for the content.</remarks>
        /// <seealso cref="CategoryPaths"/>
        public IList<string> Tags { get; set; }

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
        /// Gets or sets arbitrary JSON metadata for the content.
        /// </summary>
        public PropertyBag Metadata { get; set; }

        /// <summary>
        /// Converts the DTO to a <see cref="Content"/> object ready to be stored.
        /// </summary>
        /// <param name="slug">The slug for the new item.</param>
        /// <returns>A new <see cref="Content"/> object.</returns>
        public Content AsContent(string slug)
        {
            return new Content
            {
                Author = this.Author,
                CategoryPaths = this.CategoryPaths,
                Culture = this.Culture,
                Description = this.Description,
                Id = this.Id,
                Slug = slug,
                Tags = this.Tags,
                Title = this.Title,
            };
        }
    }
}
