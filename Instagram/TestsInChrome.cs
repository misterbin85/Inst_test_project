using System;
using Instagram.Extensions;
using Instagram.Pages;
using Ninject;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Instagram
{
  [TestFixture]
  public class TestsInChrome : SetUp
  {
      private IWebDriver Driver;

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
          Driver.OpenPage<InstagramSignUpPage>(new Uri("https://www.instagram.com/"), new object[]{Driver})
                .OpenLogin()
                .LoginToInstagram("", "", true)
                .SearchForAHashTag("#montenegro")
                .LoadMoreResults();
      }

    }
}
