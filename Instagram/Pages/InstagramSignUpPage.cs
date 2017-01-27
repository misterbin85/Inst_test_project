using System;
using Instagram.Extensions;
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

        public InstagramSignUpPage()
        {
            this.Driver = Inj.Driver;
            PageFactory.InitElements(Driver, this);
        }

#endregion

#region 'Methods'

        public void Go(Uri pageUri)
        {
            Driver.NavigateGoToUrl(pageUri);
        }


        public InstagramLoginPage OpenLogin()
        {
            this.LoginLink.Click();
            return new InstagramLoginPage();
        } 

#endregion

    }
}
