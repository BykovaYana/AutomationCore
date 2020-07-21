using Deloitte.Automation.Extensions.Selenium;
using OpenQA.Selenium;
using System;

namespace Deloitte.Automation.Core
{
    public abstract class BasePage
    {
        public virtual TPage As<TPage>(bool @unsafe = false) where TPage : BasePage, new()
        {
            try
            {
                return (TPage)this;
            }
            catch (InvalidCastException)
            {
                if (@unsafe)
                {
                    throw;
                }
                return new TPage();
            }
        }
        public void WaitForProcessingFinished() => Driver.Instance.InvisibilityOfElementLocated(By.XPath("//div[@class='overlay progresslayer']"), TestConfig.Settings.DefaultTimeout * 2);
    }
}