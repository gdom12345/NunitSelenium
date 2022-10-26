namespace NunitSelenium.Selenium
{
    internal class DriverSettings
    {
        public DriverType driverType { get; set; }
        public Uri siteUri { get; set; }

        //Maybe move Uri into BaseClass
        //Rethink Type, need to study generics more
        public Type pageObjectType { get; set; }
    }

    public enum DriverType
    {
        CHROME
    }
}
