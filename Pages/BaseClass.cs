using OpenQA.Selenium;

namespace NunitSelenium.Pages
{
    internal abstract class BaseClass
    {
        protected WebDriver driver;
        public abstract void WaitForPageLoad();
    }
}
