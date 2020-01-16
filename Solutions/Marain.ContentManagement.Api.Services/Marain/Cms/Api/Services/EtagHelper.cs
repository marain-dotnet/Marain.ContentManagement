// <copyright file="EtagHelper.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Api.Services
{
    /// <summary>
    /// Helper methods for working with etags.
    /// </summary>
    public static class EtagHelper
    {
        /// <summary>
        /// Compares the given if-none-match header value to an etag, and returns true if the
        /// two match.
        /// </summary>
        /// <param name="ifNoneMatchHeaderValue">The value from the If-None-Match header.</param>
        /// <param name="etag">The value to compare.</param>
        /// <returns>True if the etags match, false if not.</returns>
        public static bool IsMatch(string ifNoneMatchHeaderValue, string etag)
        {
            return !string.IsNullOrEmpty(ifNoneMatchHeaderValue) && etag == ifNoneMatchHeaderValue;
        }

        /// <summary>
        /// Constructs a single etag value by combining the list of supplied values together. The resulting
        /// etag will be sensitive to the order of items in the list.
        /// </summary>
        /// <param name="discriminator">An arbitrary discriminator value that can be used to differentiate
        /// between different representations of items with the same underlying hash codes.</param>
        /// <param name="etags">The list of individual etags to combine.</param>
        /// <returns>A single combined etag.</returns>
        public static string BuildEtag(string discriminator, params string[] etags)
        {
            int hashCode = 160482331 * discriminator.GetHashCode();

            foreach (string current in etags)
            {
                hashCode = (hashCode * -179424319) + current.GetHashCode();
            }

            return string.Concat("\"", hashCode.ToString(), "\"");
        }
    }
}
