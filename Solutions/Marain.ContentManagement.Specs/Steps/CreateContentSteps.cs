namespace Marain.ContentManagement.Specs.Steps
{
    using System;
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
            ContentSummaries summaries = await store.GetContentSummariesAsync(ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, ContentStoreDriver.SubstituteContent(slug)), limit, ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, ContentStoreDriver.SubstituteContent(continuationToken)));
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
                ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, ContentStoreDriver.SubstituteContent(slug))),
                ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, ContentStoreDriver.SubstituteContent(id))));

        }

        [When(@"I get the published content for Slug '(.*)' and call it '(.*)'")]
        public async Task WhenIGetThePublishedContentForSlugAndCallIt(string slug, string contentName)
        {
            try
            {
                IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
                Content content = await store.GetPublishedContentAsync(ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, ContentStoreDriver.SubstituteContent(slug)));
                this.scenarioContext.Set(content, contentName);
            }
            catch(Exception ex)
            {
                this.scenarioContext.Set(ex);
            }
        }

        [Given(@"I archive the content with Slug '(.*)' and id '(.*)'")]
        public async Task GivenIArchiveTheContentWithSlugAndId(string slug, string id)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            await store.ArchiveContentAsync(
                ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, ContentStoreDriver.SubstituteContent(slug))),
                ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, ContentStoreDriver.SubstituteContent(id))));
        }

        [Then(@"it should throw a ContentNotFoundException")]
        public void ThenItShouldThrowAContentNotFoundException()
        {
            Assert.IsInstanceOf<ContentNotFoundException>(this.scenarioContext.Get<Exception>());
        }

        [When(@"I get the publication history for Slug '(.*)' with limit '(.*)' and continuationToken '(.*)' and call it '(.*)'")]
        public async Task WhenIGetThePublicationHistoryForSlugAndCallIt(string slug, int limit, string continuationToken, string contentSummariesName)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            ContentSummariesWithState summaries = await store.GetPublicationHistoryAsync(ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, ContentStoreDriver.SubstituteContent(slug)), limit, ContentStoreDriver.GetObjectValue<string>(this.scenarioContext, ContentStoreDriver.SubstituteContent(continuationToken)));
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


    }
}
