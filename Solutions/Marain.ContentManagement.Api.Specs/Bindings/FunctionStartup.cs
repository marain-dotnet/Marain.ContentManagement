namespace Marain.ContentManagement.Specs.Bindings
{
    using Menes;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Azure.WebJobs.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class FunctionStartup<TWebJobStartup>
        where TWebJobStartup : IWebJobsStartup, new()
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();

            var builder = new WebJobBuilder(services);
            var targetStartup = new TWebJobStartup();
            targetStartup.Configure(builder);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment _)
        {
            var openApiRouteHandler = new RouteHandler(
                async context =>
                {
                    IOpenApiHost<HttpRequest, IActionResult> handler = context.RequestServices.GetRequiredService<IOpenApiHost<HttpRequest, IActionResult>>();
                    IActionResult result = await handler.HandleRequestAsync(context.Request, context).ConfigureAwait(false);
                    var actionContext = new ActionContext(context, context.GetRouteData(), new ActionDescriptor());
                    await result.ExecuteResultAsync(actionContext).ConfigureAwait(false);
                });

            var routeBuilder = new RouteBuilder(app, openApiRouteHandler);

            routeBuilder.MapRoute("CatchAll", "{*path}");
            IRouter router = routeBuilder.Build();
            app.UseRouter(router);
        }
    }
}
