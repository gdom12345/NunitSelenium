using NunitSelenium.Pages;

namespace NunitSelenium.Selenium
{
    internal class DriverSettings
    {
        public DriverType driverType { get; set; }
        public Uri siteUri { get; set; }

        //Maybe move Uri into BaseClass
        public BaseClass classToWaitFor { get; set; }
    }

    public enum DriverType
    {
        CHROME
    }
}
