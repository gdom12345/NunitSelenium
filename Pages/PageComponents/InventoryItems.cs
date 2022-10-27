using NunitSelenium.Framework;
using NunitSelenium.Selenium;
using OpenQA.Selenium;

namespace NunitSelenium.Pages.PageComponents
{
    public class InventoryItems
    {
        private const string INVENTORY_ITEM_XPATH = "//div[@class='inventory_item']";

        private WebDriver driver;

        public InventoryItems(WebDriver driver)
        {
            this.driver = driver;
        }

        public List<ShopItem> GetShopItems()
        {
            TableParser tableParser = new TableParser(driver);
            var table = tableParser.ParseTable(
                INVENTORY_ITEM_XPATH + "//div[@class='inventory_item_label']/a|"
                + INVENTORY_ITEM_XPATH + "//div[@class='inventory_item_desc']|"
                + INVENTORY_ITEM_XPATH + "//div[@class='inventory_item_price']",
                new string[] { "Name", "Description", "Price" });
            return ReflectionUtils.GetList<ShopItem>(table);

        }

        public ShopItem GetShopItem(string itemName)
        {
            var shopItem = GetShopItems().Find(item => item.Name == itemName);
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
