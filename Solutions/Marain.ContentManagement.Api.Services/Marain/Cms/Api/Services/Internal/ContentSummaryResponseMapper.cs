// <copyright file="ContentSummaryResponseMapper.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Api.Services.Internal
{
    using Menes;
    using Menes.Hal;
    using Menes.Links;

    /// <summary>
    /// Maps <see cref="ContentSummary"/> to and from a <see cref="HalDocument"/>.
    /// </summary>
    public class ContentSummaryResponseMapper : IHalDocumentMapper<ContentSummary, ResponseMappingContext>
    {
        private readonly IHalDocumentFactory halDocumentFactory;
        private readonly IOpenApiWebLinkResolver linkResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentSummaryResponseMapper"/> class.
        /// </summary>
        /// <param name="halDocumentFactory">The factory with which to create <see cref="HalDocument"/> instances.</param>
        /// <param name="linkResolver">The link resolver to build the links collection.</param>
        public ContentSummaryResponseMapper(IHalDocumentFactory halDocumentFactory, IOpenApiWebLinkResolver linkResolver)
        {
            this.halDocumentFactory = halDocumentFactory;
            this.linkResolver = linkResolver;
        }

        /// <inheritdoc/>
        public void ConfigureLinkMap(IOpenApiLinkOperationMap links)
        {
            links.MapByContentTypeAndRelationTypeAndOperationId<ContentSummary>(Constants.LinkRelations.Self, ContentSummaryService.GetContentSummaryOperationId);
            links.MapByContentTypeAndRelationTypeAndOperationId<ContentSummary>(Constants.LinkRelations.Content, ContentService.GetContentOperationId);
            links.MapByContentTypeAndRelationTypeAndOperationId<ContentSummary>(Constants.LinkRelations.History, ContentHistoryService.GetContentHistoryOperationId);
        }

        /// <inheritdoc/>
        public HalDocument Map(ContentSummary resource, ResponseMappingContext context)
        {
            HalDocument response = this.halDocumentFactory.CreateHalDocumentFrom(resource);

            response.ResolveAndAddByOwnerAndRelationType(
                this.linkResolver,
                resource,
                Constants.LinkRelations.Self,
                (Constants.ParameterNames.TenantId, context.TenantId),
                (Constants.ParameterNames.Slug, resource.Slug),
                (Constants.ParameterNames.ContentId, resource.Id));

            response.ResolveAndAddByOwnerAndRelationType(
                this.linkResolver,
                resource,
                Constants.LinkRelations.Content,
                (Constants.ParameterNames.TenantId, context.TenantId),
                (Constants.ParameterNames.Slug, resource.Slug),
                (Constants.ParameterNames.ContentId, resource.Id));

            response.ResolveAndAddByOwnerAndRelationType(
                this.linkResolver,
                resource,
                Constants.LinkRelations.History,
                (Constants.ParameterNames.TenantId, context.TenantId),
                (Constants.ParameterNames.Slug, resource.Slug));

            return response;
        }
    }
}
