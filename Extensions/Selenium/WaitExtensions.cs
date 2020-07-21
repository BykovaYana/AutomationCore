using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Deloitte.Automation.Extensions.Selenium
{
    public static class WebElementConditions
    {
        public static Func<ISearchContext, IReadOnlyCollection<IWebElement>> ElementsPresent(By locator) => e => e.FindElements(locator);
    }

    public static class WaitExtensions
    {
        public const int MediumTimeout = 4;

        public static List<string> WaitForElementsWithText(this IWebDriver driver, By locator, int? timeoutInSec = null)
        {
            return driver.WaitFor(ctx => ctx.FindElements(locator).Select(x => x.Text).ToList(),
                                       60, typeof(NullReferenceException), typeof(StaleElementReferenceException));
        }

        public static IWebElement WaitForElementPresent(this IWebDriver driver, By locator, int? timeoutInSec = null)
        {
            return driver.WaitFor(ctx => ctx.FindElement(locator), null, new Type[] { typeof(NoSuchElementException) });
        }
        
        public static bool WaitForTextPresentInElement(this IWebDriver driver, IWebElement element, string expectedText, int? timeoutInSec = null)
        {
            return driver.WaitFor(ctx => element.Text.Equals(expectedText), exceptionTypes: new Type[] { typeof(NoSuchElementException), typeof(NullReferenceException), typeof(StaleElementReferenceException) });
        }

        public static IWebElement WaitForChildElementPresent(this IWebDriver driver, IWebElement rootElement, By locator, int? timeoutInSec = null)
        {
            return rootElement.ToDriver().WaitFor(ctx => ctx.FindElement(locator), null, new Type[] { typeof(NoSuchElementException), typeof(StaleElementReferenceException) });
        }

        public static TResult WaitFor<TResult>(this IWebDriver driver, Func<ISearchContext, TResult> condition, int? timeoutInSec = null, params Type[] exceptionTypes)
        {
            var wait = new DefaultWait<ISearchContext>(driver)
            {
                Timeout = TimeSpan.FromSeconds(timeoutInSec ?? TestConfig.Settings.DefaultTimeout)
            };
            if (exceptionTypes != null)
            {
                wait.IgnoreExceptionTypes(exceptionTypes);
            }

            return wait.Until(condition);
        }

        public static bool WaitForElementDisappeared(this IWebDriver driver, By locator,
            int timeout = MediumTimeout, string message = null)
        {
            try
            {
                var wait = Wait(driver, timeout, message);
                wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
                wait.Until(ctx => ctx.FindElement(locator).Displayed);

                LoggingHelper.LogError("Element is not disappeared, but should.");
                return false;
            }
            catch (WebDriverTimeoutException)
            {
                return true;
            }
            catch (NoSuchElementException)
            {
                return true;
            }
        }

        public static bool WaitForPageToLoad(this IWebDriver driver, int timeoutInSec = MediumTimeout)
        {
            return driver.Wait(timeoutInSec).Until(ctx => driver.JsExecutor().ExecuteScript("return document.readyState").Equals("complete"));
        }

        public static void WaitUrlContains(this IWebDriver driver, string url, int timeoutInSec)
        {
            Wait(driver, timeoutInSec).Until(ExpectedConditions.UrlContains(url));
        }

        public static void WaitUntilElementExists(this IWebDriver driver, By locator, int timeoutInSec = 4)
        {
            Wait(driver, timeoutInSec).Until(ExpectedConditions.ElementExists(locator));
        }

        public static void WaitElementToBeClickable(this IWebElement element, int timeoutInSec)
        {
            Wait(element.ToDriver(), timeoutInSec).Until(ExpectedConditions.ElementToBeClickable(element));
        }

        public static void WaitElementToBeSelected(this IWebElement element, int timeoutInSec)
        {
            Wait(element.ToDriver(), timeoutInSec).Until(ExpectedConditions.ElementToBeClickable(element));
        }

        public static void InvisibilityOfElementLocated(this IWebDriver driver, By locator, int timeoutInSec)
        {
            var wait = Wait(driver, timeoutInSec);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        public static IWebElement WaitElementIsVisible(this IWebDriver driver, By locator, int timeoutInSec)
        {
            return Wait(driver, timeoutInSec).Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public static void WaitElementToBeClickable(this IWebDriver driver, By locator, int timeoutInSec)
        {
            Wait(driver, timeoutInSec).Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        private static WebDriverWait Wait(this IWebDriver driver, int timeout = MediumTimeout,
            string message = null)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(timeout)) { Message = message };
        }
    }
}