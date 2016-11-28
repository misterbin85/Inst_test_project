using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Instagram.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Instagram.Extensions
{
    public static class IwebDriverExtensions
    {
        public static IWait<IWebDriver> Wait(this IWebDriver driver, int timeoutSeconds = CommonWait.DefaultTimeoutSeconds)
        {
            return new CommonWait(driver, timeoutSeconds * 1000);
        }


        public static void CLearBrowserLocalStorage(this IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.localStorage.clear();");
        }


        public static void NavigateGoToUrl(this IWebDriver driver, Uri uri)
        {
            driver.Navigate().GoToUrl(uri);
            driver.Wait().Until(d => (bool) (d as IJavaScriptExecutor).ExecuteScript("return document.readyState == 'complete'"));
        }


        public static IEnumerable<Cookie> GetBrowserCookies(this IWebDriver driver)
        {
            return driver.Manage().Cookies.AllCookies.Select(c => new Cookie(c.Name, c.Value, c.Domain, c.Path, DateTime.Now));
        }


        public static T OpenPage<T>(this IWebDriver driver, Uri pageUri, object[] param = null) where T : new ()
        {       
            driver.NavigateGoToUrl(pageUri);     
            return (T)Activator.CreateInstance(typeof(T), param);
        }


        public static IWebElement WaitForElementExists(this IWebDriver driver, By locator, int timeoutSeconds = CommonWait.DefaultTimeoutSeconds)
        {
            driver.Wait(timeoutSeconds).Until(ExpectedConditions.ElementExists(locator));
            return driver.FindElement(locator);
        }


        public static IWebElement WaitForElementVisible(this IWebDriver driver, By locator, int timeoutSeconds = CommonWait.DefaultTimeoutSeconds)
        {
            driver.Wait(timeoutSeconds).Until(ExpectedConditions.ElementIsVisible(locator));
            return driver.FindElement(locator);
        }


        public static IWebElement WaitForElementClickable(this IWebDriver driver, By locator, int timeoutSeconds = CommonWait.DefaultTimeoutSeconds)
        {
            driver.Wait(timeoutSeconds).Until(ExpectedConditions.ElementToBeClickable(locator));
            return driver.FindElement(locator);
        }


        public static void WaitForElementInvisible(this IWebDriver driver, By locator, int timeoutSeconds = CommonWait.DefaultTimeoutSeconds)
        {
            driver.Wait(timeoutSeconds).Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }


        public static bool IsElementExists(this IWebDriver driver, By by, int attemptsToWait = 0)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (Exception)
            {
                if (attemptsToWait == 0) return false;

                attemptsToWait--;
                Thread.Sleep(1000);
                IsElementExists(driver, by, attemptsToWait);

                return false;
            }
        }


    }
}
