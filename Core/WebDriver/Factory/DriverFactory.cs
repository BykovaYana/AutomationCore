using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Deloitte.Automation.Core.WebDriver
{
    public static class DriverFactory
    {
        public static IDriverCreator GetDriverCreator<T>() where T : IWebDriver
        {
            if (typeof(T) == typeof(ChromeDriver))
            {
                return new ChromeDriverCreator();

            }
            throw new NotImplementedException($"{typeof(T).Name} doesn't supported.");
        }
    }
}
