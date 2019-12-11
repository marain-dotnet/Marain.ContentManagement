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
    [NUnit.Framework.DescriptionAttribute("GetContent")]
    public partial class GetContentFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "GetContent.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "GetContent", null, ProgrammingLanguage.CSharp, ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("Requesting an item by slug and Id returns the item")]
        public virtual void RequestingAnItemBySlugAndIdReturnsTheItem()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Requesting an item by slug and Id returns the item", null, ((string[])(null)));
#line 3
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line hidden
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
                        "Expected",
                        "myid",
                        "myslug",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
#line 4
 testRunner.Given("a content item has been created", ((string)(null)), table9, "Given ");
#line 7
 testRunner.When("I request the content with slug \'{Expected.Slug}\' and Id \'{Expected.Id}\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 8
 testRunner.Then("the response should have a status of \'200\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 9
 testRunner.And("the response body should contain the content item \'Expected\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
 testRunner.And("the ETag header should be set", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 11
 testRunner.And("the Cache header should be set to \'max-age=31536000\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Requesting an item by slug and Id with an etag that matches the item returns a 30" +
            "4 Not Modified")]
        public virtual void RequestingAnItemBySlugAndIdWithAnEtagThatMatchesTheItemReturnsA304NotModified()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Requesting an item by slug and Id with an etag that matches the item returns a 30" +
                    "4 Not Modified", null, ((string[])(null)));
#line 13
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
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
                        "Fragment"});
            table10.AddRow(new string[] {
                        "Expected",
                        "myid",
                        "myslug",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
#line 14
 testRunner.Given("a content item has been created", ((string)(null)), table10, "Given ");
#line 17
 testRunner.And("I have requested the content with slug \'{Expected.Slug}\' and Id \'{Expected.Id}\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
 testRunner.When("I request the content with slug \'{Expected.Slug}\' and Id \'{Expected.Id}\' using th" +
                    "e etag returned by the previous request", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 19
 testRunner.Then("the response should have a status of \'304\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 20
 testRunner.And("there should be no response body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Requesting an item by slug and Id with an etag that does not matches the item ret" +
            "urns the item")]
        public virtual void RequestingAnItemBySlugAndIdWithAnEtagThatDoesNotMatchesTheItemReturnsTheItem()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Requesting an item by slug and Id with an etag that does not matches the item ret" +
                    "urns the item", null, ((string[])(null)));
#line 22
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line hidden
            TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
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
            table11.AddRow(new string[] {
                        "Expected",
                        "myid",
                        "myslug",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
#line 23
 testRunner.Given("a content item has been created", ((string)(null)), table11, "Given ");
#line 26
 testRunner.When("I request the content with slug \'{Expected.Slug}\' and Id \'{Expected.Id}\' using a " +
                    "random etag", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 27
 testRunner.Then("the response should have a status of \'200\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 28
 testRunner.And("the response body should contain the content item \'Expected\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
 testRunner.And("the ETag header should be set", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 30
 testRunner.And("the Cache header should be set to \'max-age=31536000\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Requesting item that does not exist returns a 404 Not Found")]
        public virtual void RequestingItemThatDoesNotExistReturnsA404NotFound()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Requesting item that does not exist returns a 404 Not Found", null, ((string[])(null)));
#line 32
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 33
 testRunner.Given("there is no content available", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 34
 testRunner.When("I request the content with slug \'thisismyslug\' and Id \'thisismyid\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 35
 testRunner.Then("the response should have a status of \'404\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Requesting item with a valid slug but invalid Id returns a 404 Not Found")]
        public virtual void RequestingItemWithAValidSlugButInvalidIdReturnsA404NotFound()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Requesting item with a valid slug but invalid Id returns a 404 Not Found", null, ((string[])(null)));
#line 37
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line hidden
            TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
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
            table12.AddRow(new string[] {
                        "Expected",
                        "myid",
                        "myslug",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
#line 38
 testRunner.Given("a content item has been created", ((string)(null)), table12, "Given ");
#line 41
 testRunner.When("I request the content with slug \'{Expected.Slug}\' and Id \'myotherid\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 42
 testRunner.Then("the response should have a status of \'404\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Requesting item with a valid Id but valid slug returns a 404 Not Found")]
        public virtual void RequestingItemWithAValidIdButValidSlugReturnsA404NotFound()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Requesting item with a valid Id but valid slug returns a 404 Not Found", null, ((string[])(null)));
#line 44
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line hidden
            TechTalk.SpecFlow.Table table13 = new TechTalk.SpecFlow.Table(new string[] {
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
            table13.AddRow(new string[] {
                        "Expected",
                        "myid",
                        "myslug",
                        "First tag; Second tag",
                        "/standard/content;/books/hobbit;/books/lotr",
                        "Bilbo Baggins",
                        "{newguid}",
                        "This is the title",
                        "A description of the content",
                        "fr-FR",
                        "This is the fragment of text"});
#line 45
 testRunner.Given("a content item has been created", ((string)(null)), table13, "Given ");
#line 48
 testRunner.When("I request the content with slug \'myotherslug\' and Id \'{Expected.Id}\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 49
 testRunner.Then("the response should have a status of \'404\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
