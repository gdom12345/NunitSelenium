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
            try
            {
                return findWebElement().Displayed;
            }
            //Element not being present in domain is functionally equivalent from end-user perspective
            catch (NoSuchElementException)
            {
                return false;
            }

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
