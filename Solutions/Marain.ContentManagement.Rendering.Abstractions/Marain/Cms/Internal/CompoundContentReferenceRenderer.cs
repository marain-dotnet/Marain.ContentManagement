﻿// <copyright file="CompoundContentReferenceRenderer.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Internal
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Corvus.Extensions.Json;

    /// <summary>
    /// Writes the content for each of the <see cref="CompoundContentReferencePayload.Children"/>, in order, to the output stream.
    /// </summary>
    public class CompoundContentReferenceRenderer : IContentRenderer
    {
        /// <summary>
        /// Gets the registered content type for the renderer.
        /// </summary>
        public const string RegisteredContentType = CompoundContentReferencePayload.RegisteredContentType + ContentRendererFactory.RendererSuffix;

        private readonly IContentRendererFactory contentRendererFactory;
        private readonly IContentStore contentStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompoundContentReferenceRenderer"/> class.
        /// </summary>
        /// <param name="contentRendererFactory">The <see cref="IContentRendererFactory"/> used to create renderers for child content.</param>
        /// <param name="contentStore">The content store from which content can be retrieved.</param>
        public CompoundContentReferenceRenderer(IContentRendererFactory contentRendererFactory, IContentStore contentStore)
        {
            this.contentRendererFactory = contentRendererFactory;
            this.contentStore = contentStore;
        }

        /// <summary>
        /// Gets the content type for the renderer.
        /// </summary>
        public string ContentType => RegisteredContentType;

        /// <inheritdoc/>
        public async Task RenderAsync(TextWriter output, Content parentContent, IContentPayload currentPayload, PropertyBag context)
        {
            if (currentPayload is CompoundContentReferencePayload compoundPayload)
            {
                foreach (ContentReference child in compoundPayload.Children)
                {
                    Content content = await this.contentStore.GetContentAsync(child.Id, child.Slug).ConfigureAwait(false);
                    IContentRenderer renderer = this.contentRendererFactory.GetRendererFor(content.ContentPayload);
                    await renderer.RenderAsync(output, content, content.ContentPayload, context).ConfigureAwait(false);
                }
            }
            else
            {
                throw new ArgumentException(nameof(currentPayload));
            }
        }
    }
}