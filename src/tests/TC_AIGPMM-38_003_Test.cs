using NUnit.Framework;
using SauceDemo.Framework;
using SauceDemo.PageObjects;
namespace SauceDemo.Tests
{
    /// <summary>
    /// TC_AIGPMM-38_003
    /// Attempt to continue with all mandatory fields blank and verify error handling
    /// Test data: standard_user / secret_sauce ; First/Last/Zip blank
    /// </summary>
    [TestFixture]
    public class TC_AIGPMM_38_003_Test : BaseTest
    {
        [Test]
        public void CheckoutYourInfo_AllFieldsBlank_ShowsError()
        {
            var login = new LoginPage(Driver);
            login.Login("standard_user", "secret_sauce");
            var products = new ProductsPage(Driver);
            products.AddProductToCart("Sauce Labs Backpack");
            products.OpenCart();
            var cart = new CartPage(Driver);
            cart.ClickCheckout();
            var yourInfo = new CheckoutYourInfoPage(Driver);
            // Leave fields blank
            yourInfo.EnterYourInformation(string.Empty, string.Empty, string.Empty);
            yourInfo.ClickContinue();
            Assert.IsTrue(yourInfo.IsErrorDisplayed(), "Error should be displayed when required fields are blank");
            var errorText = yourInfo.GetErrorMessageText();
            Assert.IsNotEmpty(errorText, "Error message text should be present");
        }
    }
}