// <copyright file="ContentDriver.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Drivers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using Corvus.Extensions;
    using Marain.Cms;
    using Marain.Cms.Api.Client;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented

    /// <summary>
    /// Spec Driver for the content store.
    /// </summary>
    public static class ContentDriver
    {
        public static void Compare(Cms.Content expected, Cms.Content actual)
        {
            CompareCommonElements(expected, actual);
            ComparePayloads(expected.ContentPayload, actual.ContentPayload);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Slug, actual.Slug);
            Assert.AreEqual(expected.OriginalSource, actual.OriginalSource);
        }

        public static void Compare(Cms.Content expected, Cms.Api.Client.Content actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Slug, actual.Slug);
            Assert.AreEqual(expected.Author.UserId, actual.Author.UserId);
            Assert.AreEqual(expected.Author.UserName, actual.Author.UserName);
            Assert.AreEqual(string.Join(';', expected.CategoryPaths), string.Join(';', actual.CategoryPaths));
            Assert.AreEqual(string.Join(';', expected.Tags), string.Join(';', actual.Tags));
            Assert.AreEqual(expected.Culture.Name, actual.Culture);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.Title, actual.Title);
        }

        public static void Compare(Cms.Content expected, ContentResponse actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Slug, actual.Slug);
            Assert.AreEqual(expected.Author.UserId, actual.Author.UserId);
            Assert.AreEqual(expected.Author.UserName, actual.Author.UserName);
            Assert.AreEqual(string.Join(';', expected.CategoryPaths), string.Join(';', actual.CategoryPaths));
            Assert.AreEqual(string.Join(';', expected.Tags), string.Join(';', actual.Tags));
            Assert.AreEqual(expected.Culture.Name, actual.Culture);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.Title, actual.Title);
        }

        public static void CompareACopy(Cms.Content expected, Cms.Content actual)
        {
            // The slug and ID are expected to differ.
            CompareCommonElements(expected, actual);
            ComparePayloads(expected.ContentPayload, actual.ContentPayload);
            Assert.AreEqual(new ContentReference(expected.Slug, expected.Id), actual.OriginalSource.Value);
        }

        public static void Compare(Cms.Content expected, Cms.ContentSummary actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Slug, actual.Slug);
            Assert.AreEqual(expected.Author, actual.Author);
            Assert.AreEqual(string.Join(';', expected.CategoryPaths), string.Join(';', actual.CategoryPaths));
            Assert.AreEqual(string.Join(';', expected.Tags), string.Join(';', actual.Tags));
            Assert.AreEqual(expected.Culture, actual.Culture);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.Title, actual.Title);
        }

        public static void Compare(Cms.Content expected, ContentSummaryResponse actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Slug, actual.Slug);
            Assert.AreEqual(expected.Author.UserId, actual.Author.UserId);
            Assert.AreEqual(expected.Author.UserName, actual.Author.UserName);
            Assert.AreEqual(string.Join(';', expected.CategoryPaths), string.Join(';', actual.CategoryPaths));
            Assert.AreEqual(string.Join(';', expected.Tags), string.Join(';', actual.Tags));
            Assert.AreEqual(expected.Culture.Name, actual.Culture);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.Title, actual.Title);
        }

        public static void Compare(Cms.ContentState expected, Cms.ContentWithState actual)
        {
            Assert.AreEqual(expected.WorkflowId, actual.WorkflowId);
            Assert.AreEqual(expected.StateName, actual.StateName);
        }

        public static void Compare(Cms.ContentState expected, ContentWithStateResponse actual)
        {
            Assert.AreEqual(expected.WorkflowId, actual.WorkflowId);
            Assert.AreEqual(expected.StateName, actual.StateName);
        }

        public static void Compare(Cms.ContentState expected, ContentStateResponse actual)
        {
            Assert.AreEqual(expected.WorkflowId, actual.WorkflowId);
            Assert.AreEqual(expected.StateName, actual.StateName);
            Assert.AreEqual(expected.Slug, actual.Slug);
            Assert.AreEqual(expected.ChangedBy.UserId, actual.ChangedBy.UserId);
            Assert.AreEqual(expected.ChangedBy.UserName, actual.ChangedBy.UserName);
        }

        public static T GetObjectValue<T>(ScenarioContext scenarioContext, string property)
        {
            property = SubstituteContent(property);

            if (property is null)
            {
                return default;
            }

            if (!(property.StartsWith('{') && property.EndsWith('}')))
            {
                // We assume it must be a string as it has no substitution
                return CastTo<T>.From(property);
            }

            int indexOfDot = property.IndexOf('.');
            string contextKey = property.Substring(1, indexOfDot - 1);
            string propertyName = property.Substring(indexOfDot + 1, property.Length - (indexOfDot + 2));

            object contextValue = scenarioContext.ContainsKey(contextKey) ? scenarioContext[contextKey] : null;
            if (contextValue is null)
            {
                return default;
            }

            PropertyInfo propertyInfo = contextValue.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null)
            {
                throw new InvalidOperationException($"The property called {propertyName} was not found on an instance of the type {contextValue.GetType().Name}");
            }

            if (!typeof(T).IsAssignableFrom(propertyInfo.PropertyType))
            {
                throw new InvalidOperationException($"The property called {propertyName} of type {propertyInfo.PropertyType.Name} is not an instance of the requested type {typeof(T).Name}");
            }

            return CastTo<T>.From(propertyInfo.GetValue(contextValue));
        }

        public static CreateContentRequest ContentAsCreateContentRequest(Cms.Content item)
        {
            return new CreateContentRequest
            {
                Author = new Cms.Api.Client.CmsIdentity
                {
                    UserId = item.Author.UserId,
                    UserName = item.Author.UserName,
                },
                CategoryPaths = new ObservableCollection<string>(item.CategoryPaths),
                Culture = item.Culture?.Name,
                Description = item.Description,
                Id = item.Id,
                Tags = new ObservableCollection<string>(item.Tags),
                Title = item.Title,
                ContentPayload = ContentPayloadAsCreateContentRequestPayload(item.ContentPayload),
            };
        }

        public static ContentPayload ContentPayloadAsCreateContentRequestPayload(IContentPayload payload)
        {
            if (payload == null)
            {
                return null;
            }

            return new ContentPayload
            {
                ContentType = payload?.ContentType,
            };
        }

        /// <summary>
        /// Matches content summaries to a list of expected content.
        /// </summary>
        /// <param name="expected">The list of expected content items.</param>
        /// <param name="summaries">The list of content summaries to compare against.</param>
        public static void MatchSummariesToContent(List<Cms.Content> expected, Cms.ContentSummaries summaries)
        {
            Assert.AreEqual(expected.Count, summaries.Summaries.Count);
            expected.ForEachAtIndex((expectedContent, i) => Compare(expectedContent, summaries.Summaries[i]));
        }

        /// <summary>
        /// Sets a content fragment payload on a content instance.
        /// </summary>
        /// <param name="content">The content instance on which to set the payload.</param>
        /// <param name="row">The row from which to get the content fragment.</param>
        public static void SetContentFragment(Cms.Content content, TableRow row)
        {
            content.ContentPayload = new ContentFragmentPayload { Fragment = SubstituteContent(row["Fragment"]) };
        }

        /// <summary>
        /// Sets a content workflow payload on a content instance.
        /// </summary>
        /// <param name="content">The content instance on which to set the payload.</param>
        /// <param name="row">The row from which to get the slug for the workflow content.</param>
        public static void SetContentWorkflow(Cms.Content content, TableRow row)
        {
            content.ContentPayload = new PublicationWorkflowContentPayload { Slug = SubstituteContent(row["ContentSlug"]) };
        }

        /// <summary>
        /// Sets a markdown payload on a content instance.
        /// </summary>
        /// <param name="content">The content instance on which to set the payload.</param>
        /// <param name="row">The row from which to get the markdown.</param>
        public static void SetContentMarkdown(Cms.Content content, TableRow row)
        {
            content.ContentPayload = new MarkdownPayload { Markdown = SubstituteContent(row["Markdown"]) };
        }

        /// <summary>
        /// Sets a Liquid payload on a content instance.
        /// </summary>
        /// <param name="content">The content instance on which to set the payload.</param>
        /// <param name="row">The row from which to get the markdown.</param>
        public static void SetContentLiquid(Cms.Content content, TableRow row)
        {
            content.ContentPayload = new LiquidPayload { Template = SubstituteContent(row["Liquid template"]) };
        }

        /// <summary>
        /// Sets a Liquid with markdown payload on a content instance.
        /// </summary>
        /// <param name="content">The content instance on which to set the payload.</param>
        /// <param name="row">The row from which to get the markdown.</param>
        public static void SetContentLiquidMarkdown(Cms.Content content, TableRow row)
        {
            content.ContentPayload = new LiquidWithMarkdownPayload { Template = SubstituteContent(row["Liquid with markdown template"]) };
        }

        /// <summary>
        /// Sets a content fragment payload on a content instance.
        /// </summary>
        /// <param name="scenarioContext">The scenario context.</param>
        /// <param name="content">The content instance on which to set the payload.</param>
        /// <param name="row">The row from which to get the content fragment.</param>
        public static void SetAbTestSet(ScenarioContext scenarioContext, Cms.Content content, TableRow row)
        {
            content.ContentPayload = scenarioContext.Get<AbTestSetPayload>(row["AbTestSetName"]);
        }

        public static (Cms.Content, string) GetContentFor(TableRow row)
        {
            var content = new Cms.Content
            {
                Id = SubstituteContent(row["Id"]),
                Author = new Cms.CmsIdentity(SubstituteContent(row["Author.Id"]), SubstituteContent(row["Author.Name"])),
                Culture = CultureInfo.GetCultureInfo(SubstituteContent(row["Culture"])),
                Description = SubstituteContent(row["Description"]),
                Slug = SubstituteContent(row["Slug"]),
                Title = SubstituteContent(row["Title"]),
            };

            content.CategoryPaths.AddRange(SplitAndTrim(SubstituteContent(row["CategoryPaths"])));
            content.Tags.AddRange(SplitAndTrim(SubstituteContent(row["Tags"])));

            return (content, row["Name"]);
        }

        public static (Cms.ContentState, string) GetContentStateFor(TableRow row)
        {
            var contentState = new Cms.ContentState
            {
                Id = SubstituteContent(row["Id"]),
                ChangedBy = new Cms.CmsIdentity(SubstituteContent(row["ChangedBy.Id"]), SubstituteContent(row["ChangedBy.Name"])),
                ContentId = SubstituteContent(row["ContentId"]),
                Slug = SubstituteContent(row["Slug"]),
                WorkflowId = SubstituteContent(row["WorkflowId"]),
                StateName = SubstituteContent(row["StateName"]),
            };

            return (contentState, row["Name"]);
        }

        public static string SubstituteContent(string v)
        {
            if (v == "{null}")
            {
                return null;
            }

            return v.Replace("{newguid}", Guid.NewGuid().ToString()).Replace(@"\n", "\n").Replace(@"\r", "\r").Replace(@"\t", "\t");
        }

        private static IList<string> SplitAndTrim(string value)
        {
            return value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToList();
        }

        private static void CompareCommonElements(Cms.Content expected, Cms.Content actual)
        {
            Assert.AreEqual(expected.Author, actual.Author);
            Assert.AreEqual(string.Join(';', expected.CategoryPaths), string.Join(';', actual.CategoryPaths));
            Assert.AreEqual(string.Join(';', expected.Tags), string.Join(';', actual.Tags));
            Assert.AreEqual(expected.Culture, actual.Culture);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.Title, actual.Title);
        }

        private static void ComparePayloads(IContentPayload expected, IContentPayload actual)
        {
            // If they are both null, no checking is required.
            if (expected == null && actual == null)
            {
                return;
            }

            // Otherwise they must both be not null
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.GetType(), actual.GetType());

            if (expected is ContentFragmentPayload expectedFragment)
            {
                CompareContentFragments(expectedFragment, actual as ContentFragmentPayload);
                return;
            }

            throw new InvalidOperationException($"Unexpected content payload type: {expected.GetType().Name}");
        }

        private static void CompareContentFragments(ContentFragmentPayload expected, ContentFragmentPayload actual)
        {
            Assert.AreEqual(expected.Fragment, actual.Fragment);
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented