// <copyright file="ContentSummariesWithStateMapper.cs" company="Endjin Limited">
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
    public class ContentSummariesWithStateMapper : IHalDocumentMapper<ContentSummariesWithState, ContentSummariesWithStateMappingContext>
    {
        private readonly IHalDocumentFactory halDocumentFactory;
        private readonly IOpenApiWebLinkResolver linkResolver;
        private readonly ContentSummaryWithStateMapper contentSummaryWithStateMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentMapper"/> class.
        /// </summary>
        /// <param name="halDocumentFactory">The factory with which to create <see cref="HalDocument"/> instances.</param>
        /// <param name="linkResolver">The link resolver to build the links collection.</param>
        /// <param name="contentSummaryWithStateMapper">The mapper for individual <see cref="ContentSummaryWithState"/> items.</param>
        public ContentSummariesWithStateMapper(
            IHalDocumentFactory halDocumentFactory,
            IOpenApiWebLinkResolver linkResolver,
            ContentSummaryWithStateMapper contentSummaryWithStateMapper)
        {
            this.halDocumentFactory = halDocumentFactory;
            this.linkResolver = linkResolver;
            this.contentSummaryWithStateMapper = contentSummaryWithStateMapper;
        }

        /// <inheritdoc/>
        public void ConfigureLinkMap(IOpenApiLinkOperationMap links)
        {
            links.MapByContentTypeAndRelationTypeAndContextAndOperationId<ContentSummariesWithState>(Constants.LinkRelations.Self, WorkflowContentHistoryService.GetWorkflowStateHistoryOperationId, WorkflowContentHistoryService.GetWorkflowStateHistoryOperationId);
            links.MapByContentTypeAndRelationTypeAndContextAndOperationId<ContentSummariesWithState>(Constants.LinkRelations.Next, WorkflowContentHistoryService.GetWorkflowStateHistoryOperationId, WorkflowContentHistoryService.GetWorkflowStateHistoryOperationId);

            links.MapByContentTypeAndRelationTypeAndContextAndOperationId<ContentSummariesWithState>(Constants.LinkRelations.Self, WorkflowContentHistoryService.GetWorkflowHistoryOperationId, WorkflowContentHistoryService.GetWorkflowHistoryOperationId);
            links.MapByContentTypeAndRelationTypeAndContextAndOperationId<ContentSummariesWithState>(Constants.LinkRelations.Next, WorkflowContentHistoryService.GetWorkflowHistoryOperationId, WorkflowContentHistoryService.GetWorkflowHistoryOperationId);
        }

        /// <inheritdoc/>
        public HalDocument Map(ContentSummariesWithState resource, ContentSummariesWithStateMappingContext context)
        {
            IEnumerable<HalDocument> mappedSummaries = resource.Summaries.Select(x => this.contentSummaryWithStateMapper.Map(x, context));
            HalDocument response = this.halDocumentFactory.CreateHalDocumentFrom(new { Summaries = mappedSummaries.ToArray() });

            response.ResolveAndAddByOwnerAndRelationTypeAndContext(
                this.linkResolver,
                resource,
                Constants.LinkRelations.Self,
                context.TargetOperationId,
                (Constants.ParameterNames.TenantId, context.TenantId),
                (Constants.ParameterNames.Slug, context.Slug),
                (Constants.ParameterNames.WorkflowId, context.WorkflowId),
                (Constants.ParameterNames.StateName, context.StateName),
                (Constants.ParameterNames.Limit, context.Limit),
                (Constants.ParameterNames.ContinuationToken, context.ContinuationToken));

            if (!string.IsNullOrEmpty(resource.ContinuationToken))
            {
                response.ResolveAndAddByOwnerAndRelationTypeAndContext(
                    this.linkResolver,
                    resource,
                    Constants.LinkRelations.Next,
                    context.TargetOperationId,
                    (Constants.ParameterNames.TenantId, context.TenantId),
                    (Constants.ParameterNames.Slug, context.Slug),
                    (Constants.ParameterNames.WorkflowId, context.WorkflowId),
                    (Constants.ParameterNames.StateName, context.StateName),
                    (Constants.ParameterNames.Limit, context.Limit),
                    (Constants.ParameterNames.ContinuationToken, resource.ContinuationToken));
            }

            return response;
        }
    }
}
