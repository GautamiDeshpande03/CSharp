using NUnit.Framework;
using SauceDemo.Framework;
using SauceDemo.PageObjects;
namespace SauceDemo.Tests
{
    /// <summary>
    /// TC_AIGPMM-38_005
    /// Verify Cancel on Checkout: Your Information navigates back to Your Cart without data loss
    /// Test data: standard_user / secret_sauce ; Emma Stone ; 02118
    /// </summary>
    [TestFixture]
    public class TC_AIGPMM_38_005_Test : BaseTest
    {
        [Test]
        public void CancelFromYourInformation_ReturnsToCart_CartIntact()
        {
            var login = new LoginPage(Driver);
            login.Login("standard_user", "secret_sauce");
            var products = new ProductsPage(Driver);
            products.AddProductToCart("Sauce Labs Backpack");
            products.OpenCart();
            var cart = new CartPage(Driver);
            var before = cart.GetCartItemNames();
            Assert.IsTrue(before.Count > 0, "Cart should contain items before checkout");
            cart.ClickCheckout();
            var yourInfo = new CheckoutYourInfoPage(Driver);
            yourInfo.EnterYourInformation("Emma", "Stone", "02118");
            yourInfo.ClickCancel();
            // After cancel, Cart page should be visible and items persist.
            // We use CartPage methods by reusing existing page object
            var after = cart.GetCartItemNames();
            Assert.AreEqual(before.Count, after.Count, "Cart item count should remain the same after cancel");
        }
    }
}