// <copyright file="PublicationSteps.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Steps
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Marain.Cms;
    using Marain.ContentManagement.Specs.Bindings;
    using Marain.ContentManagement.Specs.Drivers;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented

    [Binding]
    public class PublicationSteps
    {
        private readonly FeatureContext featureContext;
        private readonly ScenarioContext scenarioContext;

        public PublicationSteps(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            this.featureContext = featureContext;
            this.scenarioContext = scenarioContext;
        }

        [Given("I publish the content with Slug '(.*)' and id '(.*)'")]
        public async Task GivenIPublishTheContentWithSlugAndId(string slug, string id)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            await store.PublishContentAsync(
                SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug),
                SpecHelpers.ParseSpecValue<string>(this.scenarioContext, id),
                new CmsIdentity("SomeId", "SomeName")).ConfigureAwait(false);
        }

        [When("I get the published content for Slug '(.*)' and call it '(.*)'")]
        public async Task WhenIGetThePublishedContentForSlugAndCallIt(string slug, string contentName)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            Content content =
                await store.GetPublishedContentAsync(SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug)).ConfigureAwait(false);
            this.scenarioContext.Set(content, contentName);
        }

        [Then("getting the published content for Slug '(.*)' throws a ContentNotFoundException")]
        public void ThenGettingThePublishedContentForSlugThrowsAContentNotFoundException(string slug)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            Assert.ThrowsAsync<ContentNotFoundException>(
                () => store.GetPublishedContentAsync(SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug)),
                "ContentNotFoundException should have been thrown.");
        }

        [Given("I archive the content with Slug '(.*)'")]
        [When("I archive the content with Slug '(.*)'")]
        public async Task GivenIArchiveTheContentWithSlug(string slug)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            await store.ArchiveContentAsync(
                SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug),
                new CmsIdentity("SomeId", "SomeName")).ConfigureAwait(false);
        }

        [Given("I draft the content with Slug '(.*)'")]
        public async Task GivenIDraftTheContentWithSlug(string slug)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            await store.MakeDraftContentAsync(
                SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug),
                new CmsIdentity("SomeId", "SomeName")).ConfigureAwait(false);
        }

        [When("I get the published state history and corresponding content summaries for Slug '(.*)' with limit '(.*)' and continuationToken '(.*)' and call them '(.*)'")]
        public async Task WhenIGetThePublishedStateHistoryAndCorrespondingContentSummariesForSlugAndCallIt(string slug, int limit, string continuationToken, string name)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            ContentStates states = await store.GetPublishedHistory(
                SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug),
                limit,
                SpecHelpers.ParseSpecValue<string>(this.scenarioContext, continuationToken)).ConfigureAwait(false);

            List<ContentSummary> summaries = await store.GetContentSummariesForStatesAsync(states.States).ConfigureAwait(false);
            this.scenarioContext.Set((states, summaries), name);
        }

        [Then("the publication state histories and corresponding content summaries called '(.*)' should match")]
        public void ThenThePublishedStateHistoriesAndCorrespondingContentSummariesWithStateCalledShouldMatch(string name, Table table)
        {
            (ContentStates states, List<ContentSummary> summaries) = this.scenarioContext.Get<(ContentStates, List<ContentSummary>)>(name);

            var expectedContent = table.Rows.Select(s => this.scenarioContext.Get<Content>(s["ContentName"])).ToList();
            var expectedStates = table.Rows.Select(s => s["StateName"]).ToList();
            ContentStoreDriver.MatchStatesAndSummariesToContent(expectedContent, expectedStates, summaries, states.States);
        }

        [Then("getting the content for the publication workflow for Slug '(.*)' should throw a ContentNotFoundException")]
        public void ThenGettingTheContentForThePublicationWorkflowForSlugShouldThrowAContentNotFoundException(string slug)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            Assert.ThrowsAsync<ContentNotFoundException>(
                () => store.GetContentPublicationStateAsync(SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug)),
                "ContentNotFoundException should have been thrown.");
        }

        [Then("getting the publication state for Slug '(.*)' should throw a ContentNotFoundException")]
        public void ThenGettingThePublicationStateForSlugShouldThrowAContentNotFoundException(string slug)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            Assert.ThrowsAsync<ContentNotFoundException>(
                () => store.GetContentPublicationStateAsync(SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug)),
                "ContentNotFoundException should have been thrown.");
        }

        [When("I get the content publication state for Slug '(.*)' and call it '(.*)'")]
        public async Task WhenIGetTheContentPublicationStateForSlugAndCallItAsync(string slug, string contentName)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            ContentState content = await store.GetContentPublicationStateAsync(SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug)).ConfigureAwait(false);
            this.scenarioContext.Set(content, contentName);
        }

        [When("I get the content for the content state called '(.*)' and call it '(.*)'")]
        public async Task WhenIGetTheContentForTheContentStateCalledAndCallItAsync(string stateName, string contentName)
        {
            ContentState state = this.scenarioContext.Get<ContentState>(stateName);
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            Content content = await store.GetContentAsync(state.ContentId, state.Slug).ConfigureAwait(false);

            this.scenarioContext.Set(content, contentName);
        }

        [When("I get the publication history and corresponding content summaries for Slug '(.*)' with limit '(.*)' and call it '(.*)'")]
        public Task WhenIGetThePublicationHistoryForSlugWithLimitAndCallItAsync(string slug, int limit, string contentSummariesName)
        {
            return this.WhenIGetThePublicationHistoryForSlugWithLimitAndContinuationTokenAndCallItAsync(slug, limit, null, contentSummariesName);
        }

        [When("I get the publication history and corresponding content summaries for Slug '(.*)' with limit '(.*)' and continuationToken from previous results called '(.*)' and call it '(.*)'")]
        public async Task WhenIGetThePublicationHistoryForSlugWithLimitAndContinuationTokenAndCallItAsync(string slug, int limit, string continuationTokenSource, string contentSummariesName)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);

            string continuationToken = continuationTokenSource == null
                ? null
                : this.scenarioContext.Get<(ContentStates States, List<ContentSummary> Summaries)>(continuationTokenSource).States.ContinuationToken;

            ContentStates states = await store.GetPublicationHistory(
                SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug),
                limit,
                continuationToken).ConfigureAwait(false);

            List<ContentSummary> summaries = await store.GetContentSummariesForStatesAsync(states.States).ConfigureAwait(false);
            this.scenarioContext.Set((states, summaries), contentSummariesName);
        }

        [When("I move the content from Slug '(.*)' to '(.*)' and call it '(.*)'")]
        public async Task WhenIMoveTheContentFromSlugToAndCallItAsync(string sourceSlug, string destinationSlug, string copyName)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            Content result = await store.MoveContentForPublicationAsync(
                    SpecHelpers.ParseSpecValue<string>(this.scenarioContext, destinationSlug),
                    SpecHelpers.ParseSpecValue<string>(this.scenarioContext, sourceSlug),
                    new CmsIdentity("SomeId", "SomeName")).ConfigureAwait(false);
            this.scenarioContext.Set(result, copyName);
        }

        [When("I copy the content from Slug '(.*)' to '(.*)' and call it '(.*)'")]
        public async Task WhenICopyTheContentFromSlugToAndCallIt(string sourceSlug, string destinationSlug, string copyName)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            Content result = await store.CopyContentForPublicationAsync(
                    SpecHelpers.ParseSpecValue<string>(this.scenarioContext, destinationSlug),
                    SpecHelpers.ParseSpecValue<string>(this.scenarioContext, sourceSlug),
                    new CmsIdentity("SomeId", "SomeName")).ConfigureAwait(false);
            this.scenarioContext.Set(result, copyName);
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented