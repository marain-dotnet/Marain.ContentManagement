﻿// <copyright file="ContentStateBindings.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Steps
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Corvus.SpecFlow.Extensions;
    using Marain.Cms;
    using Marain.Cms.Api.Client;
    using Marain.ContentManagement.Specs.Bindings;
    using Marain.ContentManagement.Specs.Drivers;
    using Marain.ContentManagement.Specs.Helpers;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented

    [Binding]
    public class ContentStateBindings
    {
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;

        public ContentStateBindings(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
        }

        [Given("a workflow state has been set for the content item")]
        [Given("workflow states have been set for the content items")]
        public async Task GivenAWorkflowStateHasBeenSetForTheContentItem(Table table)
        {
            ITenantedContentStoreFactory contentStoreFactory = ContainerBindings.GetServiceProvider(this.featureContext).GetRequiredService<ITenantedContentStoreFactory>();
            IContentStore store = await contentStoreFactory.GetContentStoreForTenantAsync(this.featureContext.GetCurrentTenantId()).ConfigureAwait(false);

            foreach (TableRow row in table.Rows)
            {
                (Cms.ContentState state, string name) = ContentSpecHelpers.GetContentStateFor(row);

                // Cater for the fact that the content item could be in either of scenario or feature context.
                state.ContentId = SpecHelpers.ParseSpecValue<string>(this.scenarioContext, state.ContentId);
                state.ContentId = SpecHelpers.ParseSpecValue<string>(this.featureContext, state.ContentId);
                Cms.ContentState storedContentState = await store.SetContentWorkflowStateAsync(state.Slug, state.ContentId, state.WorkflowId, state.StateName, state.ChangedBy).ConfigureAwait(false);
                this.scenarioContext.Set(storedContentState, name);
            }
        }

        [When("I request the workflow state for slug '(.*)' and workflow Id '(.*)'")]
        public Task WhenIRequestTheWorkflowStateForSlugAndWorkflowId(string slug, string workflowId)
        {
            return this.WhenIRequestTheWorkflowStateForSlugAndWorkflowIdWithEmbedded(null, slug, workflowId);
        }

        [When("I request the workflow state with embedded '(.*)' for slug '(.*)' and workflow Id '(.*)'")]
        public async Task WhenIRequestTheWorkflowStateForSlugAndWorkflowIdWithEmbedded(string embed, string slug, string workflowId)
        {
            string resolvedSlug = SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug);
            string resolvedWorkflowId = SpecHelpers.ParseSpecValue<string>(this.scenarioContext, workflowId);
            Embed? resolvedEmbed = string.IsNullOrEmpty(embed) ? (Embed?)null : Enum.Parse<Embed>(embed, true);

            try
            {
                ContentClient client = this.featureContext.Get<ContentClient>();
                SwaggerResponse<ContentStateResponse> response = await client.GetWorkflowStateAsync(
                    this.featureContext.GetCurrentTenantId(),
                    resolvedSlug,
                    resolvedWorkflowId,
                    resolvedEmbed).ConfigureAwait(false);

                this.scenarioContext.StoreLastApiResponse(response);
            }
            catch (SwaggerException ex)
            {
                this.scenarioContext.StoreLastApiException(ex);
            }
        }

        [Given("I have requested workflow state history for slug '(.*)', workflow Id '(.*)' and state name '(.*)'")]
        [When("I request workflow state history for slug '(.*)', workflow Id '(.*)' and state name '(.*)'")]
        public Task WhenIRequestContentHistoryWithStateForSlugWorkflowIdAndState(string slug, string workflowId, string stateName)
        {
            return this.WhenIRequestWorkflowStateHistoryWithEmbeddedForSlugWorkflowIdAndStateName(null, slug, workflowId, stateName);
        }

        [When("I request workflow state history with embedded '(.*)' for slug '(.*)', workflow Id '(.*)' and state name '(.*)'")]
        public Task WhenIRequestWorkflowStateHistoryWithEmbeddedForSlugWorkflowIdAndStateName(string embed, string slug, string workflowId, string stateName)
        {
            return this.GetWorkflowStateHistoryAndStoreResponseAsync(slug, workflowId, stateName, null, null, embed);
        }

        [Given("I have requested workflow state history for slug '(.*)' and workflow Id '(.*)'")]
        [When("I request workflow state history for slug '(.*)' and workflow Id '(.*)'")]
        public Task WhenIRequestContentHistoryWithStateForSlugAndWorkflowId(string slug, string workflowId)
        {
            return this.WhenIRequestWorkflowStateHistoryWithEmbeddedForSlugAndWorkflowId(null, slug, workflowId);
        }

        [When("I request workflow state history with embedded '(.*)' for slug '(.*)' and workflow Id '(.*)'")]
        public Task WhenIRequestWorkflowStateHistoryWithEmbeddedForSlugAndWorkflowId(string embed, string slug, string workflowId)
        {
            return this.GetWorkflowStateHistoryAndStoreResponseAsync(slug, workflowId, null, null, embed);
        }

        [When("I request workflow state history for slug '(.*)', workflow Id '(.*)' and state name '(.*)' with the contination token from the previous response")]
        public Task WhenIRequestContentHistoryWithStateForSlugWorkflowIdAndStateNameWithTheContinationTokenFromThePreviousResponse(string slug, string workflowId, string stateName)
        {
            SwaggerResponse<ContentStatesResponse> previousResponse = this.scenarioContext.GetLastApiResponse<ContentStatesResponse>();

            string continuationToken = previousResponse.Result.ExtractContinuationToken();

            // Now stash the summaries themselves so we can verify that the ones we get back when we call using the
            // continuation token aren't the same.
            this.scenarioContext.Set(previousResponse.Result.States.ToArray());

            return this.GetWorkflowStateHistoryAndStoreResponseAsync(slug, workflowId, stateName, null, continuationToken, null);
        }

        [When("I request workflow state history for slug '(.*)' and workflow Id '(.*)' with the contination token from the previous response")]
        public Task WhenIRequestContentHistoryWithStateForSlugAndWorkflowIdWithTheContinationTokenFromThePreviousResponse(string slug, string workflowId)
        {
            SwaggerResponse<ContentStatesResponse> previousResponse = this.scenarioContext.GetLastApiResponse<ContentStatesResponse>();

            string continuationToken = previousResponse.Result.ExtractContinuationToken();

            // Now stash the summaries themselves so we can verify that the ones we get back when we call using the
            // continuation token aren't the same.
            this.scenarioContext.Set(previousResponse.Result.States.ToArray());

            return this.GetWorkflowStateHistoryAndStoreResponseAsync(slug, workflowId, null, continuationToken, null);
        }

        [When("I request workflow state history for slug '(.*)', workflow Id '(.*)' and state name '(.*)' with a limit of (.*) items")]
        public Task WhenIRequestContentHistoryWithStateForSlugWorkflowIdAndStateNameWithALimitOfItems(string slug, string workflowId, string stateName, int limit)
        {
            return this.GetWorkflowStateHistoryAndStoreResponseAsync(slug, workflowId, stateName, limit, null, null);
        }

        [When("I request workflow state history for slug '(.*)' and workflow Id '(.*)' with a limit of (.*) items")]
        public Task WhenIRequestContentHistoryWithStateForSlugAndWorkflowIdWithALimitOfItems(string slug, string workflowId, int limit)
        {
            return this.GetWorkflowStateHistoryAndStoreResponseAsync(slug, workflowId, limit, null, null);
        }

        [Then("the response body should contain content state matching '(.*)'")]
        public void ThenTheResponseBodyShouldContentStateMatching(string stateName)
        {
            SwaggerResponse<ContentStateResponse> actual = this.scenarioContext.GetLastApiResponse<ContentStateResponse>();
            Assert.IsNotNull(actual);

            Cms.ContentState expectedState = this.scenarioContext.Get<Cms.ContentState>(stateName);

            ContentSpecHelpers.Compare(expectedState, actual.Result);
        }

        [Then("the response should contain (.*) content states")]
        public void ThenTheResponseShouldContainContentStates(int expectedCount)
        {
            SwaggerResponse<ContentStatesResponse> response = this.scenarioContext.GetLastApiResponse<ContentStatesResponse>();

            ObservableCollection<ContentStateResponse> states = response.Result.States;

            Assert.IsNotNull(states);
            Assert.IsNotEmpty(states);

            Assert.AreEqual(expectedCount, states.Count);
        }

        [Then("the response should contain another (.*) content states")]
        public void ThenTheResponseShouldContainAnotherContentStates(int expectedCount)
        {
            SwaggerResponse<ContentStatesResponse> response = this.scenarioContext.GetLastApiResponse<ContentStatesResponse>();

            ObservableCollection<ContentStateResponse> states = response.Result.States;

            Assert.IsNotNull(states);
            Assert.IsNotEmpty(states);

            Assert.AreEqual(expectedCount, states.Count);

            // Compare against previous set, which have been stored in the scenario context.
            ContentStateResponse[] previousStates = this.scenarioContext.Get<ContentStateResponse[]>();

            foreach (ContentStateResponse current in states)
            {
                Assert.IsFalse(previousStates.Any(x => x.Id == current.Id));
            }
        }

        [Then("each content state in the response should contain a '(.*)' link")]
        public void ThenEachContentStateInTheResponseShouldContainALink(string linkRel)
        {
            SwaggerResponse<ContentStatesResponse> response = this.scenarioContext.GetLastApiResponse<ContentStatesResponse>();

            Assert.IsTrue(response.Result.States.All(x => x._links.ContainsKey(linkRel)));
        }

        [Then("each content state in the response should contain an embedded resource called '(.*)'")]
        public void ThenEachContentStateInTheResponseShouldContainAnEmbeddedResourceCalled(string linkRel)
        {
            SwaggerResponse<ContentStatesResponse> response = this.scenarioContext.GetLastApiResponse<ContentStatesResponse>();

            Assert.IsTrue(response.Result.States.All(x => x._embedded.ContainsKey(linkRel)));
        }

        private async Task GetWorkflowStateHistoryAndStoreResponseAsync(string slug, string workflowId, int? limit, string continuationToken, string embed)
        {
            string resolvedSlug = SpecHelpers.ParseSpecValue<string>(this.featureContext, slug);
            string resolvedWorkflowId = SpecHelpers.ParseSpecValue<string>(this.featureContext, workflowId);
            Embed2? resolvedEmbed = string.IsNullOrEmpty(embed) ? (Embed2?)null : Enum.Parse<Embed2>(embed, true);

            ContentClient client = this.featureContext.Get<ContentClient>();
            SwaggerResponse<ContentStatesResponse> response = await client.GetWorkflowHistoryAsync(
                this.featureContext.GetCurrentTenantId(),
                resolvedWorkflowId,
                resolvedSlug,
                limit,
                continuationToken,
                resolvedEmbed).ConfigureAwait(false);

            this.scenarioContext.StoreLastApiResponse(response);
        }

        private async Task GetWorkflowStateHistoryAndStoreResponseAsync(string slug, string workflowId, string stateName, int? limit, string continuationToken, string embed)
        {
            string resolvedSlug = SpecHelpers.ParseSpecValue<string>(this.featureContext, slug);
            string resolvedWorkflowId = SpecHelpers.ParseSpecValue<string>(this.featureContext, workflowId);
            string resolvedStateName = SpecHelpers.ParseSpecValue<string>(this.featureContext, stateName);
            Embed2? resolvedEmbed = string.IsNullOrEmpty(embed) ? (Embed2?)null : Enum.Parse<Embed2>(embed, true);

            ContentClient client = this.featureContext.Get<ContentClient>();
            SwaggerResponse<ContentStatesResponse> response = await client.GetWorkflowStateHistoryAsync(
                this.featureContext.GetCurrentTenantId(),
                resolvedWorkflowId,
                resolvedStateName,
                resolvedSlug,
                limit,
                continuationToken,
                resolvedEmbed).ConfigureAwait(false);

            this.scenarioContext.StoreLastApiResponse(response);
        }
    }
}
