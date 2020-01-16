// <copyright file="ScenarioContextApiCallExtensions.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Helpers
{
    using Marain.Cms.Api.Client;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Extension methods for <c>ScenarioContext</c> that assist with storing and retrieving API responses.
    /// </summary>
    public static class ScenarioContextApiCallExtensions
    {
        private const string LastApiResponseKey = "LastApiResponse";

        private const string LastApiExceptionKey = "LastApiException";

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
