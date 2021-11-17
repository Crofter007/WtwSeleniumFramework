using OpenQA.Selenium;
using WillisTowersWatson.Core;
using WillisTowersWatson.Objects;

namespace WillisTowersWatson.StepActions
{
    class IctActions : IctObjects
    {
        private const string baseUrl = "http://www.willistowerswatson.com/";

        public IctActions(IWebDriver driver) : base(driver)
        {
        }

        public void NavigateTo(string url)
        {
            Driver.NavigateTo($"{baseUrl}{url}");

            if (CookieWindowIsDispayed())
            {
                Driver.SwitchTo().Frame(CookieWindowFrame);
                AgreeButton.Click();
                Driver.SwitchTo().DefaultContent();
            }                
        }

        private bool CookieWindowIsDispayed()
        {
            return Driver.IsElementPresent(By.CssSelector("iframe[title='TrustArc Cookie Consent Manager']"), 3);
        }

        public void SelectRegionAndLanguage(string region, string language)
        {
            Driver.WaitForPageLoad();
            LangSelector.Click();
            Driver.WaitFor(d => LangSelector.GetAttribute("aria-expanded").Equals("true"));

            AmericasRegionSelector.Click();
            Driver.WaitFor(d => AmericasRegionSelector.GetAttribute("aria-expanded").Equals("true"));

            var linkCode = $"{GetLanguageCode(language)}-{GetRegionCode(region)}";
            Driver.WaitFor(d => LangCodeLink(linkCode).Displayed);

            //Actions actionProvider = new Actions(Driver);
            //actionProvider.MoveToElement(LangCodeLink(linkCode));

            LangCodeLink(linkCode).Click();

            Driver.WaitForPageLoad();
            Driver.WaitFor(d => d.Url.Contains($"www.willistowerswatson.com/{linkCode}"));
        }

        private string GetRegionCode(string region)
        {
            switch(region)
            {
                case "United States":
                    return "US";
                default:
                    return "GB";
            }
        }

        private string GetLanguageCode(string lang)
        {
            switch (lang)
            {
                case "Spanish":
                    return "es";
                default:
                    return "en";
            }
        }

        public void SearchFor(string text)
        {
            Driver.WaitForPageLoad();
            SearchButton.Click();
            Driver.WaitFor(d => SearchButton.GetAttribute("aria-expanded").Equals("true"));

            Driver.WaitForPageLoad();
            SearchInput.Click();
            SearchInput.SendKeys(text);

            Driver.WaitForPageLoad();

            SearchIcon.Click();
        }

        public bool ResultsDisplayed()
        {
            Driver.WaitForPageLoad();
            Driver.WaitFor(d => d.FindElement(ResultsTitle).Displayed, 20);
            return Driver.IsElementPresent(ResultsTitle, 10);
        }

        public void SortByDate()
        {
            if (DateSortBy.GetAttribute("class").Contains("selected"))
                return;
            else
            {
                DateSortBy.Click();
                Driver.WaitForPageLoad();
            }
        }

        public void FilterContentType(string type)
        {
            Driver.WaitForPageLoad();
            ContentTypeFilter(type).SendKeys(Keys.Enter);
            Driver.WaitFor(d => ContentTypeFilter(type).GetAttribute("aria-label").Contains("Unselect"));
            Driver.WaitForPageLoad();
        }

        public bool AtricleLinkTextStartsWith(string linkText)
        {
            int i = 0;
            bool nextPage;
            do
            {
                foreach (var link in ArticleLinks)
                {
                    if (!link.Text.StartsWith(linkText))
                        return false;
                }

                nextPage = Driver.IsElementPresent(NextPageButtonLocator, 2);

                if (nextPage)
                {
                    NextPageButton.Click();
                    Driver.WaitForPageLoad();
                }

                i++;
            } while (nextPage && i < 10);

            return true;
        }
    }
}
