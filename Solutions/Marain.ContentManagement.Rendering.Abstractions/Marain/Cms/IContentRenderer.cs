// <copyright file="IContentRenderer.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System.IO;
    using System.Threading.Tasks;
    using Corvus.Extensions.Json;

    /// <summary>
    /// Implemented by types that render an <see cref="IContentPayload"/>.
    /// </summary>
    public interface IContentRenderer
    {
        /// <summary>
        /// Render the provided content to the output stream.
        /// </summary>
        /// <param name="output">The output text writer to which to render the content.</param>
        /// <param name="parentContent">The root content element which contains this payload.</param>
        /// <param name="currentPayload">The current payload to be rendered.</param>
        /// <param name="context">The context property bag.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public Task RenderAsync(TextWriter output, Content parentContent, IContentPayload currentPayload, PropertyBag context);
    }
}
