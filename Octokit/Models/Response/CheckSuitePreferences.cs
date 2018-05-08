using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
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

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Preferences: {0}, Repository: {1}", Preferences.DebuggerDisplay, Repository.DebuggerDisplay);
    }
}
