// <copyright file="CreateContentSteps.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Steps
{
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
    public class CreateContentSteps
    {
        private readonly FeatureContext featureContext;
        private readonly ScenarioContext scenarioContext;

        public CreateContentSteps(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            this.featureContext = featureContext;
            this.scenarioContext = scenarioContext;
        }

        [Given("I have created content with a content fragment")]
        public async Task GivenIHaveCreatedContentWithAContentFragmentCalled(Table contentTable)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            foreach (TableRow row in contentTable.Rows)
            {
                (Content content, string name) = ContentSpecHelpers.GetContentFor(row);
                ContentSpecHelpers.SetContentFragment(content, row);
                await store.StoreContentAsync(content).ConfigureAwait(false);
                this.scenarioContext.Set(content, name);
            }
        }

        [Then("getting the content with Id '(.*)' and Slug '(.*)' throws a ContentNotFoundException")]
        public void ThenGettingTheContentWithIdAndSlugThrowsAContentNotFoundException(string contentId, string slug)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);

            Assert.ThrowsAsync<ContentNotFoundException>(
                () => store.GetContentAsync(
                    SpecHelpers.ParseSpecValue<string>(this.scenarioContext, contentId),
                    SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug)),
                "ContentNotFoundException should have been thrown.");
        }

        [When("I get the content with Id '(.*)' and Slug '(.*)' and call it '(.*)'")]
        public async Task WhenIGetTheContentWithIdAndSlugAndCallIt(string id, string slug, string contentName)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);

            Content content = await store.GetContentAsync(
                SpecHelpers.ParseSpecValue<string>(this.scenarioContext, id),
                SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug)).ConfigureAwait(false);

            this.scenarioContext.Set(content, contentName);
        }

        [Then("the content called '(.*)' should match the content called '(.*)'")]
        public void ThenTheContentCalledShouldMatchTheContentCalled(string expectedName, string actualName)
        {
            Content expected = this.scenarioContext.Get<Content>(expectedName);
            Content actual = this.scenarioContext.Get<Content>(actualName);

            ContentSpecHelpers.Compare(expected, actual);
        }

        [When("I get the content summaries for Slug '(.*)' with limit '(.*)' and continuationToken '(.*)' and call it '(.*)'")]
        public async Task WhenIGetTheContentSummariesForSlugWithLimitAndContinuationTokenAndCallIt(string slug, int limit, string continuationToken, string contentSummariesName)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            ContentSummaries summaries = await store.GetContentSummariesAsync(SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug), limit, SpecHelpers.ParseSpecValue<string>(this.scenarioContext, continuationToken)).ConfigureAwait(false);
            this.scenarioContext.Set(summaries, contentSummariesName);
        }

        [Then("the content summaries called '(.*)' should match")]
        public void ThenTheContentSummariesCalledShouldMatch(string contentSummariesName, Table table)
        {
            ContentSummaries summaries = this.scenarioContext.Get<ContentSummaries>(contentSummariesName);
            var expected = table.Rows.Select(s => this.scenarioContext.Get<Content>(s["ContentName"])).ToList();
            ContentSpecHelpers.MatchSummariesToContent(expected, summaries);
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
            Content content = await store.GetPublishedContentAsync(SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug)).ConfigureAwait(false);
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
        public async Task GivenIArchiveTheContentWithSlugAndId(string slug)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            await store.ArchiveContentAsync(
                SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug),
                new CmsIdentity("SomeId", "SomeName")).ConfigureAwait(false);
        }

        [Given("I draft the content with Slug '(.*)'")]
        public async Task GivenIDraftTheContentWithSlugAndId(string slug)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            await store.MakeDraftContentAsync(
                SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug),
                new CmsIdentity("SomeId", "SomeName")).ConfigureAwait(false);
        }

        [When("I get the publication history for Slug '(.*)' with limit '(.*)' and continuationToken '(.*)' and call it '(.*)'")]
        public async Task WhenIGetThePublicationHistoryForSlugAndCallIt(string slug, int limit, string continuationToken, string contentSummariesName)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            ContentSummariesWithState summaries = await store.GetPublicationHistory(SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug), limit, SpecHelpers.ParseSpecValue<string>(this.scenarioContext, continuationToken)).ConfigureAwait(false);
            this.scenarioContext.Set(summaries, contentSummariesName);
        }

        [When("I get the published history for Slug '(.*)' with limit '(.*)' and continuationToken '(.*)' and call it '(.*)'")]
        public async Task WhenIGetThePublishedHistoryForSlugAndCallIt(string slug, int limit, string continuationToken, string contentSummariesName)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            ContentSummariesWithState summaries = await store.GetPublishedHistory(SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug), limit, SpecHelpers.ParseSpecValue<string>(this.scenarioContext, continuationToken)).ConfigureAwait(false);
            this.scenarioContext.Set(summaries, contentSummariesName);
        }

        [Then("the content summaries with state called '(.*)' should match")]
        public void ThenTheContentSummariesWithStateCalledShouldMatch(string contentSummariesName, Table table)
        {
            ContentSummariesWithState summaries = this.scenarioContext.Get<ContentSummariesWithState>(contentSummariesName);
            var expectedContent = table.Rows.Select(s => this.scenarioContext.Get<Content>(s["ContentName"])).ToList();
            var expectedStates = table.Rows.Select(s => s["StateName"]).ToList();
            ContentStoreDriver.MatchSummariesToContent(expectedContent, expectedStates, summaries);
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

        [Then("the content called '(.*)' should be copied to the content called '(.*)'")]
        public void ThenTheContentCalledShouldBeACopyOfTheContentCalled(string expectedName, string actualName)
        {
            Content expected = this.scenarioContext.Get<Content>(expectedName);
            Content actual = this.scenarioContext.Get<Content>(actualName);

            ContentSpecHelpers.CompareACopy(expected, actual);
        }

        [When("I get the content for Slug '(.*)' and call it '(.*)'")]
        public async Task WhenIGetTheContentForSlugAndCallItAsync(string slug, string contentName)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            ContentWithState content = await store.GetContentWithStateAsync(SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug)).ConfigureAwait(false);
            this.scenarioContext.Set(content, contentName);
        }

        [Then("the content called '(.*)' should be in the state '(.*)'")]
        public void ThenTheContentCalledShouldBeInTheState(string contentName, string stateName)
        {
            ContentWithState actual = this.scenarioContext.Get<ContentWithState>(contentName);
            Assert.AreEqual(stateName, actual.StateName);
        }

        [Then("getting the publication state for Slug '(.*)' should throw a ContentNotFoundException")]
        public void ThenGettingThePublicationStateForSlugShouldThrowAContentNotFoundException(string slug)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            Assert.ThrowsAsync<ContentNotFoundException>(
                () => store.GetContentWorkflowStateAsync(SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug), WellKnownWorkflowId.ContentPublication),
                "ContentNotFoundException should have been thrown.");
        }

        [Then("getting the content for the publication workflow for Slug '(.*)' should throw a ContentNotFoundException")]
        public void ThenGettingTheContentForThePublicationWorkflowForSlugShouldThrowAContentNotFoundException(string slug)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            Assert.ThrowsAsync<ContentNotFoundException>(
                () => store.GetContentForWorkflowAsync(SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug), WellKnownWorkflowId.ContentPublication),
                "ContentNotFoundException should have been thrown.");
        }

        [Then("the content called '(.*)' should match the content with state called '(.*)'")]
        public void ThenTheContentCalledShouldMatchTheContentWithStateCalled(string expectedName, string actualName)
        {
            Content expected = this.scenarioContext.Get<Content>(expectedName);
            ContentWithState actual = this.scenarioContext.Get<ContentWithState>(actualName);

            ContentSpecHelpers.Compare(expected, actual.Content);
        }

        [Then("the content called '(.*)' should be copied to the content with state called '(.*)'")]
        public void ThenTheContentCalledShouldBeCopiedToTheContentWithStateCalled(string expectedName, string actualName)
        {
            Content expected = this.scenarioContext.Get<Content>(expectedName);
            ContentWithState actual = this.scenarioContext.Get<ContentWithState>(actualName);

            ContentSpecHelpers.CompareACopy(expected, actual.Content);
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented