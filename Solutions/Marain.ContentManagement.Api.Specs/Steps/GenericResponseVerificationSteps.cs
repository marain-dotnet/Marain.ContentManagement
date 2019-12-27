// <copyright file="GenericResponseVerificationSteps.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Steps
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented

    using System.Linq;
    using Marain.Cms.Api.Client;
    using Marain.ContentManagement.Specs.Bindings;
    using Marain.ContentManagement.Specs.Drivers;
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
            Assert.IsNotNull(cacheHeader);
            Assert.AreEqual(expectedValue, cacheHeader);
        }

        [Then("there should be no response body")]
        public void ThenThereShouldBeNoResponseBody()
        {
            // TODO: How?
            ////HttpResponseMessage response = this.scenarioContext.GetLastApiResponse();
            ////byte[] responseBytes = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

            ////Assert.IsEmpty(responseBytes);
        }

        [Then("the response should contain a '(.*)' link")]
        public void ThenTheResponseShouldContainALink(string linkRel)
        {
            // TODO: How?
            ////JObject response = await this.scenarioContext.GetLastApiResponseBodyAsJObjectAsync().ConfigureAwait(false);

            ////JToken link = response["_links"]?[linkRel];
            ////Assert.IsNotNull(link);

            ////string url = link["href"].Value<string>();
            ////Assert.IsFalse(string.IsNullOrEmpty(url));
        }

        [Then("the ETag header should be set")]
        public void ThenTheETagHeaderShouldBeSet()
        {
            SwaggerResponse response = this.scenarioContext.GetLastApiResponse();
            string etagHeader = response.Headers["ETag"]?.First();

            Assert.IsNotNull(etagHeader);

            Assert.IsNotNull(etagHeader);
            Assert.IsNotEmpty(etagHeader);
        }

        [Then("the ETag header should be set to '(.*)'")]
        public void ThenTheETagHeaderShouldBeSetTo(string property)
        {
            SwaggerResponse response = this.scenarioContext.GetLastApiResponse();
            string etagHeader = response.Headers["ETag"]?.First();

            Assert.IsNotNull(etagHeader);

            Assert.IsNotNull(etagHeader);
            Assert.IsNotEmpty(etagHeader);

            string expected = ContentDriver.GetObjectValue<string>(this.scenarioContext, property);
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

            // TODO: How?
            ////JObject responseBody = await this.scenarioContext.GetLastApiResponseBodyAsJObjectAsync().ConfigureAwait(false);

            ////JToken link = responseBody["_links"]?[linkRel];
            ////Assert.IsNotNull(link);

            ////string selfLinkUrl = link["href"].Value<string>();
            ////var selfLinkUri = new Uri(selfLinkUrl, UriKind.RelativeOrAbsolute);

            ////Assert.AreEqual(locationHeader, selfLinkUri);
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented
