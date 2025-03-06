using OpenQA.Selenium;

namespace SpecFlowFramework.PageObjects
{
    [Binding]
    public abstract class PageModel
    {
        private readonly GlobalSettings _settings;
        public IWebDriver driver => _settings.WebDriver;
        public GlobalSettings Settings => _settings;

        public PageModel(GlobalSettings settings)
        {
            _settings = settings;
        }
    }
}
