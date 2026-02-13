/*
 TC_AIGPMM-38_004
 Verify error when continuing without First Name filled on Checkout: Your Information
 Language: C#
 Framework: Selenium + NUnit
 POM: Uses Page Object Model - all UI interactions are inside page classes
 Notes:
  - Placeholders used for element locators per request ("use placeholders")
  - Application base URL used from traversal context: https://www.saucedemo.com/
*/
using NUnit.Framework;
using OpenQA.Selenium;
using Project.Core;
using Project.Pages;
namespace Project.Tests
{
    [TestFixture]
    public class TC_AIGPMM_38_004_Test : BaseTest
    {
        [Test]
        public void CheckoutInfo_MissingFirstName_ShowsValidationError()
        {
            var loginPage = new LoginPage(Driver, Wait);
            var productsPage = new ProductsPage(Driver, Wait);
            var cartPage = new CartPage(Driver, Wait);
            var checkoutInfo = new CheckoutInformationPage(Driver, Wait);
            // Test data
            const string username = "standard_user";
            const string password = "secret_sauce";
            const string productName = "Sauce Labs Fleece Jacket";
            const string lastName = "Anderson";
            const string postalCode = "10001";
            // Navigate & login
            Driver.Navigate().GoToUrl(BaseUrl);
            loginPage.Login(username, password);
            // Add product and open cart
            productsPage.AddProductToCart(productName);
            productsPage.OpenCart();
            // Click Checkout
            cartPage.ClickCheckout();
            // Leave First Name empty, enter Last Name and Postal Code, Click Continue
            checkoutInfo.EnterCheckoutInformation("", lastName, postalCode);
            checkoutInfo.ClickContinue();
            // Assert validation error displayed and still on checkout info page
            Assert.IsTrue(checkoutInfo.IsOnCheckoutInformationPage(), "Expected to remain on Checkout: Your Information page when First Name is missing.");
            Assert.IsTrue(checkoutInfo.IsValidationErrorDisplayed(), "Expected validation error message when First Name is missing.");
        }
    }
}