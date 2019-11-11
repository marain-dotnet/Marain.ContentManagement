// <copyright file="MarkdownRendererContentFactoryExtensions.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Microsoft.Extensions.DependencyInjection
{
    using System.Linq;
    using Corvus.ContentHandling;
    using Marain.Cms;
    using Marain.Cms.Internal;
    using Markdig;

    /// <summary>
    /// Extensions for the <see cref="ContentFactory"/>.
    /// </summary>
    public static class MarkdownRendererContentFactoryExtensions
    {
        /// <summary>
        /// Registers the content management content types with the factory.
        /// </summary>
        /// <param name="factory">The factory with which to register the content.</param>
        /// <returns>The factory with the content registered.</returns>
        public static ContentFactory RegisterMarkdownRenderer(this ContentFactory factory)
        {
            factory.RegisterTransientContent<MarkdownPayload>();
            factory.RegisterTransientContent<MarkdownRenderer>();
            return factory;
        }

        /// <summary>
        /// Add content management content to the container.
        /// </summary>
        /// <param name="serviceCollection">The service collection to which to add the content.</param>
        /// <returns>The service collection wth the content added.</returns>
        public static IServiceCollection AddMarkdownRenderer(this IServiceCollection serviceCollection)
        {
            if (!serviceCollection.Any(s => s.ServiceType == typeof(MarkdownPipeline)))
            {
                serviceCollection.AddSingleton(new MarkdownPipelineBuilder().UseAdvancedExtensions().Build());
            }

            serviceCollection.AddContentManagementRendering();

            return serviceCollection.AddContent(factory =>
            {
                factory.RegisterMarkdownRenderer();
            });
        }
    }
}
