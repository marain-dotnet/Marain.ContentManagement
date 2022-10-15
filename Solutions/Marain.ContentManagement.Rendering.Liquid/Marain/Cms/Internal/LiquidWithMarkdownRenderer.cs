// <copyright file="LiquidWithMarkdownRenderer.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Internal
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Corvus.Extensions.Json;
    using Corvus.Json;
    using DotLiquid;
    using Markdig;

    /// <summary>
    /// Writes a liquid template containing markdown to the output stream.
    /// </summary>
    public class LiquidWithMarkdownRenderer : IContentRenderer
    {
        /// <summary>
        /// Gets the registered content type for the renderer.
        /// </summary>
        public const string RegisteredContentType = LiquidWithMarkdownPayload.RegisteredContentType + ContentRendererFactory.RendererSuffix;

        private readonly MarkdownPipeline pipeline;

        /// <summary>
        /// Initializes a new instance of the <see cref="LiquidWithMarkdownRenderer"/> class.
        /// </summary>
        /// <param name="pipeline">The pipeline to use for the liquid with markdown rendering.</param>
        public LiquidWithMarkdownRenderer(MarkdownPipeline pipeline)
        {
            this.pipeline = pipeline;
        }

        /// <summary>
        /// Gets the content type for the renderer.
        /// </summary>
        public string ContentType => RegisteredContentType;

        /// <inheritdoc/>
        public async Task RenderAsync(TextWriter output, Content parentContent, IContentPayload currentPayload, IPropertyBag context)
        {
            if (currentPayload is LiquidWithMarkdownPayload liquid)
            {
                var template = Template.Parse(liquid.Template);
                string markdown = await template.RenderAsync(Hash.FromAnonymousObject(new { content = new ContentDrop(parentContent) })).ConfigureAwait(false);
                Markdown.ToHtml(markdown, output, this.pipeline);
            }
            else
            {
                throw new ArgumentException(nameof(currentPayload));
            }
        }
    }
}