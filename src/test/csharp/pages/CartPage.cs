/*
 CartPage POM
 - Methods to verify cart contents and navigate to checkout
 - Placeholder locators used for cart items and checkout button
*/
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Linq;
namespace Project.Pages
{
    public class CartPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly By _cartItemsContainer = By.XPath("//div[contains(@class,'cart_list') or contains(@class,'cart_contents')]");
        private readonly By _checkoutButton = By.XPath("//button[contains(.,'Checkout') or @id='checkout']");
        // Product row relative xpath
        private readonly string _productRowXPathTemplate = "//div[contains(@class,'cart_item')][.//div[contains(.,\"{0}\")]]";
        public CartPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            _wait.Until(ExpectedConditions.ElementIsVisible(_cartItemsContainer));
        }
        public bool IsOnCartPage()
        {
            return _driver.Url.Contains("/cart") || _wait.Until(d => d.FindElement(_cartItemsContainer).Displayed);
        }
        public bool IsProductInCart(string productName)
        {
            var productRow = By.XPath(string.Format(_productRowXPathTemplate, productName));
            try
            {
                return _driver.FindElements(productRow).Any();
            }
            catch
            {
                return false;
            }
        }
        public void ClickCheckout()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(_checkoutButton)).Click();
            // Wait for checkout info page
            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(.,'Checkout: Your Information') or //input[@id='first-name'] or //input[@name='firstName']]")));
        }
    }
}