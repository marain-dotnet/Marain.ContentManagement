namespace Marain.ContentManagement.Specs.Steps
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Web;
    using Marain.ContentManagement.Specs.Bindings;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

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
            string path = $"/{this.scenarioContext.CurrentTenantId()}/marain/content/{HttpUtility.UrlEncode(slug)}?contentId={HttpUtility.UrlEncode(id)}";

            HttpRequestMessage request = this.scenarioContext.CreateApiRequest(path);
            return this.scenarioContext.SendApiRequestAndStoreResponseAsync(request);
        }

        [When("I request the content with slug '(.*)' and Id '(.*)' using the etag returned by the previous request")]
        public Task WhenIRequestTheContentWithSlugAndIdUsingTheEtagReturnedByThePreviousRequest(string slug, string id)
        {
            HttpResponseMessage lastResponse = this.scenarioContext.GetLastApiResponse();
            EntityTagHeaderValue lastEtag = lastResponse.Headers.ETag;

            string path = $"/{this.scenarioContext.CurrentTenantId()}/marain/content/{HttpUtility.UrlEncode(slug)}?contentId={HttpUtility.UrlEncode(id)}";

            HttpRequestMessage request = this.scenarioContext.CreateApiRequest(path);
            request.Headers.IfNoneMatch.Add(lastEtag);

            return this.scenarioContext.SendApiRequestAndStoreResponseAsync(request);
        }

        [When(@"I request the content with slug '(.*)' and Id '(.*)' using a random etag")]
        public Task WhenIRequestTheContentWithSlugAndIdUsingARandomEtag(string slug, string id)
        {
            string path = $"/{this.scenarioContext.CurrentTenantId()}/marain/content/{HttpUtility.UrlEncode(slug)}?contentId={HttpUtility.UrlEncode(id)}";

            HttpRequestMessage request = this.scenarioContext.CreateApiRequest(path);
            request.Headers.IfNoneMatch.Add(new EntityTagHeaderValue($"\"{Guid.NewGuid().ToString()}\""));

            return this.scenarioContext.SendApiRequestAndStoreResponseAsync(request);
        }
    }
}
