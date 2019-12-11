// <copyright file="ContentItemBindings.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Steps
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Web;
    using Marain.Cms;
    using Marain.ContentManagement.Specs.Bindings;
    using Marain.ContentManagement.Specs.Drivers;
    using TechTalk.SpecFlow;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented

    [Binding]
    public class ContentItemBindings
    {
        private readonly ScenarioContext scenarioContext;

        public ContentItemBindings(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [When("I request the content with slug '(.*)' and Id '(.*)'")]
        [Given("I have requested the content with slug '(.*)' and Id '(.*)'")]
        public Task WhenIRequestTheContentWithSlug(string slug, string id)
        {
            string resolvedSlug = ContentDriver.GetObjectValue<string>(this.scenarioContext, slug);
            string resolvedId = ContentDriver.GetObjectValue<string>(this.scenarioContext, id);

            string path = $"/{this.scenarioContext.CurrentTenantId()}/marain/content/{HttpUtility.UrlEncode(resolvedSlug)}?contentId={HttpUtility.UrlEncode(resolvedId)}";

            HttpRequestMessage request = this.scenarioContext.CreateApiRequest(path);
            return this.scenarioContext.SendApiRequestAndStoreResponseAsync(request);
        }

        [When("I request the content with its state for slug '(.*)' and workflow Id '(.*)'")]
        public Task WhenIRequestTheContentWithItsStateForSlugAndWorkflowId(string slug, string workflowId)
        {
            string resolvedSlug = ContentDriver.GetObjectValue<string>(this.scenarioContext, slug);
            string resolvedWorkflowId = ContentDriver.GetObjectValue<string>(this.scenarioContext, workflowId);

            string path = $"/{this.scenarioContext.CurrentTenantId()}/marain/content/workflow/{HttpUtility.UrlEncode(resolvedWorkflowId)}/content/{HttpUtility.UrlEncode(resolvedSlug)}";

            HttpRequestMessage request = this.scenarioContext.CreateApiRequest(path);
            return this.scenarioContext.SendApiRequestAndStoreResponseAsync(request);
        }

        [When("I request the content with slug '(.*)' and Id '(.*)' using the etag returned by the previous request")]
        public Task WhenIRequestTheContentWithSlugAndIdUsingTheEtagReturnedByThePreviousRequest(string slug, string id)
        {
            HttpResponseMessage lastResponse = this.scenarioContext.GetLastApiResponse();
            EntityTagHeaderValue lastEtag = lastResponse.Headers.ETag;

            string resolvedSlug = ContentDriver.GetObjectValue<string>(this.scenarioContext, slug);
            string resolvedId = ContentDriver.GetObjectValue<string>(this.scenarioContext, id);

            string path = $"/{this.scenarioContext.CurrentTenantId()}/marain/content/{HttpUtility.UrlEncode(resolvedSlug)}?contentId={HttpUtility.UrlEncode(resolvedId)}";

            HttpRequestMessage request = this.scenarioContext.CreateApiRequest(path);
            request.Headers.IfNoneMatch.Add(lastEtag);

            return this.scenarioContext.SendApiRequestAndStoreResponseAsync(request);
        }

        [When("I request the content with slug '(.*)' and Id '(.*)' using a random etag")]
        public Task WhenIRequestTheContentWithSlugAndIdUsingARandomEtag(string slug, string id)
        {
            string resolvedSlug = ContentDriver.GetObjectValue<string>(this.scenarioContext, slug);
            string resolvedId = ContentDriver.GetObjectValue<string>(this.scenarioContext, id);

            string path = $"/{this.scenarioContext.CurrentTenantId()}/marain/content/{HttpUtility.UrlEncode(resolvedSlug)}?contentId={HttpUtility.UrlEncode(resolvedId)}";

            HttpRequestMessage request = this.scenarioContext.CreateApiRequest(path);
            request.Headers.IfNoneMatch.Add(new EntityTagHeaderValue($"\"{Guid.NewGuid().ToString()}\""));

            return this.scenarioContext.SendApiRequestAndStoreResponseAsync(request);
        }

        [Given("I have requested that the content '(.*)' is created")]
        [When("I request that the content '(.*)' is created")]
        [When("I issue a second request that the content '(.*)' is created")]
        public Task WhenIRequestThatTheContentIsCreated(string contentItem)
        {
            Content item = this.scenarioContext.Get<Content>(contentItem);

            string path = $"/{this.scenarioContext.CurrentTenantId()}/marain/content/{HttpUtility.UrlEncode(item.Slug)}";

            HttpRequestMessage request = this.scenarioContext.CreateApiRequestWithJson(path, HttpMethod.Post, item);

            return this.scenarioContext.SendApiRequestAndStoreResponseAsync(request);
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented