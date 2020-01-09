// <copyright file="LiquidRenderer.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Internal
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Corvus.Extensions.Json;
    using DotLiquid;

    /// <summary>
    /// Writes a liquid template to the output stream.
    /// </summary>
    public class LiquidRenderer : IContentRenderer
    {
        /// <summary>
        /// Gets the registered content type for the renderer.
        /// </summary>
        public const string RegisteredContentType = LiquidPayload.RegisteredContentType + ContentRendererFactory.RendererSuffix;

        /// <summary>
        /// Gets the content type for the renderer.
        /// </summary>
        public string ContentType => RegisteredContentType;

        /// <inheritdoc/>
        public async Task RenderAsync(TextWriter output, Content parentContent, IContentPayload currentPayload, PropertyBag context)
        {
            if (currentPayload is LiquidPayload liquid)
            {
                var template = Template.Parse(liquid.Template);
                string rendered = await template.RenderAsync(Hash.FromAnonymousObject(new { content = new ContentDrop(parentContent) })).ConfigureAwait(false);
                await output.WriteAsync(rendered).ConfigureAwait(false);
            }
            else
            {
                throw new ArgumentException(nameof(currentPayload));
            }
        }
    }
}