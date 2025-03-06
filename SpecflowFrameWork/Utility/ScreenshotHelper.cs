using OpenQA.Selenium;

namespace SpecFlowFramework.Utility
{
    internal class ScreenshotHelper
    {
        public static (string screenshotFileName, string screenshotFullPath) TakeScreenshot(IWebDriver webDriver, string destFolder, string testName)
        {
            try
            {
                var fileName = GetScreenshotFileName(testName);
                var fullPath = Path.Combine(destFolder, fileName);

                System.IO.Directory.CreateDirectory(destFolder);

                Screenshot ss = ((ITakesScreenshot)webDriver).GetScreenshot();
                ss.SaveAsFile(fullPath);

                return (fileName, fullPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private static string GetScreenshotFileName(string testName)
        {
            string cleanTestName = testName.Trim().Replace(" ", "_");

            return $"{cleanTestName}_{DateTime.UtcNow.ToString("MM-dd-HH-mm-ss")}.png";
        }
    }
}
