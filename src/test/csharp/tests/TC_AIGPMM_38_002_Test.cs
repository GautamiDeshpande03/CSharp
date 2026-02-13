/*
 TC_AIGPMM-38_002
 Verify successful checkout with multiple products from cart
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
    public class TC_AIGPMM_38_002_Test : BaseTest
    {
        [Test]
        public void CompleteCheckout_MultipleProducts_ShouldShowSuccessAndCorrectTotals()
        {
            var loginPage = new LoginPage(Driver, Wait);
            var productsPage = new ProductsPage(Driver, Wait);
            var cartPage = new CartPage(Driver, Wait);
            var checkoutInfo = new CheckoutInformationPage(Driver, Wait);
            var overviewPage = new CheckoutOverviewPage(Driver, Wait);
            var finishPage = new FinishPage(Driver, Wait);
            // Test data
            const string username = "standard_user";
            const string password = "secret_sauce";
            string[] products = new[] { "Sauce Labs Backpack", "Sauce Labs Bike Light", "Sauce Labs Bolt T-Shirt" };
            const string firstName = "Jennifer";
            const string lastName = "Martinez";
            const string postalCode = "90210";
            // 1. Navigate to application
            Driver.Navigate().GoToUrl(BaseUrl);
            // 2. Login
            loginPage.Login(username, password);
            // 3. Add multiple products to cart
            foreach (var product in products)
            {
                productsPage.AddProductToCart(product);
            }
            // 4. Open Cart
            productsPage.OpenCart();
            // 5. Click Checkout
            cartPage.ClickCheckout();
            // 6. Enter checkout info and Continue
            checkoutInfo.EnterCheckoutInformation(firstName, lastName, postalCode);
            checkoutInfo.ClickContinue();
            // 7. On Overview verify expected items and totals
            Assert.IsTrue(overviewPage.IsOnOverviewPage(), "Expected to be on Checkout: Overview page.");
            foreach (var product in products)
            {
                Assert.IsTrue(overviewPage.IsProductInOverview(product), $"Product '{product}' should appear in Overview.");
            }
            // Validate item total, tax and total exist and are formatted
            var itemTotal = overviewPage.GetItemTotal();
            var tax = overviewPage.GetTax();
            var total = overviewPage.GetTotal();
            Assert.IsTrue(itemTotal > 0, "Item total should be greater than 0.");
            Assert.IsTrue(tax >= 0, "Tax should be zero or positive.");
            Assert.AreEqual(itemTotal + tax, total, 0.01, "Total should equal Item Total + Tax.");
            // 8. Finish
            overviewPage.ClickFinish();
            // Verify Finish page success message and logo
            Assert.IsTrue(finishPage.IsThankYouMessageDisplayed(), "'THANK YOU FOR YOUR ORDER' should be displayed on Finish page.");
            Assert.IsTrue(finishPage.IsPonyExpressLogoDisplayed(), "Pony Express logo should be visible on Finish page.");
        }
    }
}