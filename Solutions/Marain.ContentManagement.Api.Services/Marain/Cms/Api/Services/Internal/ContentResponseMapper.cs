// <copyright file="ContentResponseMapper.cs" company="Endjin Limited">
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
    public class ContentResponseMapper : IHalDocumentMapper<Content, ResponseMappingContext>
    {
        private readonly IHalDocumentFactory halDocumentFactory;
        private readonly IOpenApiWebLinkResolver linkResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentResponseMapper"/> class.
        /// </summary>
        /// <param name="halDocumentFactory">The factory with which to create <see cref="HalDocument"/> instances.</param>
        /// <param name="linkResolver">The link resolver to build the links collection.</param>
        public ContentResponseMapper(IHalDocumentFactory halDocumentFactory, IOpenApiWebLinkResolver linkResolver)
        {
            this.halDocumentFactory = halDocumentFactory;
            this.linkResolver = linkResolver;
        }

        /// <inheritdoc/>
        public void ConfigureLinkMap(IOpenApiLinkOperationMap links)
        {
            links.MapByContentTypeAndRelationTypeAndOperationId<Content>(Constants.LinkRelations.Self, ContentService.GetContentOperationId);
            links.MapByContentTypeAndRelationTypeAndOperationId<Content>(Constants.LinkRelations.ContentSummary, ContentSummaryService.GetContentSummaryOperationId);
            links.MapByContentTypeAndRelationTypeAndOperationId<Content>(Constants.LinkRelations.History, ContentHistoryService.GetContentHistoryOperationId);
        }

        /// <inheritdoc/>
        public HalDocument Map(Content resource, ResponseMappingContext context)
        {
            HalDocument response = this.halDocumentFactory.CreateHalDocumentFrom(resource);

            response.ResolveAndAddByOwnerAndRelationType(
                this.linkResolver,
                resource,
                Constants.LinkRelations.Self,
                ("tenantId", context.TenantId),
                ("slug", resource.Slug),
                ("contentId", resource.Id));

            response.ResolveAndAddByOwnerAndRelationType(
                this.linkResolver,
                resource,
                Constants.LinkRelations.ContentSummary,
                ("tenantId", context.TenantId),
                ("slug", resource.Slug),
                ("contentId", resource.Id));

            response.ResolveAndAddByOwnerAndRelationType(
                this.linkResolver,
                resource,
                Constants.LinkRelations.History,
                ("tenantId", context.TenantId),
                ("slug", resource.Slug));

            return response;
        }
    }
}
