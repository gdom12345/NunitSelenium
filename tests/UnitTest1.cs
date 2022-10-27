using CsvHelper;
using CsvHelper.Configuration;
using NunitSelenium.Framework;
using NunitSelenium.Pages;
using NunitSelenium.Pages.PageComponents;
using NunitSelenium.Selenium;
using System.Globalization;

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
            List<ShopItem> expectedInventoryItems = getExpectedShopItemsFromFile();
            CollectionAssert.AreEqual(expectedInventoryItems, inventoryItems);

        }

        //Genericize this and ship it to Utility class
        private List<ShopItem> getExpectedShopItemsFromFile()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture);
            using (var reader = new StreamReader(FileUtils.getResourcesFolder() + "\\shopitems.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<ShopItem>();
                return new List<ShopItem>(records);
            }
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