using System;
using Instagram.Extensions;
using Instagram.Pages;
using Ninject;
using NUnit.Framework;
using OpenQA.Selenium;
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
            Driver = kernel.Get<IBrowsers>().GetChromeDriver();
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Manage().Cookies.DeleteAllCookies();
            Driver.CLearBrowserLocalStorage();
        }

        [Test]
        [Description("Make Likes")]
        public void Try()
        {
            var instagramMainFeedPage = Driver.OpenPage<InstagramSignUpPage>(new Uri(URL), new object[] { Driver })
                  .OpenLogin()
                  .LoginToInstagram(_userName, _password);

            foreach (string hashtag in hashtags)
            {
                instagramMainFeedPage
                  .OpenResultsForAHashTag(hashtag)
                  .LoadMoreResults()
                  .OpenFirstPostDetails()
                  .MakeLikesOnPostDetails(50);
            }


        }

    }
}
