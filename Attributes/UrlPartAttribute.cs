using System;

namespace Deloitte.Automation.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class UrlPartAttribute : Attribute
    {
        private readonly string _urlPart;
        public string UrlPart
        {
            get
            {
                return _urlPart;
            }
        }

        public UrlPartAttribute(string urlPart)
        {
            _urlPart = urlPart;
        }
    }
}
