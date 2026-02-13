/*
 FinishPage POM
 - Methods to validate success message and logo on the finish page
 - Uses placeholder locators for message and logo
*/
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
namespace Project.Pages
{
    public class FinishPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly By _thankYouMessage = By.XPath("//h2[contains(translate(.,'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),'THANK YOU') or contains(.,'THANK YOU FOR YOUR ORDER')]");
        private readonly By _ponyExpressLogo = By.XPath("//img[contains(@alt,'Pony Express') or contains(@src,'pony') or contains(@class,'pony')]");
        public FinishPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            // Wait is lazy; tests call methods to check visibility
        }
        public bool IsThankYouMessageDisplayed()
        {
            try
            {
                return _wait.Until(ExpectedConditions.ElementIsVisible(_thankYouMessage)).Displayed;
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
                return _driver.FindElements(_ponyExpressLogo).Count > 0 && _driver.FindElement(_ponyExpressLogo).Displayed;
            }
            catch
            {
                return false;
            }
        }
    }
}