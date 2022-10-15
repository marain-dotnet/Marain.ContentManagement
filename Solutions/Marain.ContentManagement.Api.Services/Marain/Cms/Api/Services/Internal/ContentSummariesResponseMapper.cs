// <copyright file="ContentSummariesResponseMapper.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Api.Services.Internal
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Menes;
    using Menes.Hal;
    using Menes.Links;

    /// <summary>
    /// Maps <see cref="Content"/> to and from a <see cref="HalDocument"/>.
    /// </summary>
    public class ContentSummariesResponseMapper : IHalDocumentMapper<ContentSummaries, ContentSummariesResponseMappingContext>
    {
        private readonly IHalDocumentFactory halDocumentFactory;
        private readonly IOpenApiWebLinkResolver linkResolver;
        private readonly ContentSummaryResponseMapper contentSummaryMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentResponseMapper"/> class.
        /// </summary>
        /// <param name="halDocumentFactory">The factory with which to create <see cref="HalDocument"/> instances.</param>
        /// <param name="linkResolver">The link resolver to build the links collection.</param>
        /// <param name="contentSummaryMapper">The mapper for individual <see cref="ContentSummary"/> items.</param>
        public ContentSummariesResponseMapper(
            IHalDocumentFactory halDocumentFactory,
            IOpenApiWebLinkResolver linkResolver,
            ContentSummaryResponseMapper contentSummaryMapper)
        {
            this.halDocumentFactory = halDocumentFactory;
            this.linkResolver = linkResolver;
            this.contentSummaryMapper = contentSummaryMapper;
        }

        /// <inheritdoc/>
        public void ConfigureLinkMap(IOpenApiLinkOperationMap links)
        {
            links.MapByContentTypeAndRelationTypeAndOperationId<ContentSummaries>(Constants.LinkRelations.Self, ContentHistoryService.GetContentHistoryOperationId);
            links.MapByContentTypeAndRelationTypeAndOperationId<ContentSummaries>(Constants.LinkRelations.Next, ContentHistoryService.GetContentHistoryOperationId);
        }

        /// <inheritdoc/>
        public ValueTask<HalDocument> MapAsync(ContentSummaries resource, ContentSummariesResponseMappingContext context)
        {
            IEnumerable<Task<HalDocument>> mappedSummaries = resource.Summaries.Select(async x => await this.contentSummaryMapper.MapAsync(x, context));
            HalDocument response = this.halDocumentFactory.CreateHalDocumentFrom(new { Summaries = mappedSummaries.ToArray() });

            response.ResolveAndAddByOwnerAndRelationType(
                this.linkResolver,
                resource,
                Constants.LinkRelations.Self,
                (Constants.ParameterNames.TenantId, context.TenantId),
                (Constants.ParameterNames.Slug, context.Slug),
                (Constants.ParameterNames.Limit, context.Limit),
                (Constants.ParameterNames.ContinuationToken, context.ContinuationToken));

            if (!string.IsNullOrEmpty(resource.ContinuationToken))
            {
                response.ResolveAndAddByOwnerAndRelationType(
                    this.linkResolver,
                    resource,
                    Constants.LinkRelations.Next,
                    (Constants.ParameterNames.TenantId, context.TenantId),
                    (Constants.ParameterNames.Slug, context.Slug),
                    (Constants.ParameterNames.Limit, context.Limit),
                    (Constants.ParameterNames.ContinuationToken, resource.ContinuationToken));
            }

            return ValueTask.FromResult(response);
        }
    }
}
