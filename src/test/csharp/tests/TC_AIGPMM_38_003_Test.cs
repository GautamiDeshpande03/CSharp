/*
 TC_AIGPMM-38_003
 Verify Cancel button on Checkout: Your Information page navigates back to cart
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
    public class TC_AIGPMM_38_003_Test : BaseTest
    {
        [Test]
        public void CheckoutInfo_CancelNavigatesBackToCart_PreservesCartContents()
        {
            var loginPage = new LoginPage(Driver, Wait);
            var productsPage = new ProductsPage(Driver, Wait);
            var cartPage = new CartPage(Driver, Wait);
            var checkoutInfo = new CheckoutInformationPage(Driver, Wait);
            // Test data
            const string username = "standard_user";
            const string password = "secret_sauce";
            const string productName = "Sauce Labs Onesie";
            // Navigate & login
            Driver.Navigate().GoToUrl(BaseUrl);
            loginPage.Login(username, password);
            // Add product and open cart
            productsPage.AddProductToCart(productName);
            productsPage.OpenCart();
            // Click Checkout -> arrives at Checkout: Your Information
            cartPage.ClickCheckout();
            // Immediately click Cancel
            checkoutInfo.ClickCancel();
            // Verify return to cart and product still present
            Assert.IsTrue(cartPage.IsOnCartPage(), "Expected to be on Your Cart page after cancelling checkout info.");
            Assert.IsTrue(cartPage.IsProductInCart(productName), $"Product '{productName}' should remain in cart after cancelling checkout.");
        }
    }
}