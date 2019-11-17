// <copyright file="ContentManagementContentFactoryExtensions.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Microsoft.Extensions.DependencyInjection
{
    using Corvus.ContentHandling;
    using Marain.Cms;

    /// <summary>
    /// Extensions for the <see cref="ContentFactory"/>.
    /// </summary>
    public static class ContentManagementContentFactoryExtensions
    {
        /// <summary>
        /// Registers the content management content types with the factory.
        /// </summary>
        /// <param name="factory">The factory with which to register the content.</param>
        /// <returns>The factory with the content registered.</returns>
        public static ContentFactory RegisterContentManagementContent(this ContentFactory factory)
        {
            factory.RegisterTransientContent<Content>();
            factory.RegisterTransientContent<ContentState>();
            factory.RegisterTransientContent<ContentFragment>();
            factory.RegisterTransientContent<AbTestSet>();
            factory.RegisterTransientContent<CompoundPayload>();
            factory.RegisterTransientContent<CompoundContent>();
            return factory;
        }

        /// <summary>
        /// Add content management content to the container.
        /// </summary>
        /// <param name="serviceCollection">The service collection to which to add the content.</param>
        /// <returns>The service collection wth the content added.</returns>
        public static IServiceCollection AddContentManagementContent(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddContent(factory => factory.RegisterContentManagementContent());
        }
    }
}
