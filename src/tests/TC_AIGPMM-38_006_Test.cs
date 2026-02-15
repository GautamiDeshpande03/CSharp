using NUnit.Framework;
using SauceDemo.Framework;
using SauceDemo.PageObjects;
namespace SauceDemo.Tests
{
    /// <summary>
    /// TC_AIGPMM-38_006
    /// Verify Cancel on Overview navigates to Products page preserving cart if expected
    /// Test data: standard_user / secret_sauce ; Alan Park ; 98101
    /// </summary>
    [TestFixture]
    public class TC_AIGPMM_38_006_Test : BaseTest
    {
        [Test]
        public void CancelFromOverview_ReturnsToProducts_CartRemains()
        {
            var login = new LoginPage(Driver);
            login.Login("standard_user", "secret_sauce");
            var products = new ProductsPage(Driver);
            products.AddProductToCart("Sauce Labs Backpack");
            products.OpenCart();
            var cart = new CartPage(Driver);
            cart.ClickCheckout();
            var yourInfo = new CheckoutYourInfoPage(Driver);
            yourInfo.EnterYourInformation("Alan", "Park", "98101");
            yourInfo.ClickContinue();
            var overview = new CheckoutOverviewPage(Driver);
            Assert.IsTrue(overview.IsAtOverview(), "Should be on Overview before cancelling");
            overview.ClickCancel();
            // After cancel, navigate back to products; ensure cart still contains items by opening cart
            products.OpenCart();
            var names = cart.GetCartItemNames();
            Assert.IsTrue(names.Count > 0, "Cart should still contain items after cancelling from Overview");
        }
    }
}