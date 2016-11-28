using NUnit.Framework;
using Ninject;

namespace Instagram
{
  public class SetUp
  {
    public StandardKernel kernel;

      [OneTimeSetUp]
      public virtual void OneTimeSetUp()
      {
          kernel = new StandardKernel();
          kernel.Bind<IBrowsers>().To<Browsers>();
      }

      [OneTimeTearDown]
      public virtual void OneTimeTearDown()
      {
          kernel.Get<IBrowsers>().DisposeCurrentBrowser();
          kernel.Dispose();
      }
  }
}
