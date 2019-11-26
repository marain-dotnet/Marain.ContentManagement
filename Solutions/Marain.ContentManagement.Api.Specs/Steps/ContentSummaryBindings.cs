// <copyright file="ContentSummaryBindings.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Steps
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    using Marain.ContentManagement.Specs.Bindings;
    using Marain.ContentManagement.Specs.Drivers;
    using TechTalk.SpecFlow;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented

    [Binding]
    public class ContentSummaryBindings
    {
        private readonly ScenarioContext scenarioContext;

        public ContentSummaryBindings(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [When("I request content history for slug '(.*)'")]
        public Task WhenIRequestContentHistoryForSlug(string slug)
        {
            string resolvedSlug = ContentDriver.GetObjectValue<string>(this.scenarioContext, slug);

            string path = $"/{this.scenarioContext.CurrentTenantId()}/marain/content/history/{HttpUtility.UrlEncode(resolvedSlug)}";

            HttpRequestMessage request = this.scenarioContext.CreateApiRequest(path);
            return this.scenarioContext.SendApiRequestAndStoreResponseAsync(request);
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented