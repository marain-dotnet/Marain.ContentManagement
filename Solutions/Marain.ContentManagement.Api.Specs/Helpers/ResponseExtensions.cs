// <copyright file="ResponseExtensions.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Helpers
{
    using System;
    using System.Reflection;
    using System.Web;
    using Marain.Cms.Api.Client;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Extension methods for the <see cref="SwaggerResponse"/> class.
    /// </summary>
    public static class ResponseExtensions
    {
        /// <summary>
        /// Attempts to obtain the Result associated with the <see cref="SwaggerResponse"/> as the specified type.
        /// </summary>
        /// <typeparam name="T">The type to return the Result as.</typeparam>
        /// <param name="response">The response to extract the result from.</param>
        /// <returns>The result.</returns>
        public static T ResultAs<T>(this SwaggerResponse response)
        {
            PropertyInfo resourceProperty = response.GetType().GetProperty("Result");
            return (T)resourceProperty.GetValue(response);
        }

        /// <summary>
        /// Extracts the continuation token from the "next" link in the links collection of the given
        /// response.
        /// </summary>
        /// <param name="response">The response to extract the token from.</param>
        /// <returns>The continuation token.</returns>
        public static string ExtractContinuationToken(this Resource response)
        {
            // Extract the continuation token from the response... it's in the "next" header
            string nextUri = (string)response._links["next"].AdditionalProperties["href"];
            int startIndex = nextUri.IndexOf("continuationToken") + 18;
            int endIndex = nextUri.IndexOf("&", startIndex);
            int length = endIndex == -1 ? nextUri.Length - startIndex : endIndex - startIndex;
            return HttpUtility.UrlDecode(nextUri.Substring(startIndex, length));
        }

        /// <summary>
        /// Retrieves the embedded document with the given rel.
        /// </summary>
        /// <typeparam name="T">The type of the embedded document.</typeparam>
        /// <param name="resource">The resource containing the embedded document.</param>
        /// <param name="rel">The name of the embedded document.</param>
        /// <returns>The embedded document.</returns>
        public static T GetEmbeddedDocument<T>(this Resource resource, string rel)
            where T : Resource
        {
            if (resource._embedded.TryGetValue(rel, out ResourceEmbeddedResource val))
            {
                // Problem here: The result could be either a single Resource or an array. As a result, it's not possible
                // to directly cast it to the specified type.
                // The easiest way to get it there is to serialize it to a JObject and then back to the required type.
                var serialized = JObject.FromObject(val);
                return serialized.ToObject<T>();
            }

            throw new ArgumentException("There is no embedded resource with the specified relation name.");
        }
    }
}
