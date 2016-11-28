using Instagram.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Instagram.Pages
{
    public class InstagramLoginPage
    {

        #region 'Fields and controls'

        private IWebDriver Driver;
        private const string UserNameInputPath = "//input[@name='username']";
        private const string PasswordInputPath = "//input[@name='password']";
        private const string LoginButtonPath = "//button[contains(@class,'_aj7mu')]";


        [FindsBy(How = How.XPath, Using = UserNameInputPath)]
        private IWebElement UserName;

        [FindsBy(How = How.XPath, Using = PasswordInputPath)]
        private IWebElement Password;

        [FindsBy(How = How.XPath, Using = LoginButtonPath)]
        private IWebElement LoginButton;

        #endregion

        #region 'Constructor'

        public InstagramLoginPage(IWebDriver driver)
        {
            driver.Wait(5).Until(ExpectedConditions.ElementIsVisible(By.XPath(UserNameInputPath)));
            this.Driver = driver;
            PageFactory.InitElements(Driver, this);
        }

        #endregion

        #region 'Methods'

        public InstagramMainFeedPage LoginToInstagram(string userName, string password, bool submit = false)
        {
            this.UserName.SendKeys(userName);
            this.Password.SendKeys(password);
            if (submit)
            {
                this.LoginButton.Click();
            }

            return new InstagramMainFeedPage(Driver);
        }

        #endregion
    }
}
