using Deloitte.Automation.Attributes;
using Deloitte.Automation.Core.WebDriver;
using Deloitte.Automation.Extensions;
using Deloitte.Automation.Extensions.Selenium;
using OpenQA.Selenium;
using System;
using System.Threading;
using static Deloitte.Automation.Core.DriverConfig;

namespace Deloitte.Automation.Core
{
    public static class Driver
    {
        public static IWebDriver Instance =>
            ThreadLocalContext.Value.CheckNotNull(nameof(Driver),
                "Driver = null. Probably driver was not initialized - use InitWebDriver() method first");

        private static readonly ThreadLocal<IWebDriver> ThreadLocalContext = new ThreadLocal<IWebDriver>();

        public static void InitWebDriver<T>(string downloadFilePath) where T : IWebDriver
        {
            LoggingHelper.LogInformation($"Initialization of the driver ({typeof(T).Name}) has been started for thread {Thread.CurrentThread.ManagedThreadId}");
            var driverCreator = DriverFactory.GetDriverCreator<T>();
            if (IsSeleniumGridExecution)
            {
                ThreadLocalContext.Value = driverCreator.GetRemoteDriver(SeleniumGridUrl, IsHeadlessExecution, downloadFilePath);
            }
            else
            {
                ThreadLocalContext.Value = driverCreator.GetLocalDriver(IsHeadlessExecution, downloadFilePath);
            }
        }

        public static TPage InstanceOf<TPage>(string url) where TPage : BasePage, new()
        {
            Instance.Navigate().GoToUrl(url);
            return new TPage();
        }

        public static void GoToPage(Type type)
        {
            var urlPart = type.GetAttributeValue((UrlPartAttribute atr) => atr?.UrlPart ?? "");
            var url = new Uri(BaseUrl, urlPart).AbsoluteUri;

            LoggingHelper.LogInformation($"Open page with url part:{urlPart}");

            Instance.Url = url;
            Instance.WaitForPageToLoad();
        }

        public static void Close()
        {
            try
            {
                LoggingHelper.LogInformation("Try to close driver");
                ThreadLocalContext.Value.Quit();
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError(ex.StackTrace);
            }
            finally
            {
                ThreadLocalContext.Value = null;
                LoggingHelper.LogInformation("Driver should be closed");
            }
        }
    }
}