// <copyright file="ContentManagementCosmosContainerBindings.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Bindings
{
    using System;
    using System.Threading.Tasks;
    using Corvus.Azure.Cosmos.Tenancy;
    using Corvus.SpecFlow.Extensions;
    using Corvus.Tenancy;
    using Marain.Cms;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Extensions.DependencyInjection;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Specflow bindings to support a tenanted cloud blob container.
    /// </summary>
    [Binding]
    public static class ContentManagementCosmosContainerBindings
    {
        /// <summary>
        /// The key for the specs container in the feature context.
        /// </summary>
        public const string ContentManagementSpecsContainer = "ContentManagementSpecsContainer";

        /// <summary>
        /// The key for the content store in the feature context.
        /// </summary>
        public const string ContentManagementSpecsContentStore = "ContentManagementSpecsContentStore";

        /// <summary>
        /// Initializes the container before each feature's tests are run.
        /// </summary>
        /// <param name="featureContext">The SpecFlow test context.</param>
        [BeforeFeature("@setupContainer", Order = ContainerBeforeFeatureOrder.PopulateServiceCollection)]
        public static void InitializeContainer(FeatureContext featureContext)
        {
            ContainerBindings.ConfigureServices(
                featureContext,
                serviceCollection =>
                {
                    serviceCollection.AddSingleton(featureContext);
                    serviceCollection.AddSingleton(s => s.GetRequiredService<FeatureContext>().Get<IContentStore>(ContentManagementCosmosContainerBindings.ContentManagementSpecsContentStore));
                });
        }

        /// <summary>
        /// Set up a tenanted Cloud Blob Container for the feature.
        /// </summary>
        /// <param name="featureContext">The feature context.</param>
        /// <remarks>Note that this sets up a resource in Azure and will incur cost. Ensure the corresponding tear down operation is always run, or verify manually after a test run.</remarks>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [BeforeFeature("@setupTenantedCosmosContainer", Order = ContainerBeforeFeatureOrder.ServiceProviderAvailable)]
        public static async Task SetupCosmosContainerForRootTenant(FeatureContext featureContext)
        {
            IServiceProvider serviceProvider = ContainerBindings.GetServiceProvider(featureContext);
            ITenantCosmosContainerFactory factory = serviceProvider.GetRequiredService<ITenantCosmosContainerFactory>();
            ITenantProvider tenantProvider = serviceProvider.GetRequiredService<ITenantProvider>();

            string containerBase = Guid.NewGuid().ToString();

            CosmosConfiguration config = tenantProvider.Root.GetDefaultCosmosConfiguration();
            config.DatabaseName = "endjinspecssharedthroughput";
            config.DisableTenantIdPrefix = true;
            tenantProvider.Root.SetDefaultCosmosConfiguration(config);

            Container contentManagementSpecsContainer = await factory.GetContainerForTenantAsync(
                tenantProvider.Root,
                new CosmosContainerDefinition("endjinspecssharedthroughput", $"{containerBase}contentmanagementspecs", Content.PartitionKeyPath, databaseThroughput: 400)).ConfigureAwait(false);

            featureContext.Set(contentManagementSpecsContainer, ContentManagementSpecsContainer);
            featureContext.Set<IContentStore>(new CosmosContentStore(contentManagementSpecsContainer), ContentManagementSpecsContentStore);
        }

        /// <summary>
        /// Tear down the tenanted Cloud Blob Container for the feature.
        /// </summary>
        /// <param name="featureContext">The feature context.</param>
        /// <returns>A <see cref="Task"/> which completes once the operation has completed.</returns>
        [AfterFeature("@setupTenantedCosmosContainer", Order = 100000)]
        public static Task TeardownCosmosDB(FeatureContext featureContext)
        {
            return featureContext.RunAndStoreExceptionsAsync(
                async () => await featureContext.Get<Container>(ContentManagementSpecsContainer).DeleteContainerAsync().ConfigureAwait(false));
        }

        /// <summary>
        /// Get the cosmos container that we have set up.
        /// </summary>
        /// <param name="featureContext">The feature context in which to find the container.</param>
        /// <returns>The container we have set up in the feature context.</returns>
        public static Container GetCosmosContainer(FeatureContext featureContext)
        {
            return featureContext.Get<Container>(ContentManagementSpecsContainer);
        }

        /// <summary>
        /// Get the <see cref="IContentStore"/> that we have set up.
        /// </summary>
        /// <param name="featureContext">The feature context in which to find the store.</param>
        /// <returns>The content store that we have set up in the feature context.</returns>
        public static IContentStore GetContentStore(FeatureContext featureContext)
        {
            return featureContext.Get<IContentStore>(ContentManagementSpecsContentStore);
        }
    }
}