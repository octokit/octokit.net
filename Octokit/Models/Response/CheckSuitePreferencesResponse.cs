using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckSuitePreferencesResponse
    {
        public CheckSuitePreferencesResponse()
        {
        }

        public CheckSuitePreferencesResponse(CheckSuitePreferences preferences, Repository repository)
        {
            Preferences = preferences;
            Repository = repository;
        }

        public CheckSuitePreferences Preferences { get; protected set; }

        public Repository Repository { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Preferences: {0}, Repository: {1}", Preferences.DebuggerDisplay, Repository.DebuggerDisplay);
    }
}
