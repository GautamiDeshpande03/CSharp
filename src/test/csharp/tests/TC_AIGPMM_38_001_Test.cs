/*
 TC_AIGPMM-38_001
 Verify successful checkout with single product from cart to finish page
 Language: C#
 Framework: Selenium + NUnit
 POM: Uses Page Object Model - all UI interactions are inside page classes
 Notes:
  - Placeholders used for element locators per request ("use placeholders")
  - Application base URL used from traversal context: https://www.saucedemo.com/
  - Test data taken from the provided test case
*/
using NUnit.Framework;
using OpenQA.Selenium;
using Project.Core;
using Project.Pages;
namespace Project.Tests
{
    [TestFixture]
    public class TC_AIGPMM_38_001_Test : BaseTest
    {
        [Test]
        public void CompleteCheckout_SingleProduct_ShouldShowSuccess()
        {
            var loginPage = new LoginPage(Driver, Wait);
            var productsPage = new ProductsPage(Driver, Wait);
            var cartPage = new CartPage(Driver, Wait);
            var checkoutInfo = new CheckoutInformationPage(Driver, Wait);
            var overviewPage = new CheckoutOverviewPage(Driver, Wait);
            var finishPage = new FinishPage(Driver, Wait);
            // Test data (from test case)
            const string username = "standard_user";
            const string password = "secret_sauce";
            const string productName = "Sauce Labs Backpack";
            const string firstName = "Michael";
            const string lastName = "Thompson";
            const string postalCode = "78701";
            // 1. Navigate to application
            Driver.Navigate().GoToUrl(BaseUrl);
            // 2. Login
            loginPage.Login(username, password);
            // 3. Add Sauce Labs Backpack to cart
            productsPage.AddProductToCart(productName);
            // 4. Open Cart
            productsPage.OpenCart();
            // 5. Click Checkout
            cartPage.ClickCheckout();
            // 6. Enter checkout info and Continue
            checkoutInfo.EnterCheckoutInformation(firstName, lastName, postalCode);
            checkoutInfo.ClickContinue();
            // 7. On Overview, Finish
            Assert.IsTrue(overviewPage.IsOnOverviewPage(), "Expected to be on Checkout: Overview page before finishing.");
            overviewPage.ClickFinish();
            // 8. Verify Finish page success message and logo
            Assert.IsTrue(finishPage.IsThankYouMessageDisplayed(), "'THANK YOU FOR YOUR ORDER' should be displayed on Finish page.");
            Assert.IsTrue(finishPage.IsPonyExpressLogoDisplayed(), "Pony Express logo should be visible on Finish page.");
        }
    }
}