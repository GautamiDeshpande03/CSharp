/*
 CheckoutInformationPage POM
 - Encapsulates entering first/last/postal code and actions: Continue, Cancel
 - Includes validation detection for missing fields
 - Uses placeholder locators
*/
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
namespace Project.Pages
{
    public class CheckoutInformationPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly By _firstNameInput = By.XPath("//input[@id='first-name' or @name='firstName' or @placeholder='First Name']");
        private readonly By _lastNameInput = By.XPath("//input[@id='last-name' or @name='lastName' or @placeholder='Last Name']");
        private readonly By _postalCodeInput = By.XPath("//input[@id='postal-code' or @name='postalCode' or @placeholder='Zip/Postal Code']");
        private readonly By _continueButton = By.XPath("//input[@id='continue' or //button[contains(.,'Continue')]]");
        private readonly By _cancelButton = By.XPath("//button[contains(.,'Cancel') or @id='cancel']");
        private readonly By _errorMessage = By.XPath("//h3[contains(@data-test,'error') or contains(@class,'error-message') or contains(.,'Error')]");
        public CheckoutInformationPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            // Do not block here; caller should be on this page
        }
        public bool IsOnCheckoutInformationPage()
        {
            try
            {
                return _driver.Url.Contains("/checkout-step-one") || _wait.Until(d => d.FindElement(_firstNameInput).Displayed);
            }
            catch
            {
                return false;
            }
        }
        public void EnterCheckoutInformation(string firstName, string lastName, string postalCode)
        {
            // First Name
            var first = _wait.Until(ExpectedConditions.ElementIsVisible(_firstNameInput));
            first.Clear();
            if (!string.IsNullOrEmpty(firstName))
            {
                first.SendKeys(firstName);
            }
            // Last Name
            var last = _wait.Until(ExpectedConditions.ElementIsVisible(_lastNameInput));
            last.Clear();
            if (!string.IsNullOrEmpty(lastName))
            {
                last.SendKeys(lastName);
            }
            // Postal Code
            var zip = _wait.Until(ExpectedConditions.ElementIsVisible(_postalCodeInput));
            zip.Clear();
            if (!string.IsNullOrEmpty(postalCode))
            {
                zip.SendKeys(postalCode);
            }
        }
        public void ClickContinue()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(_continueButton)).Click();
            // If validation occurs, an error message may appear - calling test must verify
        }
        public void ClickCancel()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(_cancelButton)).Click();
            // Wait for cart or products page artifact (non-blocking)
            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class,'cart_list') or contains(.,'Products')]")));
        }
        public bool IsValidationErrorDisplayed()
        {
            try
            {
                return _driver.FindElements(_errorMessage).Count > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}