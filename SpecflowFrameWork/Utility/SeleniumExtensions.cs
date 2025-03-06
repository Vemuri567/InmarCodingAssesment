using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using TechTalk.SpecFlow.Infrastructure;
using OpenQA.Selenium.Interactions;

namespace SpecFlowFramework.Utility
{
    public static class SeleniumExtensions
    {
        public static ISpecFlowOutputHelper OutputHelper { get; set; }


        public static void ClearAndSendKeys(this IWebElement element, string text)
        {
            OutputHelper.WriteLine($"Clearing the field and sending keys: {text}");
            element.Clear();
            element.SendKeys(text);
        }

        public static IWebElement WaitForVisible(this IWebDriver driver, By by, TimeSpan? tSpan = null)
        {
            OutputHelper.WriteLine($"Waiting on element to be visible by XPath: {by}");
            WebDriverWait wait = new WebDriverWait(driver, tSpan ?? TimeSpan.FromSeconds(20))
            { PollingInterval = TimeSpan.FromSeconds(2) };

            return wait.Until(drv => drv.FindElement(by));
        }

        public static string GetText(this IWebDriver driver, By by, TimeSpan? tSpan = null)
        {
            OutputHelper.WriteLine($"Waiting on element to be visible by XPath: {by}");
            IWebElement element = driver.WaitForVisible(by, tSpan);
            string text=element.Text;
            OutputHelper.WriteLine(text);
            return text;
        }
        public static IReadOnlyCollection<IWebElement> WaitForElements(this IWebDriver driver, By by, TimeSpan? tspan = null)
        {
            OutputHelper.WriteLine($"Waiting on elements by XPath: {by}");
            WebDriverWait wait = new WebDriverWait(driver, tspan ?? TimeSpan.FromSeconds(20)) { PollingInterval = TimeSpan.FromSeconds(2) };
            return wait.Until(drv => drv.FindElements(by));
        }
        
        /// An expectation for checking that all elements present on the web page that match the Locator.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <returns>The list of <see cref="IWebElement"/> once it is located.</returns>
        public static Func<IWebDriver, IReadOnlyCollection<IWebElement>> PresenceOfAllElementsLocatedBy(By locator)
        {
            OutputHelper.WriteLine($"Trying to find elements at: {locator}");

            return (driver) =>
            {
                try
                {
                    var elements = driver.FindElements(locator);
                    return elements.Any() ? elements : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        public static IReadOnlyCollection<IWebElement> WaitForElementsToExist(this IWebDriver driver, By by, TimeSpan? tSpan = null)
        {
            // TODO: How to log element this?
            OutputHelper.WriteLine($"Waiting for element to exist at: {by}");

            WebDriverWait wait = new WebDriverWait(driver, tSpan ?? TimeSpan.FromSeconds(20));
            return wait.Until(PresenceOfAllElementsLocatedBy(by));
        }

        
        public static bool DoElementsExist(this IWebDriver driver, By by, TimeSpan? tSpan = null)
        {
            OutputHelper.WriteLine($"Checking if element exists: {by}");
            driver.Manage().Timeouts().PageLoad.Add(System.TimeSpan.FromSeconds(30));

            try
            {
                driver.WaitForElementsToExist(by, tSpan ?? TimeSpan.FromSeconds(20));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsElementPresent(this IWebDriver driver, By by)
        {
            OutputHelper.WriteLine($"Checking to see if element is present: {by}");

            try
            {
                var elements = driver.FindElements(by);
                if (elements.Count != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static void MouseHoverAction(this IWebElement el, IWebDriver driver)
        {
            OutputHelper.WriteLine($"Trying to hover over element");
            Actions builder = new Actions(driver);
            builder.MoveToElement(el).Perform();
        }

        public static void WaitForElementNotExist(this IWebDriver driver, By by)
        {
            OutputHelper.WriteLine($"Waiting for element to not exist: {by}");
            WaitForElementNotExist(driver, by, null);
        }

        public static void WaitForElementNotExist(this IWebDriver driver, By by, TimeSpan? tp = null)
        {
            OutputHelper.WriteLine($"Waiting for element to not exist: {by}");
            // Total maximum wait time of 20 seconds
            var totalWaitTime = tp ?? TimeSpan.FromSeconds(30);

            // First 10 seconds: wait for the element to be visible (or do nothing if already visible)
            new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(d =>
            {
                // Wait until the element is either visible or we have checked for 10 seconds
                if (!IsElementPresent(d, by))
                {
                    return false; // If it's not present, exit the wait (no element to wait for)
                }
                return true; // Continue waiting if element is found
            });

            // Second stage (next 10 seconds): wait for the element to no longer exist (be invisible or removed)
            new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(d =>
            {
                // The element should no longer be present or visible
                return !IsElementPresent(d, by);
            });
        }

    }
}

