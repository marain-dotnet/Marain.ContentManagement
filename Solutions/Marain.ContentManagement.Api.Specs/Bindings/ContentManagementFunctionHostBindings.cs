namespace Marain.ContentManagement.Specs.Bindings
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

    [Binding]
    public static class ContentManagementFunctionHostBindings
    {
        public const string BaseUrl = "http://localhost:8765";

        private const string LastApiResponseBodyKey = "LastApiResponseBody";

        [BeforeScenario(Order = 0)]
        public static async Task StartContentManagementFunction(ScenarioContext context)
        {
            IWebHostBuilder builder = WebHost.CreateDefaultBuilder();
            builder.UseUrls(BaseUrl);
            builder.UseStartup<FunctionStartup<Marain.Cms.Api.Host.Startup>>();
            IWebHost host = builder.Build();

            await host.StartAsync().ConfigureAwait(false);

            context.Set(host);

            // Create a client for the test
            var client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };

            context.Set(client);
        }

        [AfterScenario(Order = int.MaxValue)]
        public static async Task StopContentManagementFunction(ScenarioContext context)
        {
            // This tears down the service provider too, so needs to happen last.
            IWebHost host = context.Get<IWebHost>();
            await host.StopAsync().ConfigureAwait(false);
            host.Dispose();

            HttpClient httpClient = context.Get<HttpClient>();
            httpClient.Dispose();
        }

        public static IServiceProvider ServiceProvider(this ScenarioContext context)
        {
            return context.Get<IWebHost>().Services;
        }

        public static HttpClient HttpClient(this ScenarioContext context)
        {
            return context.Get<HttpClient>();
        }

        public static HttpRequestMessage CreateApiRequest(this ScenarioContext _, string uriPath, HttpMethod method = null)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(uriPath, UriKind.Relative)
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Method = method ?? HttpMethod.Get;
            return request;
        }

        public static HttpRequestMessage CreateApiRequestWithJson(this ScenarioContext context, string uriPath, HttpMethod method, object data)
        {
            HttpRequestMessage request = context.CreateApiRequest(uriPath, method);

            string json = JsonConvert.SerializeObject(data);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return request;
        }

        public static async Task SendApiRequestAndStoreResponseAsync(this ScenarioContext context, HttpRequestMessage request)
        {
            HttpResponseMessage response = await context.HttpClient().SendAsync(request).ConfigureAwait(false);
            context.Set(response);
        }

        public static HttpResponseMessage GetLastApiResponse(this ScenarioContext context)
        {
            return context.Get<HttpResponseMessage>();
        }

        public static async Task<T> GetLastApiResponseBodyAsync<T>(this ScenarioContext context)
        {
            if (context.TryGetValue(LastApiResponseBodyKey, out T previouslyDeserializedResponse))
            {
                return previouslyDeserializedResponse;
            }

            HttpResponseMessage response = context.GetLastApiResponse();

            Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType, "Response does not have a content type of 'application/json' so cannot deserialize it.");

            string result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            T deserializedResponse = JsonConvert.DeserializeObject<T>(result);

            context.Set(deserializedResponse, LastApiResponseBodyKey);

            return deserializedResponse;
        }
    }
}
