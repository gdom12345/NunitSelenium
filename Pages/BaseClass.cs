using NunitSelenium.Framework;
using OpenQA.Selenium;

namespace NunitSelenium.Pages
{
    internal abstract class BaseClass
    {
        protected WebDriver driver;

        public abstract void WaitForPageLoad();

        public BaseClass(WebDriver driver)
        {
            this.driver = driver;
            ReflectionUtils.InitElements(this, driver);

        }
    }
}
