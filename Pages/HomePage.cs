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

        public override void WaitForPageLoad()
        {
            WebElement webElement;
            WaitUtils.WaitFor(() => firstProductName.Displayed(),
                TimeSpan.FromSeconds(30));
            Thread.Sleep(300);
        }

        public HomePage(WebDriver driver) : base(driver)
        {
        }
    }
}
