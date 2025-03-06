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
    public class CheckoutPage : PageModel
    {
        public CheckoutPage(GlobalSettings settings) : base(settings)
        {
        }

        private IWebElement _productNameInCheckoutPage => driver.WaitForVisible(By.XPath("//strong[@class='product-item-name']"));
        private IWebElement _noOfOrdersInCart => driver.WaitForVisible(By.XPath("//span[text()='Item in Cart']//preceding-sibling::span[@data-bind]"));
        private IWebElement _firstName => driver.WaitForVisible(By.XPath("//input[@name='firstname']"));
        private IWebElement _lastName => driver.WaitForVisible(By.XPath("//input[@name='lastname']"));
        private IWebElement _streetAddess => driver.WaitForVisible(By.XPath("//input[@name='street[0]']"));
        private IWebElement _city => driver.WaitForVisible(By.XPath("//input[@name='city']"));
        private IWebElement _selectState => driver.WaitForVisible(By.XPath("//select[@name='region_id']"));
        private IWebElement _zip => driver.WaitForVisible(By.XPath("//input[@name='postcode']"));
        private IWebElement _phoneNo => driver.WaitForVisible(By.XPath("//input[@name='telephone']"));
        private IWebElement _country => driver.WaitForVisible(By.XPath("//select[@name='country_id']"));
        private IWebElement _bestwayShippingMethod => driver.WaitForVisible(By.XPath($"//input[@value='tablerate_bestway']"));
        private IWebElement _nextButton => driver.WaitForVisible(By.XPath("//span[text()='Next']"));
        private IWebElement _orderSummaryQuantity(string productName) => driver.WaitForVisible(By.XPath($"//span[text()='Order Summary']/..//strong[text()='{productName}']/..//div[@class='details-qty']//span[@class='value']"));
        private IWebElement _orderSummaryPrice(string productName) => driver.WaitForVisible(By.XPath($"//span[text()='Order Summary']/..//strong[text()='{productName}']/../../..//div[@class='subtotal']//span[@class='price']"));

        public string GetQuantitiesInCheckOutPage()
        {
            return _noOfOrdersInCart.Text;
        }

        public void ClickOnQuantitiesInCheckOutPage()
        {
            _noOfOrdersInCart.Click();
        }

        public string GetProductNameInCheckOutPage()
        {
            return _productNameInCheckoutPage.Text;
        }

        public void ClickOnShippingMethod()
        {
            _bestwayShippingMethod.Click();
        }
        public void ClickOnNextButton()
        {
            _nextButton.Click();
        }

        public void WaitForLoader()
        {
            driver.WaitForElementNotExist(By.XPath("//img[contains(@title,'Loading')]"));
        }

        public string GetQuantityInOrderSummary(string productName)
        {
            return _orderSummaryQuantity(productName).Text;
        }

        public string GetPriceInOrderSummary(string productName)
        {
            return _orderSummaryPrice(productName).Text;
        }



    }
}
