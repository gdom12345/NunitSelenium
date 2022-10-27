using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace NunitSelenium.Selenium
{
    public class WaitUtils
    {
        public static void WaitFor(Func<bool> condition, TimeSpan timeToWait)
        {
            WebDriver driver = null;
            WebDriverWait wait = new WebDriverWait(driver, timeToWait);
            wait.IgnoreExceptionTypes(
                typeof(NoSuchElementException),
                typeof(StaleElementReferenceException));
            wait.Until(driver => condition);
        }
    }

}
