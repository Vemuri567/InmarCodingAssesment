using SpecFlowFramework;
using SpecFlowFramework.Hooks;
using TechTalk.SpecFlow.Infrastructure;

namespace UITestCases.Hooks
{
    [Binding]
    public class Hooks : GlobalHooks
    {
        public Hooks(GlobalSettings settings, ScenarioContext scenarioContext, FeatureContext featureContext, ISpecFlowOutputHelper specflowOutputHelper) : base(settings, scenarioContext, featureContext, specflowOutputHelper)
        {
        }
    }
}
