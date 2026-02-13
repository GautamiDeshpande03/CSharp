/*
 BaseTest - central test setup and teardown
 - Initializes ChromeDriver per test (clean session)
 - Provides WebDriverWait instance for pages
 - Provides BaseUrl (application URL)
 Notes:
  - Uses placeholders/defaults; change driver/options as needed for CI
  - Tests inherit from this base class
*/
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
namespace Project.Core
{
    public abstract class BaseTest
    {
        protected IWebDriver Driver { get; private set; }
        protected WebDriverWait Wait { get; private set; }
        // Use traversal URL (explicitly provided in traversal steps)
        protected string BaseUrl => "https://www.saucedemo.com/";
        [SetUp]
        public virtual void SetUp()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--start-maximized");
            // Add headless if required: chromeOptions.AddArgument("--headless");
            Driver = new ChromeDriver(chromeOptions);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2); // short implicit; prefer explicit waits
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));
        }
        [TearDown]
        public virtual void TearDown()
        {
            try
            {
                Driver?.Quit();
            }
            catch (Exception)
            {
                // Ignore errors on quit
            }
        }
    }
}