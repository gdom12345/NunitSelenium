using NunitSelenium.Framework;
using NunitSelenium.Pages;
using NunitSelenium.Pages.PageComponents;
using NunitSelenium.Selenium;

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
        public void ValidateWeCanAddDataDrivenSetToCart(ShopItem item)
        {
            homePage.AddItemToCart(item.Name);
            shoppingCartPage = homePage.NavigateToShoppingCart();
            ShopItem shopItem = shoppingCartPage.GetInventoryItems().GetShopItem(item.Name);
            Assert.NotNull(shopItem);
            Assert.AreEqual(item, shopItem);
        }

        private static IEnumerable<TestCaseData> ShoppingListData()
        {
            List<ShopItem> expectedInventoryItems = ReflectionUtils
             .getListFromCsvFile<ShopItem>("shopitems.csv");
            foreach (ShopItem item in expectedInventoryItems)
            {
                yield return new TestCaseData(item);
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