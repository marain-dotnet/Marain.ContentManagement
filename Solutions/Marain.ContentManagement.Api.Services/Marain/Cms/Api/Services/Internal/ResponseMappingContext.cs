// <copyright file="ResponseMappingContext.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Api.Services.Internal
{
    /// <summary>
    /// General class for context items common to all response mapping.
    /// </summary>
    public class ResponseMappingContext
    {
        /// <summary>
        /// Gets or sets the current tenant Id.
        /// </summary>
        public string TenantId { get; set; }
    }
}
