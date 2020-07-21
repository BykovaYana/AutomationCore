using System;
using System.Collections.Generic;

namespace Deloitte.Automation.Core.Extensions
{
    public static class StringExtensions
    {
        public const char Delimiter = '=';
        public static KeyValuePair<string, object> ParseWithDelimiter(this string input, char splitter = Delimiter)
        {
            var prms = input.Split(splitter);
            if (prms.Length != 2)
                throw new ArgumentException($"Preference count doesn't equal to 2. Please use '{splitter}' delimiter");

            return new KeyValuePair<string, object>(prms[0], prms[1]);
        }
        public static bool ContainsDelimeter(this string input, char splitter = Delimiter)
        {
            return input.Contains(splitter.ToString());
        }
    }
}
