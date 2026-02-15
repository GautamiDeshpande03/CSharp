using OpenQA.Selenium;
using SauceDemo.Framework;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
namespace SauceDemo.PageObjects
{
    /// <summary>
    /// Cart page POM.
    /// </summary>
    public class CartPage
    {
        private readonly IWebDriver _driver;
        private static readonly By CheckoutButton = By.XPath("//button[@id='checkout' or contains(.,'Checkout')]"); // placeholder
        private static readonly By CartItemNames = By.XPath("//div[contains(@class,'cart_item')]//div[contains(@class,'inventory_item_name')]"); // placeholder
        private static readonly By CartItemQuantity = By.XPath("//div[contains(@class,'cart_quantity')]"); // placeholder
        public CartPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public IList<string> GetCartItemNames()
        {
            var els = _driver.FindElements(CartItemNames);
            return els.Select(e => e.Text.Trim()).ToList();
        }
        public IList<int> GetCartItemQuantities()
        {
            var els = _driver.FindElements(CartItemQuantity);
            var list = new List<int>();
            foreach (var el in els)
            {
                if (int.TryParse(el.Text.Trim(), out int q))
                {
                    list.Add(q);
                }
                else
                {
                    list.Add(1);
                }
            }
            return list;
        }
        public void ClickCheckout()
        {
            var checkout = WaitHelpers.WaitForElementVisible(_driver, CheckoutButton, 10);
            checkout.Click();
        }
    }
}