// <copyright file="GenericResponseVerificationSteps.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Steps
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented

    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Marain.ContentManagement.Specs.Bindings;
    using Marain.ContentManagement.Specs.Drivers;
    using Newtonsoft.Json.Linq;
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
        public async Task ThenTheResponseShouldHaveAStatusOf(int expectedStatusCode)
        {
            HttpResponseMessage response = this.scenarioContext.GetLastApiResponse();

            // Get the content as a string so we can include it if we fail the test.
            string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.AreEqual((HttpStatusCode)expectedStatusCode, response.StatusCode, content);
        }

        [Then("the Cache header should be set to '(.*)'")]
        public void ThenTheCacheHeaderShouldBeSetTo(string expectedValue)
        {
            HttpResponseMessage response = this.scenarioContext.GetLastApiResponse();
            CacheControlHeaderValue cacheHeader = response.Headers.CacheControl;
            Assert.IsNotNull(cacheHeader);
            Assert.AreEqual(expectedValue, cacheHeader.ToString());
        }

        [Then("there should be no response body")]
        public async Task ThenThereShouldBeNoResponseBody()
        {
            HttpResponseMessage response = this.scenarioContext.GetLastApiResponse();
            byte[] responseBytes = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

            Assert.IsEmpty(responseBytes);
        }

        [Then("the response should contain a '(.*)' link")]
        public async Task ThenTheResponseShouldContainALink(string linkRel)
        {
            JObject response = await this.scenarioContext.GetLastApiResponseBodyAsJObjectAsync().ConfigureAwait(false);

            JToken link = response["_links"]?[linkRel];
            Assert.IsNotNull(link);

            string url = link["href"].Value<string>();
            Assert.IsFalse(string.IsNullOrEmpty(url));
        }

        [Then("the ETag header should be set")]
        public void ThenTheETagHeaderShouldBeSet()
        {
            HttpResponseMessage response = this.scenarioContext.GetLastApiResponse();
            EntityTagHeaderValue etagHeader = response.Headers.ETag;

            Assert.IsNotNull(etagHeader);

            Assert.IsNotNull(etagHeader.Tag);
            Assert.IsNotEmpty(etagHeader.Tag);
        }

        [Then("the ETag header should be set to '(.*)'")]
        public void ThenTheETagHeaderShouldBeSetTo(string property)
        {
            HttpResponseMessage response = this.scenarioContext.GetLastApiResponse();
            EntityTagHeaderValue etagHeader = response.Headers.ETag;

            Assert.IsNotNull(etagHeader);

            Assert.IsNotNull(etagHeader.Tag);
            Assert.IsNotEmpty(etagHeader.Tag);

            string expected = ContentDriver.GetObjectValue<string>(this.scenarioContext, property);
            Assert.AreEqual(expected, etagHeader.Tag);
        }

        [Then("the Location header should be set")]
        public void ThenTheLocationHeaderShouldBeSet()
        {
            HttpResponseMessage response = this.scenarioContext.GetLastApiResponse();
            Uri locationHeader = response.Headers.Location;

            Assert.IsNotNull(locationHeader);
        }

        [Then("the location header should match the response '(.*)' link")]
        public async Task ThenTheLocationHeaderShouldMatchTheResponseLink(string linkRel)
        {
            HttpResponseMessage response = this.scenarioContext.GetLastApiResponse();
            Uri locationHeader = response.Headers.Location;

            JObject responseBody = await this.scenarioContext.GetLastApiResponseBodyAsJObjectAsync().ConfigureAwait(false);

            JToken link = responseBody["_links"]?[linkRel];
            Assert.IsNotNull(link);

            string selfLinkUrl = link["href"].Value<string>();
            var selfLinkUri = new Uri(selfLinkUrl, UriKind.RelativeOrAbsolute);

            Assert.AreEqual(locationHeader, selfLinkUri);
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented
