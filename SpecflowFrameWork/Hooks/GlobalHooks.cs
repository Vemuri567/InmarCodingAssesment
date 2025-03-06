using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using SpecFlowFramework.Utility;
using System.Reflection;
using TechTalk.SpecFlow.Infrastructure;


namespace SpecFlowFramework.Hooks
{
    public class GlobalHooks
    {
        public GlobalSettings _settings;
        public ScenarioContext _scenarioContext;
        public FeatureContext _featureContext;
        public ISpecFlowOutputHelper _specflowOutputHelper;
        public GlobalHooks(GlobalSettings settings,ScenarioContext scenarioContext, FeatureContext featureContext, ISpecFlowOutputHelper specflowOutputHelper)
        {
            _settings = settings;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            _specflowOutputHelper= specflowOutputHelper;
            _settings.Scenario= _scenarioContext;
            _settings.Feature = _featureContext;
            SeleniumExtensions.OutputHelper= _specflowOutputHelper;
        }

        [BeforeScenario]
        public void GetDriver()
        {
            _settings.WebDriver = GetWebDriver();
        }


        [AfterScenario]
        public void KillDriver()
        {
            var currentContext = TestContext.CurrentContext;
            var outcome = currentContext.Result.Outcome.Status.ToString();

            try
            {
                TestContextHelper.AttachResultsToTest(_settings.WebDriver, _specflowOutputHelper, currentContext, outcome);
            }
            catch { }

            if (_settings.WebDriver != null)
            {
              
                _settings.WebDriver.Quit();
            }
        }

         

        public static string GetSolutionDirectoryPath()
        {
            string assemblyDirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Directory.GetParent(assemblyDirPath).Parent.Parent.FullName;

        }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        protected virtual IWebDriver GetWebDriver()
        {
            if (_settings.WebDriver != null)
            {
                return _settings.WebDriver;
            }
            else
            {
                ChromeOptions chromeOptions = new ChromeOptions();
                string driverPath = string.Concat(GetSolutionDirectoryPath(), "\\Executables\\chromedriver.exe");
                chromeOptions.AddArgument("--no-sandbox");
                IWebDriver driver = new ChromeDriver(driverPath, chromeOptions, TimeSpan.FromSeconds(120));
                driver.Manage().Window.Maximize();
                return driver;
            }
        }
    }
}
