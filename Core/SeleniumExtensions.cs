using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;

namespace WillisTowersWatson.Core
{
    public static class SeleniumExtensions
    {
        public const int PollingInterval = 100;

        public static void NavigateTo(this IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
            driver.WaitForPageLoad();
        }

        public static void WaitForPageLoad(this IWebDriver driver, int timeoutSeconds = 30)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds))
            {
                PollingInterval = TimeSpan.FromMilliseconds(PollingInterval)
            };
            wait.IgnoreExceptionTypes(typeof(Exception));

            wait.Until(webDriver => driver.PageLoaded());
        }

        private static bool PageLoaded(this IWebDriver driver)
        {
            return driver.ExecuteJavaScript<string>("return document.readyState").ToLower() == "complete";
        }

        public static bool IsElementPresent(this IWebDriver driver, By by, int timeoutSeconds = 5)
        {
            bool present;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutSeconds);

            try
            {
                driver.FindElement(by);
                present = true;
            }
            catch (NoSuchElementException)
            {
                present = false;
            }
            finally
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }

            return present;
        }

        public static void WaitFor(this IWebDriver driver, Predicate<IWebDriver> predicate, int timeoutSeconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            wait.IgnoreExceptionTypes(typeof(Exception));
            wait.Until(d => predicate(d));
        }

        public static IWebElement WaitUntilClickable(this IWebDriver driver, By by, int timeoutSeconds = 20)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds))
            {
                PollingInterval = TimeSpan.FromMilliseconds(PollingInterval)
            };
            wait.IgnoreExceptionTypes(typeof(Exception), typeof(StaleElementReferenceException));
            return wait.Until(ExpectedConditions.ElementToBeClickable(by));
        }
    }
}
