// <copyright file="AbTestSetRenderer.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Internal
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Corvus.Extensions.Json;

    /// <summary>
    /// Writes a content fragment to the output stream.
    /// </summary>
    public class AbTestSetRenderer : IContentRenderer
    {
        /// <summary>
        /// The context key for the AB Test id.
        /// </summary>
        public const string AbTestIdContextKey = "AbTestId";

        /// <summary>
        /// Gets the registered content type for the renderer.
        /// </summary>
        public const string RegisteredContentType = AbTestSet.RegisteredContentType + ContentRendererFactory.RendererSuffix;

        private readonly IContentRendererFactory contentRendererFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbTestSetRenderer"/> class.
        /// </summary>
        /// <param name="contentRendererFactory">The <see cref="IContentRendererFactory"/> used to create renderers for child content.</param>
        public AbTestSetRenderer(IContentRendererFactory contentRendererFactory)
        {
            this.contentRendererFactory = contentRendererFactory;
        }

        /// <summary>
        /// Gets the content type for the renderer.
        /// </summary>
        public string ContentType => RegisteredContentType;

        /// <inheritdoc/>
        public async Task RenderAsync(TextWriter output, Content parentContent, IContentPayload currentPayload, PropertyBag context)
        {
            if (!context.TryGet(AbTestIdContextKey, out string abTestId))
            {
                throw new InvalidOperationException($"The context must contain the '{AbTestIdContextKey}' property");
            }

            if (currentPayload is AbTestSet testSet)
            {
                Content content = await testSet.GetContentForAbGroupAsync(abTestId).ConfigureAwait(false);
                IContentRenderer renderer = this.contentRendererFactory.GetRendererFor(content.ContentPayload);
                await renderer.RenderAsync(output, content, content.ContentPayload, context).ConfigureAwait(false);
            }
        }
    }
}