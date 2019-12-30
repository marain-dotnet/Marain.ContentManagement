// <copyright file="SwaggerResponseExtensions.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using Marain.Cms.Api.Client;

    /// <summary>
    /// Extension methods for the <see cref="SwaggerResponse"/> class.
    /// </summary>
    public static class SwaggerResponseExtensions
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
    }
}
