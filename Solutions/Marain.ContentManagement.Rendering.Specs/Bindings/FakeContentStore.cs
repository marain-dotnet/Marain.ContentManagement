// <copyright file="FakeContentStore.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Bindings
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Marain.Cms;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.

    internal class FakeContentStore : IContentStore
    {
        private readonly Dictionary<string, ContentState> contentStateBySlug = new Dictionary<string, ContentState>();

        private readonly Dictionary<string, Content> contentBySlug = new Dictionary<string, Content>();

        public void SetContentState(Content content, string stateName)
        {
            this.contentBySlug[content.Slug] = content;
            this.contentStateBySlug[content.Slug] = new ContentState
            {
                ContentId = content.Id,
                Slug = content.Slug,
                StateName = stateName,
                UnixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                WorkflowId = WellKnownWorkflowId.ContentPublication,
            };
        }

        public Task<ContentState> GetContentStateForWorkflowAsync(string slug, string workflowId)
        {
            return Task.FromResult(this.contentStateBySlug[slug]);
        }

        public Task<Content> GetContentAsync(string contentId, string slug)
        {
            return Task.FromResult(this.contentBySlug[slug]);
        }

        public Task<ContentSummary> GetContentSummaryAsync(string contentId, string slug)
        {
            throw new NotImplementedException();
        }

        public Task<ContentSummaries> GetContentSummariesAsync(string slug, int limit = 20, string continuationToken = null)
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

        public Task<ContentStates> GetContentStatesForWorkflowAsync(string slug, string workflowId, string stateName = null, int limit = 20, string continuationToken = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<ContentSummary>> GetContentSummariesForStatesAsync(IList<ContentState> states)
        {
            throw new NotImplementedException();
        }
    }
}

#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented