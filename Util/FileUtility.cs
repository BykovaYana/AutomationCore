using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace Deloitte.Automation.Util
{
    public static class FileUtility
    {
        public static string GetFullPath(params string[] filePathPart)
        {
            return Path.Combine(TestContext.CurrentContext.TestDirectory, Path.Combine(filePathPart));
        }

        public static string GetFileWithRetry(string folderPath, string extension, DateTime timeLimit, int retryCount = 6)
        {
            string fileName = string.Empty;
            int i = 0;
            while (i < retryCount)
            {
                try
                {
                    fileName = Directory.GetFiles(folderPath, $"*{extension}", SearchOption.AllDirectories)
                                          .Select(s => new FileInfo(s))
                                          .First(s => s.CreationTime.CompareTo(timeLimit) == 1).Name;
                    break;
                }
                catch (InvalidOperationException)
                {
                    i++;
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                }
            }
            return fileName;

        }

        public static T GetEntityFromJson<T>(string filePath)
        {
            var fileContent = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(fileContent);
        }
    }
}
