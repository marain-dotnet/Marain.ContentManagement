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
    [NUnit.Framework.DescriptionAttribute("Get workflow state")]
    [NUnit.Framework.CategoryAttribute("perFeatureContainer")]
    [NUnit.Framework.CategoryAttribute("useTransientTenant")]
    [NUnit.Framework.CategoryAttribute("useContentManagementApi")]
    public partial class GetWorkflowStateFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "GetWorkflowState.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Get workflow state", null, ProgrammingLanguage.CSharp, new string[] {
                        "perFeatureContainer",
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
#line 6
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line hidden
            TechTalk.SpecFlow.Table table22 = new TechTalk.SpecFlow.Table(new string[] {
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
            table22.AddRow(new string[] {
                        "Expected",
                        "myid1",
                        "myslug1",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "en-GB",
                        "This is the fragment of text"});
#line 7
 testRunner.Given("a content item has been created", ((string)(null)), table22, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table23 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Id",
                        "ContentId",
                        "Slug",
                        "WorkflowId",
                        "StateName",
                        "ChangedBy.Name",
                        "ChangedBy.Id"});
            table23.AddRow(new string[] {
                        "Expected-State",
                        "{newguid}",
                        "myid1",
                        "myslug1",
                        "{newguid}",
                        "retired",
                        "Frodo Baggins",
                        "{newguid}"});
#line 10
 testRunner.And("a workflow state has been set for the content item", ((string)(null)), table23, "And ");
#line 13
 testRunner.When("I request the workflow state for slug \'{Expected.Slug}\' and workflow Id \'{Expecte" +
                    "d-State.WorkflowId}\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 14
 testRunner.Then("the response should have a status of \'200\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 15
 testRunner.And("the response body should contain content state matching \'Expected-State\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
 testRunner.And("the response should contain a \'self\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
 testRunner.And("the response should contain a \'content\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
 testRunner.And("the response should contain a \'contentsummary\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Requesting an item by slug and workflow Id and with embedded Content returns the " +
            "item and corresponding content")]
        public virtual void RequestingAnItemBySlugAndWorkflowIdAndWithEmbeddedContentReturnsTheItemAndCorrespondingContent()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Requesting an item by slug and workflow Id and with embedded Content returns the " +
                    "item and corresponding content", null, ((string[])(null)));
#line 20
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
                        "myid4",
                        "myslug4",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "en-GB",
                        "This is the fragment of text"});
#line 21
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
                        "myid4",
                        "myslug4",
                        "{newguid}",
                        "retired",
                        "Frodo Baggins",
                        "{newguid}"});
#line 24
 testRunner.And("a workflow state has been set for the content item", ((string)(null)), table25, "And ");
#line 27
 testRunner.When("I request the workflow state with embedded \'content\' for slug \'{Expected.Slug}\' a" +
                    "nd workflow Id \'{Expected-State.WorkflowId}\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 28
 testRunner.Then("the response should have a status of \'200\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 29
 testRunner.And("the response body should contain content state matching \'Expected-State\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 30
 testRunner.And("the response should contain a \'self\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 31
 testRunner.And("the response should contain a \'content\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 32
 testRunner.And("the response should contain a \'contentsummary\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 33
 testRunner.And("the response should contain an embedded resource called \'content\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
 testRunner.And("the embedded resource called \'content\' should match the content called \'Expected\'" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Requesting an item by slug and workflow Id and with embedded content summary retu" +
            "rns the item and corresponding content summary")]
        public virtual void RequestingAnItemBySlugAndWorkflowIdAndWithEmbeddedContentSummaryReturnsTheItemAndCorrespondingContentSummary()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Requesting an item by slug and workflow Id and with embedded content summary retu" +
                    "rns the item and corresponding content summary", null, ((string[])(null)));
#line 36
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
                        "myid5",
                        "myslug5",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "en-GB",
                        "This is the fragment of text"});
#line 37
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
                        "myid5",
                        "myslug5",
                        "{newguid}",
                        "retired",
                        "Frodo Baggins",
                        "{newguid}"});
#line 40
 testRunner.And("a workflow state has been set for the content item", ((string)(null)), table27, "And ");
#line 43
 testRunner.When("I request the workflow state with embedded \'contentsummary\' for slug \'{Expected.S" +
                    "lug}\' and workflow Id \'{Expected-State.WorkflowId}\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 44
 testRunner.Then("the response should have a status of \'200\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 45
 testRunner.And("the response body should contain content state matching \'Expected-State\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 46
 testRunner.And("the response should contain a \'self\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 47
 testRunner.And("the response should contain a \'content\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 48
 testRunner.And("the response should contain a \'contentsummary\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 49
 testRunner.And("the response should contain an embedded resource called \'contentsummary\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 50
 testRunner.And("the embedded resource called \'contentsummary\' should be a summary of the content " +
                    "called \'Expected\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Requesting an item when the content item does not exist returns a 404 Not Found")]
        public virtual void RequestingAnItemWhenTheContentItemDoesNotExistReturnsA404NotFound()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Requesting an item when the content item does not exist returns a 404 Not Found", null, ((string[])(null)));
#line 52
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 53
 testRunner.Given("there is no content available", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 54
 testRunner.When("I request the workflow state for slug \'myslug/\' and workflow Id \'myworkflowid\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 55
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
#line 57
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
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
                        "Expected",
                        "myid2",
                        "myslug2",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "en-GB",
                        "This is the fragment of text"});
#line 58
 testRunner.Given("a content item has been created", ((string)(null)), table28, "Given ");
#line 61
 testRunner.When("I request the workflow state for slug \'{Expected.Slug}\' and workflow Id \'{newguid" +
                    "}\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 62
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
#line 64
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line hidden
            TechTalk.SpecFlow.Table table29 = new TechTalk.SpecFlow.Table(new string[] {
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
            table29.AddRow(new string[] {
                        "Expected",
                        "myid3",
                        "myslug3",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "en-GB",
                        "This is the fragment of text"});
#line 65
 testRunner.Given("a content item has been created", ((string)(null)), table29, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table30 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Id",
                        "ContentId",
                        "Slug",
                        "WorkflowId",
                        "StateName",
                        "ChangedBy.Name",
                        "ChangedBy.Id"});
            table30.AddRow(new string[] {
                        "Expected-State",
                        "{newguid}",
                        "myid3",
                        "myslug3",
                        "{newguid}",
                        "retired",
                        "Frodo Baggins",
                        "{newguid}"});
#line 68
 testRunner.And("a workflow state has been set for the content item", ((string)(null)), table30, "And ");
#line 71
 testRunner.When("I request the workflow state for slug \'{Expected.Slug}\' and workflow Id \'{newguid" +
                    "}\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 72
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
#line 74
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line hidden
            TechTalk.SpecFlow.Table table31 = new TechTalk.SpecFlow.Table(new string[] {
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
            table31.AddRow(new string[] {
                        "Expected",
                        "myid6",
                        "myslug6",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "en-GB",
                        "This is the fragment of text"});
#line 75
 testRunner.Given("a content item has been created", ((string)(null)), table31, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table32 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Id",
                        "ContentId",
                        "Slug",
                        "WorkflowId",
                        "StateName",
                        "ChangedBy.Name",
                        "ChangedBy.Id"});
            table32.AddRow(new string[] {
                        "Expected-State",
                        "{newguid}",
                        "myid6",
                        "myslug6",
                        "{newguid}",
                        "retired",
                        "Frodo Baggins",
                        "{newguid}"});
#line 78
 testRunner.And("a workflow state has been set for the content item", ((string)(null)), table32, "And ");
#line 81
 testRunner.When("I request the workflow state for slug \'myotherslug\' and workflow Id \'{Expected-St" +
                    "ate.WorkflowId}\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 82
 testRunner.Then("the response should have a status of \'404\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion