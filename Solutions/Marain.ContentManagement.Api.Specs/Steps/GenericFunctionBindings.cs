namespace Marain.ContentManagement.Specs.Steps
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Marain.ContentManagement.Specs.Bindings;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

    [Binding]
    public class GenericFunctionBindings
    {
        private readonly ScenarioContext scenarioContext;

        public GenericFunctionBindings(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Then("the response should have a status of '(.*)'")]
        public void ThenTheResponseShouldHaveAStatusOf(int expectedStatusCode)
        {
            HttpResponseMessage response = this.scenarioContext.GetLastApiResponse();
            Assert.AreEqual((HttpStatusCode)expectedStatusCode, response.StatusCode);
        }

        [Then("there should be no response body")]
        public async Task ThenThereShouldBeNoResponseBody()
        {
            HttpResponseMessage response = this.scenarioContext.GetLastApiResponse();
            byte[] responseBytes = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

            Assert.IsEmpty(responseBytes);
        }
    }
}
