using NunitSelenium.Framework;
using NunitSelenium.Pages;
using NunitSelenium.Selenium;

namespace NunitSelenium.tests
{

    public class Tests : BaseTest
    {
        private SignOnPage signOnPage;

        [SetUp]
        public void Setup()
        {
            driver = DriverSetup.InitializeWebDriver(driverSettings);
            signOnPage = new SignOnPage(driver);
        }

        [Test]
        public void Test1()
        {

            Assert.Pass();
        }
    }
}