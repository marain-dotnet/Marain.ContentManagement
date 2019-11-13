// <copyright file="LiquidContent.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Internal
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using DotLiquid;

    /// <summary>
    /// A tag to render content inside a liquid template.
    /// </summary>
    public class LiquidContent : Tag
    {
        private readonly IContentStore contentStore;
        private readonly IContentRendererFactory contentRendererFactory;
        private string slug;

        /// <summary>
        /// Initializes a new instance of the <see cref="LiquidContent"/> class.
        /// </summary>
        /// <param name="contentStore">The content store.</param>
        /// <param name="contentRendererFactory">The content renderer factory.</param>
        public LiquidContent(IContentStore contentStore, IContentRendererFactory contentRendererFactory)
        {
            this.contentStore = contentStore;
            this.contentRendererFactory = contentRendererFactory;
        }

        /// <inheritdoc/>
        public override void Initialize(string tagName, string markup, List<string> tokens)
        {
            base.Initialize(tagName, markup, tokens);
            this.slug = tokens[0];
        }

        /// <inheritdoc/>
        public override Task RenderAsync(Context context, TextWriter result)
        {
            return context.Stack(async () =>
            {
                Content content = await this.contentStore.GetPublishedContentAsync(this.slug).ConfigureAwait(false);
                IContentRenderer renderer = this.contentRendererFactory.GetRendererFor(content.ContentPayload);
                await renderer.RenderAsync(result, content, content.ContentPayload, null).ConfigureAwait(false);
            });
        }
    }
}
