using Instagram.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Instagram.Pages
{
    public class InstagramSearchResultsPage : InstagramMainFeedPage
    {

#region 'Fields and controls'

        private IWebDriver Driver;

        [FindsBy(How = How.ClassName, Using = "_oidfu")]
        private IWebElement LoadMoreButton;

#endregion

#region 'Constructor'

        public InstagramSearchResultsPage(IWebDriver driver)
            : base(driver)
        {
            driver.Wait().Until(ExpectedConditions.ElementIsVisible(By.ClassName("_s53mj")));
            this.Driver = driver;
            PageFactory.InitElements(driver, this);
        }

#endregion

#region 'Methods'

        public InstagramSearchResultsPage LoadMoreResults()
        {            
            this.LoadMoreButton.ScrollIntoView(Driver);
            this.LoadMoreButton.Click();
            return this;
        }

#endregion

    }
}
