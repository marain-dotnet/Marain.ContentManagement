using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marain.Cms;
using Marain.ContentManagement.Specs.Bindings;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Marain.ContentManagement.Specs.Steps
{
    [Binding]
    public class ContentSetupBindings
    {
        private readonly ScenarioContext scenarioContext;

        public ContentSetupBindings(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Given("there is no content available")]
        public void GivenThereIsNoContentAvailable()
        {
        }

        [Given("a content item has been created")]
        public async Task GivenAContentItemHasBeenCreated(Table table)
        {
            IEnumerable<Content> data = table.CreateSet(
                row => new Content
                {
                    Author = new CmsIdentity(row["Author Id"], row["Author UserName"])
                });

            ITenantedContentStoreFactory contentStoreFactory = this.scenarioContext.ServiceProvider().GetRequiredService<ITenantedContentStoreFactory>();
            IContentStore store = await contentStoreFactory.GetContentStoreForTenantAsync(this.scenarioContext.CurrentTenantId()).ConfigureAwait(false);

            Content[] storedContentItems = await Task.WhenAll(data.Select(data => store.StoreContentAsync(data))).ConfigureAwait(false);

            this.scenarioContext.Set(storedContentItems);
        }
    }
}
