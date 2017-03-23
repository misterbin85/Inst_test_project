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

        private const string ArticleCss = "article";
        private const string OpenHeartPath = ".//span[contains(@class,'coreSpriteHeartOpen')]";
        private const string FullHeartPath = ".//span[contains(@class,'coreSpriteHeartFull')]";
        private const string CloseDetailsButtonPath = "//button[@class='_3eajp']";
        private const string RightPaginatorArrowCss = "a[class$=coreSpriteRightPaginationArrow]";

        private static int numberOfLikedPics;
        Random r;

        [FindsBy(How = How.CssSelector, Using = ArticleCss)]
        private IWebElement MainArticleHolder;

        [FindsBy(How = How.XPath, Using = OpenHeartPath)]
        private IWebElement OpenHeart;

        [FindsBy(How = How.XPath, Using = CloseDetailsButtonPath)]
        private IWebElement CloseButton;

        [FindsBy(How = How.CssSelector, Using = RightPaginatorArrowCss)]
        private IWebElement RightPaginatorArrow;

        #endregion


        #region 'Constructor'

        public PostDetails()
        {
            this.Driver = Inj.Driver;
            Driver.WaitPageLoaded();

            Driver.WaitForElementExists(By.CssSelector(ArticleCss), 10).WaitForVisible();           

            PageFactory.InitElements(Driver, this);
        }

        #endregion


        #region 'Methods'

        public InstagramSearchResultsPage PutLikeAndClose()
        {
            if (AlreadyLiked())
            {
                return ClosePostDetailsPage();
            }
            this.OpenHeart.ClickJs();
            Driver.WaitForElementExists(By.XPath(FullHeartPath), 2);
            return ClosePostDetailsPage();
        }


        private bool AlreadyLiked()
        {
            if (Driver.IsElementExists(By.XPath(OpenHeartPath), 3)) return false;

            Console.WriteLine("'Like' is already posted. Continue to next...");
            return true;
        }

        public InstagramSearchResultsPage PutLikesOnPostDetails(int numberOfLikedPosts)
        {
            for (var i = 0; i < numberOfLikedPosts; i++)
            {
                bool flag = false;
                try
                {
                    flag = Driver.FindElement(By.XPath("//body[contains(@class, 'dialog-404')]")).Enabled;
                }
                catch (Exception){ }

                if (flag)
                {
                    Console.WriteLine("404 ERROR");
                    Driver.Navigate().Back();
                    break;
                }

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
            Console.WriteLine("Liked = " + numberOfLikedPics);
            return ClosePostDetailsPage();
        }


        private void PutLike()
        {
            r = new Random();
            Driver.WaitForElementVisible(By.XPath(OpenHeartPath));
            this.OpenHeart.ClickJsEvent();
            Driver.WaitForElementExists(By.XPath(FullHeartPath), 2);
            Thread.Sleep(GetRandomTime(r, 1500, 2000));
        }

        //maybe need to change returned type 
        private PostDetails GoToNextPostDetails()
        {
            bool isRightPaginationArrow = true;
            try
            {
                isRightPaginationArrow = Driver.WaitForElementExists(By.XPath(RightPaginatorArrowPath)).Enabled;
            }
            catch (Exception)
            {

            }
            if (isRightPaginationArrow)
            {
                Driver.WaitForElementExists(By.XPath(RightPaginatorArrowPath), 2).ClickJs();
            }
            else
            {
                //some logic
            }
          
            Driver.WaitForElementExists(By.CssSelector(RightPaginatorArrowCss), 2).ClickJs();
            return new PostDetails();
        }


        private InstagramSearchResultsPage ClosePostDetailsPage()
        {
            this.CloseButton.ClickJs();
            return new InstagramSearchResultsPage();
        }

        private static int GetRandomTime(Random rand, int first, int second)
        {
            return rand.Next(first, second);
        }

        #endregion

    }
}
