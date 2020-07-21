using Deloitte.Automation.Util;
using System;
using static Deloitte.Automation.TestConfig;

namespace Deloitte.Automation.Core
{
    public static class DriverConfig
    {
        public const string BaseUrlEnvVar = "base_url";
        public const string SeleniumGridExecutionlEnvVar = "selenium_grid_execution";
        public const string IsHeadlessExecutionEnvVar = "is_headless_execution";
        public const string GridUrlEnvVar = "selenium_grid_url";

        public static string SeleniumGridUrl
        {
            get
            {
                var gridVar = VariablesUtility.GetEnvVariable(GridUrlEnvVar);
                return gridVar ?? Settings.GridUrl;
            }
        }

        public static bool IsHeadlessExecution
        {
            get
            {
                var isHeadless = VariablesUtility.GetEnvVariable(IsHeadlessExecutionEnvVar);
                if (isHeadless == null)
                {
                    return Settings.IsHeadlessExecution;
                }
                return bool.Parse(isHeadless);
            }
        }

        public static bool IsSeleniumGridExecution
        {
            get
            {
                var executionTypeVar = VariablesUtility.GetEnvVariable(SeleniumGridExecutionlEnvVar);
                if (executionTypeVar == null)
                {
                    return Settings.SeleniumGridExecution;
                }

                return bool.Parse(executionTypeVar);
            }
        }

        public static Uri BaseUrl
        {
            get
            {
                var urlVar = VariablesUtility.GetEnvVariable(BaseUrlEnvVar);
                if (urlVar == null)
                {
                    return Settings.BaseUrl;
                }

                return new Uri(urlVar);
            }
        }
    }
}
