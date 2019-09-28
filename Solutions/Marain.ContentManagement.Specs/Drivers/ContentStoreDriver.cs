namespace Marain.ContentManagement.Specs.Drivers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using Corvus.Extensions;
    using Marain.Cms;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Spec Driver for the content store.
    /// </summary>
    public class ContentStoreDriver
    {
        public static void Compare(Content expected, Content actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Slug, actual.Slug);
            Assert.AreEqual(expected.Author, actual.Author);
            Assert.AreEqual(string.Join(';', expected.CategoryPaths), string.Join(';', actual.CategoryPaths));
            Assert.AreEqual(string.Join(';', expected.Tags), string.Join(';', actual.Tags));
            Assert.AreEqual(expected.Culture, actual.Culture);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.OriginalSource, actual.OriginalSource);
            ComparePayloads(expected.ContentPayload, actual.ContentPayload);
        }

        public static void CompareACopy(Content expected, Content actual)
        {
            // The slug and ID are expected to differ.
            Assert.AreEqual(expected.Author, actual.Author);
            Assert.AreEqual(string.Join(';', expected.CategoryPaths), string.Join(';', actual.CategoryPaths));
            Assert.AreEqual(string.Join(';', expected.Tags), string.Join(';', actual.Tags));
            Assert.AreEqual(expected.Culture, actual.Culture);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.Title, actual.Title);
            ComparePayloads(expected.ContentPayload, actual.ContentPayload);
            Assert.AreEqual(new ContentSource(expected.Slug, expected.Id), actual.OriginalSource.Value);
        }

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

        private static void ComparePayloads(IContentPayload expected, IContentPayload actual)
        {
            Assert.AreEqual(expected.GetType(), actual.GetType());

            if (expected is ContentFragment expectedFragment)
            {
                CompareContentFragments(expectedFragment, actual as ContentFragment);
                return;
            }

            throw new InvalidOperationException($"Unexpected content payload type: {expected.GetType().Name}");
        }

        private static void CompareContentFragments(ContentFragment expected, ContentFragment actual)
        {
            Assert.AreEqual(expected.Fragment, actual.Fragment);
        }

        public static T GetObjectValue<T>(ScenarioContext scenarioContext, string property)
        {
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

        /// <summary>
        /// Sets a content fragment payload on a content instance.
        /// </summary>
        /// <param name="content">The content instance on which to set the payload.</param>
        /// <param name="row">The row from which to get the content fragment.</param>
        public static void SetContentFragment(Content content, TableRow row)
        {
            content.ContentPayload = new ContentFragment { Fragment = SubstituteContent(row["Fragment"]) };
        }

        /// <summary>
        /// Sets a content fragment payload on a content instance.
        /// </summary>
        /// <param name="scenarioContext">The scenario context.</param>
        /// <param name="content">The content instance on which to set the payload.</param>
        /// <param name="row">The row from which to get the content fragment.</param>
        public static void SetAbTestSet(ScenarioContext scenarioContext, Content content, TableRow row)
        {
            content.ContentPayload = scenarioContext.Get<AbTestSet>(row["AbTestSetName"]);
        }

        public static (Content, string) GetContentFor(TableRow row)
        {
            var content = new Content
            {
                Id = SubstituteContent(row["Id"]),
                Author = new CmsIdentity(SubstituteContent(row["Author.Id"]), SubstituteContent(row["Author.Name"])),
                Culture = CultureInfo.GetCultureInfo(SubstituteContent(row["Culture"])),
                Description = SubstituteContent(row["Description"]),
                Slug = SubstituteContent(row["Slug"]),
                Title = SubstituteContent(row["Title"]),
            };

            content.CategoryPaths.AddRange(SplitAndTrim(SubstituteContent(row["CategoryPaths"])));
            content.Tags.AddRange(SplitAndTrim(SubstituteContent(row["Tags"])));

            return (content, row["Name"]);
        }

        public static string SubstituteContent(string v)
        {
            if (v == "{null}")
            {
                return null;
            }

            return v.Replace("{newguid}", Guid.NewGuid().ToString());
        }

        private static IList<string> SplitAndTrim(string value)
        {
            return value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToList();
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
