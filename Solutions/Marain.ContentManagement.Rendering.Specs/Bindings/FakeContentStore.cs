namespace Marain.ContentManagement.Specs.Bindings
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Marain.Cms;

#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
    internal class FakeContentStore : IContentStore
    {
        private readonly Dictionary<string, ContentWithState> contentBySlug = new Dictionary<string, ContentWithState>();

        public void SetContentState(Content content, string stateName)
        {
            this.contentBySlug[content.Slug] =
                new ContentWithState(content, stateName, DateTimeOffset.UtcNow, WellKnownWorkflowId.ContentPublication);
        }

        public Task<Content> GetContentAsync(string contentId, string slug)
        {
            throw new NotImplementedException();
        }

        public Task<ContentWithState> GetContentForWorkflowAsync(string slug, string workflowId)
        {
            return Task.FromResult(this.contentBySlug[slug]);
        }

        public Task<ContentSummaries> GetContentSummariesAsync(string slug, int limit = 20, string continuationToken = null)
        {
            throw new NotImplementedException();
        }

        public Task<ContentSummariesWithState> GetContentSummariesForWorkflowAsync(string slug, string workflowId, string stateName = null, int limit = 20, string continuationToken = null)
        {
            throw new NotImplementedException();
        }

        public Task<ContentSummary> GetContentSummaryAsync(string contentId, string slug)
        {
            throw new NotImplementedException();
        }

        public Task<ContentState> GetContentWorkflowStateAsync(string slug, string workflowId)
        {
            throw new NotImplementedException();
        }

        public Task<ContentState> SetContentWorkflowStateAsync(string slug, string contentId, string workflowId, string stateName, CmsIdentity stateChangedBy)
        {
            throw new NotImplementedException();
        }

        public Task<Content> StoreContentAsync(Content content)
        {
            throw new NotImplementedException();
        }
    }
}
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.