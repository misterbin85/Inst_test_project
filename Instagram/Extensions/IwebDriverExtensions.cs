using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            driver.WaitPageLoaded();
        }

        public static void WaitPageLoaded(this IWebDriver driver)
        {
            driver.Wait().Until(d => (bool)(d as IJavaScriptExecutor).ExecuteScript("return document.readyState == 'complete'"));
        }

        public static IEnumerable<Cookie> GetBrowserCookies(this IWebDriver driver)
        {
            return driver.Manage().Cookies.AllCookies.Select(c => new Cookie(c.Name, c.Value, c.Domain, c.Path, DateTime.Now));
        }       


        public static IWebElement WaitForElementExists(this IWebDriver driver, By locator, int timeoutSeconds = CommonWait.DefaultTimeoutSeconds)
        {
            return driver.Wait(timeoutSeconds).Until(ExpectedConditions.ElementExists(locator));
        }


        public static IWebElement WaitForElementVisible(this IWebDriver driver, By locator, int timeoutSeconds = CommonWait.DefaultTimeoutSeconds)
        {
            return driver.Wait(timeoutSeconds).Until(ExpectedConditions.ElementIsVisible(locator));            
        }


        public static IWebElement WaitForElementClickable(this IWebDriver driver, By locator, int timeoutSeconds = CommonWait.DefaultTimeoutSeconds)
        {
            return driver.Wait(timeoutSeconds).Until(ExpectedConditions.ElementToBeClickable(locator));            
        }


        public static bool WaitForElementInvisible(this IWebDriver driver, By locator, int timeoutSeconds = CommonWait.DefaultTimeoutSeconds)
        {
            return driver.Wait(timeoutSeconds).Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }


        public static bool IsElementExists(this IWebDriver driver, By by, int attemptsToWait = 0)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                if (attemptsToWait == 0) return false;

                attemptsToWait--;
                Thread.Sleep(1000);
                return IsElementExists(driver, by, attemptsToWait);                
            }
        }


    }
}
