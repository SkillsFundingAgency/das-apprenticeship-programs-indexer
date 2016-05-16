﻿namespace Sfa.Das.WebTest.Pages
{
    using OpenQA.Selenium;

    using Sfa.Das.WebTest.Infrastructure;

    [PageNavigation("/apprenticeship/standard")]
    public class StandardDetailsPage : SharedTemplatePage
    {
        public By PostcodeSearchBox => By.CssSelector("#search-box-bottom");

        public By SearchButton => By.CssSelector("#postcode-form-bottom .postcode-search-button");
    
        public By ShortlistLink => By.ClassName("shortlist-link");
    }
}