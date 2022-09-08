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

        /// <summary>
        /// The check suite preferences
        /// </summary>
        public CheckSuitePreferences Preferences { get; private set; }

        /// <summary>
        /// The repository the check suite preferences are related to
        /// </summary>
        public Repository Repository { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Preferences: {0}, Repository: {1}", Preferences.DebuggerDisplay, Repository.DebuggerDisplay);
    }
}
