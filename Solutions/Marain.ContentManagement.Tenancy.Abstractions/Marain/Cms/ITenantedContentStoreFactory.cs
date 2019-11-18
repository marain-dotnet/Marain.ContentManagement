// <copyright file="ITenantedContentStoreFactory.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System.Threading.Tasks;
    using Corvus.Tenancy;

    /// <summary>
    /// Interface for a factory that knows how to construct a content factory for
    /// a specific tenant.
    /// </summary>
    public interface ITenantedContentStoreFactory
    {
        /// <summary>
        /// Retrieves an <see cref="IContentStore"/> for the specified <see cref="Tenant"/>.
        /// </summary>
        /// <param name="tenantId">The Id of the tenant to retrieve a content store for.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<IContentStore> GetContentStoreForTenantAsync(string tenantId);
    }
}
