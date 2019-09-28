namespace Marain.ContentManagement.Specs.Steps
{
    using System.Linq;
    using System.Threading.Tasks;
    using Marain.Cms;
    using Marain.ContentManagement.Specs.Bindings;
    using Marain.ContentManagement.Specs.Drivers;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

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

        [Given(@"I have created content with a content fragment")]
        public async Task GivenIHaveCreatedContentWithAContentFragmentCalled(Table contentTable)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            foreach (TableRow row in contentTable.Rows)
            {
                (Content content, string name) = ContentStoreDriver.GetContentFor(row);
                ContentStoreDriver.SetContentFragment(content, row);
                await store.StoreContentAsync(content);
                this.scenarioContext.Set(content, name);
            }
        }

        [Then(@"getting the content with Id '(.*)' and Slug '(.*)' throws a ContentNotFoundException")]
        public async Task ThenGettingTheContentWithIdAndSlugThrowsAContentNotFoundException(string contentId, string slug)
        {
            try
            {
                IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
                Content content = await store.GetContentAsync(
                    ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, contentId),
                    ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, slug));
                Assert.Fail("ContentNotFoundException should have been thrown.");
            }
            catch (ContentNotFoundException)
            {
            }
            catch
            {
                Assert.Fail("ContentNotFoundException should have been thrown.");
            }
        }


        [When(@"I get the content with Id '(.*)' and Slug '(.*)' and call it '(.*)'")]
        public async Task WhenIGetTheContentWithIdAndSlugAndCallIt(string id, string slug, string contentName)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);

            Content content = await store.GetContentAsync(
                ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, id),
                ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, slug));

            this.scenarioContext.Set(content, contentName);
        }

        [Then(@"the content called '(.*)' should match the content called '(.*)'")]
        public void ThenTheContentCalledShouldMatchTheContentCalled(string expectedName, string actualName)
        {
            Content expected = this.scenarioContext.Get<Content>(expectedName);
            Content actual = this.scenarioContext.Get<Content>(actualName);

            ContentStoreDriver.Compare(expected, actual);
        }

        [When(@"I get the content summaries for Slug '(.*)' with limit '(.*)' and continuationToken '(.*)' and call it '(.*)'")]
        public async Task WhenIGetTheContentSummariesForSlugWithLimitAndContinuationTokenAndCallIt(string slug, int limit, string continuationToken, string contentSummariesName)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            ContentSummaries summaries = await store.GetContentSummariesAsync(ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, slug), limit, ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, continuationToken));
            this.scenarioContext.Set(summaries, contentSummariesName);
        }

        [Then(@"the content summaries called '(.*)' should match")]
        public void ThenTheContentSummariesCalledShouldMatch(string contentSummariesName, Table table)
        {
            ContentSummaries summaries = this.scenarioContext.Get<ContentSummaries>(contentSummariesName);
            var expected = table.Rows.Select(s => this.scenarioContext.Get<Content>(s["ContentName"])).ToList();
            ContentStoreDriver.MatchSummariesToContent(expected, summaries);
        }

        [Given(@"I publish the content with Slug '(.*)' and id '(.*)'")]
        public async Task GivenIPublishTheContentWithSlugAndId(string slug, string id)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            await store.PublishContentAsync(
                ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, slug),
                ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, id),
                new CmsIdentity("SomeId", "SomeName"));

        }

        [When(@"I get the published content for Slug '(.*)' and call it '(.*)'")]
        public async Task WhenIGetThePublishedContentForSlugAndCallIt(string slug, string contentName)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            Content content = await store.GetPublishedContentAsync(ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, slug));
            this.scenarioContext.Set(content, contentName);
        }

        [Then(@"getting the published content for Slug '(.*)' throws a ContentNotFoundException")]
        public async Task ThenGettingThePublishedContentForSlugThrowsAContentNotFoundException(string slug)
        {
            try
            {
                IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
                Content content = await store.GetPublishedContentAsync(ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, slug));
                Assert.Fail("ContentNotFoundException should have been thrown.");
            }
            catch (ContentNotFoundException)
            {
            }
            catch
            {
                Assert.Fail("ContentNotFoundException should have been thrown.");
            }
        }

        [Given(@"I archive the content with Slug '(.*)'")]
        [When(@"I archive the content with Slug '(.*)'")]
        public async Task GivenIArchiveTheContentWithSlugAndId(string slug)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            await store.ArchiveContentAsync(
                ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, slug),
                new CmsIdentity("SomeId", "SomeName"));
        }

        [Given(@"I draft the content with Slug '(.*)'")]
        public async Task GivenIDraftTheContentWithSlugAndId(string slug)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            await store.MakeDraftContentAsync(
                ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, slug),
                new CmsIdentity("SomeId", "SomeName"));
        }

        [When(@"I get the publication history for Slug '(.*)' with limit '(.*)' and continuationToken '(.*)' and call it '(.*)'")]
        public async Task WhenIGetThePublicationHistoryForSlugAndCallIt(string slug, int limit, string continuationToken, string contentSummariesName)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            ContentSummariesWithState summaries = await store.GetPublicationHistory(ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, slug), limit, ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, continuationToken));
            this.scenarioContext.Set(summaries, contentSummariesName);
        }

        [When(@"I get the published history for Slug '(.*)' with limit '(.*)' and continuationToken '(.*)' and call it '(.*)'")]
        public async Task WhenIGetThePublishedHistoryForSlugAndCallIt(string slug, int limit, string continuationToken, string contentSummariesName)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            ContentSummariesWithState summaries = await store.GetPublishedHistory(ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, slug), limit, ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, continuationToken));
            this.scenarioContext.Set(summaries, contentSummariesName);
        }

        [Then(@"the content summaries with state called '(.*)' should match")]
        public void ThenTheContentSummariesWithStateCalledShouldMatch(string contentSummariesName, Table table)
        {
            ContentSummariesWithState summaries = this.scenarioContext.Get<ContentSummariesWithState>(contentSummariesName);
            var expectedContent = table.Rows.Select(s => this.scenarioContext.Get<Content>(s["ContentName"])).ToList();
            var expectedStates = table.Rows.Select(s => s["StateName"]).ToList();
            ContentStoreDriver.MatchSummariesToContent(expectedContent, expectedStates, summaries);
        }

        [When(@"I move the content from Slug '(.*)' to '(.*)' and call it '(.*)'")]
        public async Task WhenIMoveTheContentFromSlugToAndCallItAsync(string sourceSlug, string destinationSlug, string copyName)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            Content result = await store.MoveContentForPublicationAsync(
                    ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, destinationSlug),
                    ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, sourceSlug),
                    new CmsIdentity("SomeId", "SomeName"));
            this.scenarioContext.Set(result, copyName);
        }

        [When(@"I copy the content from Slug '(.*)' to '(.*)' and call it '(.*)'")]
        public async Task WhenICopyTheContentFromSlugToAndCallIt(string sourceSlug, string destinationSlug, string copyName)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            Content result = await store.CopyContentForPublicationAsync(
                    ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, destinationSlug),
                    ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, sourceSlug),
                    new CmsIdentity("SomeId", "SomeName"));
            this.scenarioContext.Set(result, copyName);
        }


        [Then(@"the content called '(.*)' should be copied to the content called '(.*)'")]
        public void ThenTheContentCalledShouldBeACopyOfTheContentCalled(string expectedName, string actualName)
        {
            Content expected = this.scenarioContext.Get<Content>(expectedName);
            Content actual = this.scenarioContext.Get<Content>(actualName);

            ContentStoreDriver.CompareACopy(expected, actual);
        }

        [When(@"I get the content for Slug '(.*)' and call it '(.*)'")]
        public async Task WhenIGetTheContentForSlugAndCallItAsync(string slug, string contentName)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            ContentWithState content = await store.GetContentWithStateAsync(ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, slug));
            this.scenarioContext.Set(content, contentName);
        }

        [Then(@"the content called '(.*)' should be in the state '(.*)'")]
        public void ThenTheContentCalledShouldBeInTheState(string contentName, string stateName)
        {
            ContentWithState actual = this.scenarioContext.Get<ContentWithState>(contentName);
            Assert.AreEqual(stateName, actual.StateName);
        }

        [Then(@"getting the publication state for Slug '(.*)' should throw a ContentNotFoundException")]
        public async Task ThenGettingThePublicationStateForSlugShouldThrowAContentNotFoundException(string slug)
        {
            try
            {
                IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
                ContentState state = await store.GetContentWorkflowStateAsync(ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, slug), WellKnownWorkflowId.ContentPublication);
                Assert.Fail("ContentNotFoundException should have been thrown.");
            }
            catch (ContentNotFoundException)
            {
            }
            catch
            {
                Assert.Fail("ContentNotFoundException should have been thrown.");
            }
        }

        [Then(@"getting the content for the publication workflow for Slug '(.*)' should throw a ContentNotFoundException")]
        public async Task ThenGettingTheContentForThePublicationWorkflowForSlugShouldThrowAContentNotFoundException(string slug)
        {
            try
            {
                IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
                ContentWithState state = await store.GetContentForWorkflowAsync(ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, slug), WellKnownWorkflowId.ContentPublication);
                Assert.Fail("ContentNotFoundException should have been thrown.");
            }
            catch (ContentNotFoundException)
            {
            }
            catch
            {
                Assert.Fail("ContentNotFoundException should have been thrown.");
            }
        }

        [Then(@"the content called '(.*)' should match the content with state called '(.*)'")]
        public void ThenTheContentCalledShouldMatchTheContentWithStateCalled(string expectedName, string actualName)
        {
            Content expected = this.scenarioContext.Get<Content>(expectedName);
            ContentWithState actual = this.scenarioContext.Get<ContentWithState>(actualName);

            ContentStoreDriver.Compare(expected, actual.Content);
        }

        [Then(@"the content called '(.*)' should be copied to the content with state called '(.*)'")]
        public void ThenTheContentCalledShouldBeCopiedToTheContentWithStateCalled(string expectedName, string actualName)
        {
            Content expected = this.scenarioContext.Get<Content>(expectedName);
            ContentWithState actual = this.scenarioContext.Get<ContentWithState>(actualName);

            ContentStoreDriver.CompareACopy(expected, actual.Content);
        }
    }
}
