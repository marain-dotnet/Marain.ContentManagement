// <copyright file="ContentItemVerificationSteps.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Steps
{
    using System.Collections.ObjectModel;
    using Marain.Cms.Api.Client;
    using Marain.ContentManagement.Specs.Bindings;
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
        public void ThenTheResponseShouldContainContentSummaries(int expectedCount)
        {
            SwaggerResponse<ContentSummaries> response = this.scenarioContext.GetLastApiResponse<Cms.Api.Client.ContentSummaries>();

            ObservableCollection<ContentSummaryResponse> summaries = response.Result.Summaries;

            Assert.IsNotNull(summaries);
            Assert.IsNotEmpty(summaries);

            Assert.AreEqual(expectedCount, summaries.Count);
        }

        [Then("the response should contain (.*) embedded content summaries with state")]
        public void ThenTheResponseShouldContainEmbeddedContentSummariesWithState(int expectedCount)
        {
            SwaggerResponse<ContentSummariesWithStateResponse> response = this.scenarioContext.GetLastApiResponse<ContentSummariesWithStateResponse>();

            ObservableCollection<ContentSummaryWithStateResponse> summaries = response.Result.Summaries;

            Assert.IsNotNull(summaries);
            Assert.IsNotEmpty(summaries);

            Assert.AreEqual(expectedCount, summaries.Count);
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented