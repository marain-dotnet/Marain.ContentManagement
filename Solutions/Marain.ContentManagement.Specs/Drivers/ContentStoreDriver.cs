namespace Marain.ContentManagement.Specs.Drivers
{
    using System.Collections.Generic;
    using Corvus.Extensions;
    using Marain.Cms;
    using NUnit.Framework;

    /// <summary>
    /// Spec Driver for the content store.
    /// </summary>
    public class ContentStoreDriver
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

        public static void Compare(Content expectedContent, string expectedStateName, ContentSummaryWithState actual)
        {
            Assert.AreEqual(expectedContent.Id, actual.ContentSummary.Id);
            Assert.AreEqual(expectedContent.Slug, actual.ContentSummary.Slug);
            Assert.AreEqual(expectedContent.Author, actual.ContentSummary.Author);
            Assert.AreEqual(string.Join(';', expectedContent.CategoryPaths), string.Join(';', actual.ContentSummary.CategoryPaths));
            Assert.AreEqual(string.Join(';', expectedContent.Tags), string.Join(';', actual.ContentSummary.Tags));
            Assert.AreEqual(expectedContent.Culture, actual.ContentSummary.Culture);
            Assert.AreEqual(expectedContent.Description, actual.ContentSummary.Description);
            Assert.AreEqual(expectedContent.Title, actual.ContentSummary.Title);
            Assert.AreEqual(expectedStateName, actual.StateName);
        }

        /// <summary>
        /// Matches content summaries to a list of expected content.
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="summaries"></param>
        public static void MatchSummariesToContent(List<Content> expected, ContentSummaries summaries)
        {
            Assert.AreEqual(expected.Count, summaries.Summaries.Count);
            expected.ForEachAtIndex((expectedContent, i) =>
            {
                Compare(expectedContent, summaries.Summaries[i]);
            });
        }

        internal static void MatchSummariesToContent(List<Content> expectedContent, List<string> expectedStates, ContentSummariesWithState summaries)
        {
            Assert.AreEqual(expectedContent.Count, summaries.Summaries.Count);
            expectedContent.ForEachAtIndex((ec, i) =>
            {
                Compare(ec, expectedStates[i], summaries.Summaries[i]);
            });
        }
    }
}
