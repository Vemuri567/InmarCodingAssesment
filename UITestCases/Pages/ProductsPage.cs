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
    public class ProductsPage : PageModel
    {
        public ProductsPage(GlobalSettings settings) : base(settings)
        {
        }

        private IReadOnlyCollection<IWebElement> _shoppingOptions => driver.WaitForElements(By.XPath("//div[@class='filter-options']//div[@class='filter-options-title']"));
        private IWebElement _shoppingOptionsCategory(string category) => driver.WaitForVisible(By.XPath($"//div[@class='filter-options']//div[text()='{category}']"));
        private IWebElement _filterCategory(string name) => driver.WaitForVisible(By.XPath($"//div[@class='filter-options']//a[contains(text(),'{name}')]"));
        private bool _verifyProductName(string name) => driver.IsElementPresent(By.XPath($"//a[@class='product-item-link' and contains(text(),'{name}')]"));
        private IWebElement _productName(string name) => driver.WaitForVisible(By.XPath($"//a[@class='product-item-link' and contains(text(),'{name}')]"));

        public int ShoppingOptionsCount()
        {
            return _shoppingOptions.Count();
        }

        public void ClickOnShoppingOptionsByCategory(string category)
        {
            _shoppingOptionsCategory(category).Click();
        }

        public void ClickOnShoppingOptionsBySubCategory(string name)
        {
            _filterCategory(name).Click();
        }

        public bool VerifyProductName(string name)
        {
            return _verifyProductName(name);
        }

        public void ClickOnProductName(string name)
        {
            _productName(name).Click();
        }

    }
}
