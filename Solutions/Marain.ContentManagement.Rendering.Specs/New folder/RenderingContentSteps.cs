﻿namespace Marain.ContentManagement.Specs.Steps
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Corvus.SpecFlow.Extensions;
    using Marain.Cms;
    using Marain.ContentManagement.Specs.Drivers;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

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

        [Given(@"I have created content")]
        public void GivenIHaveCreatedContent(Table contentTable)
        {
            foreach (TableRow row in contentTable.Rows)
            {
                (Content content, string name) = ContentDriver.GetContentFor(row);
                if (row.ContainsKey("Fragment"))
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

        [When(@"I render the content called '(.*)' to '(.*)'")]
        public async Task WhenIRenderTheContentCalledTo(string contentName, string outputName)
        {
            Content content = this.scenarioContext.Get<Content>(contentName);
            IContentRendererFactory rendererFactory = ContainerBindings.GetServiceProvider(this.featureContext).GetService<IContentRendererFactory>();
            IContentRenderer renderer = rendererFactory.GetRendererFor(content.ContentPayload);
            using var stream = new MemoryStream();
            using var writer = new StreamWriter(stream, Encoding.UTF8, 1024, true);
            await renderer.RenderAsync(writer, content, content.ContentPayload, null);
            await writer.FlushAsync();
            stream.Position = 0;
            using var reader = new StreamReader(stream, Encoding.UTF8, true, 1024, true);
            this.scenarioContext.Set(await reader.ReadToEndAsync(), outputName);
        }

        [Then(@"the output called '(.*)' should match '(.*)'")]
        public void ThenTheOutputCalledShouldMatch(string outputName, string outputString)
        {
            Assert.AreEqual(ContentDriver.GetObjectValue<string>(this.scenarioContext, outputString), this.scenarioContext.Get<string>(outputName));
        }
    }
}
