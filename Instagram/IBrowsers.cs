using OpenQA.Selenium;

namespace Instagram
{
    public interface IBrowsers
    {
        IWebDriver ChromeDriverInstance { get; set; }
        IWebDriver PhantomDriverInstance { get; set; }

        IWebDriver GetChromeDriver();
        IWebDriver GetPhantomDriver();

        void DisposeCurrentBrowser();
    }
}
