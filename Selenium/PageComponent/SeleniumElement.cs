using OpenQA.Selenium;

namespace NunitSelenium.Selenium.PageComponent
{
    public class SeleniumElement
    {
        public WebDriver driver { get; set; }

        public By by { get; set; }

        public IWebElement findWebElement()
        {
            return driver.FindElement(by);
        }

        public bool Displayed()
        {
            return findWebElement().Displayed;
        }
        public void Click()
        {
            findWebElement().Click();
        }

        public string GetText()
        {
            return new WebElementUtils(findWebElement()).GetText();
        }

        public void SendKeys(string text)
        {
            new WebElementUtils(findWebElement()).SendKeys(text);
        }
    }
}
