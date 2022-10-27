using CsvHelper;
using CsvHelper.Configuration;
using NunitSelenium.Framework;
using NunitSelenium.Pages;
using NunitSelenium.Pages.PageComponents;
using NunitSelenium.Selenium;
using System.Globalization;

namespace NunitSelenium.tests
{

    public class AddToShoppingCartValidations : BaseTest
    {
        private SignOnPage signOnPage;
        private HomePage homePage;
        private ShoppingCartPage shoppingCartPage;

        [SetUp]
        public void Setup()
        {
            driver = DriverSetup.InitializeWebDriver(driverSettings);
            signOnPage = new SignOnPage(driver);
            homePage = signOnPage.signOn();
        }

        //cleanup arguments, I'll pass them as ShoppingItem
        [Test, TestCaseSource("ShoppingListData")]
        public void ValidateWeCanAddDataDrivenSetToCart(string name, string description, string price)
        {
            homePage.AddItemToCart(name);
            shoppingCartPage = homePage.NavigateToShoppingCart();
            ShopItem shopItem = shoppingCartPage.GetInventoryItems().GetShopItem(name);
            Assert.NotNull(shopItem);
            Assert.AreEqual(name, shopItem.Name);
            Assert.AreEqual(description, shopItem.Description);
            Assert.AreEqual(price, shopItem.Price);
        }

        private static IEnumerable<TestCaseData> ShoppingListData()
        {
            List<ShopItem> expectedInventoryItems = ReflectionUtils
             .getListFromCsvFile<ShopItem>("shopitems.csv");
            foreach (ShopItem item in expectedInventoryItems)
            {
                yield return new TestCaseData(item.Name, item.Description, item.Price)
                    .SetName("Check That We can add " + item.Name);
            }
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
            try
            {
                if (shoppingCartPage != null)
                {
                    shoppingCartPage.ClearCart();
                    shoppingCartPage = null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }
    }
}