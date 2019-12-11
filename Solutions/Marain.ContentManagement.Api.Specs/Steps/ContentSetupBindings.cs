// <copyright file="ContentSetupBindings.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Steps
{
    using System.Threading.Tasks;
    using Marain.Cms;
    using Marain.ContentManagement.Specs.Bindings;
    using Marain.ContentManagement.Specs.Drivers;
    using Microsoft.Extensions.DependencyInjection;
    using TechTalk.SpecFlow;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented

    [Binding]
    public class ContentSetupBindings
    {
        private readonly ScenarioContext scenarioContext;

        public ContentSetupBindings(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Given("there is no content available")]
        public void GivenThereIsNoContentAvailable()
        {
        }

        [Given("a content item has been created")]
        [Given("content items have been created")]
        public async Task GivenAContentItemHasBeenCreated(Table table)
        {
            ITenantedContentStoreFactory contentStoreFactory = this.scenarioContext.ServiceProvider().GetRequiredService<ITenantedContentStoreFactory>();
            IContentStore store = await contentStoreFactory.GetContentStoreForTenantAsync(this.scenarioContext.CurrentTenantId()).ConfigureAwait(false);

            foreach (TableRow row in table.Rows)
            {
                (Content content, string name) = ContentDriver.GetContentFor(row);
                Content storedContent = await store.StoreContentAsync(content).ConfigureAwait(false);
                this.scenarioContext.Set(storedContent, name);
            }
        }

        [Given("I have a new content item")]
        public void GivenIHaveANewContentItem(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                (Content content, string name) = ContentDriver.GetContentFor(row);
                this.scenarioContext.Set(content, name);
            }
        }

        [Given("a workflow state has been set for the content item")]
        public async Task GivenAWorkflowStateHasBeenSetForTheContentItem(Table table)
        {
            ITenantedContentStoreFactory contentStoreFactory = this.scenarioContext.ServiceProvider().GetRequiredService<ITenantedContentStoreFactory>();
            IContentStore store = await contentStoreFactory.GetContentStoreForTenantAsync(this.scenarioContext.CurrentTenantId()).ConfigureAwait(false);

            foreach (TableRow row in table.Rows)
            {
                (ContentState state, string name) = ContentDriver.GetContentStateFor(row);
                ContentState storedContentState = await store.SetContentWorkflowStateAsync(state.Slug, state.ContentId, state.WorkflowId, state.StateName, state.ChangedBy).ConfigureAwait(false);
                this.scenarioContext.Set(storedContentState, name);
            }
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented