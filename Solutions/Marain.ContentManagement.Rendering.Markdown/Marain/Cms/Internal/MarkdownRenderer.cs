// <copyright file="MarkdownRenderer.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Internal
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Corvus.Extensions.Json;
    using Markdig;

    /// <summary>
    /// Writes a content fragment to the output stream.
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
        /// Gets or sets the default encoding for the stream.
        /// </summary>
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// Gets or sets the default buffer size for the stream.
        /// </summary>
        public int BufferSize { get; set; } = 1024;

        /// <summary>
        /// Gets the content type for the renderer.
        /// </summary>
        public string ContentType => RegisteredContentType;

        /// <inheritdoc/>
        public Task RenderAsync(Stream output, Content parentContent, IContentPayload currentPayload, PropertyBag context)
        {
            if (currentPayload is MarkdownPayload markdown)
            {
                using var writer = new StreamWriter(output, this.Encoding, this.BufferSize, true);
                Markdown.ToHtml(markdown.Markdown, writer, this.pipeline);
            }

            return Task.CompletedTask;
        }
    }
}