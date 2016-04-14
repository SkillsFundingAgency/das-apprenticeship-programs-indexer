﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.42000
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Sfa.Das.WebTest.AcceptanceTests.Test.Features.Sprint8
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("ProviderDetailsforFrameworks")]
    public partial class ProviderDetailsforFrameworksFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "ProviderDetailsFramework.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "ProviderDetailsforFrameworks", "In order to chose a provider from available providers\r\nAs an employer \r\nI want to" +
                    " be see full provider detail on provider page", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
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
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Provider with more than one loocation,show only one location as choosen by employ" +
            "er")]
        [NUnit.Framework.IgnoreAttribute()]
        public virtual void ProviderWithMoreThanOneLoocationShowOnlyOneLocationAsChoosenByEmployer()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Provider with more than one loocation,show only one location as choosen by employ" +
                    "er", new string[] {
                        "ignore"});
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
testRunner.Given("I have a provider with more than on location", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
testRunner.When("I chose a provider from result page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 10
testRunner.Then("I should see only one location on provider detail page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Provider with 100% employer based training should not show venue  details on prov" +
            "ider detail page")]
        [NUnit.Framework.IgnoreAttribute()]
        public virtual void ProviderWith100EmployerBasedTrainingShouldNotShowVenueDetailsOnProviderDetailPage()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Provider with 100% employer based training should not show venue  details on prov" +
                    "ider detail page", new string[] {
                        "ignore"});
#line 13
this.ScenarioSetup(scenarioInfo);
#line 14
testRunner.Given("I have provider with full training at employer location", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 15
testRunner.When("I choose this provider from result page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 16
testRunner.Then("I should see provider detail page with no veunue details", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify provider detail page")]
        [NUnit.Framework.TestCaseAttribute("25", "B46 3DJ", null)]
        [NUnit.Framework.TestCaseAttribute("12", "CV7 8ED", null)]
        [NUnit.Framework.TestCaseAttribute("17", "cv1 2WT", null)]
        public virtual void VerifyProviderDetailPage(string id, string postcode, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify provider detail page", exampleTags);
#line 19
this.ScenarioSetup(scenarioInfo);
#line 20
testRunner.Given(string.Format("I am on Standard \'{0}\' detail page", id), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 21
testRunner.And(string.Format("I enter \'{0}\' in provider search box", postcode), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
testRunner.When("I search Search for provider", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 23
testRunner.And("I select any of the provider from the list", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 24
testRunner.Then("I should see \"Provider name\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 25
testRunner.And("I should see \"Employer satisfaction\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 26
testRunner.And("I should see \"Learner satisfaction\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 27
testRunner.And("I should see \"Website course page\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 28
testRunner.And("I should see \"Website contact page\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
testRunner.And("I should see \"Standard name\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 30
testRunner.And("I should see \"Training structure\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 31
testRunner.And("I should see \"Training location\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 32
testRunner.And("I should see \"phone\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 33
testRunner.And("I should see \"email\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify training options")]
        [NUnit.Framework.IgnoreAttribute()]
        public virtual void VerifyTrainingOptions()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify training options", new string[] {
                        "ignore"});
#line 42
this.ScenarioSetup(scenarioInfo);
#line 43
testRunner.Given("I have provider \'<providername>\' with all of traning modes", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 44
testRunner.When("I open provider detail page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 45
testRunner.Then("under training modes I should see \"block release\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 46
testRunner.And("I should see \"day release\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 47
testRunner.And("I  should see \"at your location\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 48
testRunner.And("I should see location name.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify end to end provider detail page")]
        [NUnit.Framework.IgnoreAttribute()]
        public virtual void VerifyEndToEndProviderDetailPage()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify end to end provider detail page", new string[] {
                        "ignore"});
#line 51
this.ScenarioSetup(scenarioInfo);
#line 52
testRunner.Given("I have a provider with updated info in course directory", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 53
testRunner.When("I open provider detail page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 54
testRunner.Then("I should see updated provider info in provider detail page.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Should be able to navigate back to provider result page from detail page.")]
        [NUnit.Framework.IgnoreAttribute()]
        public virtual void ShouldBeAbleToNavigateBackToProviderResultPageFromDetailPage_()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Should be able to navigate back to provider result page from detail page.", new string[] {
                        "ignore"});
#line 57
this.ScenarioSetup(scenarioInfo);
#line 58
testRunner.Given("I am on provider detail page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 59
testRunner.When("I click on back link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 60
testRunner.Then("I shoudl be able to return back to provider list page.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("When provider detail page is bookmarked, it should show only provider detail page" +
            " when provider is still active.( FCS file to have provider inactive and test boo" +
            "kmarked link)")]
        [NUnit.Framework.IgnoreAttribute()]
        public virtual void WhenProviderDetailPageIsBookmarkedItShouldShowOnlyProviderDetailPageWhenProviderIsStillActive_FCSFileToHaveProviderInactiveAndTestBookmarkedLink()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When provider detail page is bookmarked, it should show only provider detail page" +
                    " when provider is still active.( FCS file to have provider inactive and test boo" +
                    "kmarked link)", new string[] {
                        "ignore"});
#line 64
this.ScenarioSetup(scenarioInfo);
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
