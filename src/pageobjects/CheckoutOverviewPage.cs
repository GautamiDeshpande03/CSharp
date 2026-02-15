using OpenQA.Selenium;
using SauceDemo.Framework;
using System;
using System.Globalization;
using System.Linq;
namespace SauceDemo.PageObjects
{
    /// <summary>
    /// Checkout: Overview page POM.
    /// Provides methods for reading item totals and completing the order.
    /// </summary>
    public class CheckoutOverviewPage
    {
        private readonly IWebDriver _driver;
        // Placeholder locators for overview page elements
        private static readonly By OverviewHeaderTitle = By.XPath("//span[contains(.,'Checkout: Overview') or contains(@class,'title')]"); // placeholder
        private static readonly By FinishButton = By.XPath("//button[contains(.,'Finish') or @id='finish']"); // placeholder
        private static readonly By CancelButton = By.XPath("//button[contains(.,'Cancel')]"); // placeholder
        private static readonly By ItemTotalLabel = By.XPath("//div[contains(@class,'summary_subtotal_label')]"); // placeholder
        private static readonly By TaxLabel = By.XPath("//div[contains(@class,'summary_tax_label')]"); // placeholder
        private static readonly By TotalLabel = By.XPath("//div[contains(@class,'summary_total_label')]"); // placeholder
        private static readonly By PaymentInfo = By.XPath("//div[contains(.,'Payment Information') or contains(@class,'payment_info')]"); // placeholder
        private static readonly By ShippingInfo = By.XPath("//div[contains(.,'Shipping Information') or contains(@class,'shipping_info')]"); // placeholder
        public CheckoutOverviewPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public bool IsAtOverview()
        {
            try
            {
                return WaitHelpers.WaitForElementVisible(_driver, OverviewHeaderTitle, 5) != null;
            }
            catch
            {
                return false;
            }
        }
        public double GetItemTotal()
        {
            var el = WaitHelpers.WaitForElementVisible(_driver, ItemTotalLabel, 5);
            return ParseMoneyFromLabel(el.Text);
        }
        public double GetTax()
        {
            var el = WaitHelpers.WaitForElementVisible(_driver, TaxLabel, 5);
            return ParseMoneyFromLabel(el.Text);
        }
        public double GetTotal()
        {
            var el = WaitHelpers.WaitForElementVisible(_driver, TotalLabel, 5);
            return ParseMoneyFromLabel(el.Text);
        }
        public void ClickFinish()
        {
            var finish = WaitHelpers.WaitForElementVisible(_driver, FinishButton, 10);
            finish.Click();
        }
        public void ClickCancel()
        {
            var cancel = WaitHelpers.WaitForElementVisible(_driver, CancelButton, 5);
            cancel.Click();
        }
        public bool IsPaymentInfoDisplayed()
        {
            try
            {
                return WaitHelpers.WaitForElementVisible(_driver, PaymentInfo, 3) != null;
            }
            catch
            {
                return false;
            }
        }
        public bool IsShippingInfoDisplayed()
        {
            try
            {
                return WaitHelpers.WaitForElementVisible(_driver, ShippingInfo, 3) != null;
            }
            catch
            {
                return false;
            }
        }
        private double ParseMoneyFromLabel(string label)
        {
            // Label may look like "Item total: $39.98" or "Tax: $3.20"
            if (string.IsNullOrWhiteSpace(label)) return 0.0;
            var idx = label.LastIndexOf('$');
            if (idx >= 0)
            {
                var money = label.Substring(idx + 1).Trim();
                if (double.TryParse(money, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
                {
                    return value;
                }
            }
            // Fallback: try to extract numeric part
            var digits = new string(label.Where(c => char.IsDigit(c) || c == '.' || c == ',').ToArray());
            digits = digits.Replace(",", "");
            if (double.TryParse(digits, NumberStyles.Any, CultureInfo.InvariantCulture, out double fallback))
            {
                return fallback;
            }
            return 0.0;
        }
    }
}