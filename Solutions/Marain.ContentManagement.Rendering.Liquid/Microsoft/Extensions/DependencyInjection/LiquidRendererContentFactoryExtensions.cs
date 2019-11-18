// <copyright file="LiquidRendererContentFactoryExtensions.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Microsoft.Extensions.DependencyInjection
{
    using Corvus.ContentHandling;
    using Marain.Cms;
    using Marain.Cms.Internal;

    /// <summary>
    /// Extensions for the <see cref="ContentFactory"/>.
    /// </summary>
    public static class LiquidRendererContentFactoryExtensions
    {
        /// <summary>
        /// Registers the content management content types with the factory.
        /// </summary>
        /// <param name="factory">The factory with which to register the content.</param>
        /// <returns>The factory with the content registered.</returns>
        public static ContentFactory RegisterLiquidRenderer(this ContentFactory factory)
        {
            factory.RegisterTransientContent<LiquidPayload>();
            factory.RegisterTransientContent<LiquidWithMarkdownPayload>();
            factory.RegisterTransientContent<LiquidRenderer>();
            factory.RegisterTransientContent<LiquidWithMarkdownRenderer>();
            return factory;
        }

        /// <summary>
        /// Add content management content to the container.
        /// </summary>
        /// <param name="serviceCollection">The service collection to which to add the content.</param>
        /// <returns>The service collection wth the content added.</returns>
        public static IServiceCollection AddLiquidRenderer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddContentManagementRendering();
            return serviceCollection.AddContent(factory => factory.RegisterLiquidRenderer());
        }
    }
}
