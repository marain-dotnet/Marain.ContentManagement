﻿namespace Marain.ContentManagement.Specs.Steps
{
    using System.Threading.Tasks;
    using Corvus.SpecFlow.Extensions;
    using Marain.Cms;
    using Marain.ContentManagement.Specs.Bindings;
    using Marain.ContentManagement.Specs.Drivers;
    using Microsoft.Extensions.DependencyInjection;
    using TechTalk.SpecFlow;

    [Binding]
    public class AbTestSteps
    {
        private readonly FeatureContext featureContext;
        private readonly ScenarioContext scenarioContext;

        public AbTestSteps(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            this.featureContext = featureContext;
            this.scenarioContext = scenarioContext;
        }

        [Given(@"I have created an AbTest set called '(.*)' with the content")]
        public void GivenIHaveCreatedAnAbTestSetCalledWithTheContent(string testSetName, Table table)
        {
            AbTestSet testSet = ContainerBindings.GetServiceProvider(this.featureContext).GetRequiredService<AbTestSet>();
            foreach (TableRow row in table.Rows)
            {
                Content content = this.scenarioContext.Get<Content>(row["ContentName"]);
                testSet.AbTestContentMap.Add(row["Key"], new ContentSource(content.Slug, content.Id));
            }

            this.scenarioContext.Set(testSet, testSetName);
        }

        [Given(@"I have created content with an AbTest set")]
        public async Task GivenIHaveCreatedContentWithAnAbTestSet(Table contentTable)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            foreach (TableRow row in contentTable.Rows)
            {
                (Content content, string name) = ContentStoreDriver.GetContentFor(row);
                ContentStoreDriver.SetAbTestSet(this.scenarioContext, content, row);
                await store.StoreContentAsync(content);
                this.scenarioContext.Set(content, name);
            }
        }

        [When(@"I get the ABTest content called '(.*)' from the content called '(.*)' and call it '(.*)'")]
        public async Task WhenIGetTheABTestContentCalledFromTheContentCalledAndCallIt(string abTestGroup, string contentName, string actualName)
        {
            Content content = this.scenarioContext.Get<Content>(contentName);
            var testSet = content.ContentPayload as AbTestSet;
            Content abcontent = await testSet.GetContentForAbGroupAsync(abTestGroup);
            this.scenarioContext.Set(abcontent, actualName);
        }

    }
}