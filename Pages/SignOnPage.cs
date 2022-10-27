using NunitSelenium.Config;
using NunitSelenium.Selenium;
using NunitSelenium.Selenium.PageComponent;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace NunitSelenium.Pages
{
    internal class SignOnPage : BaseClass
    {
        [FindsBy(How = How.Id, Using = "user-name")]
        public SeleniumElement usernameTextbox;

        [FindsBy(How = How.Id, Using = "password")]
        public SeleniumElement passwordTextbox;

        [FindsBy(How = How.Id, Using = "login-button")]
        public SeleniumElement loginButton;

        public HomePage signOn(SignOnInfo signOnInfo)
        {
            usernameTextbox.SendKeys(signOnInfo.user);
            passwordTextbox.SendKeys(signOnInfo.password);
            loginButton.Click();

            HomePage homePage = new HomePage(driver);
            homePage.WaitForPageLoad();
            return homePage;
        }

        //Sign on with default values
        public HomePage signOn()
        {
            return signOn(new SignOnInfo
            {
                user = ConfigData.environment.standardLogin,
                password = ConfigData.environment.standardPassword
            });
        }

        public override void WaitForPageLoad()
        {
            WaitUtils.WaitFor(() => usernameTextbox.Displayed(),
                TimeSpan.FromSeconds(30));
            Thread.Sleep(500);
        }

        public SignOnPage(WebDriver driver) : base(driver)
        {

        }
    }

    public class SignOnInfo
    {
        public string user { get; set; }
        public string password { get; set; }
    }
}
