using NunitSelenium.Pages.PageComponents;
using NunitSelenium.Selenium;
using NunitSelenium.Selenium.PageComponent;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace NunitSelenium.Pages
{
    internal class ShoppingCartPage : BaseClass
    {
        [FindsBy(How = How.Id, Using = "checkout")]
        public SeleniumElement checkoutButton;

        //This will end up with MULTIPLE matches, use sparingly
        [FindsBy(How = How.XPath, Using = "//button[text()='Remove']")]
        public SeleniumElement removeButton;

        public void ClearCart()
        {
            int counter = 0;
            while (removeButton.Displayed() && counter < 10)
            {
                removeButton.Click();
                //Replace this with better solution
                Thread.Sleep(1000);
                counter++;
            }

        }


        public InventoryItems GetInventoryItems()
        {
            return new InventoryItems(driver);
        }





        public override void WaitForPageLoad()
        {
            WaitUtils.WaitFor(() => checkoutButton.Displayed(),
                TimeSpan.FromSeconds(30));
            Thread.Sleep(500);
        }

        public ShoppingCartPage(WebDriver driver) : base(driver)
        {
        }
    }
}
