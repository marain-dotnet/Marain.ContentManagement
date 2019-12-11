// <copyright file="ContentWithStateMapper.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Api.Services.Internal
{
    using Menes;
    using Menes.Hal;
    using Menes.Links;

    /// <summary>
    /// Maps <see cref="ContentWithState"/> to and from a <see cref="HalDocument"/>.
    /// </summary>
    public class ContentWithStateMapper : IHalDocumentMapper<ContentWithState, IOpenApiContext>
    {
        private readonly IHalDocumentFactory halDocumentFactory;
        private readonly IOpenApiWebLinkResolver linkResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentWithState"/> class.
        /// </summary>
        /// <param name="halDocumentFactory">The factory with which to create <see cref="HalDocument"/> instances.</param>
        /// <param name="linkResolver">The link resolver to build the links collection.</param>
        public ContentWithStateMapper(IHalDocumentFactory halDocumentFactory, IOpenApiWebLinkResolver linkResolver)
        {
            this.halDocumentFactory = halDocumentFactory;
            this.linkResolver = linkResolver;
        }

        /// <inheritdoc/>
        public void ConfigureLinkMap(IOpenApiLinkOperationMap links)
        {
            links.Map<ContentWithState>("self", WorkflowContentService.GetWorkflowContentOperationId);
        }

        /// <inheritdoc/>
        public HalDocument Map(ContentWithState resource, IOpenApiContext context)
        {
            HalDocument response = this.halDocumentFactory.CreateHalDocumentFrom(resource);
            response.ResolveAndAdd(
                this.linkResolver,
                resource,
                "self",
                ("tenantId", context.CurrentTenantId),
                ("slug", resource.Content.Slug),
                ("workflowId", resource.WorkflowId));

            return response;
        }
    }
}
