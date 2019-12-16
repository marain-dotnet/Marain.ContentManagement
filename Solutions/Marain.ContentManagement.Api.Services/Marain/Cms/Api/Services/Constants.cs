﻿// <copyright file="Constants.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Api.Services
{
    /// <summary>
    /// Constants for standard recurring items.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Common link relation names.
        /// </summary>
        public static class LinkRelations
        {
            /// <summary>
            /// Link relation for "self" links.
            /// </summary>
            public const string Self = "self";

            /// <summary>
            /// Link relation for "next" links.
            /// </summary>
            public const string Next = "next";
        }

        /// <summary>
        /// Common parameter names.
        /// </summary>
        public static class ParameterNames
        {
            /// <summary>
            /// The current tenant Id.
            /// </summary>
            public const string TenantId = "tenantId";

            /// <summary>
            /// Maximum number of items to return from a request.
            /// </summary>
            public const string Limit = "limit";

            /// <summary>
            /// Current content slug.
            /// </summary>
            public const string Slug = "slug";

            /// <summary>
            /// The requested content Id.
            /// </summary>
            public const string ContentId = "contentId";

            /// <summary>
            /// Continuation token for the current search request.
            /// </summary>
            public const string ContinuationToken = "continuationToken";
        }
    }
}