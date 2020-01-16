// <copyright file="ContentRendererFactory.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Internal
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Implements the <see cref="IContentRendererFactory"/> interface over our
    /// content type resolution mechanism.
    /// </summary>
    public class ContentRendererFactory : IContentRendererFactory
    {
        /// <summary>
        /// The suffix for a content renderer.
        /// </summary>
        public const string RendererSuffix = "+contentrenderer";

        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentRendererFactory"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider from which to retrieve the renderer.</param>
        public ContentRendererFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <inheritdoc/>
        public IContentRenderer GetRendererFor(IContentPayload contentPayload)
        {
            return this.serviceProvider.GetRequiredContent<IContentRenderer>(contentPayload.ContentType + RendererSuffix);
        }
    }
}
