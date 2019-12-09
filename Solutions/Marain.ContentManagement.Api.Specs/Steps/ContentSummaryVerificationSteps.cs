// <copyright file="ContentSummaryVerificationSteps.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Steps
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented

    using System.Threading.Tasks;
    using Marain.Cms;
    using Marain.ContentManagement.Specs.Bindings;
    using Marain.ContentManagement.Specs.Drivers;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

    [Binding]
    public class ContentSummaryVerificationSteps
    {
        private readonly ScenarioContext scenarioContext;

        public ContentSummaryVerificationSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Then("the response body should contain a summary of the content item '(.*)'")]
        public async Task ThenTheResponseBodyShouldContainTheContentItem(string itemName)
        {
            ContentSummary actual = await this.scenarioContext.GetLastApiResponseBodyAsync<ContentSummary>().ConfigureAwait(false);
            Assert.IsNotNull(actual);

            Content expected = this.scenarioContext.Get<Content>(itemName);

            ContentDriver.Compare(expected, actual);
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented