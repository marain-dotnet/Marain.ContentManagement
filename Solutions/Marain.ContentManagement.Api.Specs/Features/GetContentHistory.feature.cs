// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:3.0.0.0
//      SpecFlow Generator Version:3.0.0.0
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
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Get content history")]
    public partial class GetContentHistoryFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "GetContentHistory.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Get content history", null, ProgrammingLanguage.CSharp, ((string[])(null)));
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
        public virtual void ScenarioTearDown()
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
        
        public virtual void FeatureBackground()
        {
#line 3
#line hidden
            TechTalk.SpecFlow.Table table14 = new TechTalk.SpecFlow.Table(new string[] {
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
            table14.AddRow(new string[] {
                        "Content0",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content1",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content2",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content3",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content4",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content5",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content6",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content7",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content8",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content9",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content10",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content11",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content12",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content13",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content14",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content15",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content16",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content17",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content18",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content19",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content20",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content21",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content22",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content23",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content24",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content25",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content26",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content27",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content28",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
            table14.AddRow(new string[] {
                        "Content29",
                        "{newguid}",
                        "slug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
#line 4
 testRunner.Given("content items have been created", ((string)(null)), table14, "Given ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Basic history retrieval without specifying a limit or continuation token")]
        public virtual void BasicHistoryRetrievalWithoutSpecifyingALimitOrContinuationToken()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Basic history retrieval without specifying a limit or continuation token", null, ((string[])(null)));
#line 37
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 3
this.FeatureBackground();
#line 38
 testRunner.When("I request content history for slug \'{Content0.Slug}\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 39
 testRunner.Then("the response should have a status of \'200\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 40
 testRunner.And("the response should contain 20 embedded content summaries", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
 testRunner.And("the ETag header should be set", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 42
 testRunner.And("the response should contain a \'self\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 43
 testRunner.And("the response should contain a \'next\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("History retrieval specifying a continuation token")]
        public virtual void HistoryRetrievalSpecifyingAContinuationToken()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("History retrieval specifying a continuation token", null, ((string[])(null)));
#line 45
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 3
this.FeatureBackground();
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("History retrieval specifying a limit")]
        public virtual void HistoryRetrievalSpecifyingALimit()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("History retrieval specifying a limit", null, ((string[])(null)));
#line 47
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 3
this.FeatureBackground();
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("History retrieval with fewer items than the limit doesn\'t include a continuation " +
            "token")]
        public virtual void HistoryRetrievalWithFewerItemsThanTheLimitDoesntIncludeAContinuationToken()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("History retrieval with fewer items than the limit doesn\'t include a continuation " +
                    "token", null, ((string[])(null)));
#line 49
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 3
this.FeatureBackground();
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
