using OpenQA.Selenium;
using SauceDemo.Framework;
namespace SauceDemo.PageObjects
{
    /// <summary>
    /// Page object for the Login page.
    /// All locators are placeholders as requested ("use placeholders").
    /// Replace XPaths with exact locators if/when provided.
    /// </summary>
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        // Placeholder locators (use exact locators when available)
        private static readonly By UsernameField = By.XPath("//input[@id='user-name']"); // placeholder
        private static readonly By PasswordField = By.XPath("//input[@id='password']"); // placeholder
        private static readonly By LoginButton = By.XPath("//input[@id='login-button']"); // placeholder
        private static readonly By ProductsPageIdentifier = By.XPath("//div[contains(@class,'inventory_list')]"); // placeholder for Products page load
        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public void Login(string username, string password)
        {
            var userEl = WaitHelpers.WaitForElementVisible(_driver, UsernameField);
            userEl.Clear();
            userEl.SendKeys(username);
            var passEl = WaitHelpers.WaitForElementVisible(_driver, PasswordField);
            passEl.Clear();
            passEl.SendKeys(password);
            var loginBtn = WaitHelpers.WaitForElementVisible(_driver, LoginButton);
            loginBtn.Click();
            // Wait for Products page to load
            WaitHelpers.WaitForElementVisible(_driver, ProductsPageIdentifier, 10);
        }
        public bool IsAtLoginPage()
        {
            try
            {
                return WaitHelpers.WaitForElementVisible(_driver, LoginButton, 5) != null;
            }
            catch
            {
                return false;
            }
        }
    }
}