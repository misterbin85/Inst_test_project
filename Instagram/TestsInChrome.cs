using System;
using Instagram.Extensions;
using Instagram.Pages;
using Ninject;
using NUnit.Framework;
using System.Configuration;

namespace Instagram
{
    [TestFixture]
    public class TestsInChrome : SetUp
    {
        private IWebDriver Driver;
        private const string URL = "https://www.instagram.com/";

        private string _userName = ConfigurationManager.AppSettings["UserName"];
        private string _password = ConfigurationManager.AppSettings["Password"];

        string[] hashtags = System.IO.File.ReadAllLines(ConfigurationManager.AppSettings["Hashtags"]);


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
        [Description("Make Likes")]
        public void Try()
        {
            InstPages.InstagramSignUpP.Go(new Uri("https://www.instagram.com/"));
            InstPages.InstagramSignUpP.OpenLogin()
                .LoginToInstagram(_userName, _password)
                .OpenResultsForAHashTag("flickr")
                .LoadMoreResults()
                .OpenFirstPostDetails()
                .MakeLikesOnPostDetails(10);
        }

    }
}
