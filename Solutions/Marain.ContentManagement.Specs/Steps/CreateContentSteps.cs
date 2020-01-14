// <copyright file="CreateContentSteps.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Steps
{
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

        [Then("the content called '(.*)' should be copied to the content called '(.*)'")]
        public void ThenTheContentCalledShouldBeACopyOfTheContentCalled(string expectedName, string actualName)
        {
            Content expected = this.scenarioContext.Get<Content>(expectedName);
            Content actual = this.scenarioContext.Get<Content>(actualName);

            ContentSpecHelpers.CompareACopy(expected, actual);
        }

        [Then("the content state called '(.*)' should be in the state '(.*)'")]
        public void ThenTheContentCalledShouldBeInTheState(string contentName, string stateName)
        {
            ContentState actual = this.scenarioContext.Get<ContentState>(contentName);
            Assert.AreEqual(stateName, actual.StateName);
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented