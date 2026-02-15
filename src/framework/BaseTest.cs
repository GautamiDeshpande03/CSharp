using NUnit.Framework;
using OpenQA.Selenium;
namespace SauceDemo.Framework
{
    public class BaseTest
    {
        protected IWebDriver Driver;
        protected string BaseUrl = "https://www.saucedemo.com/"; // From traversal steps
        [SetUp]
        public void SetUp()
        {
            Driver = DriverFactory.CreateDriver(headless: false);
            Driver.Navigate().GoToUrl(BaseUrl);
            WaitHelpers.WaitForPageLoad(Driver);
        }
        [TearDown]
        public void TearDown()
        {
            if (Driver != null)
            {
                try
                {
                    Driver.Quit();
                }
                catch
                {
                    // swallow exceptions in teardown to not mask test results
                }
            }
        }
    }
}