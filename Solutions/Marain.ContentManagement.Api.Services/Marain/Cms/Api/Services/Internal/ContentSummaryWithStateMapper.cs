// <copyright file="ContentSummaryWithStateMapper.cs" company="Endjin Limited">
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
    public class ContentSummaryWithStateMapper : IHalDocumentMapper<ContentSummaryWithState, ResponseMappingContext>
    {
        private readonly IHalDocumentFactory halDocumentFactory;
        private readonly IOpenApiWebLinkResolver linkResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentSummaryWithStateMapper"/> class.
        /// </summary>
        /// <param name="halDocumentFactory">The factory with which to create <see cref="HalDocument"/> instances.</param>
        /// <param name="linkResolver">The link resolver to build the links collection.</param>
        public ContentSummaryWithStateMapper(IHalDocumentFactory halDocumentFactory, IOpenApiWebLinkResolver linkResolver)
        {
            this.halDocumentFactory = halDocumentFactory;
            this.linkResolver = linkResolver;
        }

        /// <inheritdoc/>
        public void ConfigureLinkMap(IOpenApiLinkOperationMap links)
        {
            links.Map<ContentSummaryWithState>("content", ContentService.GetContentOperationId);
        }

        /// <inheritdoc/>
        public HalDocument Map(ContentSummaryWithState resource, ResponseMappingContext context)
        {
            HalDocument response = this.halDocumentFactory.CreateHalDocumentFrom(resource);

            ////response.ResolveAndAdd(
            ////    this.linkResolver,
            ////    resource,
            ////    Constants.LinkRelations.Self,
            ////    (Constants.ParameterNames.TenantId, context.TenantId),
            ////    (Constants.ParameterNames.Slug, resource.Slug),
            ////    (Constants.ParameterNames.ContentId, resource.Id));

            response.ResolveAndAdd(
                this.linkResolver,
                resource,
                "content",
                (Constants.ParameterNames.TenantId, context.TenantId),
                (Constants.ParameterNames.Slug, resource.ContentSummary.Slug),
                (Constants.ParameterNames.ContentId, resource.ContentSummary.Id));

            return response;
        }
    }
}
