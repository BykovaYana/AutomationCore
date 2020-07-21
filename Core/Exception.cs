using System;

namespace Deloitte.Automation.Core
{
    public class SeleniumException : Exception
    {
        public SeleniumException(string message) : base(message)
        {
        }
    }
}
