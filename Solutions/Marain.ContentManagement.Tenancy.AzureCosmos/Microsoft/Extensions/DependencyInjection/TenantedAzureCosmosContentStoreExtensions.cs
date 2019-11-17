// <copyright file="TenantedAzureCosmosContentStoreExtensions.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Microsoft.Extensions.DependencyInjection
{
    using System.Linq;
    using Corvus.Azure.Cosmos.Tenancy;
    using Marain.Cms;
    using Marain.ContentManagement;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Service collection extensions to add implementations of content stores.
    /// </summary>
    public static class TenantedAzureCosmosContentStoreExtensions
    {
        /// <summary>
        /// Adds Cosmos-based implementation of <see cref="ITenantedContentStoreFactory"/> to the service container.
        /// </summary>
        /// <param name="services">
        /// The collection.
        /// </param>
        /// <param name="configuration">The configuration from which to initialize the factory.</param>
        /// <returns>
        /// The configured <see cref="IServiceCollection"/>.
        /// </returns>
        public static IServiceCollection AddTenantedAzureCosmosContentStore(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new System.ArgumentNullException(nameof(configuration));
            }

            if (services.Any(s => s.ServiceType is ITenantedContentStoreFactory))
            {
                return services;
            }

            var containerDefinition = new CosmosContainerDefinition("content", "content", Content.PartitionKeyPath);
            services.AddSingleton(containerDefinition);

            services.AddTenantCosmosContainerFactory(configuration);

            services.AddSingleton<ITenantedContentStoreFactory, TenantedCosmosContentStoreFactory>();

            return services;
        }
    }
}
