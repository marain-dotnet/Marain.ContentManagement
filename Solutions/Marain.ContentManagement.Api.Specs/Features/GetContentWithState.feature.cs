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
    [NUnit.Framework.DescriptionAttribute("GetContentWithState")]
    [NUnit.Framework.CategoryAttribute("useTransientTenant")]
    [NUnit.Framework.CategoryAttribute("useContentManagementApi")]
    public partial class GetContentWithStateFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "GetContentWithState.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "GetContentWithState", null, ProgrammingLanguage.CSharp, new string[] {
                        "useTransientTenant",
                        "useContentManagementApi"});
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
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Requesting an item by slug and workflow Id returns the item")]
        public virtual void RequestingAnItemBySlugAndWorkflowIdReturnsTheItem()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Requesting an item by slug and workflow Id returns the item", null, ((string[])(null)));
#line 5
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line hidden
            TechTalk.SpecFlow.Table table21 = new TechTalk.SpecFlow.Table(new string[] {
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
            table21.AddRow(new string[] {
                        "Expected",
                        "myid",
                        "myslug",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "en-GB",
                        "This is the fragment of text"});
#line 6
 testRunner.Given("a content item has been created", ((string)(null)), table21, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table22 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Id",
                        "ContentId",
                        "Slug",
                        "WorkflowId",
                        "StateName",
                        "ChangedBy.Name",
                        "ChangedBy.Id"});
            table22.AddRow(new string[] {
                        "Expected-State",
                        "{newguid}",
                        "myid",
                        "myslug",
                        "{newguid}",
                        "retired",
                        "Frodo Baggins",
                        "{newguid}"});
#line 9
 testRunner.And("a workflow state has been set for the content item", ((string)(null)), table22, "And ");
#line 12
 testRunner.When("I request the content with its state for slug \'{Expected.Slug}\' and workflow Id \'" +
                    "{Expected-State.WorkflowId}\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 13
 testRunner.Then("the response should have a status of \'200\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 14
 testRunner.And("the response body should contain content and state matching content \'Expected\' an" +
                    "d state \'Expected-State\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Requesting an item when the content item does not exist returns a 404 Not Found")]
        public virtual void RequestingAnItemWhenTheContentItemDoesNotExistReturnsA404NotFound()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Requesting an item when the content item does not exist returns a 404 Not Found", null, ((string[])(null)));
#line 16
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 17
 testRunner.Given("there is no content available", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 18
 testRunner.When("I request the content with its state for slug \'myslug/\' and workflow Id \'myworkfl" +
                    "owid\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 19
 testRunner.Then("the response should have a status of \'404\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Requesting an item when the content item exists but there is no corresponding con" +
            "tent state returns a 404 Not Found")]
        public virtual void RequestingAnItemWhenTheContentItemExistsButThereIsNoCorrespondingContentStateReturnsA404NotFound()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Requesting an item when the content item exists but there is no corresponding con" +
                    "tent state returns a 404 Not Found", null, ((string[])(null)));
#line 21
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line hidden
            TechTalk.SpecFlow.Table table23 = new TechTalk.SpecFlow.Table(new string[] {
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
            table23.AddRow(new string[] {
                        "Expected",
                        "myid",
                        "myslug",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "en-GB",
                        "This is the fragment of text"});
#line 22
 testRunner.Given("a content item has been created", ((string)(null)), table23, "Given ");
#line 25
 testRunner.When("I request the content with its state for slug \'{Expected.Slug}\' and workflow Id \'" +
                    "{newguid}\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 26
 testRunner.Then("the response should have a status of \'404\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Requesting an item with a valid slug but invalid workflow Id returns a 404 Not Fo" +
            "und")]
        public virtual void RequestingAnItemWithAValidSlugButInvalidWorkflowIdReturnsA404NotFound()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Requesting an item with a valid slug but invalid workflow Id returns a 404 Not Fo" +
                    "und", null, ((string[])(null)));
#line 28
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line hidden
            TechTalk.SpecFlow.Table table24 = new TechTalk.SpecFlow.Table(new string[] {
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
            table24.AddRow(new string[] {
                        "Expected",
                        "myid",
                        "myslug",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "en-GB",
                        "This is the fragment of text"});
#line 29
 testRunner.Given("a content item has been created", ((string)(null)), table24, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table25 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Id",
                        "ContentId",
                        "Slug",
                        "WorkflowId",
                        "StateName",
                        "ChangedBy.Name",
                        "ChangedBy.Id"});
            table25.AddRow(new string[] {
                        "Expected-State",
                        "{newguid}",
                        "myid",
                        "myslug",
                        "{newguid}",
                        "retired",
                        "Frodo Baggins",
                        "{newguid}"});
#line 32
 testRunner.And("a workflow state has been set for the content item", ((string)(null)), table25, "And ");
#line 35
 testRunner.When("I request the content with its state for slug \'{Expected.Slug}\' and workflow Id \'" +
                    "{newguid}\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 36
 testRunner.Then("the response should have a status of \'404\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Requesting an item with an invalid slug and a valid workflow Id returns a 404 Not" +
            " Found")]
        public virtual void RequestingAnItemWithAnInvalidSlugAndAValidWorkflowIdReturnsA404NotFound()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Requesting an item with an invalid slug and a valid workflow Id returns a 404 Not" +
                    " Found", null, ((string[])(null)));
#line 38
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line hidden
            TechTalk.SpecFlow.Table table26 = new TechTalk.SpecFlow.Table(new string[] {
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
            table26.AddRow(new string[] {
                        "Expected",
                        "myid",
                        "myslug",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "en-GB",
                        "This is the fragment of text"});
#line 39
 testRunner.Given("a content item has been created", ((string)(null)), table26, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table27 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Id",
                        "ContentId",
                        "Slug",
                        "WorkflowId",
                        "StateName",
                        "ChangedBy.Name",
                        "ChangedBy.Id"});
            table27.AddRow(new string[] {
                        "Expected-State",
                        "{newguid}",
                        "myid",
                        "myslug",
                        "{newguid}",
                        "retired",
                        "Frodo Baggins",
                        "{newguid}"});
#line 42
 testRunner.And("a workflow state has been set for the content item", ((string)(null)), table27, "And ");
#line 45
 testRunner.When("I request the content with its state for slug \'myotherslug\' and workflow Id \'{Exp" +
                    "ected-State.WorkflowId}\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 46
 testRunner.Then("the response should have a status of \'404\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
