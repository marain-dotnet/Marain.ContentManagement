﻿// <copyright file="ContentSummaryBindings.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Steps
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Web;
    using Marain.Cms.Api.Client;
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

        [Given("I have requested content history for slug '(.*)'")]
        [When("I request content history for slug '(.*)'")]
        public async Task WhenIRequestContentHistoryForSlug(string slug)
        {
            string resolvedSlug = ContentDriver.GetObjectValue<string>(this.scenarioContext, slug);

            ContentClient client = this.scenarioContext.Get<ContentClient>();
            SwaggerResponse<ContentSummaries> response = await client.GetContentHistoryAsync(
                this.scenarioContext.CurrentTenantId(),
                resolvedSlug,
                null,
                null,
                null).ConfigureAwait(false);

            this.scenarioContext.StoreLastApiResponse(response);
        }

        [When("I request content history for slug '(.*)' with the contination token from the previous response")]
        public async Task WhenIRequestContentHistoryForSlugWithTheContinationTokenFromThePreviousResponse(string slug)
        {
            SwaggerResponse<ContentSummaries> previousResponse = this.scenarioContext.GetLastApiResponse<ContentSummaries>();

            // Extract the continuation token from the response... it's in the "next" header
            string nextUri = (string)previousResponse.Result._links["next"].AdditionalProperties["href"];
            int startIndex = nextUri.IndexOf("continuationToken") + 18;
            int endIndex = nextUri.IndexOf("&", startIndex);
            int length = endIndex == -1 ? nextUri.Length - startIndex : endIndex - startIndex;
            string continuationToken = HttpUtility.UrlDecode(nextUri.Substring(startIndex, length));

            // Now stash the summaries themselves so we can verify that the ones we get back when we call using the
            // continuation token aren't the same.
            this.scenarioContext.Set(previousResponse.Result.Summaries.ToArray());

            string resolvedSlug = ContentDriver.GetObjectValue<string>(this.scenarioContext, slug);

            ContentClient client = this.scenarioContext.Get<ContentClient>();
            SwaggerResponse<ContentSummaries> response = await client.GetContentHistoryAsync(
                this.scenarioContext.CurrentTenantId(),
                resolvedSlug,
                null,
                continuationToken,
                null).ConfigureAwait(false);

            this.scenarioContext.StoreLastApiResponse(response);
        }

        [When("I request content history for slug '(.*)' with a limit of (.*) items")]
        public async Task WhenIRequestContentHistoryForSlugWithALimitOfItems(string slug, int limit)
        {
            string resolvedSlug = ContentDriver.GetObjectValue<string>(this.scenarioContext, slug);

            ContentClient client = this.scenarioContext.Get<ContentClient>();
            SwaggerResponse<ContentSummaries> response = await client.GetContentHistoryAsync(
                this.scenarioContext.CurrentTenantId(),
                resolvedSlug,
                limit,
                null,
                null).ConfigureAwait(false);

            this.scenarioContext.StoreLastApiResponse(response);
        }

        [When("I request content history with state for slug '(.*)', workflow Id '(.*)' and state name '(.*)'")]
        public async Task WhenIRequestContentHistoryWithStateForSlugWorkflowIdAndState(string slug, string workflowId, string stateName)
        {
            string resolvedSlug = ContentDriver.GetObjectValue<string>(this.scenarioContext, slug);
            string resolvedWorkflowId = ContentDriver.GetObjectValue<string>(this.scenarioContext, workflowId);
            string resolvedStateName = ContentDriver.GetObjectValue<string>(this.scenarioContext, stateName);

            ContentClient client = this.scenarioContext.Get<ContentClient>();
            SwaggerResponse<ContentSummariesWithStateResponse> response = await client.GetWorkflowStateHistoryAsync(
                this.scenarioContext.CurrentTenantId(),
                resolvedWorkflowId,
                resolvedStateName,
                resolvedSlug,
                null,
                null).ConfigureAwait(false);

            this.scenarioContext.StoreLastApiResponse(response);
        }

        [When("I request the content summary with slug '(.*)' and Id '(.*)'")]
        [Given("I have requested the content summary with slug '(.*)' and Id '(.*)'")]
        public async Task WhenIRequestTheContentSummaryWithSlugAndId(string slug, string id)
        {
            string resolvedSlug = ContentDriver.GetObjectValue<string>(this.scenarioContext, slug);
            string resolvedId = ContentDriver.GetObjectValue<string>(this.scenarioContext, id);

            try
            {
                ContentClient client = this.scenarioContext.Get<ContentClient>();
                SwaggerResponse<ContentSummaryResponse> response = await client.GetContentSummaryAsync(
                    this.scenarioContext.CurrentTenantId(),
                    resolvedSlug,
                    resolvedId,
                    null).ConfigureAwait(false);

                this.scenarioContext.StoreLastApiResponse(response);
            }
            catch (SwaggerException ex)
            {
                this.scenarioContext.StoreLastApiException(ex);
            }
        }

        [When("I request the content summary with slug '(.*)' and Id '(.*)' using the etag returned by the previous request")]
        public async Task WhenIRequestTheContentWithSlugAndIdUsingTheEtagReturnedByThePreviousRequest(string slug, string id)
        {
            SwaggerResponse<ContentSummaryResponse> lastResponse = this.scenarioContext.GetLastApiResponse<ContentSummaryResponse>();
            string lastEtag = lastResponse.Headers["ETag"].First();
            this.scenarioContext.ClearLastApiResponse();

            string resolvedSlug = ContentDriver.GetObjectValue<string>(this.scenarioContext, slug);
            string resolvedId = ContentDriver.GetObjectValue<string>(this.scenarioContext, id);

            ContentClient client = this.scenarioContext.Get<ContentClient>();

            try
            {
                SwaggerResponse<ContentSummaryResponse> response = await client.GetContentSummaryAsync(
                    this.scenarioContext.CurrentTenantId(),
                    resolvedSlug,
                    resolvedId,
                    lastEtag).ConfigureAwait(false);

                this.scenarioContext.StoreLastApiResponse(response);
            }
            catch (SwaggerException ex)
            {
                this.scenarioContext.StoreLastApiException(ex);
            }
        }

        [When("I request the content summary with slug '(.*)' and Id '(.*)' using a random etag")]
        public async Task WhenIRequestTheContentWithSlugAndIdUsingARandomEtag(string slug, string id)
        {
            string resolvedSlug = ContentDriver.GetObjectValue<string>(this.scenarioContext, slug);
            string resolvedId = ContentDriver.GetObjectValue<string>(this.scenarioContext, id);

            try
            {
                ContentClient client = this.scenarioContext.Get<ContentClient>();
                SwaggerResponse<ContentSummaryResponse> response = await client.GetContentSummaryAsync(
                    this.scenarioContext.CurrentTenantId(),
                    resolvedSlug,
                    resolvedId,
                    Guid.NewGuid().ToString()).ConfigureAwait(false);

                this.scenarioContext.StoreLastApiResponse(response);
            }
            catch (SwaggerException ex)
            {
                this.scenarioContext.StoreLastApiException(ex);
            }
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented