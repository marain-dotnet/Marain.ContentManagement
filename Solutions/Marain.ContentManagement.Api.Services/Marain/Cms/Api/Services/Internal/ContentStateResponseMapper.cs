// <copyright file="ContentStateResponseMapper.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Api.Services.Internal
{
    using System.Threading.Tasks;
    using Menes;
    using Menes.Hal;
    using Menes.Links;

    /// <summary>
    /// Maps <see cref="ContentState"/> to and from a <see cref="HalDocument"/>.
    /// </summary>
    public class ContentStateResponseMapper : IHalDocumentMapper<ContentState, ContentStateResponseMappingContext>
    {
        private readonly IHalDocumentFactory halDocumentFactory;
        private readonly IOpenApiWebLinkResolver linkResolver;
        private readonly ContentResponseMapper contentResponseMapper;
        private readonly ContentSummaryResponseMapper contentSummaryResponseMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentStateResponseMapper"/> class.
        /// </summary>
        /// <param name="halDocumentFactory">The factory with which to create <see cref="HalDocument"/> instances.</param>
        /// <param name="linkResolver">The link resolver to build the links collection.</param>
        /// <param name="contentResponseMapper">The mapper to use for embedded <see cref="Content"/> documents.</param>
        /// <param name="contentSummaryResponseMapper">The mapper to use for embedded <see cref="ContentSummary"/> documents.</param>
        public ContentStateResponseMapper(
            IHalDocumentFactory halDocumentFactory,
            IOpenApiWebLinkResolver linkResolver,
            ContentResponseMapper contentResponseMapper,
            ContentSummaryResponseMapper contentSummaryResponseMapper)
        {
            this.halDocumentFactory = halDocumentFactory;
            this.linkResolver = linkResolver;
            this.contentResponseMapper = contentResponseMapper;
            this.contentSummaryResponseMapper = contentSummaryResponseMapper;
        }

        /// <inheritdoc/>
        public void ConfigureLinkMap(IOpenApiLinkOperationMap links)
        {
            links.MapByContentTypeAndRelationTypeAndOperationId<ContentState>(Constants.LinkRelations.Self, WorkflowStateService.GetWorkflowStateOperationId);
            links.MapByContentTypeAndRelationTypeAndOperationId<ContentState>(Constants.LinkRelations.Content, ContentService.GetContentOperationId);
            links.MapByContentTypeAndRelationTypeAndOperationId<ContentState>(Constants.LinkRelations.ContentSummary, ContentSummaryService.GetContentSummaryOperationId);
        }

        /// <inheritdoc/>
        public async ValueTask<HalDocument> MapAsync(ContentState resource, ContentStateResponseMappingContext context)
        {
            HalDocument response = this.halDocumentFactory.CreateHalDocumentFrom(resource);

            response.ResolveAndAddByOwnerAndRelationType(
                this.linkResolver,
                resource,
                Constants.LinkRelations.Self,
                (Constants.ParameterNames.TenantId, context.TenantId),
                (Constants.ParameterNames.Slug, resource.Slug),
                (Constants.ParameterNames.ContentId, resource.Id),
                (Constants.ParameterNames.Embed, context.Embed));

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
                Constants.LinkRelations.ContentSummary,
                (Constants.ParameterNames.TenantId, context.TenantId),
                (Constants.ParameterNames.Slug, resource.Slug),
                (Constants.ParameterNames.ContentId, resource.Id));

            if (context.Content != null)
            {
                response.AddEmbeddedResource(
                    Constants.LinkRelations.Content,
                    await this.contentResponseMapper.MapAsync(context.Content, context).ConfigureAwait(false));
            }
            else if (context.ContentSummary != null)
            {
                response.AddEmbeddedResource(
                    Constants.LinkRelations.ContentSummary,
                    await this.contentSummaryResponseMapper.MapAsync(context.ContentSummary, context));
            }

            return response;
        }
    }
}
