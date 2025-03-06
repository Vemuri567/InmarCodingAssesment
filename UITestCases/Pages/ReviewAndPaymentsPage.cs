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
    internal class ReviewAndPaymentsPage : PageModel
    {
        public ReviewAndPaymentsPage(GlobalSettings settings) : base(settings)
        {
        }

        private IWebElement _orderTotal => driver.WaitForVisible(By.XPath("//tr[@class='grand totals']//td//span[@class='price']"));
        
        private IWebElement _placeOrder => driver.WaitForVisible(By.XPath("//span[text()='Place Order']"));
        
        private IWebElement _shippingMethodInReviewPage => driver.WaitForVisible(By.XPath("//span[@class='label' and text()='Shipping']//following-sibling::span"));
        private IWebElement _shippingMethodPriceInReviewPage => driver.WaitForVisible(By.XPath("//tr[@class='totals shipping excl']//td//span[@data-th='Shipping']"));
        private IWebElement _reviewPagecartSubTotal => driver.WaitForVisible(By.XPath("//span[@data-th='Cart Subtotal']"));
        private bool _verifypaymentMethodBillingAddressPage => driver.DoElementsExist(By.XPath("//div[@class='payment-method-billing-address']//div[@class='billing-address-details']"));
        private IWebElement _paymentMethodBillingAddress => driver.WaitForVisible(By.XPath("//div[@class='payment-method-billing-address']//div[@class='billing-address-details']"));
        private IWebElement _quantityInReviewPage => driver.WaitForVisible(By.XPath("//div[@class='details-qty']//span[@class='value']"));
        private IWebElement _priceInReviewPage => driver.WaitForVisible(By.XPath("//div[@class='subtotal']//span[@class='price']"));

        public string GetQuantityInOrderSUmmaryPage()
        {
            return _quantityInReviewPage.Text;
        }

        public string GetPriceInOrderSUmmaryPage()
        {
            return _priceInReviewPage.Text;
        }
        public string GetShippingMethod()
        {
            return _shippingMethodInReviewPage.Text;
        }

        public string GetShippingTotalPrice()
        {
            return _shippingMethodPriceInReviewPage.Text;
        }

        public string GetOrderTotal()
        {
            return _orderTotal.Text;
        }
      

        public void ClickOnPlaceOrder()
        {
            _placeOrder.Click();
        }

  
        public bool VerifyPaymentMethodPage()
        {
            return _verifypaymentMethodBillingAddressPage;
        }

        public string GetBillingAddress()
        {
            return _paymentMethodBillingAddress.Text;
        }

        public string GetCartSubTotalInOrderSummary()
        {
            return _reviewPagecartSubTotal.Text;
        }
    }
}
