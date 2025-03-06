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
    public class OrderConfirmationPage : PageModel
    {
        public OrderConfirmationPage(GlobalSettings settings) : base(settings)
        {
        }
        private string _orderNo => driver.GetText(By.XPath("//p[text()='Your order number is: ']//a[@class='order-number']//strong"));
        public string GetOrderNo()
        {
            return _orderNo;
        }
    }
}
