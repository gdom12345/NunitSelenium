using NunitSelenium.Framework;
using NunitSelenium.Selenium;
using OpenQA.Selenium;

namespace NunitSelenium.Pages.PageComponents
{
    public class InventoryItems
    {
        private const string INVENTORY_ITEM_XPATH = "//div[@class='inventory_item' or @class='cart_item']";

        private WebDriver driver;

        public InventoryItems(WebDriver driver)
        {
            this.driver = driver;
        }

        public void AddItemToCart(string itemName)
        {
            IWebElement addButton = driver.FindElement(
                By.XPath(INVENTORY_ITEM_XPATH + "[.//*[@class='inventory_item_name'][text()=\""
                + itemName + "\"]]//button[text()='Add to cart']"));
            addButton.Click();
            WaitUtils.WaitFor(() => addButton.Text.ToLower() != "add to cart", TimeSpan.FromSeconds(2));
            Thread.Sleep(300);
        }

        public List<ShopItem> shopItems
        {
            get
            {
                TableParser tableParser = new TableParser(driver);
                var table = tableParser.ParseTable(
                    INVENTORY_ITEM_XPATH + "//a[./div[@class='inventory_item_name']]|"
                    + INVENTORY_ITEM_XPATH + "//div[@class='inventory_item_desc']|"
                    + INVENTORY_ITEM_XPATH + "//div[@class='inventory_item_price']",
                    new string[] { "Name", "Description", "Price" });
                return ReflectionUtils.GetList<ShopItem>(table);
            }
        }




        public ShopItem GetShopItem(string itemName)
        {
            var shopItem = shopItems.Find(item => item.Name == itemName);
            if (shopItem == null)
            {
                throw new Exception("Did NOT find Shop Item " + itemName);
            }
            return shopItem;
        }

    }

    public class ShopItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ShopItem item &&
                   Name == item.Name &&
                   Description == item.Description &&
                   Price == item.Price;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, Price);
        }


    }
}
