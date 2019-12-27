// <copyright file="ContentItemVerificationSteps.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Steps
{
    using System.Linq;
    using System.Threading.Tasks;
    using Marain.ContentManagement.Specs.Bindings;
    using Newtonsoft.Json.Linq;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented

    [Binding]
    public class ContentItemVerificationSteps
    {
        private readonly ScenarioContext scenarioContext;

        public ContentItemVerificationSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Then("the response should contain (.*) embedded content summaries")]
        public async Task ThenTheResponseShouldContainContentSummaries(int expectedCount)
        {
            JObject response = await this.scenarioContext.GetLastApiResponseBodyAsJObjectAsync().ConfigureAwait(false);

            JToken summaries = response["summaries"];

            Assert.IsNotNull(summaries);
            Assert.AreEqual(JTokenType.Array, summaries.Type);

            var summariesArray = summaries as JArray;

            Assert.AreEqual(expectedCount, summariesArray.Children().Count());
        }

        [Then("the response should contain (.*) embedded content summaries with state")]
        public async Task ThenTheResponseShouldContainEmbeddedContentSummariesWithState(int expectedCount)
        {
            JObject response = await this.scenarioContext.GetLastApiResponseBodyAsJObjectAsync().ConfigureAwait(false);

            JToken summaries = response["summaries"];

            Assert.IsNotNull(summaries);
            Assert.AreEqual(JTokenType.Array, summaries.Type);

            var summariesArray = summaries as JArray;

            Assert.AreEqual(expectedCount, summariesArray.Children().Count());
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented