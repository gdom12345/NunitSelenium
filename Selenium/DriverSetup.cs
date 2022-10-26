using NunitSelenium.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace NunitSelenium.Selenium
{
    internal class DriverSetup
    {
        public static WebDriver InitializeWebDriver(DriverSettings driverSettings)
        {
            WebDriver driver = null;
            switch (driverSettings.driverType)
            {
                //look into Webdriver Manager for C#
                case DriverType.CHROME:
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.PageLoadStrategy = PageLoadStrategy.None;
                    driver = new ChromeDriver(Environment.CurrentDirectory, chromeOptions);
                    break;
                default:
                    throw new Exception("Unrecognized Driver Type of "
                        + driverSettings.driverType);
            }


            driver.Navigate().GoToUrl(driverSettings.siteUri);
            BaseClass pageObject = Activator.CreateInstance(driverSettings.pageObjectType, driver) as BaseClass;

            return driver;
        }
    }
}
