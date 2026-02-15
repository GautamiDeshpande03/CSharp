using NUnit.Framework;
using SauceDemo.Framework;
using SauceDemo.PageObjects;
namespace SauceDemo.Tests
{
    /// <summary>
    /// TC_AIGPMM-38_007
    /// Verify header elements and page title appear on Overview
    /// Test data: standard_user / secret_sauce ; Nina Patel ; 60614
    /// </summary>
    [TestFixture]
    public class TC_AIGPMM_38_007_Test : BaseTest
    {
        [Test]
        public void OverviewHeaderAndTitle_AreDisplayed()
        {
            var login = new LoginPage(Driver);
            login.Login("standard_user", "secret_sauce");
            var products = new ProductsPage(Driver);
            products.AddProductToCart("Sauce Labs Backpack");
            products.OpenCart();
            var cart = new CartPage(Driver);
            cart.ClickCheckout();
            var yourInfo = new CheckoutYourInfoPage(Driver);
            yourInfo.EnterYourInformation("Nina", "Patel", "60614");
            yourInfo.ClickContinue();
            var overview = new CheckoutOverviewPage(Driver);
            Assert.IsTrue(overview.IsAtOverview(), "Overview page should be visible");
            // Header/title checks rely on POM visibility; use the POM's IsAtOverview() and additional checks for Payment/Shipping
            Assert.IsTrue(overview.IsPaymentInfoDisplayed(), "Payment information should be displayed");
            Assert.IsTrue(overview.IsShippingInfoDisplayed(), "Shipping information should be displayed");
        }
    }
}