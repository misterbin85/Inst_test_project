using System;
using Instagram.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Instagram.Pages
{
    public class PostDetails
    {

#region 'Fields and controls'

        private IWebDriver Driver;

        private const string ArticlePath = "//article[@class='_djxz1 _j5hrx']";
        private const string OpenHeartPath = ".//span[contains(@class,'coreSpriteHeartOpen')]";
        private const string FullHeartPath = ".//span[contains(@class,'coreSpriteHeartFull')]";
        private const string CloseDetailsButtonPath = "//button[@class='_3eajp']";


        [FindsBy(How = How.XPath, Using = ArticlePath)]
        private IWebElement MainArticleHolder;

        [FindsBy(How = How.XPath, Using = OpenHeartPath)]
        private IWebElement OpenHeart;

        [FindsBy(How = How.XPath, Using = CloseDetailsButtonPath)]
        private IWebElement CloseButton;

#endregion


#region 'Constructor'

        public PostDetails(IWebDriver driver)
        {
            driver.WaitForElementExists(By.XPath(ArticlePath), 5);
            this.Driver = driver;
            PageFactory.InitElements(driver, this);
        }

#endregion


#region 'Methods'

        public InstagramSearchResultsPage PutLikeAndClose()
        {
            if (!Driver.IsElementExists(By.XPath(OpenHeartPath), 1))
            {
                Console.WriteLine("'Like' is already posted. Continue to next...");
                return ClosePostDetailsPage();
            }
            this.OpenHeart.ClickJs(Driver);
            Driver.WaitForElementExists(By.XPath(FullHeartPath), 2);
            return ClosePostDetailsPage();
        }


        private InstagramSearchResultsPage ClosePostDetailsPage()
        {
            this.CloseButton.ClickJs(Driver);
            return new InstagramSearchResultsPage(Driver);
        }

#endregion

    }
}
