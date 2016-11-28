using Instagram.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;
using System.Linq;

namespace Instagram.Pages
{
    public class InstagramSearchResultsPage : InstagramMainFeedPage
    {

#region 'Fields and controls'

        private IWebDriver Driver;

        [FindsBy(How = How.ClassName, Using = "_oidfu")]
        private IWebElement LoadMoreButton;

        [FindsBy(How = How.XPath, Using = @"//div[@class='_myci9']")]
        private IList<IWebElement> LoadedRowsWithPanes;

#endregion

#region 'Constructor'

        public InstagramSearchResultsPage(IWebDriver driver)
            : base(driver)
        {
            driver.WaitForElementVisible(By.ClassName("_s53mj"));
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

        public InstagramSearchResultsPage MakeLike()
        {
            IList<IWebElement> panesInRow = this.LoadedRowsWithPanes.First().FindElements(By.TagName("a"));
            foreach (var pane in panesInRow)
            {
                OpenPostDetails(pane).PutLikeAndClose();
            }
            return this;
        }


        public PostDetails OpenPostDetails(IWebElement pane)
        {
            pane.ClickJs(Driver);
            return new PostDetails(Driver);                                               
        }

#endregion

    }
}
