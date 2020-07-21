using NUnit.Framework;
using OpenQA.Selenium.IE;
using System;

namespace Deloitte.Automation.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class IEFixtureAttribute : TestFixtureAttribute
    {
        public IEFixtureAttribute() : base(typeof(InternetExplorerDriver))
        {
            Description = "Run tests in IE";
            Category = "IE";
        }
    }
}
