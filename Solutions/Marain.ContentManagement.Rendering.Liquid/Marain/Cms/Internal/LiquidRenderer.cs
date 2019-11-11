// <copyright file="LiquidRenderer.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Internal
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Corvus.Extensions.Json;
    using DotLiquid;

    /// <summary>
    /// Writes a content fragment to the output stream.
    /// </summary>
    public class LiquidRenderer : IContentRenderer
    {
        /// <summary>
        /// Gets the registered content type for the renderer.
        /// </summary>
        public const string RegisteredContentType = LiquidPayload.RegisteredContentType + ContentRendererFactory.RendererSuffix;

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
            if (currentPayload is LiquidPayload liquid)
            {
                using var writer = new StreamWriter(output, this.Encoding, this.BufferSize, true);
                var template = Template.Parse(liquid.Template);
                template.Render(Hash.FromAnonymousObject(new { content = new ContentDrop(parentContent) }));
            }

            return Task.CompletedTask;
        }
    }
}