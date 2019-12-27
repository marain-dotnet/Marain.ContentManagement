// <copyright file="ContentSummariesMapper.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Api.Services.Internal
{
    using System.Collections.Generic;
    using System.Linq;
    using Menes;
    using Menes.Hal;
    using Menes.Links;

    /// <summary>
    /// Maps <see cref="Content"/> to and from a <see cref="HalDocument"/>.
    /// </summary>
    public class ContentSummariesMapper : IHalDocumentMapper<ContentSummaries, ContentSummariesMappingContext>
    {
        private readonly IHalDocumentFactory halDocumentFactory;
        private readonly IOpenApiWebLinkResolver linkResolver;
        private readonly ContentSummaryMapper contentSummaryMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentMapper"/> class.
        /// </summary>
        /// <param name="halDocumentFactory">The factory with which to create <see cref="HalDocument"/> instances.</param>
        /// <param name="linkResolver">The link resolver to build the links collection.</param>
        /// <param name="contentSummaryMapper">The mapper for individual <see cref="ContentSummary"/> items.</param>
        public ContentSummariesMapper(
            IHalDocumentFactory halDocumentFactory,
            IOpenApiWebLinkResolver linkResolver,
            ContentSummaryMapper contentSummaryMapper)
        {
            this.halDocumentFactory = halDocumentFactory;
            this.linkResolver = linkResolver;
            this.contentSummaryMapper = contentSummaryMapper;
        }

        /// <inheritdoc/>
        public void ConfigureLinkMap(IOpenApiLinkOperationMap links)
        {
            links.Map<ContentSummaries>(Constants.LinkRelations.Self, ContentHistoryService.GetContentHistoryOperationId);
            links.Map<ContentSummaries>(Constants.LinkRelations.Next, ContentHistoryService.GetContentHistoryOperationId);
        }

        /// <inheritdoc/>
        public HalDocument Map(ContentSummaries resource, ContentSummariesMappingContext context)
        {
            IEnumerable<HalDocument> mappedSummaries = resource.Summaries.Select(x => this.contentSummaryMapper.Map(x, context));
            HalDocument response = this.halDocumentFactory.CreateHalDocumentFrom(new { Summaries = mappedSummaries.ToArray() });

            response.ResolveAndAdd(
                this.linkResolver,
                resource,
                Constants.LinkRelations.Self,
                (Constants.ParameterNames.TenantId, context.TenantId),
                (Constants.ParameterNames.Slug, context.Slug),
                (Constants.ParameterNames.Limit, context.Limit),
                (Constants.ParameterNames.ContinuationToken, context.ContinuationToken));

            response.ResolveAndAdd(
                this.linkResolver,
                resource,
                Constants.LinkRelations.Next,
                (Constants.ParameterNames.TenantId, context.TenantId),
                (Constants.ParameterNames.Slug, context.Slug),
                (Constants.ParameterNames.Limit, context.Limit),
                (Constants.ParameterNames.ContinuationToken, resource.ContinuationToken));

            return response;
        }
    }
}
