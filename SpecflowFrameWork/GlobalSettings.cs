using OpenQA.Selenium;
using SpecFlowFramework.Utility;

namespace SpecFlowFramework
{
    public class GlobalSettings
    {
        private IWebDriver _driver;

        public GlobalSettings()
        {
            ScenarioConfiguration = new StandardScenarioConfiguration();
        }

        public IScenarioConfiguration ScenarioConfiguration { get; set; }

        public ScenarioContext Scenario { get; set; }
        public FeatureContext Feature { get; set; }

        public IWebDriver WebDriver
        {
            get { return _driver; }
            set { _driver = value; }
        }

        public static Dictionary<string, object> globalData = new Dictionary<string, object>();

        public static string Environment = AppSettingsHelper.GetAppSettingsValue("ENV_NAME");
        public static string Browser = AppSettingsHelper.GetAppSettingsValue("BrowserType");
        public static string UserName = AppSettingsHelper.GetAppSettingsValue("UserName");
        public static string UserPassword = AppSettingsHelper.GetAppSettingsValue("UserPassword");
        public static string Url = AppSettingsHelper.GetAppSettingsValue("ApplicationUrl");
        private static string _baseURL;
        public static string BaseURL
        {
            get
            {
                _baseURL = Url;
                return _baseURL;
            }
            set { _baseURL = value; }
        }

        
    }
}
