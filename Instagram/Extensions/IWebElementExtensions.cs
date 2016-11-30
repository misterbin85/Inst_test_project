using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Instagram.Extensions
{
    public static class IWebElementExtensions
    {
        public static void ScrollIntoView(this IWebElement element, IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public static void ClickJs(this IWebElement element, IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
            driver.WaitPageLoaded();
        }

        public static IWebElement WaitForVisible(this IWebElement element, double seconds = 5.0)
        {
            IWait<IWebElement> wait = new DefaultWait<IWebElement>(element);
            wait.Timeout = TimeSpan.FromSeconds(seconds);
            wait.PollingInterval = TimeSpan.FromMilliseconds(250);
            Func<IWebElement, bool> waiter = el => el.Enabled && el.Displayed;
            wait.Until(waiter);

            return element;
        }

        public static void SendText(this IWebElement element, string text)
        {
            element.Clear();
            element.SendKeys(text);
        }
    }
}
