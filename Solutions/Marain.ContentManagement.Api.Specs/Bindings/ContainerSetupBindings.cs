// <copyright file="ContainerSetupBindings.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Bindings
{
    using Corvus.SpecFlow.Extensions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using TechTalk.SpecFlow;

    /// <summary>
    /// SpecFlow binding to set up the container for content management tests.
    /// </summary>
    [Binding]
    public class ContainerSetupBindings
    {
        /// <summary>
        /// Adds necessary services to the container so that they can be made available for test setup.
        /// </summary>
        /// <param name="context">The current <c>ScenarioContext</c>.</param>
        /// <remarks>
        /// We add the services required for content management so that we can setup test data via the content store directly.
        /// We also add tenant services so that we can create transient tenants for the scenarios and clean them up after
        /// each scenario. These have to have the same configuration as is being used in the service itself for everything
        /// to work as intended.
        /// </remarks>
        [BeforeScenario("@perScenarioContainer", Order = ContainerBeforeScenarioOrder.PopulateServiceCollection)]
        public void PopulateScenarioServiceCollection(ScenarioContext context)
        {
            ContainerBindings.ConfigureServices(context, services =>
            {
                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                    .AddEnvironmentVariables()
                    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);

                IConfigurationRoot root = configurationBuilder.Build();

                services.AddSingleton(root);

                services.AddLogging();

                services.AddContentManagementContent();

                services.AddTenantCloudBlobContainerFactory(root);
                services.AddTenantProviderBlobStore();
                services.AddTenantedAzureCosmosContentStore(root);
            });
        }
    }
}
