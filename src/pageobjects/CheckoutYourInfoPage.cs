using OpenQA.Selenium;
using SauceDemo.Framework;
namespace SauceDemo.PageObjects
{
    /// <summary>
    /// Checkout: Your Information page POM.
    /// </summary>
    public class CheckoutYourInfoPage
    {
        private readonly IWebDriver _driver;
        private static readonly By FirstNameField = By.XPath("//input[@id='first-name']"); // placeholder
        private static readonly By LastNameField = By.XPath("//input[@id='last-name']"); // placeholder
        private static readonly By PostalCodeField = By.XPath("//input[@id='postal-code']"); // placeholder
        private static readonly By ContinueButton = By.XPath("//input[@id='continue' or @value='Continue' or contains(.,'Continue')]"); // placeholder
        private static readonly By CancelButton = By.XPath("//button[contains(.,'Cancel')]"); // placeholder
        private static readonly By ErrorMessageContainer = By.XPath("//h3[contains(@data-test,'error') or contains(@class,'error')]"); // placeholder
        public CheckoutYourInfoPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public void EnterYourInformation(string firstName, string lastName, string postalCode)
        {
            var fn = WaitHelpers.WaitForElementVisible(_driver, FirstNameField, 10);
            fn.Clear();
            fn.SendKeys(firstName);
            var ln = WaitHelpers.WaitForElementVisible(_driver, LastNameField, 5);
            ln.Clear();
            ln.SendKeys(lastName);
            var pc = WaitHelpers.WaitForElementVisible(_driver, PostalCodeField, 5);
            pc.Clear();
            pc.SendKeys(postalCode);
        }
        public void ClickContinue()
        {
            var cont = WaitHelpers.WaitForElementVisible(_driver, ContinueButton, 10);
            cont.Click();
        }
        public void ClickCancel()
        {
            var cancel = WaitHelpers.WaitForElementVisible(_driver, CancelButton, 5);
            cancel.Click();
        }
        public bool IsErrorDisplayed()
        {
            try
            {
                var el = WaitHelpers.WaitForElementVisible(_driver, ErrorMessageContainer, 3);
                return el != null && !string.IsNullOrWhiteSpace(el.Text);
            }
            catch
            {
                return false;
            }
        }
        public string GetErrorMessageText()
        {
            try
            {
                var el = WaitHelpers.WaitForElementVisible(_driver, ErrorMessageContainer, 3);
                return el.Text.Trim();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}