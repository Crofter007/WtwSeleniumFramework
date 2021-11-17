using TechTalk.SpecFlow;

namespace WillisTowersWatson.Core
{
    public abstract class ContextContainer
    {
        protected readonly ScenarioContext Context;

        public ContextContainer(ScenarioContext injectedContext)
        {
            Context = injectedContext;
        }
    }
}
