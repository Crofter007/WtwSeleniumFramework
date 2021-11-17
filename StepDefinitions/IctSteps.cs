using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using WillisTowersWatson.Core;
using WillisTowersWatson.StepActions;

namespace WillisTowersWatson.StepDefinitions
{
    [Binding]
    public sealed class IctSteps : ContextContainer
    {
        private readonly IctActions _ictActions;

        public IctSteps(ScenarioContext injectedContext, IWebDriver driver) : base(injectedContext)
        {
            _ictActions = new IctActions(driver);
        }

        [Given(@"I navigate to '(.*)'")]
        public void GivenINavigateTo(string url)
        {
            _ictActions.NavigateTo(url);
        }

        [When(@"I select region '(.*)' and language '(.*)'")]
        public void WhenISelectRegionAndLanguage(string region, string lang)
        {
            _ictActions.SelectRegionAndLanguage(region, lang);
        }

        [When(@"I search for '(.*)'")]
        public void WhenISearchFor(string searchText)
        {
            _ictActions.SearchFor(searchText);
        }

        [Then(@"I validate results page")]
        public void ThenIValidateResultsPage()
        {
            Assert.IsTrue(_ictActions.ResultsDisplayed());
        }

        [Then(@"I validate sort by Date")]
        public void ThenIValidateSortByDate()
        {
            _ictActions.SortByDate();
        }

        [When(@"I filter Content Type by '(.*)'")]
        public void WhenIFilterContentTypeBy(string filter)
        {
            _ictActions.FilterContentType(filter);
        }

        [Then(@"I validate article link starts with '(.*)'")]
        public void ThenIValidateArticleLinkStartsWith(string linkText)
        {
            Assert.IsTrue(_ictActions.AtricleLinkTextStartsWith(linkText));
        }

    }
}
