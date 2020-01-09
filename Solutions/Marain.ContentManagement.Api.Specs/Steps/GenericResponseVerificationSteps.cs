// <copyright file="GenericResponseVerificationSteps.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Steps
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented

    using System.Linq;
    using Marain.Cms.Api.Client;
    using Marain.ContentManagement.Specs.Drivers;
    using Marain.ContentManagement.Specs.Helpers;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

    [Binding]
    public class GenericResponseVerificationSteps
    {
        private readonly ScenarioContext scenarioContext;

        public GenericResponseVerificationSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Then("the response should have a status of '(.*)'")]
        public void ThenTheResponseShouldHaveAStatusOf(int expectedStatusCode)
        {
            if (expectedStatusCode >= 300)
            {
                SwaggerException ex = this.scenarioContext.GetLastApiException();
                Assert.AreEqual(expectedStatusCode, ex.StatusCode);
            }
            else
            {
                SwaggerResponse response = this.scenarioContext.GetLastApiResponse();
                Assert.AreEqual(expectedStatusCode, response.StatusCode);
            }
        }

        [Then("the Cache header should be set to '(.*)'")]
        public void ThenTheCacheHeaderShouldBeSetTo(string expectedValue)
        {
            SwaggerResponse response = this.scenarioContext.GetLastApiResponse();
            string cacheHeader = response.Headers["Cache-Control"]?.First();
            Assert.AreEqual(expectedValue, cacheHeader);
        }

        [Then("there should be no response body")]
        public void ThenThereShouldBeNoResponseBody()
        {
            // This is a bit of an odd one.
            // For any 200 response code, we can do this by looking for the last API response.
            // For anything else, the client will have thrown a SwaggerException. In this case we have to assume there is no
            // response body.
            if (this.scenarioContext.TryGetLastApiResponse(out SwaggerResponse response))
            {
                Assert.AreEqual(response.GetType(), typeof(SwaggerResponse));
            }
        }

        [Then("the response should contain a '(.*)' link")]
        public void ThenTheResponseShouldContainALink(string linkRel)
        {
            SwaggerResponse response = this.scenarioContext.GetLastApiResponse();
            Resource resource = response.ResultAs<Resource>();

            resource._links.TryGetValue(linkRel, out ResourceLink link);

            Assert.IsNotNull(link);
        }

        [Then("the response should not contain a '(.*)' link")]
        public void ThenTheResponseShouldNotContainALink(string linkRel)
        {
            SwaggerResponse response = this.scenarioContext.GetLastApiResponse();
            Resource resource = response.ResultAs<Resource>();

            Assert.IsFalse(resource._links.TryGetValue(linkRel, out _));
        }

        [Then("the ETag header should be set")]
        public void ThenTheETagHeaderShouldBeSet()
        {
            SwaggerResponse response = this.scenarioContext.GetLastApiResponse();
            string etagHeader = response.Headers["ETag"]?.First();

            Assert.IsNotNull(etagHeader);
            Assert.IsNotEmpty(etagHeader);
        }

        [Then("the ETag header should be set to '(.*)'")]
        public void ThenTheETagHeaderShouldBeSetTo(string property)
        {
            SwaggerResponse response = this.scenarioContext.GetLastApiResponse();
            string etagHeader = response.Headers["ETag"]?.First();

            string expected = ContentSpecHelpers.GetObjectValue<string>(this.scenarioContext, property);
            Assert.AreEqual(expected, etagHeader);
        }

        [Then("the Location header should be set")]
        public void ThenTheLocationHeaderShouldBeSet()
        {
            SwaggerResponse response = this.scenarioContext.GetLastApiResponse();
            string locationHeader = response.Headers["Location"]?.First();

            Assert.IsNotNull(locationHeader);
        }

        [Then("the location header should match the response '(.*)' link")]
        public void ThenTheLocationHeaderShouldMatchTheResponseLink(string linkRel)
        {
            SwaggerResponse response = this.scenarioContext.GetLastApiResponse();
            string locationHeader = response.Headers["Location"]?.First();

            Resource resource = response.ResultAs<Resource>();
            ResourceLink link = resource._links[linkRel];
            Assert.IsNotNull(link);

            string selfLinkUrl = (string)link.AdditionalProperties["href"];

            Assert.AreEqual(locationHeader, selfLinkUrl);
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented
