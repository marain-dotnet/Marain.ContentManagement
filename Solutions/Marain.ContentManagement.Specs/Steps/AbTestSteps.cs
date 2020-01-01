// <copyright file="AbTestSteps.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Steps
{
    using System.Threading.Tasks;
    using Corvus.SpecFlow.Extensions;
    using Marain.Cms;
    using Marain.ContentManagement.Specs.Bindings;
    using Marain.ContentManagement.Specs.Drivers;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented

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

        [Given("I have created an AbTest set called '(.*)' with the content")]
        public void GivenIHaveCreatedAnAbTestSetCalledWithTheContent(string testSetName, Table table)
        {
            AbTestSetPayload testSet = ContainerBindings.GetServiceProvider(this.featureContext).GetRequiredService<AbTestSetPayload>();
            foreach (TableRow row in table.Rows)
            {
                Content content = this.scenarioContext.Get<Content>(row["ContentName"]);
                testSet.AbTestContentMap.Add(row["Key"], new ContentReference(content.Slug, content.Id));
            }

            this.scenarioContext.Set(testSet, testSetName);
        }

        [Given("I have created content with an AbTest set")]
        public async Task GivenIHaveCreatedContentWithAnAbTestSet(Table contentTable)
        {
            IContentStore store = ContentManagementCosmosContainerBindings.GetContentStore(this.featureContext);
            foreach (TableRow row in contentTable.Rows)
            {
                (Content content, string name) = ContentDriver.GetContentFor(row);
                ContentDriver.SetAbTestSet(this.scenarioContext, content, row);
                await store.StoreContentAsync(content).ConfigureAwait(false);
                this.scenarioContext.Set(content, name);
            }
        }

        [When("I get the ABTest content called '(.*)' from the content called '(.*)' and call it '(.*)'")]
        public async Task WhenIGetTheABTestContentCalledFromTheContentCalledAndCallIt(string abTestGroup, string contentName, string actualName)
        {
            Content content = this.scenarioContext.Get<Content>(contentName);
            var testSet = content.ContentPayload as AbTestSetPayload;
            Content abcontent = await testSet.GetContentForAbGroupAsync(abTestGroup).ConfigureAwait(false);
            this.scenarioContext.Set(abcontent, actualName);
        }

        [Then(@"getting the ABTest content called '(.*)' from the content called '(.*)' should throw a ContentNotFoundException\.")]
        public async Task ThenGettingTheABTestContentCalledFromTheContentCalledShouldThrowAContentNotFoundException_(string abTestGroup, string contentName)
        {
            try
            {
                Content content = this.scenarioContext.Get<Content>(contentName);
                var testSet = content.ContentPayload as AbTestSetPayload;
                Content abcontent = await testSet.GetContentForAbGroupAsync(abTestGroup).ConfigureAwait(false);
                Assert.Fail("Should have thrown a ContentNotFoundException.");
            }
            catch (ContentNotFoundException)
            {
            }
            catch
            {
                Assert.Fail("Should have thrown a ContentNotFoundException.");
            }
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented