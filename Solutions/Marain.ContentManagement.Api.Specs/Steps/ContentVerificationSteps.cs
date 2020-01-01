// <copyright file="ContentVerificationSteps.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Steps
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented

    using Marain.Cms.Api.Client;
    using Marain.ContentManagement.Specs.Drivers;
    using Marain.ContentManagement.Specs.Helpers;
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
        public void ThenTheResponseBodyShouldContainTheContentItem(string itemName)
        {
            SwaggerResponse<ContentResponse> actual = this.scenarioContext.GetLastApiResponse<ContentResponse>();
            Assert.IsNotNull(actual);

            Cms.Content expected = this.scenarioContext.Get<Cms.Content>(itemName);

            ContentDriver.Compare(expected, actual.Result);
        }

        [Then("the response body should contain content and state matching content '(.*)' and state '(.*)'")]
        public void ThenTheResponseBodyShouldContainTheContentItemWithState(string itemName, string stateName)
        {
            SwaggerResponse<ContentWithStateResponse> actual = this.scenarioContext.GetLastApiResponse<ContentWithStateResponse>();
            Assert.IsNotNull(actual);

            Cms.Content expectedContent = this.scenarioContext.Get<Cms.Content>(itemName);
            Cms.ContentState expectedState = this.scenarioContext.Get<Cms.ContentState>(stateName);

            ContentDriver.Compare(expectedContent, actual.Result.Content);
            ContentDriver.Compare(expectedState, actual.Result);
        }

        [Then("the response body should content state matching '(.*)'")]
        public void ThenTheResponseBodyShouldContentStateMatching(string stateName)
        {
            SwaggerResponse<ContentStateResponse> actual = this.scenarioContext.GetLastApiResponse<ContentStateResponse>();
            Assert.IsNotNull(actual);

            Cms.ContentState expectedState = this.scenarioContext.Get<Cms.ContentState>(stateName);

            ContentDriver.Compare(expectedState, actual.Result);
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented