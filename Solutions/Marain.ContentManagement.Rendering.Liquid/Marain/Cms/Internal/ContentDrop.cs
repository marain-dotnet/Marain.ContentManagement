// <copyright file="ContentDrop.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Internal
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using DotLiquid;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// A safe <see cref="Drop"/> for the <see cref="Content"/> type.
    /// </summary>
    public class ContentDrop : Drop
    {
        private readonly Content content;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentDrop"/> class.
        /// </summary>
        /// <param name="content">The content from which to create the content drop.</param>
        public ContentDrop(Content content)
        {
            this.content = content ?? throw new System.ArgumentNullException(nameof(content));
            this.Tags = new ReadOnlyCollection<string>(content.Tags);
            this.CategoryPaths = new ReadOnlyCollection<string>(content.CategoryPaths);
        }

        /// <summary>
        /// Gets the author name.
        /// </summary>
        public string AuthorName => this.content.Author.UserName;

        /// <summary>
        /// Gets the author's user id.
        /// </summary>
        public string AuthorId => this.content.Author.UserId;

        /// <summary>
        /// Gets the category paths for the content.
        /// </summary>
        public IReadOnlyList<string> CategoryPaths { get; }

        /// <summary>
        /// Gets the culture name for the content.
        /// </summary>
        public string CultureName => this.content.Culture.Name;

        /// <summary>
        /// Gets the fully localized culture display name for the content.
        /// </summary>
        public string CultureDisplayName => this.content.Culture.DisplayName;

        /// <summary>
        /// Gets the English name for the culture.
        /// </summary>
        public string CultureEnglishName => this.content.Culture.EnglishName;

        /// <summary>
        /// Gets the name of the culture in its locale.
        /// </summary>
        public string CultureNativeName => this.content.Culture.NativeName;

        /// <summary>
        /// Gets the description of the content.
        /// </summary>
        public string Description => this.content.Description;

        /// <summary>
        /// Gets the slug for the content.
        /// </summary>
        public string Slug => this.content.Slug;

        /// <summary>
        /// Gets the metadata as a <see cref="JObject"/>.
        /// </summary>
        public JObject Metadata => this.content.Metadata; // TODO: https://github.com/corvus-dotnet/Corvus.Extensions.Newtonsoft.Json/blob/be42861c19409e86bb4d51a0f9c4ed1eff26a102/Solutions/Corvus.Extensions.Newtonsoft.Json/Corvus/Extensions/Json/Internal/JsonNetPropertyBag.cs

    /// <summary>
    /// Gets the tags.
    /// </summary>
    public IReadOnlyList<string> Tags { get; }

        /// <summary>
        /// Gets the title for the content.
        /// </summary>
        public string Title => this.content.Title;
    }
}
