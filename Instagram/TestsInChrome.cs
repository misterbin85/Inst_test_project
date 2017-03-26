using System;
using System.Collections.Generic;
using Instagram.Extensions;
using Instagram.Pages;
using Ninject;
using NUnit.Framework;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Instagram
{
    [TestFixture]
    public class TestsInChrome : SetUp
    {
        private static readonly Uri URL = new Uri(ConfigurationManager.AppSettings["InstagramUri"]);
        private string _userName = ConfigurationManager.AppSettings["UserName"];
        private string _password = ConfigurationManager.AppSettings["Password"];

        private List<string> hashtags = File.ReadAllLines(ConfigurationManager.AppSettings["Hashtags"]).ToList();


        [SetUp]
        public void SetUp()
        {
            Inj.Driver = Inj.kernel.Get<IBrowsers>().GetChromeDriver();
        }

        [TearDown]
        public void TearDown()
        {
            Inj.Driver.Manage().Cookies.DeleteAllCookies();
            Inj.Driver.CLearBrowserLocalStorage();
        }


        [Test]
        [TestCase(70)]
        [Description("Make Likes")]
        public void LetsPutSomeLikes(int numberOfPosts)
        {
            InstPages.InstagramSignUpP.Open(URL);
            InstagramMainFeedPage feedPage = InstPages.InstagramSignUpP.OpenLogin()
                 .LoginToInstagram(_userName, _password);
            InstagramSearchResultsPage page;
            PostDetails postdet;
            /* hashtags.ForEach(tag =>
                      feedPage.OpenResultsForAHashTag(tag)                    
                     .OpenFirstPostDetails()
                     .PutLikesOnPostDetails(numberOfPosts));*/

            foreach (string tag in hashtags)
            {
                page = feedPage.OpenResultsForAHashTag(tag);
                postdet = page.OpenFirstPostDetails();

                if (postdet.PutLikesOnPostDetails(numberOfPosts))
                {
                    try
                    {
                        postdet.ClosePostDetailsPage();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Close deatails");
                    }
                }
               
            }
        
        }       
    }
}
