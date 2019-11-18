// <copyright file="ContentManagementFunctionHostBindings.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Bindings
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Marain.Cms.Api.Host;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

    /// <summary>
    /// SpecFlow binding that will run the CMS Menes services in-memory.
    /// </summary>
    /// <remarks>
    /// As part of initialising the function, the ServiceProvider will also be initialised. This can then be accessed via
    /// an extension method that's part of this class.
    /// </remarks>
    [Binding]
    public static class ContentManagementFunctionHostBindings
    {
        /// <summary>
        /// The base Url that can be used to access the in-memory API.
        /// </summary>
        public const string BaseUrl = "http://localhost:8765";

        private const string LastApiResponseBodyKey = "LastApiResponseBody";

        /// <summary>
        /// Sets up and runs the function, using the <see cref="Startup"/> class to initialise the service provider.
        /// </summary>
        /// <param name="context">The current <see cref="ScenarioContext"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>
        /// This method is set to run first so that subsequent <see cref="BeforeScenarioAttribute"/> methods are able to
        /// access the <see cref="ServiceProvider"/> created as a result.
        /// As part of the initialisation, an <see cref="HttpClient"/> will be created and stored in
        /// the <see cref="ScenarioContext"/>.
        /// </remarks>
        [BeforeScenario(Order = 0)]
        public static async Task StartContentManagementFunction(ScenarioContext context)
        {
            IWebHostBuilder builder = WebHost.CreateDefaultBuilder();
            builder.UseUrls(BaseUrl);
            builder.UseStartup<FunctionStartup<Startup>>();
            IWebHost host = builder.Build();

            await host.StartAsync().ConfigureAwait(false);

            context.Set(host);

            // Create a client for the test
            var client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl),
            };

            context.Set(client);
        }

        /// <summary>
        /// Tears down the in-memory service.
        /// </summary>
        /// <param name="context">The current <see cref="ScenarioContext"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This tears down the service provider too, so needs to happen last.</remarks>
        [AfterScenario(Order = int.MaxValue)]
        public static async Task StopContentManagementFunction(ScenarioContext context)
        {
            IWebHost host = context.Get<IWebHost>();
            await host.StopAsync().ConfigureAwait(false);
            host.Dispose();

            HttpClient httpClient = context.Get<HttpClient>();
            httpClient.Dispose();
        }

        /// <summary>
        /// Helper method to access the current <see cref="ServiceProvider"/>.
        /// </summary>
        /// <param name="context">The current <see cref="ScenarioContext"/>.</param>
        /// <returns>The current configured <see cref="IServiceProvider"/>.</returns>
        public static IServiceProvider ServiceProvider(this ScenarioContext context)
        {
            return context.Get<IWebHost>().Services;
        }

        /// <summary>
        /// Helper method to access the current <see cref="HttpClient"/>. Prefer to use one of the other helper methods
        /// in this class to create HTTP requests and read responses.
        /// </summary>
        /// <param name="context">The current <see cref="ScenarioContext"/>.</param>
        /// <returns>The current <see cref="HttpClient"/>.</returns>
        public static HttpClient HttpClient(this ScenarioContext context)
        {
            return context.Get<HttpClient>();
        }

        /// <summary>
        /// Creates a new <see cref="HttpRequestMessage"/> to access the specified path using the given method.
        /// </summary>
        /// <param name="_">The current <see cref="ScenarioContext"/>.</param>
        /// <param name="uriPath">The relative path to the endpoint being requested.</param>
        /// <param name="method">The <see cref="HttpMethod"/> to use to access the endpoint.</param>
        /// <returns>The newly created <see cref="HttpRequestMessage"/>.</returns>
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
        public static HttpRequestMessage CreateApiRequest(this ScenarioContext _, string uriPath, HttpMethod method = null)
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(uriPath, UriKind.Relative),
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Method = method ?? HttpMethod.Get;
            return request;
        }

        /// <summary>
        /// Creates a new <see cref="HttpRequestMessage"/> to access the specified path using the given method, with the
        /// request body containing the value supplied, serialized as JSON.
        /// </summary>
        /// <param name="context">The current <see cref="ScenarioContext"/>.</param>
        /// <param name="uriPath">The relative path to the endpoint being requested.</param>
        /// <param name="method">The <see cref="HttpMethod"/> to use to access the endpoint.</param>
        /// <param name="data">The data to send in the request body.</param>
        /// <returns>The newly created <see cref="HttpRequestMessage"/>.</returns>
        public static HttpRequestMessage CreateApiRequestWithJson(this ScenarioContext context, string uriPath, HttpMethod method, object data)
        {
            HttpRequestMessage request = context.CreateApiRequest(uriPath, method);

            string json = JsonConvert.SerializeObject(data);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return request;
        }

        /// <summary>
        /// Sends the supplied <see cref="HttpRequestMessage"/> using the current <see cref="HttpClient"/>, and stores the
        /// resultant <see cref="HttpResponseMessage"/> in the <see cref="ScenarioContext"/> from where it can be accessed
        /// for use in test assertions.
        /// </summary>
        /// <param name="context">The current <see cref="ScenarioContext"/>.</param>
        /// <param name="request">The <see cref="HttpRequestMessage"/> to send.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task SendApiRequestAndStoreResponseAsync(this ScenarioContext context, HttpRequestMessage request)
        {
            HttpResponseMessage response = await context.HttpClient().SendAsync(request).ConfigureAwait(false);
            context.Set(response);
        }

        /// <summary>
        /// Gets the <see cref="HttpResponseMessage"/> from the last call to
        /// <see cref="SendApiRequestAndStoreResponseAsync(ScenarioContext, HttpRequestMessage)"/>.
        /// </summary>
        /// <param name="context">The current <see cref="ScenarioContext"/>.</param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/> from the last call to
        /// <see cref="SendApiRequestAndStoreResponseAsync(ScenarioContext, HttpRequestMessage)"/>.
        /// </returns>
        public static HttpResponseMessage GetLastApiResponse(this ScenarioContext context)
        {
            return context.Get<HttpResponseMessage>();
        }

        /// <summary>
        /// Gets the body from the response to the last call to
        /// <see cref="SendApiRequestAndStoreResponseAsync(ScenarioContext, HttpRequestMessage)"/>.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response body to.</typeparam>
        /// <param name="context">The current <see cref="ScenarioContext"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
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
