// <copyright file="ContentFragmentRenderer.cs" company="Endjin Limited">
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
  /// Writes a content fragment to the output stream.
  /// </summary>
    public class ContentFragmentRenderer : IContentRenderer
    {
        /// <summary>
        /// Gets the registered content type for the renderer.
        /// </summary>
        public const string RegisteredContentType = ContentFragmentPayload.RegisteredContentType + ContentRendererFactory.RendererSuffix;

        /// <summary>
        /// Gets the content type for the renderer.
        /// </summary>
        public string ContentType => RegisteredContentType;

        /// <inheritdoc/>
        public async Task RenderAsync(TextWriter output, Content parentContent, IContentPayload currentPayload, IPropertyBag context)
        {
            if (currentPayload is ContentFragmentPayload fragment)
            {
                await output.WriteAsync(fragment.Fragment).ConfigureAwait(false);
            }
            else
            {
                throw new ArgumentException(nameof(currentPayload));
            }
        }
    }
}