// <copyright file="RendererContentFactoryExtensions.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Microsoft.Extensions.DependencyInjection
{
    using System.Linq;
    using Corvus.ContentHandling;
    using Marain.Cms;
    using Marain.Cms.Internal;

    /// <summary>
    /// Extensions for the <see cref="ContentFactory"/>.
    /// </summary>
    public static class RendererContentFactoryExtensions
    {
        /// <summary>
        /// Registers the renderers with the factory.
        /// </summary>
        /// <param name="factory">The factory with which to register the content.</param>
        /// <returns>The factory with the content registered.</returns>
        public static ContentFactory RegisterRenderers(this ContentFactory factory)
        {
            factory.RegisterTransientContent<AbTestSetRenderer>();
            factory.RegisterTransientContent<CompoundPayloadRenderer>();
            factory.RegisterTransientContent<ContentFragmentRenderer>();
            return factory;
        }

        /// <summary>
        /// Add content management content to the container.
        /// </summary>
        /// <param name="serviceCollection">The service collection to which to add the content.</param>
        /// <returns>The service collection wth the content added.</returns>
        public static IServiceCollection AddMarkdownRenderer(this IServiceCollection serviceCollection)
        {
            if (serviceCollection.Any(s => s.ServiceType == typeof(IContentRendererFactory)))
            {
                return serviceCollection;
            }

            serviceCollection.AddContentManagementContent();
            serviceCollection.AddSingleton<IContentRendererFactory, ContentRendererFactory>();
            return serviceCollection.AddContent(factory =>
            {
                factory.RegisterRenderers();
            });
        }
    }
}
