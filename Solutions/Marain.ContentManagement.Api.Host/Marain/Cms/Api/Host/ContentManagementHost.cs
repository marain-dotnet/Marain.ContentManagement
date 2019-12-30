// <copyright file="ContentManagementHost.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms.Api.Host
{
    using System.Threading.Tasks;
    using Menes;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// The host for Content Management services.
    /// </summary>
    public class ContentManagementHost
    {
        private readonly IOpenApiHost<HttpRequest, IActionResult> host;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentManagementHost"/> class.
        /// </summary>
        /// <param name="host">The OpenApi host.</param>
        public ContentManagementHost(IOpenApiHost<HttpRequest, IActionResult> host)
        {
            this.host = host;
        }

        /// <summary>
        /// Azure Functions entry point.
        /// </summary>
        /// <param name="req">The <see cref="HttpRequest"/>.</param>
        /// <param name="executionContext">The context for the function execution.</param>
        /// <returns>An action result which comes from executing the function.</returns>
        [FunctionName("ContentManagementHost-OpenApiHostRoot")]
        public Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "put", "delete", Route = "{*path}")]HttpRequest req,
            ExecutionContext executionContext)
        {
            return this.host.HandleRequestAsync(req.Path, req.Method, req, new { ExecutionContext = executionContext });
        }
    }
}
