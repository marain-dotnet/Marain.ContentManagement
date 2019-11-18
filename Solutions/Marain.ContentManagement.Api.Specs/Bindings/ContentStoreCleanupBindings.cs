namespace Marain.ContentManagement.Specs.Bindings
{
    using System;
    using System.Threading.Tasks;
    using Corvus.Azure.Cosmos.Tenancy;
    using Corvus.Tenancy;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Extensions.DependencyInjection;
    using TechTalk.SpecFlow;

    [Binding]
    public static class ContentStoreCleanupBindings
    {
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
