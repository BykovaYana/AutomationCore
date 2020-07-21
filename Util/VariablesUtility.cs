using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Deloitte.Automation.Util
{
    public static class VariablesUtility
    {
        public static string GetEnvVariable(string varName) {
            try
            {
                var envar = Environment.GetEnvironmentVariable(varName);
                if (!string.IsNullOrEmpty(envar))
                {
                    return envar;
                }
            }
            catch (SecurityException e)
            {
                LoggingHelper.LogError(e.Message);
            }

            return null;
        }
    }
}
