using Deloitte.Automation.Extensions.Selenium;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Deloitte.Automation.Core
{
    public class CustomWebElement
    {
        public By Locator { get; }

        public CustomWebElement(By locator)
        {
            this.Locator = locator;
        }

        public IReadOnlyCollection<IWebElement> Elements
        {
            get
            {
                try
                {
                    return GetElements();
                }
                catch (StaleElementReferenceException)
                {
                    LoggingHelper.LogDebug("Re-initialize web elements since DOM has been refreshed");
                    return GetElements();
                }
            }
        }

        public IWebElement Element
        {
            get
            {
                try
                {
                    return GetElement();
                }
                catch (StaleElementReferenceException)
                {
                    LoggingHelper.LogDebug("Re-initialize web element since DOM has been refreshed");
                    return GetElement();
                }                
            }
        }
        //TODO
        private IWebElement GetElement() => Driver.Instance.WaitForElementPresent(Locator, TestConfig.Settings.DefaultTimeout);
        private IReadOnlyCollection<IWebElement> GetElements() => Driver.Instance.WaitFor(WebElementConditions.ElementsPresent(Locator));
    }
}