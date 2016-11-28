using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.PhantomJS;

namespace Instagram
{
  public class Browsers : IBrowsers
  {

#region 'Fields and properties'

      private const string chromePath = "C:\\";

      public static IWebDriver ChromeDriverInstance { get; set; }
      public static IWebDriver PhantomDriverInstance { get; set; }

      IWebDriver IBrowsers.PhantomDriverInstance
      {
          get { return PhantomDriverInstance; }
          set { PhantomDriverInstance = value; }
      }

      IWebDriver IBrowsers.ChromeDriverInstance
      {
          get { return ChromeDriverInstance; }
          set { ChromeDriverInstance = value; }
      }
         
#endregion


#region 'Methods'

    public IWebDriver GetChromeDriver()
    {
      if (ChromeDriverInstance != null) return ChromeDriverInstance;
      ChromeDriverService serv = ChromeDriverService.CreateDefaultService(chromePath);
      serv.HideCommandPromptWindow = true;
      ChromeOptions opt = new ChromeOptions();
      opt.AddArgument("--start-maximized");
      ChromeDriverInstance = new ChromeDriver(serv, opt);

      return ChromeDriverInstance;
    }    


    public IWebDriver GetPhantomDriver()
    {
      if (PhantomDriverInstance != null)
        return PhantomDriverInstance;

      const string phantomExePath = "C:\\";
      PhantomJSDriverService phDrServ = PhantomJSDriverService.CreateDefaultService(phantomExePath);
      phDrServ.IgnoreSslErrors = true;
      phDrServ.LoadImages = true;
      phDrServ.ProxyType = "none";
      phDrServ.HideCommandPromptWindow = true;
      PhantomJSOptions opt = new PhantomJSOptions();
      opt.AddAdditionalCapability("phantomjs.page.settings.userAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36");
      PhantomDriverInstance = new PhantomJSDriver(phDrServ, opt);

      return PhantomDriverInstance;
    }


      public void DisposeCurrentBrowser()
      {
          ChromeDriverInstance?.Dispose();
          PhantomDriverInstance?.Dispose();
      }

#endregion

  }
}
