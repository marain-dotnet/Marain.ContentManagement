// <copyright file="MarkdownRenderer.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Internal
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Corvus.Extensions.Json;
    using Markdig;

    /// <summary>
    /// Writes markdown content to the output stream.
    /// </summary>
    public class MarkdownRenderer : IContentRenderer
    {
        /// <summary>
        /// Gets the registered content type for the renderer.
        /// </summary>
        public const string RegisteredContentType = MarkdownPayload.RegisteredContentType + ContentRendererFactory.RendererSuffix;

        private readonly MarkdownPipeline pipeline;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownRenderer"/> class.
        /// </summary>
        /// <param name="pipeline">The pipeline to use for the markdown rendering.</param>
        public MarkdownRenderer(MarkdownPipeline pipeline)
        {
            this.pipeline = pipeline;
        }

        /// <summary>
        /// Gets the content type for the renderer.
        /// </summary>
        public string ContentType => RegisteredContentType;

        /// <inheritdoc/>
        public Task RenderAsync(TextWriter output, Content parentContent, IContentPayload currentPayload, PropertyBag context)
        {
            if (currentPayload is MarkdownPayload markdown)
            {
                Markdown.ToHtml(markdown.Markdown, output, this.pipeline);
            }
            else
            {
                return Task.FromException(new ArgumentException(nameof(currentPayload)));
            }

            return Task.CompletedTask;
        }
    }
}