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

        public static void ClickJsEvent(this IWebElement element, IWebDriver driver)
        {
            const string mouseOverScript = @"if(document.createEvent)
                                                {var evObj = document.createEvent('MouseEvents');
                                                evObj.initEvent('click', true, false);
                                                arguments[0].dispatchEvent(evObj);}
                                                else if(document.createEventObject)
                                                { arguments[0].fireEvent('onclick');}";
            ((IJavaScriptExecutor)driver).ExecuteScript(mouseOverScript, element);
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
