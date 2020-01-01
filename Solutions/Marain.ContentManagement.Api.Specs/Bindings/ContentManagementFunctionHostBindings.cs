// <copyright file="ContentManagementFunctionHostBindings.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Bindings
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Corvus.SpecFlow.Extensions;
    using Marain.Cms.Api.Client;
    using Marain.Cms.Api.Host;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
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

        private const string LastApiResponseKey = "LastApiResponse";

        private const string LastApiExceptionKey = "LastApiException";

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
        [BeforeScenario("useContentManagementApi", Order = 0)]
        public static async Task StartContentManagementFunction(ScenarioContext context)
        {
            IWebHostBuilder builder = WebHost.CreateDefaultBuilder();
            builder.UseUrls(BaseUrl);
            builder.UseStartup<FunctionStartup<Startup>>();
            IWebHost host = builder.Build();

            await host.StartAsync().ConfigureAwait(false);

            context.Set(host);

            // Create a client for the test
            var httpClient = new HttpClient();
            var client = new ContentClient(httpClient)
            {
                BaseUrl = BaseUrl,
            };

            context.Set(httpClient);
            context.Set(client);
        }

        /// <summary>
        /// Tears down the in-memory service.
        /// </summary>
        /// <param name="context">The current <see cref="ScenarioContext"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This tears down the service provider too, so needs to happen last.</remarks>
        [AfterScenario("useContentManagementApi", Order = int.MaxValue)]
        public static Task StopContentManagementFunction(ScenarioContext context)
        {
            return context.RunAndStoreExceptionsAsync(async () =>
            {
                IWebHost host = context.Get<IWebHost>();
                await host.StopAsync().ConfigureAwait(false);
                host.Dispose();

                HttpClient httpClient = context.Get<HttpClient>();
                httpClient.Dispose();
            });
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
        public static ContentClient ContentClient(this ScenarioContext context)
        {
            return context.Get<ContentClient>();
        }

        /// <summary>
        /// Stores the last API response in the specified scenario context.
        /// </summary>
        /// <param name="context">The context to store data in.</param>
        /// <param name="response">The response to store.</param>
        public static void StoreLastApiResponse(this ScenarioContext context, SwaggerResponse response)
        {
            context.Set(response, LastApiResponseKey);
        }

        /// <summary>
        /// Removes any stored API response from the scenario context.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void ClearLastApiResponse(this ScenarioContext context)
        {
            context.Remove(LastApiResponseKey);
            context.Remove(LastApiExceptionKey);
        }

        /// <summary>
        /// Retrieves the most recent API response stored in the scenario context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The most recently stored <c>SwaggerResponse</c>.</returns>
        public static SwaggerResponse GetLastApiResponse(this ScenarioContext context)
        {
            return context.Get<SwaggerResponse>(LastApiResponseKey);
        }

        /// <summary>
        /// Attempts to retrieve the most recent API response stored in the scenario context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="response">The response.</param>
        /// <returns>True if there is a response to return, false otherwise.</returns>
        public static bool TryGetLastApiResponse(this ScenarioContext context, out SwaggerResponse response)
        {
            return context.TryGetValue(LastApiResponseKey, out response);
        }

        /// <summary>
        /// Retrieves the most recent API response stored in the scenario context.
        /// </summary>
        /// <typeparam name="T">The type of the response content.</typeparam>
        /// <param name="context">The context.</param>
        /// <returns>The most recently stored <c>SwaggerResponse</c>.</returns>
        public static SwaggerResponse<T> GetLastApiResponse<T>(this ScenarioContext context)
        {
            return context.Get<SwaggerResponse<T>>(LastApiResponseKey);
        }

        /// <summary>
        /// Stores the given exception in the context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="ex">The exception to store.</param>
        public static void StoreLastApiException(this ScenarioContext context, SwaggerException ex)
        {
            context.Set(ex, LastApiExceptionKey);
        }

        /// <summary>
        /// Retrieves the most recent API response stored in the scenario context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The API response.</returns>
        public static SwaggerException GetLastApiException(this ScenarioContext context)
        {
            return context.Get<SwaggerException>(LastApiExceptionKey);
        }
    }
}
