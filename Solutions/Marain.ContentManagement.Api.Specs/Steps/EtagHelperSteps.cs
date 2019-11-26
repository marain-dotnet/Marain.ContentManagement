// <copyright file="EtagHelperSteps.cs" company="Endjin Limited">
// Copyright (c) Endjin Limited. All rights reserved.
// </copyright>

namespace Marain.ContentManagement.Specs.Steps
{
    using System.Collections.Generic;
    using System.Linq;
    using Marain.Cms.Api.Services;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented

    [Binding]
    public class EtagHelperSteps
    {
        private const string GeneratedEtagsKey = "GeneratedEtags";

        private readonly ScenarioContext scenarioContext;

        public EtagHelperSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Given("I build an etag from discriminator '(.*)' and etag '(.*)'")]
        public void GivenIBuildAnEtagFromDiscriminatorAndEtag(string discriminator, string etag)
        {
            string result = EtagHelper.BuildEtag(discriminator, etag);
            this.StoreGeneratedEtag(result);
        }

        [Given("I build an etag from discriminator '(.*)' and a list of etags")]
        public void GivenIBuildAnEtagFromDiscriminatorAndAListOfEtags(string discriminator, Table table)
        {
            string[] etags = table.Rows.Select(x => x[0]).ToArray();
            string result = EtagHelper.BuildEtag(discriminator, etags);
            this.StoreGeneratedEtag(result);
        }

        [Then("the generated etags are the same")]
        public void ThenTheGeneratedEtagsAreTheSame()
        {
            List<string> etags = this.GetGeneratedEtagsList();
            IEnumerable<string> uniqueEtags = etags.Distinct();

            Assert.AreEqual(1, uniqueEtags.Count());
        }

        [Then("the generated etags are distinct")]
        public void ThenTheGeneratedEtagsAreDistinct()
        {
            List<string> etags = this.GetGeneratedEtagsList();
            IEnumerable<string> uniqueEtags = etags.Distinct();

            Assert.AreEqual(etags.Count, uniqueEtags.Count());
        }

        private List<string> GetGeneratedEtagsList()
        {
            if (!this.scenarioContext.TryGetValue(GeneratedEtagsKey, out List<string> etags))
            {
                etags = new List<string>();
                this.scenarioContext.Set(etags, GeneratedEtagsKey);
            }

            return etags;
        }

        private void StoreGeneratedEtag(string etag)
        {
            List<string> etags = this.GetGeneratedEtagsList();
            etags.Add(etag);
        }
    }
}
