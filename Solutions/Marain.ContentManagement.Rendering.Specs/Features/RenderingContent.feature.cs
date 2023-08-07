﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Marain.ContentManagement.Rendering.Specs.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("RenderingContent")]
    [NUnit.Framework.CategoryAttribute("setupContainer")]
    public partial class RenderingContentFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = new string[] {
                "setupContainer"};
        
#line 1 "RenderingContent.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "RenderingContent", "\tIn order to render content\r\n\tAs a developer\r\n\tI want to be able to render conten" +
                    "t using a configurable rendering pipeline", ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Render a content fragment to HTML")]
        public void RenderAContentFragmentToHTML()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Render a content fragment to HTML", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 7
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
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
                            "FirstContent",
                            "{newguid}",
                            "/",
                            "First tag; Second tag",
                            "/standard/content;/books/hobbit;/books/lotr",
                            "Bilbo Baggins",
                            "{newguid}",
                            "This is the title",
                            "A description of the content",
                            "fr-FR",
                            "This is the fragment of text 1"});
#line 8
 testRunner.Given("I have created content", ((string)(null)), table1, "Given ");
#line hidden
#line 11
 testRunner.When("I render the content called \'FirstContent\' to \'FirstRendered\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 12
 testRunner.Then("the output called \'FirstRendered\' should match \'This is the fragment of text 1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Render markdown to HTML")]
        public void RenderMarkdownToHTML()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Render markdown to HTML", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 14
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
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
                            "Markdown"});
                table2.AddRow(new string[] {
                            "FirstContent",
                            "{newguid}",
                            "/",
                            "First tag; Second tag",
                            "/standard/content;/books/hobbit;/books/lotr",
                            "Bilbo Baggins",
                            "{newguid}",
                            "This is the title",
                            "A description of the content",
                            "fr-FR",
                            "This is the *fragment* of text 1"});
#line 15
 testRunner.Given("I have created content", ((string)(null)), table2, "Given ");
#line hidden
#line 18
 testRunner.When("I render the content called \'FirstContent\' to \'FirstRendered\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 19
 testRunner.Then("the output called \'FirstRendered\' should match \'<p>This is the <em>fragment</em> " +
                        "of text 1</p>\\n\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Render a liquid template to HTML")]
        public void RenderALiquidTemplateToHTML()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Render a liquid template to HTML", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 21
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
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
                            "Liquid template"});
                table3.AddRow(new string[] {
                            "FirstContent",
                            "{newguid}",
                            "/",
                            "First tag; Second tag",
                            "/standard/content;/books/hobbit;/books/lotr",
                            "Bilbo Baggins",
                            "{newguid}",
                            "This is the title",
                            "A description of the content",
                            "fr-FR",
                            "<ul>{% for tag in content.tags %}<li>{{tag}}</li>{% endfor %}</ul>"});
#line 22
 testRunner.Given("I have created content", ((string)(null)), table3, "Given ");
#line hidden
#line 25
 testRunner.When("I render the content called \'FirstContent\' to \'FirstRendered\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 26
 testRunner.Then("the output called \'FirstRendered\' should match \'<ul><li>First tag</li><li>Second " +
                        "tag</li></ul>\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Render a liquid template with markdown to HTML")]
        public void RenderALiquidTemplateWithMarkdownToHTML()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Render a liquid template with markdown to HTML", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 28
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
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
                            "Liquid with markdown template"});
                table4.AddRow(new string[] {
                            "FirstContent",
                            "{newguid}",
                            "/",
                            "First tag; Second tag",
                            "/standard/content;/books/hobbit;/books/lotr",
                            "Bilbo Baggins",
                            "{newguid}",
                            "This is the title",
                            "A description of the content",
                            "fr-FR",
                            "{% for tag in content.tags %}1. {{tag}}{% endfor %}"});
#line 29
 testRunner.Given("I have created content", ((string)(null)), table4, "Given ");
