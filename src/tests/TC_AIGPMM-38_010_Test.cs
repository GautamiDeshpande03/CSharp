using NUnit.Framework;
using SauceDemo.Framework;
using SauceDemo.PageObjects;
using System;
namespace SauceDemo.Tests
{
    /// <summary>
    /// TC_AIGPMM-38_010
    /// Verify Item Total, Tax and Total amounts are accurately calculated and displayed
    /// Test data: standard_user / secret_sauce ; Liam Nguyen ; 98109 ; Known-price items (e.g., $29.99 & $9.99)
    /// Note: Using site-displayed prices; this test computes expected totals based on displayed Item Total and Tax.
    /// </summary>
    [TestFixture]
    public class TC_AIGPMM_38_010_Test : BaseTest
    {
        [Test]
        public void PriceCalculationAccuracy_ItemTotalTaxAndTotalMatch()
        {
            var login = new LoginPage(Driver);
            login.Login("standard_user", "secret_sauce");
            var products = new ProductsPage(Driver);
            // Add two known-price items - product names are placeholders; ensure they map to actual products in test environment
            products.AddProductToCart("Sauce Labs Bike Light");  // suppose $29.99 placeholder mapping
            products.AddProductToCart("Sauce Labs Bolt T-Shirt"); // suppose $9.99 placeholder mapping
            products.OpenCart();
            var cart = new CartPage(Driver);
            cart.ClickCheckout();
            var yourInfo = new CheckoutYourInfoPage(Driver);
            yourInfo.EnterYourInformation("Liam", "Nguyen", "98109");
            yourInfo.ClickContinue();
            var overview = new CheckoutOverviewPage(Driver);
            Assert.IsTrue(overview.IsAtOverview(), "Overview should be visible");
            var itemTotal = overview.GetItemTotal();
            var tax = overview.GetTax();
            var total = overview.GetTotal();
            // Verify arithmetic relationship: itemTotal + tax == total (rounded to 2 decimals)
            Assert.AreEqual(Math.Round(itemTotal + tax, 2), Math.Round(total, 2), "Total should equal Item Total + Tax");
            // Additional sanity checks
            Assert.Greater(itemTotal, 0.0, "Item total must be > 0");
            Assert.GreaterOrEqual(tax, 0.0, "Tax must be >= 0");
            overview.ClickFinish();
            var finish = new FinishPage(Driver);
            Assert.IsTrue(finish.IsThankYouMessageDisplayed(), "Finish page should show confirmation");
        }
    }
}