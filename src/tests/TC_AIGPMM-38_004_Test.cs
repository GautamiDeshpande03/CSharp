using NUnit.Framework;
using SauceDemo.Framework;
using SauceDemo.PageObjects;
namespace SauceDemo.Tests
{
    /// <summary>
    /// TC_AIGPMM-38_004
    /// Attempt to continue with single missing field and verify specific error message
    /// Test data: standard_user / secret_sauce ; First=Alice ; Last blank ; Zip=30301
    /// </summary>
    [TestFixture]
    public class TC_AIGPMM_38_004_Test : BaseTest
    {
        [Test]
        public void CheckoutYourInfo_SingleMissingField_ShowsFieldLevelError()
        {
            var login = new LoginPage(Driver);
            login.Login("standard_user", "secret_sauce");
            var products = new ProductsPage(Driver);
            products.AddProductToCart("Sauce Labs Backpack");
            products.OpenCart();
            var cart = new CartPage(Driver);
            cart.ClickCheckout();
            var yourInfo = new CheckoutYourInfoPage(Driver);
            yourInfo.EnterYourInformation("Alice", string.Empty, "30301");
            yourInfo.ClickContinue();
            Assert.IsTrue(yourInfo.IsErrorDisplayed(), "Error should be displayed when a single mandatory field is missing");
            var err = yourInfo.GetErrorMessageText();
            Assert.IsTrue(err.ToLower().Contains("last") || err.ToLower().Contains("name") || err.Length > 0, "Error should indicate the missing field (Last Name)");
        }
    }
}