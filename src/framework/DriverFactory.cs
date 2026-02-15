using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
namespace SauceDemo.Framework
{
    public static class DriverFactory
    {
        // Creates and returns a configured ChromeDriver instance.
        // Note: Ensure chromedriver is available on PATH or use WebDriverManager in CI.
        public static IWebDriver CreateDriver(bool headless = false)
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--start-maximized");
            if (headless)
            {
                chromeOptions.AddArgument("--headless=new");
            }
            // Disable infobars and extensions for stability in CI
            chromeOptions.AddArgument("--disable-extensions");
            chromeOptions.AddArgument("--disable-infobars");
            chromeOptions.AddArgument("--disable-gpu");
            // For demo purposes: set implicit wait low; explicit waits used in POMs
            var driver = new ChromeDriver(chromeOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            return driver;
        }
    }
}