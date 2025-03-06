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
    public class ShoppingCartPage : PageModel
    {
        public ShoppingCartPage(GlobalSettings settings) : base(settings)
        {
        }

        private bool _shoppingCartTable => driver.DoElementsExist(By.XPath("//table[@id='shopping-cart-table']"));
        private IWebElement _shoppingCartPrice(string productName) => driver.WaitForVisible(By.XPath($"//table[@id='shopping-cart-table']//td//a[text()='{productName}']/../../..//following-sibling::td[@class='col price']//span[@class='price']"));
        private IWebElement _shoppingCartQunatities(string productName) => driver.WaitForVisible(By.XPath($"//table[@id='shopping-cart-table']//td//a[text()='{productName}']/../../..//following-sibling::td[@class='col qty']//input[@value]"));
        private IWebElement _shoppingCartSubTotal(string productName) => driver.WaitForVisible(By.XPath($"//table[@id='shopping-cart-table']//td//a[text()='{productName}']/../../..//following-sibling::td[@class='col subtotal']//span[@class='price']"));
        private bool _verifyProductNameInCheckOut(string productName) => driver.IsElementPresent(By.XPath($"//td//a[text()='{productName}']"));
        private IWebElement _updateShoppingCart => driver.WaitForVisible(By.XPath("//span[text()='Update Shopping Cart']"));
        private IWebElement _proceedToCheckOut => driver.WaitForVisible(By.XPath("//span[text()='Proceed to Checkout']"));
        private IWebElement _orderTotal => driver.WaitForVisible(By.XPath("//tr[@class='grand totals']//td//span[@class='price']"));
        
        public bool VerifyShoppingCartTable()
        {
            return _shoppingCartTable;
        }

        public bool VerifyProductNameInCheckOutPage(string productName)
        {
            return _verifyProductNameInCheckOut(productName);
        }

        public void UpdatequnatityInCheckoutPage(string productName, string noOfItems)
        {
            _shoppingCartQunatities(productName).ClearAndSendKeys(noOfItems);
        }

        public string GetPriceInShoppingCartPage(string productName)
        {
            return _shoppingCartPrice(productName).Text;
        }

        public string GetQuantityInShoppingCartPage(string productName)
        {
            return _shoppingCartQunatities(productName).GetAttribute("value");
        }

        public string GetSubTotalInShoppingCartPage(string productName)
        {
            return _shoppingCartSubTotal(productName).Text;
        }

        public void ClickOnUpdateShoppingCart()
        {
            _updateShoppingCart.Click();
            driver.WaitForElementNotExist(By.XPath("//img[contains(@title,'Loading')]"));
        }

        public void ClickOnProceedToCheckOut()
        {
            try
            {
                _proceedToCheckOut.Click();
            }
            catch (StaleElementReferenceException)
            {
                _proceedToCheckOut.Click();
            }
        }

        public string GetOrderTotal()
        {
            try
            {
                return _orderTotal.Text;
            }
            catch (StaleElementReferenceException)
            {
                return _orderTotal.Text;
            }
            
        }
    }
}
