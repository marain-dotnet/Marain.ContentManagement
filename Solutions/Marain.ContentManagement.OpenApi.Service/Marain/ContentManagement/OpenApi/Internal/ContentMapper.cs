// <copyright file="ContentMapper.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.OpenApi.Internal
{
    using System;
    using Marain.Cms;
    using Menes;
    using Menes.Hal;

    /// <summary>
    /// Maps <see cref="Content"/> to and from a <see cref="HalDocument"/>.
    /// </summary>
    internal class ContentMapper : IHalDocumentMapper<Content>
    {
        /// <inheritdoc/>
        public void ConfigureLinkMap(IOpenApiLinkOperationMap links)
        {
        }

        /// <inheritdoc/>
        public HalDocument Map(Content resource)
        {
            throw new NotImplementedException();
        }
    }
}
