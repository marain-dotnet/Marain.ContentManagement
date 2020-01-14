// <copyright file="ContentItemBindings.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Steps
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Marain.Cms.Api.Client;
    using Marain.ContentManagement.Specs.Bindings;
    using Marain.ContentManagement.Specs.Drivers;
    using Marain.ContentManagement.Specs.Helpers;
    using TechTalk.SpecFlow;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented

    [Binding]
    public class ContentItemBindings
    {
        private readonly FeatureContext featureContext;
        private readonly ScenarioContext scenarioContext;

        public ContentItemBindings(
            FeatureContext featureContext,
            ScenarioContext scenarioContext)
        {
            this.featureContext = featureContext;
            this.scenarioContext = scenarioContext;
        }

        [When("I request the content with slug '(.*)' and Id '(.*)'")]
        [Given("I have requested the content with slug '(.*)' and Id '(.*)'")]
        public async Task WhenIRequestTheContentWithSlug(string slug, string id)
        {
            string resolvedSlug = SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug);
            string resolvedId = SpecHelpers.ParseSpecValue<string>(this.scenarioContext, id);

            ContentClient client = this.featureContext.Get<ContentClient>();

            try
            {
                SwaggerResponse<ContentResponse> response = await client.GetContentAsync(
                    this.featureContext.GetCurrentTenantId(),
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

        [When("I request the content state for slug '(.*)' and workflow Id '(.*)'")]
        public async Task WhenIRequestTheContentStateForSlugAndWorkflowId(string slug, string workflowId)
        {
            string resolvedSlug = SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug);
            string resolvedWorkflowId = SpecHelpers.ParseSpecValue<string>(this.scenarioContext, workflowId);

            try
            {
                ContentClient client = this.featureContext.Get<ContentClient>();
                SwaggerResponse<ContentStateResponse> response = await client.GetWorkflowStateAsync(
                    this.featureContext.GetCurrentTenantId(),
                    resolvedSlug,
                    resolvedWorkflowId,
                    null).ConfigureAwait(false);

                this.scenarioContext.StoreLastApiResponse(response);
            }
            catch (SwaggerException ex)
            {
                this.scenarioContext.StoreLastApiException(ex);
            }
        }

        [When("I request the content with slug '(.*)' and Id '(.*)' using the etag returned by the previous request")]
        public async Task WhenIRequestTheContentWithSlugAndIdUsingTheEtagReturnedByThePreviousRequest(string slug, string id)
        {
            SwaggerResponse<ContentResponse> lastResponse = this.scenarioContext.GetLastApiResponse<ContentResponse>();
            string lastEtag = lastResponse.Headers["ETag"].First();
            this.scenarioContext.ClearLastApiResponse();

            string resolvedSlug = SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug);
            string resolvedId = SpecHelpers.ParseSpecValue<string>(this.scenarioContext, id);

            ContentClient client = this.featureContext.Get<ContentClient>();

            try
            {
                SwaggerResponse<ContentResponse> response = await client.GetContentAsync(
                    this.featureContext.GetCurrentTenantId(),
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

        [When("I request the content with slug '(.*)' and Id '(.*)' using a random etag")]
        public async Task WhenIRequestTheContentWithSlugAndIdUsingARandomEtag(string slug, string id)
        {
            string resolvedSlug = SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug);
            string resolvedId = SpecHelpers.ParseSpecValue<string>(this.scenarioContext, id);

            ContentClient client = this.featureContext.Get<ContentClient>();

            try
            {
                SwaggerResponse<ContentResponse> response = await client.GetContentAsync(
                    this.featureContext.GetCurrentTenantId(),
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

        [Given("I have requested that the content '(.*)' is created")]
        [When("I request that the content '(.*)' is created")]
        [When("I issue a second request that the content '(.*)' is created")]
        public async Task WhenIRequestThatTheContentIsCreated(string contentItem)
        {
            Cms.Content item = this.scenarioContext.Get<Cms.Content>(contentItem);
            CreateContentRequest createContentRequest = ContentSpecHelpers.ContentAsCreateContentRequest(item);

            ContentClient client = this.featureContext.Get<ContentClient>();

            try
            {
                SwaggerResponse<ContentResponse> response = await client.CreateContentAsync(
                    this.featureContext.GetCurrentTenantId(),
                    item.Slug,
                    createContentRequest).ConfigureAwait(false);

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