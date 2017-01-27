using System;
using Instagram.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Instagram.Pages
{
    public class BaseInstPage
    {

#region 'Fields and properties'        

        private const string AppBannerHintCloseButtonPath = "//span[@class='_8yoiv']";
        private const string DesktopNavLogoAndWordmarkPath = "//a[contains(@class,'coreSpriteDesktopNavLogoAndWordmark')]";


        private IWebDriver Driver;

        [FindsBy(How = How.XPath, Using = AppBannerHintCloseButtonPath)]
        private IWebElement AppBannerHintCloseButton;

#endregion

#region 'Constructors'

        public BaseInstPage()
        {
            this.Driver = Inj.Driver;
            Driver.WaitForElementVisible(By.XPath(DesktopNavLogoAndWordmarkPath));            
            PageFactory.InitElements(Driver, this);
        }

#endregion

        public void Go(Uri pageUri)
        {
            Driver.NavigateGoToUrl(pageUri);
        }

        public void CheckAndCloseAppBannerHint(int waitForItSec = 0)
        {
            if (!Driver.IsElementExists(By.XPath(AppBannerHintCloseButtonPath), waitForItSec)) return;
            this.AppBannerHintCloseButton.ClickJs(Driver);
            Driver.WaitForElementInvisible(By.XPath(AppBannerHintCloseButtonPath), 3);
        }
    }
}
