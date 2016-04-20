﻿namespace Sfa.Das.WebTest.Infrastructure.Selenium
{
    using System;
    using System.Configuration;
    using System.IO;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.PhantomJS;
    using OpenQA.Selenium.Remote;

    using Sfa.Das.WebTest.Infrastructure.Settings;

    using TechTalk.SpecFlow;

    public class SeleniumContext : IDisposable
    {
        private BrowserSettings _settings;

        static string host = ConfigurationManager.AppSettings["host"];
        static string testExecution = ConfigurationManager.AppSettings["testExecutionType"];
        static string saucelabsAccountName = ConfigurationManager.AppSettings["sauce_labs_account_name"];
        static string saucelabsAccountKey = ConfigurationManager.AppSettings["sauce_labs_account_key"];
        static String browserStackKey = ConfigurationManager.AppSettings["browserstack_key"];
        static String browserStacUser = ConfigurationManager.AppSettings["browserstack_user"];
        static String mDevice = ConfigurationManager.AppSettings["mobile_device"];

        public IWebDriver WebDriver { get; private set; }

        public SeleniumContext()
        {
            _settings = new BrowserSettings();

            WebDriver = CreateDriver();
            FeatureContext.Current["driver"] = WebDriver;
        }

        private IWebDriver CreateDriver()
        {
            if (!string.IsNullOrEmpty(_settings.RemoteUrl))
            {
                if (!string.IsNullOrEmpty(_settings.BrowserStackUser))
                {
                    var capabilities = FindBrowserCapability();
                    capabilities.SetCapability("os", _settings.OS);
                    capabilities.SetCapability("os_version", _settings.OSVersion);
                    capabilities.SetCapability("browser_version", _settings.Version);
                    capabilities.SetCapability("browserstack.debug", "true");
                    capabilities.SetCapability("browserstack.user", _settings.BrowserStackUser);
                    capabilities.SetCapability("browserstack.key", _settings.BrowserStackKey);
                    //capabilities.SetCapability("browserstack.selenium_version", "2.53.0");

                    Console.WriteLine($"##### Driver: Browserstack - {_settings.OS} {_settings.OSVersion} - {_settings.Browser} {_settings.Version}");
                    return new RemoteWebDriver(new Uri(_settings.RemoteUrl), capabilities);
                }
            }
            else
            {
                switch (_settings.Browser.ToLower())
                {
                    case "phantomjs":
                        return CreatePhantomJsDriver();
                    case "chrome":
                        return CreateChromeDriver();
                    case "firefox":
                        return CreateFireFoxDriver();
                    case "remote":
                        return CreateRemoteDriver();
                }
            }

            if (host == "localhost")
            {
                if (testExecution == "headless") // headlessrun is performed on deployment server.
                {
                    return CreatePhantomJsDriver();
                }

                return CreateChromeDriver();
            }

            /*
           Tests can be run on below devices available on browserstack tool. 
            Android - Samsung Galaxy S5,Google Nexus 5,Samsung Galaxy Tab 4 10.1
            IOS-iPhone 6S Plus,iPhone 6S,iPad Air,iPhone 5S
            */

            if (host == "browserstack")
            {
                return CreateBrowserStackDriver();
            }

            /* TestS can be run on below devices available in sauce labs tool
            Andriod 4.4  : Google Nexus 7 HD Emulator, Samsung Galaxy S4 Emulator,Samsung Galaxy Tab 3 Emulator.
            IOS 9.2 : iPad Air, iPhone 5, iPhone 6, iPhone 6 plus
             */

            if (host == "saucelabs")
            {
                return CreateSauceLabs();
            }

            return null;
        }

        private DesiredCapabilities FindBrowserCapability()
        {
            switch (_settings.Browser.ToLower())
            {
                case "internet explorer":
                    return DesiredCapabilities.InternetExplorer();
                case "chrome":
                    return DesiredCapabilities.Chrome();
                case "firefox":
                    return DesiredCapabilities.Firefox();
                case "android":
                    return DesiredCapabilities.Android();
                case "edge":
                    return DesiredCapabilities.Edge();
                case "safari":
                    return DesiredCapabilities.Safari();
                case "iphone":
                    return DesiredCapabilities.IPhone();
                case "ipad":
                    return DesiredCapabilities.IPad();
            }

            throw new WebDriverException($"Browser Type '{_settings.Browser}' is not supported as a remote driver.");
        }

        private IWebDriver CreateRemoteDriver()
        {
            throw new NotImplementedException();
        }

        private IWebDriver CreateFireFoxDriver()
        {
            var driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            return driver;
        }

        private static IWebDriver CreateBrowserStackDriver()
        {
            DesiredCapabilities desiredCap = new DesiredCapabilities();
            desiredCap.SetCapability("browserstack.key", browserStackKey);
            desiredCap.SetCapability("browserstack.user", browserStacUser);
            desiredCap.SetCapability("browserstack.debug", "true");

            switch (mDevice)
            {
                case "Iphone":
                    desiredCap.SetCapability("platform", "MAC"); // these values will be moved to app config.
                    desiredCap.SetCapability("browserName", "iPhone");
                    desiredCap.SetCapability("device", "iPhone 6S Plus");
                    desiredCap.SetCapability("browserVersion", "8");
                    break;

                case "Samsung":
                    desiredCap.SetCapability("platform", "Android"); // these values will be moved to app config.
                    desiredCap.SetCapability("browserName", "Android");
                    desiredCap.SetCapability("device", "Google Nexus 5");
                    desiredCap.SetCapability("browserVersion", "5.0");
                    break;
            }

            return new RemoteWebDriver(new Uri("http://hub.browserstack.com/wd/hub/"), desiredCap);
        }

        private static IWebDriver CreateSauceLabs()
        {
            DesiredCapabilities desiredCap = new DesiredCapabilities();
            desiredCap.SetCapability("platform", "MAC");
            desiredCap.SetCapability("browserName", "iPhone");
            desiredCap.SetCapability("device", "iPhone 6 Plus");
            desiredCap.SetCapability("browserVersion", "9.2");
            desiredCap.SetCapability("username", saucelabsAccountName);
            desiredCap.SetCapability("accessKey", saucelabsAccountKey);
            //desiredCap.SetCapability("browserstack.debug", "true");
            return new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub/"), desiredCap, TimeSpan.FromSeconds(600));
        }

        private static IWebDriver CreateChromeDriver()
        {
            var dir = Directory.GetCurrentDirectory();
            var driver = new ChromeDriver(Path.Combine(dir, @"..\\..\\Test\\Resources"));
            driver.Manage().Window.Maximize();
            return driver;
        }

        private static IWebDriver CreatePhantomJsDriver()
        {
            var driverService = PhantomJSDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            Console.WriteLine($"##### Driver: PhantomJs");
            return new PhantomJSDriver(driverService);
        }

        public void Dispose()
        {
            WebDriver?.Quit();
        }
    }
}