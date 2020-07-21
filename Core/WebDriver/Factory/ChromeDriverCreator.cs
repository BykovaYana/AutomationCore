using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;

namespace Deloitte.Automation.Core.WebDriver
{
    public class ChromeDriverCreator : IDriverCreator
    {

        public IWebDriver GetLocalDriver(bool isHeadless, string downloadsFolder = "")
        {
            var driver = new ChromeDriver(GetOptions(isHeadless));

            AddDoownloadsFolder(downloadsFolder, driver);

            return driver;
        }

        public IWebDriver GetRemoteDriver(string hubUri, bool isHeadless, string downloadsFolder = "")
        {
            var driver = new RemoteWebDriver(new Uri(hubUri), GetOptions(isHeadless).ToCapabilities(), TimeSpan.FromSeconds(30));

            AddDoownloadsFolder(downloadsFolder, driver as ChromeDriver);

            return driver;
        }

        private ChromeOptions GetOptions(bool isHeadless)
        {
            var options = new ChromeOptions();

            options.AddUserProfilePreference("intl.accept_languages", "en");
            options.AddUserProfilePreference("disable-popup-blocking", "true");
            options.AddArguments("--allow-no-sandbox-job", "--ignore-certificate-errors");
            options.AddExcludedArgument("enable-automation");
            options.AddAdditionalChromeOption("useAutomationExtension", false);

            if (isHeadless)
            {
                options.AddArguments("disable-gpu", "headless", "window-size=1920,1080");
            }

            return options;
        }

        private void AddDoownloadsFolder(string downloadsFolder, ChromeDriver driver)
        {
            if (!string.IsNullOrEmpty(downloadsFolder))
            {
                var param = new Dictionary<string, object>
                 {
                     { "behavior", "allow" },
                     { "downloadPath", downloadsFolder }
                 };
                driver.ExecuteChromeCommandWithResult("Page.setDownloadBehavior", param);
            }
        }
    }
}
