using OpenQA.Selenium;

namespace NunitSelenium.Selenium.PageComponent
{
    public class WebElementUtils
    {
        private IWebElement webElement;

        public WebElementUtils(IWebElement webElement)
        {
            this.webElement = webElement;
        }

        public void SendKeys(string keys)
        {
            webElement.Clear();
            webElement.SendKeys(keys);
        }

        public string GetText()
        {
            string elementType = webElement.TagName;
            if (elementType == "input")
            {
                string typeAttribute = webElement.GetAttribute("type");
                if (typeAttribute != null && typeAttribute == "textbox")
                {
                    return webElement.GetAttribute("value");
                }

            }
            return webElement.Text;
        }

    }
}
