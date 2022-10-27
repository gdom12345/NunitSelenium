using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace NunitSelenium.Selenium
{
    public class WaitUtils
    {
        public static void WaitFor(Func<bool> condition, TimeSpan timeToWait)
        {
            DefaultWait<object> defaultWait = new DefaultWait<object>(new object());
            defaultWait.Timeout = timeToWait;
            defaultWait.IgnoreExceptionTypes(
                typeof(NoSuchElementException),
                typeof(StaleElementReferenceException));
            defaultWait.Until(driver => condition);
        }
    }

}
