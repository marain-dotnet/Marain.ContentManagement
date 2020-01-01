// <copyright file="ResponseExtensions.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Helpers
{
    using System.Reflection;
    using System.Web;
    using Marain.Cms.Api.Client;

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
    }
}
