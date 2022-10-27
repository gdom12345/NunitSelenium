using Newtonsoft.Json;
using NunitSelenium.Config;
using NunitSelenium.Pages;
using NunitSelenium.Selenium;
using OpenQA.Selenium;

namespace NunitSelenium.Framework
{

    [TestFixture]
    public class BaseTest
    {
        protected DriverSettings driverSettings;
        protected WebDriver driver;
        protected SignOnInfo standardSignOn;

        [OneTimeSetUp]
        public void Init()
        {
            string configData = FileUtils.getResourceFile("Config.json");
            EnvironmentInfo environmentInfo = JsonConvert.DeserializeObject<EnvironmentInfo>(configData);
            ConfigData.environment = environmentInfo.getEnvironmentData();
            driverSettings = new DriverSettings
            {
                driverType = environmentInfo.driverType,
                siteUri = ConfigData.environment.baseUri,
                //Work on a better solution for this
                pageObjectType = Type.GetType("NunitSelenium.Pages.SignOnPage")
            };

            standardSignOn = new SignOnInfo
            {
                user = ConfigData.environment.standardLogin,
                password = ConfigData.environment.standardPassword
            };

        }
    }
}
