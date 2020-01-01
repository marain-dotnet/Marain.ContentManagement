// <copyright file="TransientTenantBindings.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Bindings
{
    using System;
    using System.Threading.Tasks;
    using Corvus.Azure.Cosmos.Tenancy;
    using Corvus.SpecFlow.Extensions;
    using Corvus.Tenancy;
    using Microsoft.Extensions.DependencyInjection;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Bindings to manage creation and deletion of tenants for test scenarios.
    /// </summary>
    [Binding]
    public static class TransientTenantBindings
    {
        /// <summary>
        /// Creates a new <see cref="ITenant"/> for the current scenario, adding a test <see cref="CosmosConfiguration"/>
        /// to the tenant data.
        /// </summary>
        /// <param name="context">The current <see cref="ScenarioContext"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>
        /// The newly created tenant is added to the <see cref="ScenarioContext"/>. Access it via the helper methods
        /// <see cref="CurrentTenant(ScenarioContext)"/> or <see cref="CurrentTenantId(ScenarioContext)"/>.
        /// </remarks>
        [BeforeScenario("useTransientTenant", Order = 10)]
        public static async Task SetupTransientTenant(ScenarioContext context)
        {
            // This needs to run after the ServiceProvider has been constructed
            IServiceProvider provider = context.ServiceProvider();
            ITenantProvider tenantProvider = provider.GetRequiredService<ITenantProvider>();

            // In order to ensure the Cosmos aspects of the Tenancy setup are fully configured, we need to resolve
            // the ITenantCosmosContainerFactory, which triggers setting default config to the root tenant.
            provider.GetRequiredService<ITenantCosmosContainerFactory>();

            ITenant rootTenant = tenantProvider.Root;
            ITenant transientTenant = await tenantProvider.CreateChildTenantAsync(rootTenant.Id).ConfigureAwait(false);

            CosmosConfiguration config = rootTenant.GetDefaultCosmosConfiguration() ?? new CosmosConfiguration();
            config.DatabaseName = "endjinspecssharedthroughput";
            config.DisableTenantIdPrefix = true;
            transientTenant.SetDefaultCosmosConfiguration(config);

            await tenantProvider.UpdateTenantAsync(transientTenant).ConfigureAwait(false);

            context.Set(transientTenant);
        }

        /// <summary>
        /// Tears down the transient tenant created for the current scenario.
        /// </summary>
        /// <param name="context">The current <see cref="ScenarioContext"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [AfterScenario("useTransientTenant")]
        public static Task TearDownTransientTenant(ScenarioContext context)
        {
            return context.RunAndStoreExceptionsAsync(() =>
            {
                IServiceProvider provider = context.ServiceProvider();
                ITenantProvider tenantProvider = provider.GetRequiredService<ITenantProvider>();

                ITenant tenant = context.Get<ITenant>();
                return tenantProvider.DeleteTenantAsync(tenant.Id);
            });
        }

        /// <summary>
        /// Retrieves the transient tenant created for the current scenario from the supplied <see cref="ScenarioContext"/>,
        /// or null if there is none.
        /// </summary>
        /// <param name="context">The current <see cref="ScenarioContext"/>.</param>
        /// <returns>The <see cref="ITenant"/>.</returns>
        public static ITenant CurrentTenant(this ScenarioContext context)
        {
            context.TryGetValue(out ITenant result);
            return result;
        }

        /// <summary>
        /// Retrieves the Id of the transient tenant created for the current scenario from the supplied scenario context.
        /// <see cref="ScenarioContext"/>.
        /// </summary>
        /// <param name="context">The current <see cref="ScenarioContext"/>.</param>
        /// <returns>The Id of the <see cref="ITenant"/>.</returns>
        /// <exception cref="ArgumentNullException">There is no current tenant.</exception>
        public static string CurrentTenantId(this ScenarioContext context)
        {
            return context.CurrentTenant().Id;
        }
    }
}
