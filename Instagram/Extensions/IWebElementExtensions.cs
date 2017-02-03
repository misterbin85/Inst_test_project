using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Instagram.Extensions
{
    public static class IWebElementExtensions
    {
        private static IWebDriver Driver = Inj.Driver;

        public static void ScrollIntoView(this IWebElement element)
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public static void ClickJs(this IWebElement element)
        {
            element.WaitForVisible();
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].focus(); arguments[0].click();", element);
            Driver.WaitPageLoaded();
        }

        public static void FocusElementJs(this IWebElement element)
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].focus();", element);
        }

        public static void ClickJsEvent(this IWebElement element)
        {            
            const string mouseClickScript = @"if(document.createEvent)
                                                {var evObj = document.createEvent('MouseEvents');
                                                evObj.initEvent('click', true, false);
                                                arguments[0].dispatchEvent(evObj);}
                                                else if(document.createEventObject)
                                                { arguments[0].fireEvent('onclick');}";
            element.WaitForVisible();
            element.FocusElementJs();
            ((IJavaScriptExecutor)Driver).ExecuteScript(mouseClickScript, element);
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
            element.WaitForVisible();
            element.Clear();
            element.SendKeys(text);
        }
    }
}
