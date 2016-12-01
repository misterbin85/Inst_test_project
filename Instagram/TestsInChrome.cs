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
        private string _userName = ConfigurationManager.AppSettings["UserName"];
        private string _password = ConfigurationManager.AppSettings["Password"];


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
        public void Try()
        {
            Driver.OpenPage<InstagramSignUpPage>(new Uri("https://www.instagram.com/"), new object[] {Driver})
                .OpenLogin()
                .LoginToInstagram(_userName, _password)
                .OpenResultsForAHashTag("montenegro")
                .LoadMoreResults()
                .OpenFirstPostDetails()
                .MakeLikesOnPostDetails(10);
        }

    }
}
