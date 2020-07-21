using OpenQA.Selenium;

namespace Deloitte.Automation.Core.WebDriver
{
    public interface IDriverCreator
    {
        IWebDriver GetLocalDriver(bool IsHeadless, string downloadsFolder = "");
        IWebDriver GetRemoteDriver(string remoteUri, bool IsHeadless, string downloadsFolder = "");
    }
}
