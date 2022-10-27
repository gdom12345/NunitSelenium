using NunitSelenium.Framework;
using NunitSelenium.Pages;
using NunitSelenium.Pages.PageComponents;
using NunitSelenium.Selenium;

namespace NunitSelenium.tests
{

    public class Tests : BaseTest
    {
        private SignOnPage signOnPage;
        private HomePage homePage;

        [SetUp]
        public void Setup()
        {
            driver = DriverSetup.InitializeWebDriver(driverSettings);
            signOnPage = new SignOnPage(driver);
        }

        [Test]
        public void Test1()
        {
            homePage = signOnPage.signOn();
            List<ShopItem> inventoryItems = homePage.GetInventoryItems().GetShopItems();
            Assert.Pass();
        }
    }
}