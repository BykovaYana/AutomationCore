using NUnit.Framework;
using System;

namespace Deloitte.Automation.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class RegressionAttribute : CategoryAttribute
    {
    }
}
