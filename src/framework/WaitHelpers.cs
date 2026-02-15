using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
namespace SauceDemo.Framework
{
    public static class WaitHelpers
    {
        public static IWebElement WaitForElementVisible(IWebDriver driver, By locator, int timeoutInSeconds = 15)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }
        public static bool WaitUntilElementInvisible(IWebDriver driver, By locator, int timeoutInSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(locator));
        }
        public static void WaitForPageLoad(IWebDriver driver, int timeoutInSeconds = 15)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(d =>
            {
                try
                {
                    var readyState = ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").ToString();
                    return readyState.Equals("complete", StringComparison.InvariantCultureIgnoreCase);
                }
                catch
                {
                    return false;
                }
            });
        }
    }
}