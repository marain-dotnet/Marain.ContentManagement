// <copyright file="PublicationWorkflowContentRenderer.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Internal
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Corvus.Extensions.Json;
    using Marain.Cms;

    /// <summary>
    /// Writes the content for the instance of the content in the state in the <see cref="WellKnownWorkflowId.ContentPublication"/> workflow that is specified by the value for the <see cref="PublicationStateContextKey"/> in the context, to the output stream.
    /// </summary>
    public class PublicationWorkflowContentRenderer : IContentRenderer
    {
        /// <summary>
        /// The context key for the publication state to render.
        /// </summary>
        public const string PublicationStateContextKey = "PublicationStateToRender";

        /// <summary>
        /// Gets the registered content type for the renderer.
        /// </summary>
        public const string RegisteredContentType = PublicationWorkflowContentPayload.RegisteredContentType + ContentRendererFactory.RendererSuffix;

        private readonly IContentRendererFactory contentRendererFactory;
        private readonly IContentStore contentStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicationWorkflowContentRenderer"/> class.
        /// </summary>
        /// <param name="contentRendererFactory">The <see cref="IContentRendererFactory"/> used to create renderers for child content.</param>
        /// <param name="contentStore">The content store.</param>
        public PublicationWorkflowContentRenderer(IContentRendererFactory contentRendererFactory, IContentStore contentStore)
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
            if (!context.TryGet(PublicationStateContextKey, out PublicationStateToRender stateToRender))
            {
                throw new InvalidOperationException($"The context must contain the '{PublicationStateContextKey}' property");
            }

            if (currentPayload is PublicationWorkflowContentPayload payload)
            {
                ContentWithState content = await this.contentStore.GetContentForWorkflowAsync(payload.Slug, WellKnownWorkflowId.ContentPublication).ConfigureAwait(false);
                if (CanRender(content, stateToRender))
                {
                    IContentRenderer renderer = this.contentRendererFactory.GetRendererFor(content.Content.ContentPayload);
                    await renderer.RenderAsync(output, content.Content, content.Content.ContentPayload, context).ConfigureAwait(false);
                }
                else
                {
                    // TODO: How are we rendering error/invalid/missing states?
                }
            }
            else
            {
                throw new ArgumentException(nameof(currentPayload));
            }
        }

        private static bool CanRender(ContentWithState content, PublicationStateToRender stateToRender)
        {
            return
                 content.StateName == ContentPublicationContentState.Published ||
                (content.StateName == ContentPublicationContentState.Draft && stateToRender == PublicationStateToRender.PublishedOrDraft);
        }
    }
}