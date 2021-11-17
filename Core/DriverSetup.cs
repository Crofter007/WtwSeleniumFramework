using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;

namespace WillisTowersWatson.Core
{
    [Binding]
    public sealed class DriverSetup
    {
        private readonly IObjectContainer _objectContainer;
        public IWebDriver Driver;
        public ScenarioContext _scenarioContext;

        public DriverSetup(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            InitChromeDriver();
            _objectContainer.RegisterInstanceAs(Driver);
        }

        private void InitChromeDriver()
        {
            var options = new ChromeOptions();

            options.AddArgument("disable-infobars");
            options.AddArgument("start-maximized");
            options.AddArgument("ignore-certificate-errors");
            options.AddArgument("disable-web-security");
            options.AddArgument("disable-popup-blocking");

            var driverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Driver = new ChromeDriver(driverPath, options);

            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Driver.Quit();
        }
    }
}
