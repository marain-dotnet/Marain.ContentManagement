// <copyright file="ContentItemBindings.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Steps
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Corvus.SpecFlow.Extensions;
    using Marain.Cms;
    using Marain.Cms.Api.Client;
    using Marain.ContentManagement.Specs.Bindings;
    using Marain.ContentManagement.Specs.Drivers;
    using Marain.ContentManagement.Specs.Helpers;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
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

        [Given("there is no content available")]
        public void GivenThereIsNoContentAvailable()
        {
        }

        [Given("a content item has been created")]
        [Given("content items have been created")]
        public async Task GivenAContentItemHasBeenCreated(Table table)
        {
            ITenantedContentStoreFactory contentStoreFactory = ContainerBindings.GetServiceProvider(this.featureContext).GetRequiredService<ITenantedContentStoreFactory>();
            IContentStore store = await contentStoreFactory.GetContentStoreForTenantAsync(this.featureContext.GetCurrentTenantId()).ConfigureAwait(false);

            foreach (TableRow row in table.Rows)
            {
                (Cms.Content content, string name) = ContentSpecHelpers.GetContentFor(row);
                Cms.Content storedContent = await store.StoreContentAsync(content).ConfigureAwait(false);
                this.scenarioContext.Set(storedContent, name);
            }
        }

        [Given("I have a new content item")]
        public void GivenIHaveANewContentItem(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                (Cms.Content content, string name) = ContentSpecHelpers.GetContentFor(row);
                this.scenarioContext.Set(content, name);
            }
        }

        [When("I request the content with slug '(.*)' and Id '(.*)'")]
        [Given("I have requested the content with slug '(.*)' and Id '(.*)'")]
        public Task WhenIRequestTheContentWithSlug(string slug, string id)
        {
            return this.RequestContentItemAndStoreResponseAsync(slug, id, null);
        }

        [When("I request the content with slug '(.*)' and Id '(.*)' using the etag returned by the previous request")]
        public Task WhenIRequestTheContentWithSlugAndIdUsingTheEtagReturnedByThePreviousRequest(string slug, string id)
        {
            SwaggerResponse<ContentResponse> lastResponse = this.scenarioContext.GetLastApiResponse<ContentResponse>();
            string lastEtag = lastResponse.Headers["ETag"].First();
            this.scenarioContext.ClearLastApiResponse();

            return this.RequestContentItemAndStoreResponseAsync(slug, id, lastEtag);
        }

        [When("I request the content with slug '(.*)' and Id '(.*)' using a random etag")]
        public Task WhenIRequestTheContentWithSlugAndIdUsingARandomEtag(string slug, string id)
        {
            return this.RequestContentItemAndStoreResponseAsync(slug, id, Guid.NewGuid().ToString());
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

        [Then("the response body should contain the content item '(.*)'")]
        public void ThenTheResponseBodyShouldContainTheContentItem(string itemName)
        {
            SwaggerResponse<ContentResponse> actual = this.scenarioContext.GetLastApiResponse<ContentResponse>();
            Assert.IsNotNull(actual);

            Cms.Content expected = this.scenarioContext.Get<Cms.Content>(itemName);

            ContentSpecHelpers.Compare(expected, actual.Result);
        }

        private async Task RequestContentItemAndStoreResponseAsync(string slug, string id, string etag)
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
                    etag).ConfigureAwait(false);

                this.scenarioContext.StoreLastApiResponse(response);
            }
            catch (SwaggerException ex)
            {
                this.scenarioContext.StoreLastApiException(ex);
            }
        }
    }
}