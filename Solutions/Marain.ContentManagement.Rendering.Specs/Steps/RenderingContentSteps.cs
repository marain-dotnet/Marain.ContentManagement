// <copyright file="RenderingContentSteps.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Steps
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Corvus.Extensions.Json;
    using Corvus.SpecFlow.Extensions;
    using Marain.Cms;
    using Marain.ContentManagement.Specs.Bindings;
    using Marain.ContentManagement.Specs.Drivers;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json.Linq;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented

    [Binding]
    public class RenderingContentSteps
    {
        private readonly FeatureContext featureContext;
        private readonly ScenarioContext scenarioContext;

        public RenderingContentSteps(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            this.featureContext = featureContext;
            this.scenarioContext = scenarioContext;
        }

        [Given("I have created content")]
        public void GivenIHaveCreatedContent(Table contentTable)
        {
            foreach (TableRow row in contentTable.Rows)
            {
                (Content content, string name) = ContentDriver.GetContentFor(row);
                if (row.ContainsKey("ContentSlug"))
                {
                    ContentDriver.SetContentWorkflow(content, row);
                }
                else if (row.ContainsKey("Fragment"))
                {
                    ContentDriver.SetContentFragment(content, row);
                }
                else if (row.ContainsKey("AbTestSetName"))
                {
                    ContentDriver.SetAbTestSet(this.scenarioContext, content, row);
                }
                else if (row.ContainsKey("Markdown"))
                {
                    ContentDriver.SetContentMarkdown(content, row);
                }
                else if (row.ContainsKey("Liquid template"))
                {
                    ContentDriver.SetContentLiquid(content, row);
                }
                else if (row.ContainsKey("Liquid with markdown template"))
                {
                    ContentDriver.SetContentLiquidMarkdown(content, row);
                }

                this.scenarioContext.Set(content, name);
            }
        }

        [Given("I publish the content called '(.*)'")]
        public void GivenIPublishTheContentCalled(string contentName)
        {
            Content content = this.scenarioContext.Get<Content>(contentName);
            FakeContentStore fakeContentStore = ContainerBindings.GetServiceProvider(this.featureContext).GetService<FakeContentStore>();
            fakeContentStore.SetContentState(content, ContentPublicationContentState.Published);
        }

        [Given("I draft the content called '(.*)'")]
        public void GivenIDraftTheContentCalled(string contentName)
        {
            Content content = this.scenarioContext.Get<Content>(contentName);
            FakeContentStore fakeContentStore = ContainerBindings.GetServiceProvider(this.featureContext).GetService<FakeContentStore>();
            fakeContentStore.SetContentState(content, ContentPublicationContentState.Draft);
        }

        [When(@"I render the content called '(.*)' to '(.*)' with the context \{(.*)}")]
        public async Task WhenIRenderTheContentCalledToWithTheContext(string contentName, string outputName, string contextJson)
        {
            Content content = this.scenarioContext.Get<Content>(contentName);
            IContentRendererFactory rendererFactory = ContainerBindings.GetServiceProvider(this.featureContext).GetService<IContentRendererFactory>();
            IContentRenderer renderer = rendererFactory.GetRendererFor(content.ContentPayload);
            using var stream = new MemoryStream();
            using var writer = new StreamWriter(stream, Encoding.UTF8, 1024, true);
            await renderer.RenderAsync(writer, content, content.ContentPayload, new PropertyBag(JObject.Parse("{" + contextJson + "}"))).ConfigureAwait(false);
            await writer.FlushAsync().ConfigureAwait(false);
            stream.Position = 0;
            using var reader = new StreamReader(stream, Encoding.UTF8, true, 1024, true);
            this.scenarioContext.Set(await reader.ReadToEndAsync().ConfigureAwait(false), outputName);
        }

        [When("I render the content called '(.*)' to '(.*)'")]
        public Task WhenIRenderTheContentCalledTo(string contentName, string outputName)
        {
            return this.WhenIRenderTheContentCalledToWithTheContext(contentName, outputName, string.Empty);
        }

        [Then("the output called '(.*)' should match '(.*)'")]
        public void ThenTheOutputCalledShouldMatch(string outputName, string outputString)
        {
            Assert.AreEqual(ContentDriver.GetObjectValue<string>(this.scenarioContext, outputString), this.scenarioContext.Get<string>(outputName));
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented