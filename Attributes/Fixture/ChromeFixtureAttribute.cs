using System;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace Deloitte.Automation.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ChromeFixtureAttribute : TestFixtureAttribute
    {
        public ChromeFixtureAttribute() : base(typeof(ChromeDriver))
        {
            Description = "Run tests in Chrome";
            Category = "Chrome";
        }
    }
}
