using OpenQA.Selenium;
using SauceDemo.Framework;
namespace SauceDemo.PageObjects
{
    /// <summary>
    /// Finish/Confirmation page POM.
    /// </summary>
    public class FinishPage
    {
        private readonly IWebDriver _driver;
        private static readonly By ThankYouHeader = By.XPath("//h2[contains(.,'THANK YOU FOR YOUR ORDER') or contains(.,'Thank you')]");
        private static readonly By PonyExpressLogo = By.XPath("//img[contains(@alt,'Pony Express') or contains(@src,'pony')]"); // placeholder
        public FinishPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public bool IsThankYouMessageDisplayed()
        {
            try
            {
                var el = WaitHelpers.WaitForElementVisible(_driver, ThankYouHeader, 5);
                return el != null && !string.IsNullOrWhiteSpace(el.Text);
            }
            catch
            {
                return false;
            }
        }
        public bool IsPonyExpressLogoDisplayed()
        {
            try
            {
                var el = WaitHelpers.WaitForElementVisible(_driver, PonyExpressLogo, 5);
                return el != null && el.Displayed;
            }
            catch
            {
                return false;
            }
        }
    }
}