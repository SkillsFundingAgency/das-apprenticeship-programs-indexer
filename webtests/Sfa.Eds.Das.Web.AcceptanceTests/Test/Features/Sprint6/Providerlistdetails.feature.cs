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
namespace Sfa.Eds.Das.Web.AcceptanceTests.Test.Features.Sprint6
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Providerlistdetails")]
    public partial class ProviderlistdetailsFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "Providerlistdetails.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Providerlistdetails", "In order to chose a provider from list\nAs an employer \nI should be able to see li" +
                    "st of providers for a standard training", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("Verify delivery mode 100 % employer based training is listed on top")]
        [NUnit.Framework.IgnoreAttribute()]
        public virtual void VerifyDeliveryMode100EmployerBasedTrainingIsListedOnTop()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify delivery mode 100 % employer based training is listed on top", new string[] {
                        "ignore"});
#line 6
this.ScenarioSetup(scenarioInfo);
#line 7
testRunner.Given("I have chosen a Standard", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 8
testRunner.When("I search for provider by postcode", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 9
testRunner.Then("I should see provider who provide employer based training is listed on top", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 10
testRunner.And("I see Distance showing text message \"Training takes place at employer\'s location\"" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify providers listed by nearest first")]
        [NUnit.Framework.IgnoreAttribute()]
        public virtual void VerifyProvidersListedByNearestFirst()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify providers listed by nearest first", new string[] {
                        "ignore"});
#line 12
this.ScenarioSetup(scenarioInfo);
#line 13
testRunner.Given("I have chosen a Standard", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 14
testRunner.When("I search for  provider by postcode", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 15
testRunner.Then("I should see matched providers list with nearest provider first in the list", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify provider has no location specified")]
        [NUnit.Framework.IgnoreAttribute()]
        public virtual void VerifyProviderHasNoLocationSpecified()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify provider has no location specified", new string[] {
                        "ignore"});
#line 17
this.ScenarioSetup(scenarioInfo);
#line 18
testRunner.Given("I have chosen a Standard", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 19
testRunner.When("I search for provider by postcode", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 20
testRunner.Then("I should see provider who has no location details", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 21
testRunner.And("I see distance showing message \"Training take place at employer\'s location\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify provider has more than one location")]
        [NUnit.Framework.IgnoreAttribute()]
        public virtual void VerifyProviderHasMoreThanOneLocation()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify provider has more than one location", new string[] {
                        "ignore"});
#line 24
this.ScenarioSetup(scenarioInfo);
#line 25
testRunner.Given("I have chosen a Standard", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 26
testRunner.When("I search for provider by postcode", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 27
testRunner.And("I have provider with  more than one location", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 28
testRunner.Then("I  see in search results same provider listed multiple times showing nearest loca" +
                    "tion first in the list.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify no providers found.")]
        [NUnit.Framework.IgnoreAttribute()]
        public virtual void VerifyNoProvidersFound_()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify no providers found.", new string[] {
                        "ignore"});
#line 31
this.ScenarioSetup(scenarioInfo);
#line 32
testRunner.Given("I have chosen a Standard", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 33
testRunner.When("I search for provider by postcode", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 34
testRunner.And("I have no providers operating in given postcode", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 35
testRunner.Then("I should see nothing in result page with message \"There are currently no training" +
                    " providers found for Standard <> in postcode <>\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify provider location name same as provider name")]
        [NUnit.Framework.IgnoreAttribute()]
        public virtual void VerifyProviderLocationNameSameAsProviderName()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify provider location name same as provider name", new string[] {
                        "ignore"});
#line 38
this.ScenarioSetup(scenarioInfo);
#line 39
testRunner.Given("I have found provider name and location name", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 40
testRunner.When("I search for provider by postcode", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 41
testRunner.Then("I should see provider name in result page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 42
testRunner.And("I should not see location name field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify Provider with additional information on the result page")]
        [NUnit.Framework.IgnoreAttribute()]
        public virtual void VerifyProviderWithAdditionalInformationOnTheResultPage()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify Provider with additional information on the result page", new string[] {
                        "ignore"});
#line 45
this.ScenarioSetup(scenarioInfo);
#line 46
testRunner.Given("I have chosen a Standard", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 47
testRunner.When("I search for provider by postcode", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 48
testRunner.Then("I should see matched provider list in the result page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 49
testRunner.And("under each provider I should see provider \"website\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 50
testRunner.And("I should see provider \"location name\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 51
testRunner.And("I should see provider \"location address\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 52
testRunner.And("I should see \"Employer satisfaction\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 53
testRunner.And("I should see \"Learner satisfaction\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify provider with no employer or learner satisfaction data")]
        [NUnit.Framework.IgnoreAttribute()]
        public virtual void VerifyProviderWithNoEmployerOrLearnerSatisfactionData()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify provider with no employer or learner satisfaction data", new string[] {
                        "ignore"});
#line 56
this.ScenarioSetup(scenarioInfo);
#line 57
testRunner.Given("I provider with no employer satisfaction data", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 58
testRunner.When("I search for a provider which doesn\'t have employer satisfaction data", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 59
testRunner.And("I see matched provider list", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 60
testRunner.Then("I see should provider with employer satisfaction field empty.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify provider list page to show only active providers for a standard")]
        [NUnit.Framework.IgnoreAttribute()]
        public virtual void VerifyProviderListPageToShowOnlyActiveProvidersForAStandard()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify provider list page to show only active providers for a standard", new string[] {
                        "ignore"});
#line 64
this.ScenarioSetup(scenarioInfo);
#line 65
testRunner.Given("I am on provider  list page page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 66
testRunner.And("I have bookmarked", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 67
testRunner.When("I open the link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 68
testRunner.Then("I should see only active providers in provider list page who currently provides t" +
                    "raining.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion