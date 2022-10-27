using NunitSelenium.Pages.PageComponents;
using NunitSelenium.Selenium;
using NunitSelenium.Selenium.PageComponent;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace NunitSelenium.Pages
{
    internal class HomePage : BaseClass
    {
        [FindsBy(How = How.XPath, Using = "(//*[@id='inventory_container']//*[@class='inventory_item_name'])[1]")]
        public SeleniumElement firstProductName;

        public InventoryItems GetInventoryItems()
        {
            return new InventoryItems(driver);
        }

        public void AddItemToCart(string name)
        {
            GetInventoryItems().AddItemToCart(name);
        }

        public override void WaitForPageLoad()
        {
            WaitUtils.WaitFor(() => firstProductName.Displayed(),
                TimeSpan.FromSeconds(30));
            Thread.Sleep(300);
        }

        public HomePage(WebDriver driver) : base(driver)
        {
        }
    }
}
