using System;
using System.Threading;
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
        private const string RightPaginatorArrowPath = "//a[contains(@class,'coreSpriteRightPaginationArrow')]";

        private static int numberOfLikedPics;
        Random r;

        [FindsBy(How = How.XPath, Using = ArticlePath)]
        private IWebElement MainArticleHolder;

        [FindsBy(How = How.XPath, Using = OpenHeartPath)]
        private IWebElement OpenHeart;

        [FindsBy(How = How.XPath, Using = CloseDetailsButtonPath)]
        private IWebElement CloseButton;

        [FindsBy(How = How.XPath, Using = RightPaginatorArrowPath)]
        private IWebElement RightPaginatorArrow;

#endregion


#region 'Constructor'

        public PostDetails(IWebDriver driver)
        {
            driver.WaitPageLoaded();
            driver.WaitForElementExists(By.XPath(ArticlePath), 10).WaitForVisible();
            this.Driver = driver;
            PageFactory.InitElements(driver, this);
        }

#endregion


#region 'Methods'

        public InstagramSearchResultsPage PutLikeAndClose()
        {
            if (AlreadyLiked())
            {
                return ClosePostDetailsPage();
            }
            this.OpenHeart.ClickJs(Driver);
            Driver.WaitForElementExists(By.XPath(FullHeartPath), 2);
            return ClosePostDetailsPage();
        }
        

        private bool AlreadyLiked()
        {
            if (Driver.IsElementExists(By.XPath(OpenHeartPath), 1)) return false;

            Console.WriteLine("'Like' is already posted. Continue to next...");
            return true;
        }

        public InstagramSearchResultsPage MakeLikesOnPostDetails(int numberOfLikedPosts)
        {
            for (var i = 0; i < numberOfLikedPosts; i++)
            {
                if (AlreadyLiked())
                {
                    GoToNextPostDetails();                    
                }
                else
                {
                    PutLike();
                    ++numberOfLikedPics;
                    GoToNextPostDetails();
                }
            }
            Console.WriteLine("numberOfLikedPics = "+ numberOfLikedPics);            
            return ClosePostDetailsPage();
        }


        private void PutLike()
        {
            r = new Random();
            
            Driver.WaitForElementVisible(By.XPath(OpenHeartPath));
            this.OpenHeart.ClickJsEvent(Driver);
            Driver.WaitForElementExists(By.XPath(FullHeartPath), 2);
            Thread.Sleep(GetRandomITime(r, 1500, 2000));
        }

        private PostDetails GoToNextPostDetails()
        {
            Driver.WaitForElementExists(By.XPath(RightPaginatorArrowPath), 2).ClickJs(Driver);
            return new PostDetails(Driver);
        }


        private InstagramSearchResultsPage ClosePostDetailsPage()
        {
            this.CloseButton.ClickJs(Driver);
            return new InstagramSearchResultsPage(Driver);
        }

        private int GetRandomITime(Random r, int first, int second)
        {
            return r.Next(first, second);
        }

#endregion

    }
}
