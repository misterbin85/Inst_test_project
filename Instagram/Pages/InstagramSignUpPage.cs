using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Instagram.Pages
{
    public class InstagramSignUpPage
    {

#region 'Properties and controls'

        private IWebDriver Driver;

        [FindsBy(How = How.ClassName, Using = "_fcn8k")]
        private IWebElement LoginLink;

#endregion

#region 'Constructor'        

        public InstagramSignUpPage(IWebDriver driver)
        {
            this.Driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public InstagramSignUpPage()
        { }

#endregion

#region 'Methods'

        public InstagramLoginPage OpenLogin()
        {
            this.LoginLink.Click();
            return new InstagramLoginPage(Driver);
        } 

#endregion

    }
}
