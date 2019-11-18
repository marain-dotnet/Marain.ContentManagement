namespace Marain.ContentManagement.Specs.Bindings
{
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.DependencyInjection;

    public class WebJobBuilder : IWebJobsBuilder
    {
        public WebJobBuilder(IServiceCollection collection)
        {
            this.Services = collection;
        }

        public IServiceCollection Services { get; }
    }
}
