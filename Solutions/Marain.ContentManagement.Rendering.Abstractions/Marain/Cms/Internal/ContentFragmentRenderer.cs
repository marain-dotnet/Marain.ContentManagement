// <copyright file="ContentFragmentRenderer.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Internal
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Corvus.Extensions.Json;

    /// <summary>
    /// Writes a content fragment to the output stream.
    /// </summary>
    public class ContentFragmentRenderer : IContentRenderer
    {
        /// <summary>
        /// Gets the registered content type for the renderer.
        /// </summary>
        public const string RegisteredContentType = ContentFragment.RegisteredContentType + ContentRendererFactory.RendererSuffix;

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
        public async Task RenderAsync(Stream output, Content parentContent, IContentPayload currentPayload, PropertyBag context)
        {
            if (currentPayload is ContentFragment fragment)
            {
                using var writer = new StreamWriter(output, this.Encoding, this.BufferSize, true);
                await writer.WriteAsync(fragment.Fragment).ConfigureAwait(false);
            }
        }
    }
}