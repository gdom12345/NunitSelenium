using NunitSelenium.Framework;
using NunitSelenium.Pages;
using NunitSelenium.Pages.PageComponents;
using NunitSelenium.Selenium;

namespace NunitSelenium.tests
{

    public class HomePageValidations : BaseTest
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
        public void ValidateShopInventory()
        {
            homePage = signOnPage.signOn();
            List<ShopItem> inventoryItems = homePage.inventoryItems.shopItems;
            List<ShopItem> expectedInventoryItems = ReflectionUtils
                .getListFromCsvFile<ShopItem>("shopitems.csv");
            CollectionAssert.AreEqual(expectedInventoryItems, inventoryItems);

        }


        [TearDown]
        public void Teardown()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}