#line hidden
#line 32
 testRunner.When("I render the content called \'FirstContent\' to \'FirstRendered\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 33
 testRunner.Then("the output called \'FirstRendered\' should match \'<ol>\\n<li>First tag1. Second tag<" +
                        "/li>\\n</ol>\\n\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Render published workflow content with the published state")]
        public void RenderPublishedWorkflowContentWithThePublishedState()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Render published workflow content with the published state", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 35
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
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
                table5.AddRow(new string[] {
                            "FirstContent",
                            "{newguid}",
                            "/",
                            "First tag; Second tag",
                            "/standard/content;/books/hobbit;/books/lotr",
                            "Bilbo Baggins",
                            "{newguid}",
                            "This is the title",
                            "A description of the content",
                            "fr-FR",
                            "This is the fragment of text 1"});
#line 36
 testRunner.Given("I have created content", ((string)(null)), table5, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
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
                            "ContentSlug"});
                table6.AddRow(new string[] {
                            "WorkflowContent",
                            "{newguid}",
                            "/wf",
                            "First tag; Second tag",
                            "/standard/content;/books/hobbit;/books/lotr",
                            "Bilbo Baggins",
                            "{newguid}",
                            "This is the title",
                            "A description of the content",
                            "fr-FR",
                            "/"});
#line 39
 testRunner.And("I have created content", ((string)(null)), table6, "And ");
#line hidden
#line 42
 testRunner.And("I publish the content called \'FirstContent\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 43
 testRunner.When("I render the content called \'WorkflowContent\' to \'FirstRendered\' with the context" +
                        " {\"PublicationStateToRender\": \"publishedOnly\"}", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 44
 testRunner.Then("the output called \'FirstRendered\' should match \'This is the fragment of text 1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Render draft workflow content with the published state")]
        public void RenderDraftWorkflowContentWithThePublishedState()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Render draft workflow content with the published state", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 46
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
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
                table7.AddRow(new string[] {
                            "FirstContent",
                            "{newguid}",
                            "/",
                            "First tag; Second tag",
                            "/standard/content;/books/hobbit;/books/lotr",
                            "Bilbo Baggins",
                            "{newguid}",
                            "This is the title",
                            "A description of the content",
                            "fr-FR",
                            "This is the fragment of text 1"});
#line 47
 testRunner.Given("I have created content", ((string)(null)), table7, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
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
                            "ContentSlug"});
                table8.AddRow(new string[] {
                            "WorkflowContent",
                            "{newguid}",
                            "/wf",
                            "First tag; Second tag",
                            "/standard/content;/books/hobbit;/books/lotr",
                            "Bilbo Baggins",
                            "{newguid}",
                            "This is the title",
                            "A description of the content",
                            "fr-FR",
                            "/"});
#line 50
 testRunner.And("I have created content", ((string)(null)), table8, "And ");
#line hidden
#line 53
 testRunner.And("I draft the content called \'FirstContent\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 54
 testRunner.When("I render the content called \'WorkflowContent\' to \'FirstRendered\' with the context" +
                        " {\"PublicationStateToRender\": \"publishedOnly\"}", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 55
 testRunner.Then("the output called \'FirstRendered\' should match \'\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Render draft workflow content with the published or draft state")]
        public void RenderDraftWorkflowContentWithThePublishedOrDraftState()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Render draft workflow content with the published or draft state", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 57
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
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
                table9.AddRow(new string[] {
                            "FirstContent",
                            "{newguid}",
                            "/",
                            "First tag; Second tag",
                            "/standard/content;/books/hobbit;/books/lotr",
                            "Bilbo Baggins",
                            "{newguid}",
                            "This is the title",
                            "A description of the content",
                            "fr-FR",
                            "This is the fragment of text 1"});
#line 58
 testRunner.Given("I have created content", ((string)(null)), table9, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
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
                            "ContentSlug"});
                table10.AddRow(new string[] {
                            "WorkflowContent",
                            "{newguid}",
                            "/wf",
                            "First tag; Second tag",
                            "/standard/content;/books/hobbit;/books/lotr",
                            "Bilbo Baggins",
                            "{newguid}",
                            "This is the title",
                            "A description of the content",
                            "fr-FR",
                            "/"});
#line 61
 testRunner.And("I have created content", ((string)(null)), table10, "And ");
#line hidden
#line 64
 testRunner.And("I draft the content called \'FirstContent\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 65
 testRunner.When("I render the content called \'WorkflowContent\' to \'FirstRendered\' with the context" +
                        " {\"PublicationStateToRender\": \"publishedOrDraft\"}", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 66
 testRunner.Then("the output called \'FirstRendered\' should match \'This is the fragment of text 1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
