using NunitSelenium.Framework;
using NunitSelenium.Selenium.PageComponent;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace NunitSelenium.Pages
{
    internal abstract class BaseClass
    {
        protected WebDriver driver;

        [FindsBy(How = How.XPath, Using = "//a[@class='shopping_cart_link']")]
        public SeleniumElement shoppingCartLink;

        //Build Annotation design to auto handle wait logic for SeleniumElement
        public ShoppingCartPage NavigateToShoppingCart()
        {
            shoppingCartLink.Click();
            ShoppingCartPage shoppingCartPage = new ShoppingCartPage(driver);
            shoppingCartPage.WaitForPageLoad();
            return shoppingCartPage;
        }


        public abstract void WaitForPageLoad();

        public BaseClass(WebDriver driver)
        {
            this.driver = driver;
            ReflectionUtils.InitElements(this, driver);

        }
    }
}
