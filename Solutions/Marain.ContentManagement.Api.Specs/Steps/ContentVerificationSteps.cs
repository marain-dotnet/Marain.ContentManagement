// <copyright file="ContentVerificationSteps.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Steps
{
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Marain.Cms;
    using Marain.ContentManagement.Specs.Bindings;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented

    [Binding]
    public class ContentVerificationSteps
    {
        private readonly ScenarioContext scenarioContext;

        public ContentVerificationSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Then("the response body should contain the content item")]
        public async Task ThenTheResponseBodyShouldContainTheContentItem()
        {
            Content actual = await this.scenarioContext.GetLastApiResponseBodyAsync<Content>().ConfigureAwait(false);
            Assert.IsNotNull(actual);

            Content expected = this.scenarioContext.Get<Content[]>().First();

            // Verify the two items are the same by serializing to JSON and comparing the results.
            string actualJson = JsonConvert.SerializeObject(actual);
            string expectedJson = JsonConvert.SerializeObject(expected);

            Assert.AreEqual(expectedJson, actualJson);
        }

        [Then("the ETag header should be set to the content item's etag")]
        public void ThenTheETagHeaderShouldBeSetToTheContentItemEtag()
        {
            HttpResponseMessage response = this.scenarioContext.GetLastApiResponse();
            EntityTagHeaderValue etagHeader = response.Headers.ETag;

            Assert.IsNotNull(etagHeader);

            Assert.IsNotNull(etagHeader.Tag);
            Assert.IsNotEmpty(etagHeader.Tag);

            Content expected = this.scenarioContext.Get<Content[]>().First();
            Assert.AreEqual(expected.ETag, etagHeader.Tag);
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented