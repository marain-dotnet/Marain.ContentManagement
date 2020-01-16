// <copyright file="TenantedCosmosContentStoreFactory.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System.Threading.Tasks;
    using Corvus.Azure.Cosmos.Tenancy;
    using Corvus.Tenancy;
    using Microsoft.Azure.Cosmos;

    /// <summary>
    /// Factory class for retrieving Cosmos-based instances of <see cref="IContentStore"/> for specific <see cref="Tenant"/>s.
    /// </summary>
    public class TenantedCosmosContentStoreFactory : ITenantedContentStoreFactory
    {
        private readonly ITenantProvider tenantProvider;
        private readonly ITenantCosmosContainerFactory containerFactory;
        private readonly CosmosContainerDefinition containerDefinition;

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantedCosmosContentStoreFactory"/> class.
        /// </summary>
        /// <param name="tenantProvider">The <see cref="ITenantProvider"/> that will be used to retrieve Tenant info.</param>
        /// <param name="containerFactory">The <see cref="ITenantCosmosContainerFactory"/> that will be used to create
        /// underlying <see cref="Container"/> instances for the content stores.</param>
        /// <param name="containerDefinition">The <see cref="CosmosContainerDefinition"/> to use when creating tenanted
        /// <see cref="Container"/> instances.</param>
        public TenantedCosmosContentStoreFactory(
            ITenantProvider tenantProvider,
            ITenantCosmosContainerFactory containerFactory,
            CosmosContainerDefinition containerDefinition)
        {
            this.tenantProvider = tenantProvider;
            this.containerFactory = containerFactory;
            this.containerDefinition = containerDefinition;
        }

        /// <inheritdoc/>
        public async Task<IContentStore> GetContentStoreForTenantAsync(string tenantId)
        {
            ITenant tenant = await this.tenantProvider.GetTenantAsync(tenantId).ConfigureAwait(false);
            Container container = await this.containerFactory.GetContainerForTenantAsync(tenant, this.containerDefinition).ConfigureAwait(false);

            // No need to cache these instances as they are lightweight wrappers around the container.
            return new CosmosContentStore(container);
        }
    }
}
