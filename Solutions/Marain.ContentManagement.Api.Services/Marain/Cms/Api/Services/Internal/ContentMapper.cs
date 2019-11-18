// <copyright file="ContentMapper.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Api.Services.Internal
{
    using Menes;
    using Menes.Hal;
    using Menes.Links;

    /// <summary>
    /// Maps <see cref="Content"/> to and from a <see cref="HalDocument"/>.
    /// </summary>
    public class ContentMapper : IHalDocumentMapper<Content, IOpenApiContext>
    {
        private readonly IHalDocumentFactory halDocumentFactory;
        private readonly IOpenApiWebLinkResolver linkResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentMapper"/> class.
        /// </summary>
        /// <param name="halDocumentFactory">The factory with which to create <see cref="HalDocument"/> instances.</param>
        /// <param name="linkResolver">The link resolver to build the links collection.</param>
        public ContentMapper(IHalDocumentFactory halDocumentFactory, IOpenApiWebLinkResolver linkResolver)
        {
            this.halDocumentFactory = halDocumentFactory;
            this.linkResolver = linkResolver;
        }

        /// <inheritdoc/>
        public void ConfigureLinkMap(IOpenApiLinkOperationMap links)
        {
            links.Map<Content>("self", ContentService.GetContentOperationId);
        }

        /// <inheritdoc/>
        public HalDocument Map(Content resource, IOpenApiContext context)
        {
            HalDocument response = this.halDocumentFactory.CreateHalDocumentFrom(resource);
            response.ResolveAndAdd(
                this.linkResolver,
                resource,
                "self",
                ("tenantId", context.CurrentTenantId),
                ("slug", resource.Slug),
                ("contentId", resource.Id));

            return response;
        }
    }
}
