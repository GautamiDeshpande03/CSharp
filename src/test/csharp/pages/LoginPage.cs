/*
 LoginPage POM
 - All interactions for login are encapsulated here
 - Uses placeholder XPaths (as requested). Replace with real locators if available.
*/
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
namespace Project.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        // Placeholder locators (use 'use placeholders' mapping)
        private readonly By _usernameInput = By.XPath("//input[@id='user-name' or @placeholder='username' or contains(@name,'user')]");
        private readonly By _passwordInput = By.XPath("//input[@id='password' or @placeholder='password' or contains(@name,'pass')]");
        private readonly By _loginButton = By.XPath("//input[@id='login-button' or @type='submit' or //button[contains(.,'Login')]]");
        public LoginPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
        }
        /// <summary>
        /// Perform login with provided username and password.
        /// Waits for Products page to load (basic check).
        /// </summary>
        public void Login(string username, string password)
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(_usernameInput)).Clear();
            _driver.FindElement(_usernameInput).SendKeys(username);
            _wait.Until(ExpectedConditions.ElementIsVisible(_passwordInput)).Clear();
            _driver.FindElement(_passwordInput).SendKeys(password);
            _wait.Until(ExpectedConditions.ElementToBeClickable(_loginButton)).Click();
            // Wait for known products page artifact - placeholder xpath for inventory container
            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class,'inventory_list') or contains(@class,'products')]")));
        }
    }
}