using NunitSelenium.Framework;
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

        public override void WaitForPageLoad()
        {
            WaitUtils.WaitFor(() => loginButton.Displayed(),
                TimeSpan.FromSeconds(30));
            Thread.Sleep(200);
        }

        public SignOnPage(WebDriver driver) : base(driver)
        {
            ReflectionUtils.InitElements(this, driver);
            string something = "";
        }
    }

    public class SignOnInfo
    {
        public string user { get; set; }
        public string password { get; set; }
    }
}
