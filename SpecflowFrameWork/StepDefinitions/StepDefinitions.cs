
namespace SpecFlowFramework.StepDefinitions
{
    public abstract class StepDefinitions:Steps
    {
        private GlobalSettings _settings;
        public GlobalSettings Settings => _settings;

        public StepDefinitions(GlobalSettings settings)
        {
            _settings = settings;
        }
    }
}

