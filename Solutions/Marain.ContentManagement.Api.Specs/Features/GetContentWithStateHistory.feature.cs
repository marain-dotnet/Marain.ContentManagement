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
    [NUnit.Framework.DescriptionAttribute("GetContentWithStateHistory")]
    public partial class GetContentWithStateHistoryFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "GetContentWithStateHistory.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "GetContentWithStateHistory", null, ProgrammingLanguage.CSharp, ((string[])(null)));
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
            TechTalk.SpecFlow.Table table28 = new TechTalk.SpecFlow.Table(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
            table28.AddRow(new string[] {
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
 testRunner.Given("content items have been created", ((string)(null)), table28, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table29 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Id",
                        "ContentId",
                        "Slug",
                        "WorkflowId",
                        "StateName",
                        "ChangedBy.Name",
                        "ChangedBy.Id"});
            table29.AddRow(new string[] {
                        "Content0-State",
                        "{newguid}",
                        "{Content0.Id}",
                        "slug1",
                        "workflow1Id",
                        "draft",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content1-State",
                        "{newguid}",
                        "{Content1.Id}",
                        "slug1",
                        "workflow1Id",
                        "draft",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content2-State",
                        "{newguid}",
                        "{Content2.Id}",
                        "slug1",
                        "workflow1Id",
                        "draft",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content3-State",
                        "{newguid}",
                        "{Content3.Id}",
                        "slug1",
                        "workflow1Id",
                        "draft",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content4-State",
                        "{newguid}",
                        "{Content4.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content5-State",
                        "{newguid}",
                        "{Content5.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content6-State",
                        "{newguid}",
                        "{Content6.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content7-State",
                        "{newguid}",
                        "{Content7.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content8-State",
                        "{newguid}",
                        "{Content8.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content9-State",
                        "{newguid}",
                        "{Content9.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content10-State",
                        "{newguid}",
                        "{Content10.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content11-State",
                        "{newguid}",
                        "{Content11.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content12-State",
                        "{newguid}",
                        "{Content12.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content13-State",
                        "{newguid}",
                        "{Content13.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content14-State",
                        "{newguid}",
                        "{Content14.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content15-State",
                        "{newguid}",
                        "{Content15.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content16-State",
                        "{newguid}",
                        "{Content16.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content17-State",
                        "{newguid}",
                        "{Content17.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content18-State",
                        "{newguid}",
                        "{Content18.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content19-State",
                        "{newguid}",
                        "{Content19.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content20-State",
                        "{newguid}",
                        "{Content20.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content21-State",
                        "{newguid}",
                        "{Content21.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content22-State",
                        "{newguid}",
                        "{Content22.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content23-State",
                        "{newguid}",
                        "{Content23.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content24-State",
                        "{newguid}",
                        "{Content24.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content25-State",
                        "{newguid}",
                        "{Content25.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content26-State",
                        "{newguid}",
                        "{Content26.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content27-State",
                        "{newguid}",
                        "{Content27.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content28-State",
                        "{newguid}",
                        "{Content28.Id}",
                        "slug1",
                        "workflow1Id",
                        "published",
                        "Frodo Baggins",
                        "{newguid}"});
            table29.AddRow(new string[] {
                        "Content29-State",
                        "{newguid}",
                        "{Content29.Id}",
                        "slug1",
                        "workflow1Id",
                        "archived",
                        "Frodo Baggins",
                        "{newguid}"});
#line 36
 testRunner.And("workflow states have been set for the content items", ((string)(null)), table29, "And ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Basic history with state retrieval by state name, without specifying a limit or c" +
            "ontinuation token")]
        public virtual void BasicHistoryWithStateRetrievalByStateNameWithoutSpecifyingALimitOrContinuationToken()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Basic history with state retrieval by state name, without specifying a limit or c" +
                    "ontinuation token", null, ((string[])(null)));
#line 69
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 3
this.FeatureBackground();
#line 70
 testRunner.When("I request content history with state for slug \'{Content0.Slug}\', workflow Id \'wor" +
                    "kflow1Id\' and state name \'published\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 71
 testRunner.Then("the response should have a status of \'200\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 72
 testRunner.And("the response should contain 20 embedded content summaries with state", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 73
 testRunner.And("the response should contain a \'self\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 74
 testRunner.And("the response should contain a \'next\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("History retrieval specifying a continuation token")]
        public virtual void HistoryRetrievalSpecifyingAContinuationToken()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("History retrieval specifying a continuation token", null, ((string[])(null)));
#line 76
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 3
this.FeatureBackground();
#line 77
 testRunner.Given("I have requested content history with state for slug \'{Content0.Slug}\', workflow " +
                    "Id \'workflow1Id\' and state name \'published\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 78
 testRunner.When("I request content history with state for slug \'{Content0.Slug}\', workflow Id \'wor" +
                    "kflow1Id\' and state name \'published\' with the contination token from the previou" +
                    "s response", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 79
 testRunner.Then("the response should have a status of \'200\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 80
 testRunner.And("the response should contain another 5 embedded content summaries with state", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 81
 testRunner.And("the response should contain a \'self\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("History retrieval specifying a limit")]
        public virtual void HistoryRetrievalSpecifyingALimit()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("History retrieval specifying a limit", null, ((string[])(null)));
#line 83
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 3
this.FeatureBackground();
#line 84
 testRunner.When("I request content history with state for slug \'{Content0.Slug}\', workflow Id \'wor" +
                    "kflow1Id\' and state name \'published\' with a limit of 5 items", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 85
 testRunner.Then("the response should have a status of \'200\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 86
 testRunner.And("the response should contain 5 embedded content summaries with state", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 87
 testRunner.And("the response should contain a \'self\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 88
 testRunner.And("the response should contain a \'next\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
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
#line 90
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 3
this.FeatureBackground();
#line 91
 testRunner.When("I request content history with state for slug \'{Content0.Slug}\', workflow Id \'wor" +
                    "kflow1Id\' and state name \'published\' with a limit of 50 items", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 92
 testRunner.Then("the response should have a status of \'200\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 93
 testRunner.And("the response should contain 25 embedded content summaries with state", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 94
 testRunner.And("the response should contain a \'self\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 95
 testRunner.And("the response should not contain a \'next\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
