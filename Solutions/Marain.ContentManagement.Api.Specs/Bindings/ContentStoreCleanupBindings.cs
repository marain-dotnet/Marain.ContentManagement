// <copyright file="ContentStoreCleanupBindings.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Bindings
{
    using System;
    using System.Threading.Tasks;
    using Corvus.Azure.Cosmos.Tenancy;
    using Corvus.Tenancy;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Extensions.DependencyInjection;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Cleans up the tenant-specific content store that will likely have been created as a result of calls to the API. This
    /// is intended to be used in conjunction with <see cref="TransientTenantBindings"/>, which will create and tear down a
    /// tenant specifically for the current scenario.
    /// </summary>
    [Binding]
    public static class ContentStoreCleanupBindings
    {
        /// <summary>
        /// Gets a reference to the tenant container used for this test, then deletes it.
        /// </summary>
        /// <param name="context">The current <see cref="ScenarioContext"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [AfterScenario]
        public static async Task ClearDownTransientTenantContentStore(ScenarioContext context)
        {
            try
            {
                ITenant currentTenant = context.CurrentTenant();

                IServiceProvider serviceProvider = context.ServiceProvider();
                ITenantCosmosContainerFactory containerFactory = serviceProvider.GetRequiredService<ITenantCosmosContainerFactory>();
                CosmosContainerDefinition containerDefinition = serviceProvider.GetRequiredService<CosmosContainerDefinition>();
                Container container = await containerFactory.GetContainerForTenantAsync(currentTenant, containerDefinition).ConfigureAwait(false);
                await container.DeleteContainerAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // TODO: Wire up to our Corvus exception handling
                Console.WriteLine(ex);
            }
        }
    }
}
