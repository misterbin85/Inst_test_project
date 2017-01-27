using Instagram.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Instagram.Pages
{
    public class InstagramLoginPage
    {

        #region 'Fields and controls'

        private IWebDriver Driver;
        private const string UserNameInputPath = "//input[@name='username']";
        private const string PasswordInputPath = "//input[@name='password']";
        private const string LoginButtonPath = "//form//button";


        [FindsBy(How = How.XPath, Using = UserNameInputPath)]
        private IWebElement UserName;

        [FindsBy(How = How.XPath, Using = PasswordInputPath)]
        private IWebElement Password;

        [FindsBy(How = How.XPath, Using = LoginButtonPath)]
        private IWebElement LoginButton;

        #endregion

#region 'Constructor'

        public InstagramLoginPage()
        {
            Driver = Inj.Driver;
            Driver.WaitForElementVisible(By.XPath(UserNameInputPath),5);
            PageFactory.InitElements(Driver, this);
        }
#endregion

        #region 'Methods'

        public InstagramMainFeedPage LoginToInstagram(string userName, string password)
        {
            this.UserName.SendKeys(userName);
            this.Password.SendKeys(password);
            this.LoginButton.Click();

            return new InstagramMainFeedPage();
        }

        #endregion
    }
}
