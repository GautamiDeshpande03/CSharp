/*
 CheckoutOverviewPage POM
 - Methods to inspect products, pricing summary and finish/cancel actions
 - Uses placeholder locators
*/
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Globalization;
using System.Text.RegularExpressions;
namespace Project.Pages
{
    public class CheckoutOverviewPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly By _overviewContainer = By.XPath("//div[contains(.,'Checkout: Overview') or contains(@class,'checkout_summary')]");
        private readonly string _productRowXPathTemplate = "//div[contains(@class,'cart_item')][.//div[contains(.,\"{0}\")]]";
        private readonly By _finishButton = By.XPath("//button[contains(.,'Finish') or @id='finish']");
        private readonly By _cancelButton = By.XPath("//button[contains(.,'Cancel') or @id='cancel']");
        private readonly By _itemTotalLabel = By.XPath("//div[contains(.,'Item total') or //div[contains(@class,'summary_subtotal_label')]]");
        private readonly By _taxLabel = By.XPath("//div[contains(.,'Tax') or //div[contains(@class,'summary_tax_label')]]");
        private readonly By _totalLabel = By.XPath("//div[contains(.,'Total') or //div[contains(@class,'summary_total_label')]]");
        public CheckoutOverviewPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            _wait.Until(ExpectedConditions.ElementIsVisible(_overviewContainer));
        }
        public bool IsOnOverviewPage()
        {
            try
            {
                return _driver.Url.Contains("/checkout-step-two") || _wait.Until(d => d.FindElement(_overviewContainer).Displayed);
            }
            catch
            {
                return false;
            }
        }
        public bool IsProductInOverview(string productName)
        {
            var productRow = By.XPath(string.Format(_productRowXPathTemplate, productName));
            return _driver.FindElements(productRow).Count > 0;
        }
        public decimal GetItemTotal()
        {
            var text = _wait.Until(ExpectedConditions.ElementIsVisible(_itemTotalLabel)).Text;
            return ParseCurrencyFromText(text);
        }
        public decimal GetTax()
        {
            var text = _wait.Until(ExpectedConditions.ElementIsVisible(_taxLabel)).Text;
            return ParseCurrencyFromText(text);
        }
        public decimal GetTotal()
        {
            var text = _wait.Until(ExpectedConditions.ElementIsVisible(_totalLabel)).Text;
            return ParseCurrencyFromText(text);
        }
        private decimal ParseCurrencyFromText(string text)
        {
            // Extract number like $32.39
            var m = Regex.Match(text, @"[\d\.,]+");
            if (m.Success)
            {
                var cleaned = m.Value.Replace(",", "");
                if (decimal.TryParse(cleaned, NumberStyles.Number | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var val))
                {
                    return val;
                }
            }
            return 0m;
        }
        public void ClickFinish()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(_finishButton)).Click();
            // Wait for finish page artifact
            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h2[contains(.,'THANK YOU FOR YOUR ORDER') or contains(.,'THANK YOU')]")));
        }
        public void ClickCancel()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(_cancelButton)).Click();
            // Wait for products page artifact
            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class,'inventory_list') or contains(.,'Products')]")));
        }
    }
}