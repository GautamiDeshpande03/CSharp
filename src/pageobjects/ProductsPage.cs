using OpenQA.Selenium;
using SauceDemo.Framework;
using System;
using System.Globalization;
using System.Linq;
namespace SauceDemo.PageObjects
{
    /// <summary>
    /// Products page POM. Provides methods to add products by name.
    /// Uses placeholder XPaths. Replace with exact locators when available.
    /// </summary>
    public class ProductsPage
    {
        private readonly IWebDriver _driver;
        // Placeholder: product card container
        private static readonly By ProductCardByNameTemplate = By.XPath("//div[contains(@class,'inventory_item') and .//div[contains(.,'{0}')]]");
        // Placeholder: Add to cart button located inside a product card
        private static readonly By AddToCartButtonRelative = By.XPath(".//button[contains(@class,'btn') and contains(.,'Add to cart')]");
        // Placeholder: Cart icon
        private static readonly By CartIcon = By.XPath("//a[@class='shopping_cart_link']");
        public ProductsPage(IWebDriver driver)
        {
            _driver = driver;
        }
        /// <summary>
        /// Adds a product to cart by its display name.
        /// </summary>
        public void AddProductToCart(string productName)
        {
            var productCardLocator = By.XPath(string.Format(CultureInfo.InvariantCulture, ProductCardByNameTemplate.ToString().Trim('/'), productName));
            // Because we built the XPath template using string.Format on the XPath string,
            // we need to reconstruct the actual By here:
            var productCardXPath = $"//div[contains(@class,'inventory_item') and .//div[contains(.,'{EscapeForXPath(productName)}')]]";
            var productCard = WaitHelpers.WaitForElementVisible(_driver, By.XPath(productCardXPath), 10);
            var addBtn = productCard.FindElements(By.XPath(".//button")).FirstOrDefault();
            if (addBtn == null)
            {
                throw new InvalidOperationException($"Add to cart button not found for product: {productName}");
            }
            addBtn.Click();
        }
        public void OpenCart()
        {
            var cart = WaitHelpers.WaitForElementVisible(_driver, CartIcon, 10);
            cart.Click();
        }
        private static string EscapeForXPath(string value)
        {
            // Simple XPath escape for single quotes by using concat; basic implementation for test strings
            if (!value.Contains("'")) return value;
            var parts = value.Split('\'');
            return string.Join("\", '\"', \"", parts);
        }
    }
}