using NUnit.Framework;
using SauceDemo.Framework;
using SauceDemo.PageObjects;
namespace SauceDemo.Tests
{
    /// <summary>
    /// TC_AIGPMM-38_001
    /// End-to-end checkout from Cart → Fill Info → Overview → Finish for single product
    /// Test data: standard_user / secret_sauce ; John Carter ; 94105 ; Sauce Labs Backpack
    /// </summary>
    [TestFixture]
    public class TC_AIGPMM_38_001_Test : BaseTest
    {
        [Test]
        public void CompleteSingleProductCheckout_EndToEnd()
        {
            var login = new LoginPage(Driver);
            login.Login("standard_user", "secret_sauce");
            var products = new ProductsPage(Driver);
            products.AddProductToCart("Sauce Labs Backpack");
            products.OpenCart();
            var cart = new CartPage(Driver);
            var items = cart.GetCartItemNames();
            Assert.IsTrue(items.Exists(n => n.Contains("Sauce Labs Backpack")), "Sauce Labs Backpack should be in the cart");
            cart.ClickCheckout();
            var yourInfo = new CheckoutYourInfoPage(Driver);
            yourInfo.EnterYourInformation("John", "Carter", "94105");
            yourInfo.ClickContinue();
            var overview = new CheckoutOverviewPage(Driver);
            Assert.IsTrue(overview.IsAtOverview(), "Should be on Checkout: Overview page");
            Assert.IsTrue(overview.IsPaymentInfoDisplayed(), "Payment Information should be displayed");
            Assert.IsTrue(overview.IsShippingInfoDisplayed(), "Shipping Information should be displayed");
            overview.ClickFinish();
            var finish = new FinishPage(Driver);
            Assert.IsTrue(finish.IsThankYouMessageDisplayed(), "Finish page should display THANK YOU message");
            Assert.IsTrue(finish.IsPonyExpressLogoDisplayed(), "Pony Express logo should be visible on Finish page");
        }
    }
}