using System;
using System.Collections.Generic;
using Instagram.Extensions;
using Instagram.Pages;
using Ninject;
using NUnit.Framework;
using System.Configuration;
using System.Linq;

namespace Instagram
{
    [TestFixture]
    public class TestsInChrome : SetUp
    {
        private static readonly Uri URL = new Uri(ConfigurationManager.AppSettings["InstagramUri"]);
        private string _userName = ConfigurationManager.AppSettings["UserName"];
        private string _password = ConfigurationManager.AppSettings["Password"];

        private List<string> hashtags = System.IO.File.ReadAllLines(ConfigurationManager.AppSettings["Hashtags"]).ToList();


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
        [TestCase(10)]
        [Description("Make Likes")]
        public void LetsPutSomeLikes(int numberOfPosts)
        {
           InstPages.InstagramSignUpP.Go(URL);
           InstagramMainFeedPage feedPage =  InstPages.InstagramSignUpP.OpenLogin()
                .LoginToInstagram(_userName, _password);

            hashtags.ForEach(
                tag => feedPage.OpenResultsForAHashTag(tag)
                    .LoadMoreResults()
                    .OpenFirstPostDetails()
                    .PutLikesOnPostDetails(numberOfPosts));
        }       
    }
}
