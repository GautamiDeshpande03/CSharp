/*
 ProductsPage POM
 - Methods to add products to cart and open cart
 - Uses placeholder/dynamic XPaths; productName is matched in product container.
*/
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
namespace Project.Pages
{
    public class ProductsPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        // Placeholder locators
        private readonly By _cartIcon = By.XPath("//a[@class='shopping_cart_link' or contains(@href,'cart') or //button[contains(.,'Cart')]]");
        // Generic product container - dynamic usage in methods
        private readonly By _inventoryContainer = By.XPath("//div[contains(@class,'inventory_list') or contains(@class,'products')]");
        public ProductsPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            _wait.Until(ExpectedConditions.ElementIsVisible(_inventoryContainer));
        }
        /// <summary>
        /// Clicks Add to cart for the provided product name.
        /// Locator uses placeholder XPath pattern; adjust to match actual DOM if necessary.
        /// </summary>
        public void AddProductToCart(string productName)
        {
            // Construct a flexible xpath to find the product's add button based on product name
            var addButtonXpath = By.XPath($"//div[contains(@class,'inventory_item')][.//div[contains(.,\"{productName}\")]]//button[contains(.,'Add to cart') or contains(@class,'btn_inventory')]");
            _wait.Until(ExpectedConditions.ElementToBeClickable(addButtonXpath)).Click();
            // Optional wait: cart badge increments or button text changes to 'Remove'
            _wait.Until(driver =>
            {
                try
                {
                    var btn = driver.FindElement(addButtonXpath);
                    return btn.Text.ToLower().Contains("remove") || btn.GetAttribute("class").Contains("remove");
                }
                catch
                {
                    return false;
                }
            });
        }
        public void OpenCart()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(_cartIcon)).Click();
            // Wait for cart page artifact
            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class,'cart_list') or //div[contains(.,'Your Cart')]]")));
        }
    }
}