using Instagram.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Instagram.Pages
{
    public abstract class BaseInstPage
    {

#region 'Fields and properties'

        private IWebDriver Driver;

        private const string AppBannerHintCloseButtonPath = "//span[@class='_8yoiv']";
        private const string DesktopNavLogoAndWordmarkPath = "//a[contains(@class,'coreSpriteDesktopNavLogoAndWordmark')]";

        [FindsBy(How = How.XPath, Using = AppBannerHintCloseButtonPath)]
        private IWebElement AppBannerHintCloseButton;

#endregion

#region 'Constructors'

        protected BaseInstPage(IWebDriver driver)
        {
            driver.WaitForElementVisible(By.XPath(DesktopNavLogoAndWordmarkPath));
            this.Driver = driver;
            PageFactory.InitElements(driver, this);
        }

        protected BaseInstPage()
        { }

#endregion

        public void CheckAndCloseAppBannerHint(int waitForItSec = 0)
        {
            if (!Driver.IsElementExists(By.XPath(AppBannerHintCloseButtonPath), waitForItSec)) return;
            this.AppBannerHintCloseButton.ClickJs(Driver);
            Driver.WaitForElementInvisible(By.XPath(AppBannerHintCloseButtonPath), 3);
        }
    }
}
