﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.3.0.0
//      SpecFlow Generator Version:3.1.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Marain.ContentManagement.Specs.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.3.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("AbTesting")]
    [NUnit.Framework.CategoryAttribute("setupContainer")]
    [NUnit.Framework.CategoryAttribute("setupTenantedCosmosContainer")]
    public partial class AbTestingFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = new string[] {
                "setupContainer",
                "setupTenantedCosmosContainer"};
        
#line 1 "AbTesting.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "AbTesting", "\tIn order to manage AB test scenarios\r\n\tAs a developer\r\n\tI want to be able to agg" +
                    "regate content instances into an AB test group", ProgrammingLanguage.CSharp, new string[] {
                        "setupContainer",
                        "setupTenantedCosmosContainer"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Create draft content and aggregate it into an AB test group")]
        public virtual void CreateDraftContentAndAggregateItIntoAnABTestGroup()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create draft content and aggregate it into an AB test group", null, tagsOfScenario, argumentsOfScenario);
#line 8
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                            "Name",
                            "Id",
                            "Slug",
                            "Tags",
                            "CategoryPaths",
                            "Author.Name",
                            "Author.Id",
                            "Title",
                            "Description",
                            "Culture",
                            "Fragment"});
                table1.AddRow(new string[] {
                            "ExpectedFirst",
                            "{newguid}",
                            "abtest/",
                            "First tag; Second tag",
                            "/standard/content;/books/hobbit;/books/lotr",
                            "Bilbo Baggins",
                            "{newguid}",
                            "This is the title",
                            "A description of the content",
                            "fr-FR",
                            "This is the fragment of text 1"});
                table1.AddRow(new string[] {
                            "ExpectedSecond",
                            "{newguid}",
                            "abtest/",
                            "First tag; Second tag",
                            "/standard/content;/books/hobbit;/books/lotr",
                            "Bilbo Baggins",
                            "{newguid}",
                            "This is the title",
                            "A description of the content",
                            "fr-FR",
                            "This is the fragment of text 2"});
                table1.AddRow(new string[] {
                            "ExpectedThird",
                            "{newguid}",
                            "abtest/",
                            "First tag; Second tag",
                            "/standard/content;/books/hobbit;/books/lotr",
                            "Bilbo Baggins",
                            "{newguid}",
                            "This is the title",
                            "A description of the content",
                            "fr-FR",
                            "This is the fragment of text 3"});
                table1.AddRow(new string[] {
                            "ExpectedFourth",
                            "{newguid}",
                            "abtest/",
                            "First tag; Second tag",
                            "/standard/content;/books/hobbit;/books/lotr",
                            "Bilbo Baggins",
                            "{newguid}",
                            "This is the title",
                            "A description of the content",
                            "fr-FR",
                            "This is the fragment of text 4"});
#line 9
 testRunner.Given("I have created content with a content fragment", ((string)(null)), table1, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                            "ContentName",
                            "Key"});
                table2.AddRow(new string[] {
                            "ExpectedFirst",
                            "Group1"});
                table2.AddRow(new string[] {
                            "ExpectedSecond",
                            "Group2"});
                table2.AddRow(new string[] {
                            "ExpectedThird",
                            "Group3"});
                table2.AddRow(new string[] {
                            "ExpectedFourth",
                            "Group4"});
#line 15
 testRunner.And("I have created an AbTest set called \'ExpectedAbTest\' with the content", ((string)(null)), table2, "And ");
#line hidden
                TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                            "Name",
                            "Id",
                            "Slug",
                            "Tags",
                            "CategoryPaths",
                            "Author.Name",
                            "Author.Id",
                            "Title",
                            "Description",
                            "Culture",
                            "AbTestSetName"});
                table3.AddRow(new string[] {
                            "ExpectedAbTestContent",
                            "{newguid}",
                            "abtest/",
                            "First tag; Second tag",
                            "/standard/content;/books/hobbit;/books/lotr",
                            "Bilbo Baggins",
                            "{newguid}",
                            "This is the title",
                            "A description of the content",
                            "fr-FR",
                            "ExpectedAbTest"});
#line 21
 testRunner.And("I have created content with an AbTest set", ((string)(null)), table3, "And ");
#line hidden
#line 24
 testRunner.When("I get the content with Id \'{ExpectedAbTestContent.Id}\' and Slug \'{ExpectedAbTestC" +
                        "ontent.Slug}\' and call it \'Actual\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 25
 testRunner.And("I get the ABTest content called \'Group1\' from the content called \'Actual\' and cal" +
                        "l it \'ActualFirst\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 26
 testRunner.And("I get the ABTest content called \'Group2\' from the content called \'Actual\' and cal" +
                        "l it \'ActualSecond\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 27
 testRunner.And("I get the ABTest content called \'Group3\' from the content called \'Actual\' and cal" +
                        "l it \'ActualThird\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 28
 testRunner.And("I get the ABTest content called \'Group4\' from the content called \'Actual\' and cal" +
                        "l it \'ActualFourth\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 29
 testRunner.Then("the content called \'ExpectedFirst\' should match the content called \'ActualFirst\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 30
 testRunner.Then("the content called \'ExpectedSecond\' should match the content called \'ActualSecond" +
                        "\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 31
 testRunner.Then("the content called \'ExpectedThird\' should match the content called \'ActualThird\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 32
 testRunner.Then("the content called \'ExpectedFourth\' should match the content called \'ActualFourth" +
                        "\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 33
 testRunner.And("getting the ABTest content called \'Group5\' from the content called \'Actual\' shoul" +
                        "d throw a ContentNotFoundException.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
