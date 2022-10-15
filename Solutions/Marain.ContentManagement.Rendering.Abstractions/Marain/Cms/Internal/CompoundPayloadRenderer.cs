// <copyright file="CompoundPayloadRenderer.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Internal
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Corvus.Extensions.Json;
  using Corvus.Json;

  /// <summary>
  /// Writes the content for each of the <see cref="CompoundPayload.Children"/>, in order, to the output stream.
  /// </summary>
    public class CompoundPayloadRenderer : IContentRenderer
    {
        /// <summary>
        /// Gets the registered content type for the renderer.
        /// </summary>
        public const string RegisteredContentType = CompoundPayload.RegisteredContentType + ContentRendererFactory.RendererSuffix;

        private readonly IContentRendererFactory contentRendererFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompoundPayloadRenderer"/> class.
        /// </summary>
        /// <param name="contentRendererFactory">The <see cref="IContentRendererFactory"/> used to create renderers for child content.</param>
        public CompoundPayloadRenderer(IContentRendererFactory contentRendererFactory)
        {
            this.contentRendererFactory = contentRendererFactory;
        }

        /// <summary>
        /// Gets the content type for the renderer.
        /// </summary>
        public string ContentType => RegisteredContentType;

        /// <inheritdoc/>
        public async Task RenderAsync(TextWriter output, Content parentContent, IContentPayload currentPayload, IPropertyBag context)
        {
            if (currentPayload is CompoundPayload compoundPayload)
            {
                foreach (IContentPayload child in compoundPayload.Children)
                {
                    IContentRenderer renderer = this.contentRendererFactory.GetRendererFor(child);
                    await renderer.RenderAsync(output, parentContent, child, context).ConfigureAwait(false);
                }
            }
            else
            {
                throw new ArgumentException(nameof(currentPayload));
            }
        }
    }
}