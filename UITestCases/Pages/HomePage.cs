using OpenQA.Selenium;
using SpecFlowFramework;
using SpecFlowFramework.PageObjects;
using SpecFlowFramework.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UITestCases.Pages
{
    public class HomePage : PageModel
    {
        public HomePage(GlobalSettings settings) : base(settings)
        {
        }

        private bool _verifyHomePageSignInButton => driver.IsElementPresent(By.XPath("//div[@class='panel header']//a[contains(text(),'Sign In')]"));
        private IWebElement _homePageSignInButton => driver.WaitForVisible(By.XPath("//div[@class='panel header']//a[contains(text(),'Sign In')]"));
        private bool _verifyWelecome => driver.DoElementsExist(By.XPath("//div[@class='panel header']//span[@class='logged-in']"));
        private IWebElement _menuItem(string item) => driver.WaitForVisible(By.XPath($"//nav[@data-action='navigation']//span[text()='{item}']"));
        private IWebElement _subMenuItem(string item) => driver.WaitForVisible(By.XPath("//span[text()='Watches']"));
   
        public void NavigateToApplication()
        {
            driver.Navigate().GoToUrl(GlobalSettings.Url);
        }

        public bool VerifySIgnInButton()
        {
            return _verifyHomePageSignInButton;
        }

        public void ClickOnHomePageSIgnInButton()
        {
            _homePageSignInButton.Click();
        }
        public bool VerifyWelcomeText()
        {
            return _verifyWelecome;
        }

        public void ClickOnMenuItem(string  item)
        {
            _menuItem(item).MouseHoverAction(driver);
        }
        public void ClickOnSubMenuItem(string item)
        {
            _subMenuItem(item).Click();
        }


        
        

        

        

        
        

        

        

        

        

       

        

    }
}
