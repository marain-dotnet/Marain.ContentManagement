// <copyright file="ContentVerificationSteps.cs" company="Endjin Limited">
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
    public class ContentVerificationSteps
    {
        private readonly ScenarioContext scenarioContext;

        public ContentVerificationSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Then("the response body should contain the content item '(.*)'")]
        public async Task ThenTheResponseBodyShouldContainTheContentItem(string itemName)
        {
            Content actual = await this.scenarioContext.GetLastApiResponseBodyAsync<Content>().ConfigureAwait(false);
            Assert.IsNotNull(actual);

            Content expected = this.scenarioContext.Get<Content>(itemName);

            ContentDriver.Compare(expected, actual);
        }

        [Then("the response body should contain content and state matching content '(.*)' and state '(.*)'")]
        public async Task ThenTheResponseBodyShouldContainTheContentItemWithState(string itemName, string stateName)
        {
            ContentWithState actual = await this.scenarioContext.GetLastApiResponseBodyAsync<ContentWithState>().ConfigureAwait(false);
            Assert.IsNotNull(actual);

            Content expectedContent = this.scenarioContext.Get<Content>(itemName);
            ContentState expectedState = this.scenarioContext.Get<ContentState>(stateName);

            ContentDriver.Compare(expectedContent, actual.Content);
            ContentDriver.Compare(expectedState, actual);
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented