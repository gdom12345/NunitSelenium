using NunitSelenium.Selenium;
using OpenQA.Selenium;

namespace NunitSelenium.tests
{

    public class Tests
    {
        private DriverSettings driverSettings;
        [SetUp]
        public void Setup()
        {
            //Move to a Base class, define driverType from configuration
            driverSettings = new DriverSettings
            {
                driverType = DriverType.CHROME,
                //Definte Uri from environment config, need to find test site
                siteUri = new Uri("https://www.google.com/"),
                //Work on a better solution for this
                pageObjectType = Type.GetType("NunitSelenium.Pages.HomePage")
            };
        }

        [Test]
        public void Test1()
        {
            WebDriver driver = DriverSetup.InitializeWebDriver(driverSettings);
            Assert.Pass();
        }
    }
}