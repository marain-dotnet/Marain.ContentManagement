// <copyright file="ContentState.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.Cms
{
    using System;
    using Corvus.Extensions;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Represents a piece of content in a particular state.
    /// </summary>
    public class ContentState
    {
        /// <summary>
        /// The registered content type of the content for the <see cref="Corvus.ContentHandling.ContentFactory"/> pattern.
        /// </summary>
        public const string RegisteredContentType = "application/vnd.marain.content.contentstate";

        private string slug;

        /// <summary>
        /// Gets or sets the unique ID of the state instance.
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the name of the state.
        /// </summary>
        public string StateName { get; set; }

        /// <summary>
        /// Gets or sets the ID of the content workflow.
        /// </summary>
        public string WorkflowId { get; set; }

        /// <summary>
        /// Gets or sets the slug for the content.
        /// </summary>
        public string Slug
        {
            get
            {
                return this.slug ?? string.Empty;
            }

            set
            {
                this.slug = new Slug(value).ToString();
            }
        }

        /// <summary>
        /// Gets or sets the identity that changed the state.
        /// </summary>
        public CmsIdentity ChangedBy { get; set; }

        /// <summary>
        /// Gets the partition key for the content.
        /// </summary>
        public string PartitionKey => PartitionKeyHelper.GetPartitionKeyFromSlug(this.Slug);

        /// <summary>
        /// Gets or sets the <see cref="Content.Id"/> to which the state applies.
        /// </summary>
        public string ContentId { get; set; }

        /// <summary>
        /// Gets or sets the timestamp at which the content went into the state.
        /// </summary>
        [JsonProperty("_ts")]
        public long UnixTimestamp { get; set; }

        /// <summary>
        /// Gets the timestamp at which the content went into the state.
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset Timestamp => DateTimeOffset.FromUnixTimeSeconds(this.UnixTimestamp);

        /// <summary>
        /// Gets the content type of the state for the <see cref="Corvus.ContentHandling.ContentFactory"/> pattern.
        /// </summary>
        public string ContentType => RegisteredContentType;
    }
}
