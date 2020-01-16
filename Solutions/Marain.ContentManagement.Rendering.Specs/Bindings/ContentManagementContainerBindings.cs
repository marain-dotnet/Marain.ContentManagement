// <copyright file="ContentManagementContainerBindings.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Rendering.Specs.Bindings
{
    using Corvus.SpecFlow.Extensions;
    using Marain.Cms;
    using Microsoft.Extensions.DependencyInjection;
    using TechTalk.SpecFlow;

    /// <summary>
    ///     Container related bindings to configure the service provider for features.
    /// </summary>
    [Binding]
    public static class ContentManagementContainerBindings
    {
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
                    // Register the fake content store as the content store
                    serviceCollection.AddSingleton<FakeContentStore>();
                    serviceCollection.AddSingleton<IContentStore>(s => s.GetService<FakeContentStore>());
                    serviceCollection.AddLiquidRenderer();
                    serviceCollection.AddMarkdownRenderer();
                });
        }
    }
}