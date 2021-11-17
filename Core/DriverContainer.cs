using OpenQA.Selenium;

namespace WillisTowersWatson.Core
{
    public abstract class DriverContainer
    {
        protected readonly IWebDriver Driver;

        protected DriverContainer(IWebDriver driver)
        {
            Driver = driver;
        }
    }
}
