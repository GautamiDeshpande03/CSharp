using NUnit.Framework;
using SauceDemo.Framework;
using SauceDemo.PageObjects;
namespace SauceDemo.Tests
{
    /// <summary>
    /// TC_AIGPMM-38_009
    /// Verify Payment Information and Shipping Information sections display on Overview
    /// Test data: standard_user / secret_sauce ; Zoe Lin ; 30303
    /// </summary>
    [TestFixture]
    public class TC_AIGPMM_38_009_Test : BaseTest
    {
        [Test]
        public void Overview_ShowsPaymentAndShippingInformation()
        {
            var login = new LoginPage(Driver);
            login.Login("standard_user", "secret_sauce");
            var products = new ProductsPage(Driver);
            products.AddProductToCart("Sauce Labs Backpack");
            products.OpenCart();
            var cart = new CartPage(Driver);
            cart.ClickCheckout();
            var yourInfo = new CheckoutYourInfoPage(Driver);
            yourInfo.EnterYourInformation("Zoe", "Lin", "30303");
            yourInfo.ClickContinue();
            var overview = new CheckoutOverviewPage(Driver);
            Assert.IsTrue(overview.IsAtOverview(), "Overview page should be visible");
            Assert.IsTrue(overview.IsPaymentInfoDisplayed(), "Payment info should be displayed");
            Assert.IsTrue(overview.IsShippingInfoDisplayed(), "Shipping info should be displayed");
        }
    }
}