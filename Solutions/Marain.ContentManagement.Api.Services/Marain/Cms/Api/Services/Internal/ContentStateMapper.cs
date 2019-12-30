// <copyright file="ContentStateMapper.cs" company="Endjin Limited">
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
    public class ContentStateMapper : IHalDocumentMapper<ContentState, IOpenApiContext>
    {
        private readonly IHalDocumentFactory halDocumentFactory;
        private readonly IOpenApiWebLinkResolver linkResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentStateMapper"/> class.
        /// </summary>
        /// <param name="halDocumentFactory">The factory with which to create <see cref="HalDocument"/> instances.</param>
        /// <param name="linkResolver">The link resolver to build the links collection.</param>
        public ContentStateMapper(IHalDocumentFactory halDocumentFactory, IOpenApiWebLinkResolver linkResolver)
        {
            this.halDocumentFactory = halDocumentFactory;
            this.linkResolver = linkResolver;
        }

        /// <inheritdoc/>
        public void ConfigureLinkMap(IOpenApiLinkOperationMap links)
        {
            links.MapByContentTypeAndRelationTypeAndOperationId<ContentState>(Constants.LinkRelations.Self, WorkflowContentService.GetWorkflowContentOperationId);
            links.MapByContentTypeAndRelationTypeAndOperationId<ContentState>("content", ContentService.GetContentOperationId);
            links.MapByContentTypeAndRelationTypeAndOperationId<ContentState>("content-with-state", WorkflowContentService.GetWorkflowContentOperationId);
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
                "content",
                (Constants.ParameterNames.TenantId, context.CurrentTenantId),
                (Constants.ParameterNames.Slug, resource.Slug),
                (Constants.ParameterNames.ContentId, resource.Id));

            response.ResolveAndAddByOwnerAndRelationType(
                this.linkResolver,
                resource,
                "content-with-state",
                (Constants.ParameterNames.TenantId, context.CurrentTenantId),
                (Constants.ParameterNames.Slug, resource.Slug),
                (Constants.ParameterNames.ContentId, resource.Id));

            return response;
        }
    }
}
