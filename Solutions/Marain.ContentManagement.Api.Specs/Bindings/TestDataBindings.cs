// <copyright file="TestDataBindings.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Bindings
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Corvus.SpecFlow.Extensions;
    using Marain.Cms;
    using Marain.ContentManagement.Specs.Drivers;
    using Microsoft.Extensions.DependencyInjection;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Bindings to create test data for a feature.
    /// </summary>
    [Binding]
    public static class TestDataBindings
    {
        /// <summary>
        /// Creates a standard test data set of 30 items and adds them to the store.
        /// </summary>
        /// <param name="featureContext">The feature context that the resulting items will be addd to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [BeforeFeature("addTestContentData", Order = BindingSequence.CreateContentTestData)]
        public static async Task CreateContentTestData(FeatureContext featureContext)
        {
            ITenantedContentStoreFactory contentStoreFactory = ContainerBindings.GetServiceProvider(featureContext).GetRequiredService<ITenantedContentStoreFactory>();
            IContentStore store = await contentStoreFactory.GetContentStoreForTenantAsync(featureContext.GetCurrentTenantId()).ConfigureAwait(false);

            for (int i = 0; i < 30; i++)
            {
                var content = new Content
                {
                    Id = Guid.NewGuid().ToString(),
                    Slug = "slug",
                    Tags = new List<string> { "First tag", "Second tag" },
                    CategoryPaths = new List<string> { "/standard/content;", "/books/hobbit;", "/books/lotr" },
                    Author = new CmsIdentity(Guid.NewGuid().ToString(), "Bilbo Baggins"),
                    Title = "This is the title",
                    Description = "A description of the content",
                    Culture = CultureInfo.GetCultureInfo("en-GB"),
                };

                Content storedContent = await store.StoreContentAsync(content).ConfigureAwait(false);
                featureContext.Set(storedContent, "Content" + i);
            }
        }

        /// <summary>
        /// Creates a standard test data set of 30 ContentState items and adds them to the store.
        /// </summary>
        /// <param name="featureContext">The feature context that the resulting items will be addd to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [BeforeFeature("addTestContentStateData", Order = BindingSequence.CreateContentStateTestData)]
        public static async Task CreateContentStateTestData(FeatureContext featureContext)
        {
            ITenantedContentStoreFactory contentStoreFactory = ContainerBindings.GetServiceProvider(featureContext).GetRequiredService<ITenantedContentStoreFactory>();
            IContentStore store = await contentStoreFactory.GetContentStoreForTenantAsync(featureContext.GetCurrentTenantId()).ConfigureAwait(false);

            for (int i = 0; i < 30; i++)
            {
                var state = new ContentState
                {
                    Id = Guid.NewGuid().ToString(),
                    ContentId = SpecHelpers.ParseSpecValue<string>(featureContext, $"{{Content{i}.Id}}"),
                    Slug = SpecHelpers.ParseSpecValue<string>(featureContext, $"{{Content{i}.Slug}}"),
                    WorkflowId = "workflow1id",
                    ChangedBy = new CmsIdentity(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
                };

                if (i < 4)
                {
                    state.StateName = "draft";
                }
                else if (i < 29)
                {
                    state.StateName = "published";
                }
                else
                {
                    state.StateName = "archived";
                }

                ContentState storedContentState = await store.SetContentWorkflowStateAsync(state.Slug, state.ContentId, state.WorkflowId, state.StateName, state.ChangedBy).ConfigureAwait(false);
                featureContext.Set(storedContentState, $"Content{i}-State");
            }
        }
    }
}
