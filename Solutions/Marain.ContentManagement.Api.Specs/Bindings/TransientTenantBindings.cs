using System;
using System.Threading.Tasks;
using Corvus.Azure.Cosmos.Tenancy;
using Corvus.Tenancy;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace Marain.ContentManagement.Specs.Bindings
{
    [Binding]
    public static class TransientTenantBindings
    {
        [BeforeScenario(Order = 10)]
        public static async Task SetupTransientTenant(ScenarioContext context)
        {
            // This needs to run after the ServiceProvider has been constructed
            IServiceProvider provider = context.ServiceProvider();
            ITenantProvider tenantProvider = provider.GetRequiredService<ITenantProvider>();

            ITenant rootTenant = tenantProvider.Root;
            ITenant transientTenant = await tenantProvider.CreateChildTenantAsync(rootTenant.Id).ConfigureAwait(false);

            CosmosConfiguration config = rootTenant.GetDefaultCosmosConfiguration() ?? new CosmosConfiguration();
            config.DatabaseName = "endjinspecssharedthroughput";
            config.DisableTenantIdPrefix = true;
            transientTenant.SetDefaultCosmosConfiguration(config);

            await tenantProvider.UpdateTenantAsync(transientTenant).ConfigureAwait(false);

            context.Set(transientTenant);
        }

        [AfterScenario]
        public static Task TearDownTransientTenant(ScenarioContext context)
        {
            IServiceProvider provider = context.ServiceProvider();
            ITenantProvider tenantProvider = provider.GetRequiredService<ITenantProvider>();

            ITenant tenant = context.Get<ITenant>();
            return tenantProvider.DeleteTenantAsync(tenant.Id);
        }

        public static ITenant CurrentTenant(this ScenarioContext context)
        {
            return context.Get<ITenant>();
        }

        public static string CurrentTenantId(this ScenarioContext context)
        {
            return context.CurrentTenant().Id;
        }
    }
}
