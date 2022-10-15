// <copyright file="ContentStatesResponseMapper.cs" company="Endjin Limited">
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
    public class ContentStatesResponseMapper : IHalDocumentMapper<ContentStates, ContentStatesResponseMappingContext>
    {
        private const string MappingContextWithState = "ContentStates-WithStateName";

        private const string MappingContextWithoutState = "ContentStates-WithoutStateName";

        private readonly IHalDocumentFactory halDocumentFactory;
        private readonly IOpenApiWebLinkResolver linkResolver;
        private readonly ContentStateResponseMapper contentStateMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentResponseMapper"/> class.
        /// </summary>
        /// <param name="halDocumentFactory">The factory with which to create <see cref="HalDocument"/> instances.</param>
        /// <param name="linkResolver">The link resolver to build the links collection.</param>
        /// <param name="contentStateMapper">The mapper for individual <see cref="ContentState"/> items.</param>
        public ContentStatesResponseMapper(
            IHalDocumentFactory halDocumentFactory,
            IOpenApiWebLinkResolver linkResolver,
            ContentStateResponseMapper contentStateMapper)
        {
            this.halDocumentFactory = halDocumentFactory;
            this.linkResolver = linkResolver;
            this.contentStateMapper = contentStateMapper;
        }

        /// <inheritdoc/>
        public void ConfigureLinkMap(IOpenApiLinkOperationMap links)
        {
            links.MapByContentTypeAndRelationTypeAndContextAndOperationId<ContentStates>(
                Constants.LinkRelations.Self,
                MappingContextWithState,
                WorkflowHistoryService.GetWorkflowStateHistoryOperationId);

            links.MapByContentTypeAndRelationTypeAndContextAndOperationId<ContentStates>(
                Constants.LinkRelations.Next,
                MappingContextWithState,
                WorkflowHistoryService.GetWorkflowStateHistoryOperationId);

            links.MapByContentTypeAndRelationTypeAndContextAndOperationId<ContentStates>(
                Constants.LinkRelations.Self,
                MappingContextWithoutState,
                WorkflowHistoryService.GetWorkflowHistoryOperationId);

            links.MapByContentTypeAndRelationTypeAndContextAndOperationId<ContentStates>(
                Constants.LinkRelations.Next,
                MappingContextWithoutState,
                WorkflowHistoryService.GetWorkflowHistoryOperationId);
        }

        /// <inheritdoc/>
        public async ValueTask<HalDocument> MapAsync(ContentStates resource, ContentStatesResponseMappingContext context)
        {
        List<HalDocument> mappedSummaries = new();

        foreach (ContentState state in resource.States)
        {
            var stateContext = new ContentStateResponseMappingContext
            {
                TenantId = context.TenantId,
                ContentSummary = context.ContentSummaries?.FirstOrDefault(summary => summary.Id == state.ContentId && summary.Slug == state.Slug),
            };

            HalDocument result = await this.contentStateMapper.MapAsync(state, stateContext).ConfigureAwait(false);
            mappedSummaries.Add(result);
        }

        HalDocument response = this.halDocumentFactory.CreateHalDocumentFrom(new { States = mappedSummaries.ToArray() });

        string linkMappingContext = string.IsNullOrEmpty(context.StateName)
            ? MappingContextWithoutState
            : MappingContextWithState;

        response.ResolveAndAddByOwnerAndRelationTypeAndContext(
            this.linkResolver,
            resource,
            Constants.LinkRelations.Self,
            linkMappingContext,
            (Constants.ParameterNames.TenantId, context.TenantId),
            (Constants.ParameterNames.WorkflowId, context.WorkflowId),
            (Constants.ParameterNames.StateName, context.StateName),
            (Constants.ParameterNames.Slug, context.Slug),
            (Constants.ParameterNames.Limit, context.Limit),
            (Constants.ParameterNames.ContinuationToken, context.ContinuationToken),
            (Constants.ParameterNames.Embed, context.Embed));

        if (!string.IsNullOrEmpty(resource.ContinuationToken))
        {
            response.ResolveAndAddByOwnerAndRelationTypeAndContext(
                this.linkResolver,
                resource,
                Constants.LinkRelations.Next,
                linkMappingContext,
                (Constants.ParameterNames.TenantId, context.TenantId),
                (Constants.ParameterNames.WorkflowId, context.WorkflowId),
                (Constants.ParameterNames.StateName, context.StateName),
                (Constants.ParameterNames.Slug, context.Slug),
                (Constants.ParameterNames.Limit, context.Limit),
                (Constants.ParameterNames.ContinuationToken, resource.ContinuationToken),
                (Constants.ParameterNames.Embed, context.Embed));
        }

        return response;
        }
    }
}
