// <copyright file="ContentStoreDriver.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Drivers
{
    using System.Collections.Generic;
    using System.Linq;
    using Corvus.Extensions;
    using Marain.Cms;
    using NUnit.Framework;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented

    /// <summary>
    /// Spec Driver for the content store.
    /// </summary>
    public static class ContentStoreDriver
    {
        public static void Compare(Content expected, ContentSummary actual)
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

        public static void MatchStatesAndSummariesToContent(
            List<Content> expectedContents,
            List<string> expectedStates,
            List<ContentSummary> actualSummaries,
            List<ContentState> actualStates)
        {
            Assert.AreEqual(expectedStates.Count, actualStates.Count);

            expectedStates.ForEachAtIndex((expectedState, i) =>
            {
                Assert.AreEqual(expectedState, actualStates[i].StateName);

                Content expectedContent = expectedContents[i];
                ContentSummary actualContent = actualSummaries.First(x => x.Id == actualStates[i].ContentId && x.Slug == actualStates[i].Slug);

                Compare(expectedContent, actualContent);
            });
        }
    }
}
