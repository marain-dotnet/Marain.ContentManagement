// <copyright file="ContentStateResponseMapper.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Api.Services.Internal
{
    using Menes;
    using Menes.Hal;
    using Menes.Links;

    /// <summary>
    /// Maps <see cref="ContentState"/> to and from a <see cref="HalDocument"/>.
    /// </summary>
    public class ContentStateResponseMapper : IHalDocumentMapper<ContentState, IOpenApiContext>
    {
        private readonly IHalDocumentFactory halDocumentFactory;
        private readonly IOpenApiWebLinkResolver linkResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentStateResponseMapper"/> class.
        /// </summary>
        /// <param name="halDocumentFactory">The factory with which to create <see cref="HalDocument"/> instances.</param>
        /// <param name="linkResolver">The link resolver to build the links collection.</param>
        public ContentStateResponseMapper(IHalDocumentFactory halDocumentFactory, IOpenApiWebLinkResolver linkResolver)
        {
            this.halDocumentFactory = halDocumentFactory;
            this.linkResolver = linkResolver;
        }

        /// <inheritdoc/>
        public void ConfigureLinkMap(IOpenApiLinkOperationMap links)
        {
            links.MapByContentTypeAndRelationTypeAndOperationId<ContentState>(Constants.LinkRelations.Self, WorkflowContentService.GetWorkflowStateOperationId);
            links.MapByContentTypeAndRelationTypeAndOperationId<ContentState>(Constants.LinkRelations.Content, ContentService.GetContentOperationId);
            links.MapByContentTypeAndRelationTypeAndOperationId<ContentState>(Constants.LinkRelations.ContentSummary, ContentSummaryService.GetContentSummaryOperationId);
        }

        /// <inheritdoc/>
        public HalDocument Map(ContentState resource, IOpenApiContext context)
        {
            HalDocument response = this.halDocumentFactory.CreateHalDocumentFrom(resource);

            response.ResolveAndAddByOwnerAndRelationType(
                this.linkResolver,
                resource,
                Constants.LinkRelations.Self,
                (Constants.ParameterNames.TenantId, context.CurrentTenantId),
                (Constants.ParameterNames.Slug, resource.Slug),
                (Constants.ParameterNames.ContentId, resource.Id));

            response.ResolveAndAddByOwnerAndRelationType(
                this.linkResolver,
                resource,
                Constants.LinkRelations.Content,
                (Constants.ParameterNames.TenantId, context.CurrentTenantId),
                (Constants.ParameterNames.Slug, resource.Slug),
                (Constants.ParameterNames.ContentId, resource.Id));

            response.ResolveAndAddByOwnerAndRelationType(
                this.linkResolver,
                resource,
                Constants.LinkRelations.ContentSummary,
                (Constants.ParameterNames.TenantId, context.CurrentTenantId),
                (Constants.ParameterNames.Slug, resource.Slug),
                (Constants.ParameterNames.ContentId, resource.Id));

            return response;
        }
    }
}
