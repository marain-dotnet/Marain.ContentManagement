// <copyright file="GenericFunctionBindings.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Steps
{
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Marain.ContentManagement.Specs.Bindings;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented

    [Binding]
    public class GenericFunctionBindings
    {
        private readonly ScenarioContext scenarioContext;

        public GenericFunctionBindings(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Then("the response should have a status of '(.*)'")]
        public void ThenTheResponseShouldHaveAStatusOf(int expectedStatusCode)
        {
            HttpResponseMessage response = this.scenarioContext.GetLastApiResponse();
            Assert.AreEqual((HttpStatusCode)expectedStatusCode, response.StatusCode);
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
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented