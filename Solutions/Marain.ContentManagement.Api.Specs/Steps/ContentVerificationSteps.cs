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
    using Marain.ContentManagement.Specs.Drivers;
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

        [Then("the response body should contain the content item '(.*)'")]
        public async Task ThenTheResponseBodyShouldContainTheContentItem(string itemName)
        {
            Content actual = await this.scenarioContext.GetLastApiResponseBodyAsync<Content>().ConfigureAwait(false);
            Assert.IsNotNull(actual);

            Content expected = this.scenarioContext.Get<Content>(itemName);

            ContentDriver.Compare(expected, actual);
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
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented