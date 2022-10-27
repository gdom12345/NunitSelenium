using NunitSelenium.Selenium;

namespace NunitSelenium.Config
{
    internal class EnvironmentInfo
    {
        public string currentEnvironment { get; set; }
        public Environment[] environments { get; set; }

        public DriverType driverType { get; set; }

        public Environment getEnvironmentData()
        {
            Environment environmentWeWant = Array.Find(environments,
                environment => environment.name == currentEnvironment);
            if (environmentWeWant == null)
            {
                throw new Exception("Did NOT find and environment for " + currentEnvironment);
            }
            return environmentWeWant;
        }
    }

    internal class Environment
    {
        public string name { get; set; }
        public Uri baseUri { get; set; }
        public string standardLogin { get; set; }
        public string standardPassword { get; set; }
    }


    //  "currentEnvironment": "QA",
    //"environments": [
    //  {
    //    "name": "QA",
    //    "baseUri": "",
    //    "standardLogin": "",
    //    "standardPassword": ""
    //  }

    //]
}
