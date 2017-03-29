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

        public bool PutLikesOnPostDetails(int numberOfLikedPosts)
        {
            bool connector = true;
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
                    connector = false;
                    break;
                }

                if (AlreadyLiked())
                {
                    if (!GoToNextPostDetails())
                    {
                        if (Driver.IsElementExists(By.CssSelector("div.error-container a")))
                        {
                            Driver.FindElement(By.CssSelector("div.error-container a")).ClickJs();
                            connector = false;
                            break;
                        }
                        else { Console.WriteLine("Why"); }
                    }
                }
                else
                {
                    PutLike();
                    ++numberOfLikedPics;
                    if (!GoToNextPostDetails())
                    {
                        //Driver.WaitForElementExists(By.CssSelector("div.error-container a"), 2).ClickJs();
                        connector = false;
                        break;
                    }
                    
                }
            }
            Console.WriteLine("Liked = " + numberOfLikedPics);
            return connector;
        }


        private void PutLike()
        {
            r = new Random();
            Driver.WaitForElementVisible(By.XPath(OpenHeartPath));
            this.OpenHeart.ClickJsEvent();
            Driver.WaitForElementExists(By.XPath(FullHeartPath), 2);
            Thread.Sleep(GetRandomTime(r, 1500, 2000));
        }

        private bool GoToNextPostDetails()
        {
            bool returnedBool = true;
         
            if (Driver.IsElementExists(By.CssSelector(RightPaginatorArrowCss)))
            {
                RightPaginatorArrow.ClickJs();
                
            }
            else
            {
                returnedBool = false;
            }
          
            return returnedBool;
        }


        public InstagramSearchResultsPage ClosePostDetailsPage()
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
