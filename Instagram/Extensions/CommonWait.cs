using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Instagram.Extensions
{
    public class CommonWait : WebDriverWait
    {
        public const int DefaultTimeoutSeconds = 15;
        private static readonly TimeSpan pollingInterval = TimeSpan.FromMilliseconds(100);

        public CommonWait(IWebDriver driver, int timeoutMilliseconds = DefaultTimeoutSeconds * 1000)
            : base(new SystemClock(), driver, TimeSpan.FromMilliseconds(timeoutMilliseconds), pollingInterval)
        { }
    }
}
