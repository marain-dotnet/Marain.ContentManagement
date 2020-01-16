// <copyright file="ContentSummaryBindings.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Steps
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Marain.Cms.Api.Client;
    using Marain.ContentManagement.Specs.Bindings;
    using Marain.ContentManagement.Specs.Drivers;
    using Marain.ContentManagement.Specs.Helpers;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented

    [Binding]
    public class ContentSummaryBindings
    {
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;

        public ContentSummaryBindings(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
        }

        [Given("I have requested content history for slug '(.*)'")]
        [When("I request content history for slug '(.*)'")]
        public Task WhenIRequestContentHistoryForSlug(string slug)
        {
            return this.RequestContentHistoryAndStoreResponseAsync(slug, null, null, null);
        }

        [When("I request content history for slug '(.*)' with the contination token from the previous response")]
        public Task WhenIRequestContentHistoryForSlugWithTheContinationTokenFromThePreviousResponse(string slug)
        {
            SwaggerResponse<ContentSummariesResponse> previousResponse = this.scenarioContext.GetLastApiResponse<ContentSummariesResponse>();

            string continuationToken = previousResponse.Result.ExtractContinuationToken();

            // Now stash the summaries themselves so we can verify that the ones we get back when we call using the
            // continuation token aren't the same.
            this.scenarioContext.Set(previousResponse.Result.Summaries.ToArray());

            return this.RequestContentHistoryAndStoreResponseAsync(slug, null, continuationToken, null);
        }

        [When("I request content history for slug '(.*)' with a limit of (.*) items")]
        public Task WhenIRequestContentHistoryForSlugWithALimitOfItems(string slug, int limit)
        {
            return this.RequestContentHistoryAndStoreResponseAsync(slug, limit, null, null);
        }

        [When("I request the content summary with slug '(.*)' and Id '(.*)'")]
        [Given("I have requested the content summary with slug '(.*)' and Id '(.*)'")]
        public Task WhenIRequestTheContentSummaryWithSlugAndId(string slug, string id)
        {
            return this.RequestContentSummaryAndStoreResponseAsync(slug, id, null);
        }

        [When("I request the content summary with slug '(.*)' and Id '(.*)' using the etag returned by the previous request")]
        public Task WhenIRequestTheContentWithSlugAndIdUsingTheEtagReturnedByThePreviousRequest(string slug, string id)
        {
            SwaggerResponse<ContentSummaryResponse> lastResponse = this.scenarioContext.GetLastApiResponse<ContentSummaryResponse>();
            string lastEtag = lastResponse.Headers["ETag"].First();
            this.scenarioContext.ClearLastApiResponse();

            return this.RequestContentSummaryAndStoreResponseAsync(slug, id, lastEtag);
        }

        [When("I request the content summary with slug '(.*)' and Id '(.*)' using a random etag")]
        public Task WhenIRequestTheContentWithSlugAndIdUsingARandomEtag(string slug, string id)
        {
            return this.RequestContentSummaryAndStoreResponseAsync(slug, id, Guid.NewGuid().ToString());
        }

        [Then("the response body should contain a summary of the content item '(.*)'")]
        public void ThenTheResponseBodyShouldContainASummaryOfTheContentItem(string itemName)
        {
            SwaggerResponse<ContentSummaryResponse> actual = this.scenarioContext.GetLastApiResponse<ContentSummaryResponse>();
            Assert.IsNotNull(actual);

            Cms.Content expected = this.scenarioContext.Get<Cms.Content>(itemName);

            ContentSpecHelpers.Compare(expected, actual.Result);
        }

        [Then("the response should contain (.*) content summaries")]
        public void ThenTheResponseShouldContainContentSummaries(int expectedCount)
        {
            SwaggerResponse<ContentSummariesResponse> response = this.scenarioContext.GetLastApiResponse<ContentSummariesResponse>();

            ObservableCollection<ContentSummaryResponse> summaries = response.Result.Summaries;

            Assert.IsNotNull(summaries);
            Assert.AreEqual(expectedCount, summaries.Count);
        }

        [Then("the response should contain another (.*) content summaries")]
        public void ThenTheResponseShouldContainAnotherContentSummaries(int expectedCount)
        {
            SwaggerResponse<ContentSummariesResponse> response = this.scenarioContext.GetLastApiResponse<ContentSummariesResponse>();

            ObservableCollection<ContentSummaryResponse> summaries = response.Result.Summaries;

            Assert.IsNotNull(summaries);
            Assert.IsNotEmpty(summaries);

            Assert.AreEqual(expectedCount, summaries.Count);

            // Compare against previous set, which have been stored in the scenario context.
            ContentSummaryResponse[] previousSummaries = this.scenarioContext.Get<ContentSummaryResponse[]>();

            foreach (ContentSummaryResponse current in summaries)
            {
                Assert.IsFalse(
                    previousSummaries.Any(x => x.Id == current.Id),
                    $"The response contained a content summary with Id '{current.Id}' and slug '{current.Slug}' that was also present in the previous response");
            }
        }

        [Then("each content summary in the response should contain a '(.*)' link")]
        public void ThenEachContentSummaryInTheResponseShouldContainALink(string linkRel)
        {
            SwaggerResponse<ContentSummariesResponse> response = this.scenarioContext.GetLastApiResponse<ContentSummariesResponse>();

            Assert.IsTrue(
                response.Result.Summaries.All(x => x._links.ContainsKey(linkRel)),
                $"Not all content summaries in the response contained the expected link, '{linkRel}'");
        }

        private async Task RequestContentHistoryAndStoreResponseAsync(
            string slug,
            int? limit,
            string continuationToken,
            string etag)
        {
            string resolvedSlug = SpecHelpers.ParseSpecValue<string>(this.featureContext, slug);

            ContentClient client = this.featureContext.Get<ContentClient>();
            SwaggerResponse<ContentSummariesResponse> response = await client.GetContentHistoryAsync(
                this.featureContext.GetCurrentTenantId(),
                resolvedSlug,
                limit,
                continuationToken,
                etag).ConfigureAwait(false);

            this.scenarioContext.StoreLastApiResponse(response);
        }

        private async Task RequestContentSummaryAndStoreResponseAsync(string slug, string id, string etag)
        {
            string resolvedSlug = SpecHelpers.ParseSpecValue<string>(this.scenarioContext, slug);
            string resolvedId = SpecHelpers.ParseSpecValue<string>(this.scenarioContext, id);

            try
            {
                ContentClient client = this.featureContext.Get<ContentClient>();
                SwaggerResponse<ContentSummaryResponse> response = await client.GetContentSummaryAsync(
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
