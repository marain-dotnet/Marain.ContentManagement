﻿// <copyright file="ContentStoreCleanupBindings.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Bindings
{
    using System;
    using System.Threading.Tasks;
    using Corvus.Azure.Cosmos.Tenancy;
    using Corvus.SpecFlow.Extensions;
    using Corvus.Tenancy;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Extensions.DependencyInjection;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Cleans up the tenant-specific content store that will likely have been created as a result of calls to the API. This
    /// is intended to be used in conjunction with <see cref="TransientTenantBindings"/>, which will create and tear down a
    /// tenant specifically for the current feature.
    /// </summary>
    [Binding]
    public static class ContentStoreCleanupBindings
    {
        /// <summary>
        /// Gets a reference to the tenant container used for this test, then deletes it.
        /// </summary>
        /// <param name="context">The current <see cref="FeatureContext"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>
        /// It's critically important that this runs to clean up Cosmos collections created as part of test execution. As such,
        /// this code runs for every feature but doesn't do anything if it can't obtain both the tenant and service provider
        /// from the <c>FeatureContext</c>.
        /// </remarks>
        [AfterFeature]
        public static Task ClearDownTransientTenantContentStore(FeatureContext context)
        {
            return context.RunAndStoreExceptionsAsync(async () =>
            {
                ITenant currentTenant = context.GetTransientTenant();

                if (currentTenant != null)
                {
                    IServiceProvider serviceProvider = ContainerBindings.GetServiceProvider(context);
                    ITenantCosmosContainerFactory containerFactory = serviceProvider.GetRequiredService<ITenantCosmosContainerFactory>();
                    CosmosContainerDefinition containerDefinition = serviceProvider.GetRequiredService<CosmosContainerDefinition>();
                    Container container = await containerFactory.GetContainerForTenantAsync(currentTenant, containerDefinition).ConfigureAwait(false);
                    await container.DeleteContainerAsync().ConfigureAwait(false);
                }
            });
        }
    }
}
