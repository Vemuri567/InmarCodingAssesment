using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow.Infrastructure;

namespace SpecFlowFramework.Utility
{
    internal class TestContextHelper
    {
        private static string GetTestName(TestContext currentContext)
        {
            return currentContext.Test.MethodName;
        }

        private static void AttachFailureScreenshot(string screenshotFileNameAndPath)
        {
            // Add attachment to test file
            TestContext.AddTestAttachment(screenshotFileNameAndPath);
        }

        private static void AttachFailureScreenshotsToLivingDoc(ISpecFlowOutputHelper specFlowOutputHelper, string screenshotFileName)
        {
            AddAttachmentToLivingDoc(specFlowOutputHelper,screenshotFileName);
        }

        private static void AddAttachmentToLivingDoc(ISpecFlowOutputHelper specFlowOutputHelper, string screenshotFileName)
        {
           
           string  addDownloadLink = $"file://{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}/Screenshots/{screenshotFileName}";
           specFlowOutputHelper.AddAttachment(addDownloadLink);
        }

        private static void AttachTestTextDetails(TestContext currentContext)
        {
            var fileAndPath = Path.Combine(currentContext.WorkDirectory, "StandardOut.log");
            File.WriteAllText(fileAndPath, "Placeholder");
            TestContext.AddTestAttachment(fileAndPath);
        }

        internal static void AttachResultsToTest(IWebDriver webDriver, ISpecFlowOutputHelper specFlowOutputHelper, TestContext currentContext, string outcome)
        {
            string testName = GetTestName(currentContext);
            if (outcome.Equals("Failed"))
            {
                (string screenshotFileName, string screenshotFileNameAndPath) = ScreenshotHelper.TakeScreenshot(webDriver, "Screenshots", testName);

                AttachFailureScreenshot(screenshotFileNameAndPath);
                AttachFailureScreenshotsToLivingDoc(specFlowOutputHelper, screenshotFileName);
            }
            else
            {
                AttachTestTextDetails(currentContext);
            }

            
        }
    }
}
