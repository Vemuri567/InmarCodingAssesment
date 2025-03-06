using OpenQA.Selenium;
using SpecFlowFramework;
using SpecFlowFramework.PageObjects;
using SpecFlowFramework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UITestCases.Pages
{
    internal class LoginPage : PageModel
    {
        public LoginPage(GlobalSettings settings) : base(settings)
        {
        }


        private bool _verifyEmailTextBox => driver.IsElementPresent(By.Id("email"));
        private IWebElement _emailTextBox => driver.WaitForVisible(By.Id("email"));
        private IWebElement _passwordTextBox => driver.WaitForVisible(By.Name("login[password]"));
         private IWebElement _signInButton => driver.WaitForVisible(By.XPath("//button[@class='action login primary']//span[text()='Sign In']"));
        

        public bool VerifyEmailTextBox()
        {
            return _verifyEmailTextBox;
        }

        public void EnterEmail(string email)
        {
            _emailTextBox.SendKeys(email);
        }

        public void EnterPassword(string password)
        {
            _passwordTextBox.SendKeys(password);
        }

        public void ClickOnSIgnInButton()
        {
            _signInButton.Click();
        }
    }
}
