﻿namespace Sfa.Das.WebTest.Pages
{
    using OpenQA.Selenium;

    using Sfa.Das.WebTest.Infrastructure;

    [PageNavigation("/")]
    public class StartPage
    {
        public By StartButton => By.Id("start-button");
    }
}