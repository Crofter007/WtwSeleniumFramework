using OpenQA.Selenium;
using System.Collections.ObjectModel;
using WillisTowersWatson.Core;

namespace WillisTowersWatson.Objects
{
    public abstract class IctObjects : DriverContainer
    {
        protected By AgreeButtonLocator => By.XPath("//a[@role='button' and text()='Agree and Proceed']");
        protected IWebElement AgreeButton => Driver.FindElement(AgreeButtonLocator);
        protected By CookieWindowFrameLocator => By.CssSelector("iframe[title='TrustArc Cookie Consent Manager']");
        protected IWebElement CookieWindowFrame => Driver.FindElement(CookieWindowFrameLocator);
        protected IWebElement LangSelector => Driver.FindElement(By.CssSelector("button[aria-controls='country-selector']"));
        protected IWebElement AmericasRegionSelector => Driver.FindElement(By.Id("region-0"));
        protected IWebElement LangCodeLink(string text) => Driver.FindElement(By.CssSelector($"a[href='/{text}']"));
        protected IWebElement SearchButton => Driver.FindElement(By.CssSelector("button[data-menu='search-menu-is-visible']"));
        protected IWebElement SearchInput => Driver.FindElement(By.CssSelector("input[aria-label='Search box']"));
        protected IWebElement SearchIcon => Driver.FindElement(By.CssSelector("a[role='button'][aria-label='Search']"));
        protected By ResultsTitle => By.XPath("//div[@class='wtw-coveo-split-header']//span[contains(text(),'Results')]");
        protected IWebElement DateSortBy => Driver.FindElement(By.CssSelector("span[aria-label='Sort results by Date']"));
        protected IWebElement ContentTypeFilter(string type) => Driver.FindElement(By.CssSelector($"div[data-title='Content Type'] li[data-value='{type}'] div[class='coveo-facet-value-checkbox']"));
        protected ReadOnlyCollection<IWebElement> ArticleLinks => Driver.FindElements(By.CssSelector("div[class='CoveoFieldValue wtw-listing-result-uri'] span"));
        protected IWebElement NextPageButton => Driver.FindElement(NextPageButtonLocator);
        protected By NextPageButtonLocator => By.CssSelector("li[role='button'][aria-label='Next']");


        public IctObjects(IWebDriver driver) : base(driver)
        {
        }
    }
}
