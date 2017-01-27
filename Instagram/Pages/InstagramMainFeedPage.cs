using System;
using Instagram.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Instagram.Pages
{
    public class InstagramMainFeedPage
    {

#region 'Fields and controls'

        IWebDriver Driver;
        private const string SearchInputPath = "//input[contains(@class, '_9x5sw')]";

        [FindsBy(How = How.XPath, Using = SearchInputPath)]
        private IWebElement SearchInput;

#endregion

#region 'Constructor'

        public InstagramMainFeedPage()
        {
            Driver = Inj.Driver;
            Driver.Wait().Until(ExpectedConditions.ElementIsVisible(By.XPath(SearchInputPath)));
            PageFactory.InitElements(Driver, this);
        }

#endregion

#region 'Methods'

        public InstagramSearchResultsPage SearchForAHashTag(string tag)
        {
            this.SearchInput.SendText(tag);            
            Driver.WaitForElementVisible(By.ClassName("_q8rex"), 10);
            this.SearchInput.SendKeys(Keys.Enter);                            

            return new InstagramSearchResultsPage();
        }

        public InstagramSearchResultsPage OpenResultsForAHashTag(string tag)
        {
            Uri uri = new Uri($"https://www.instagram.com/explore/tags/{tag}/");
            Driver.NavigateGoToUrl(uri);

            return new InstagramSearchResultsPage();
        }

#endregion

    }
}
