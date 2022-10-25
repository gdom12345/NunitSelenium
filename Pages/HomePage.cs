using OpenQA.Selenium;

namespace NunitSelenium.Pages
{
    internal class HomePage : BaseClass
    {
        public override void WaitForPageLoad()
        {
            throw new NotImplementedException();
        }

        public HomePage(WebDriver driver)
        {
            this.driver = driver;
        }
    }
}
