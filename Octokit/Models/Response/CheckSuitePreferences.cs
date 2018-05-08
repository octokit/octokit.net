namespace Octokit
{
    public class CheckSuitePreferences
    {
        public CheckSuitePreferences()
        {
        }

        public CheckSuitePreferences(AutoTriggerChecksObject preferences, Repository repository)
        {
            Preferences = preferences;
            Repository = repository;
        }

        public AutoTriggerChecksObject Preferences { get; protected set; }
        public Repository Repository { get; protected set; }
    }
}
