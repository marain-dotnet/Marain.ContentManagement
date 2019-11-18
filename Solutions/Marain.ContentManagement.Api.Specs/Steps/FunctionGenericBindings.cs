namespace Marain.ContentManagement.Specs.Steps
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    using Marain.ContentManagement.Specs.Bindings;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

    [Binding]
    public class FunctionGenericBindings
    {
        private readonly ScenarioContext scenarioContext;

        public FunctionGenericBindings(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [When("I request the content with slug '(.*)' and Id '(.*)'")]
        public Task WhenIRequestTheContentWithSlug(string slug, string id)
        {
            string path = $"/{this.scenarioContext.CurrentTenantId()}/marain/content/{HttpUtility.UrlEncode(slug)}?contentId={HttpUtility.UrlEncode(id)}";

            HttpRequestMessage request = this.scenarioContext.CreateApiRequest(path);
            return this.scenarioContext.SendApiRequestAndStoreResponseAsync(request);
        }

        [Then("the response should have a status of '(.*)'")]
        public void ThenTheResponseShouldHaveAStatusOf(int expectedStatusCode)
        {
            HttpResponseMessage response = this.scenarioContext.GetLastApiResponse();
            Assert.AreEqual((HttpStatusCode)expectedStatusCode, response.StatusCode);
        }
    }
}
