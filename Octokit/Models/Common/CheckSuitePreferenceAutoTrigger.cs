using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Enables or disables automatic creation of CheckSuite events upon pushes to the repository
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckSuitePreferenceAutoTrigger
    {
        public CheckSuitePreferenceAutoTrigger()
        {
        }

        /// <summary>
        /// Enables or disables automatic creation of CheckSuite events upon pushes to the repository
        /// </summary>
        /// <param name="appId">The Id of the GitHub App (required)</param>
        /// <param name="setting">Set to true to enable automatic creation of CheckSuite events upon pushes to the repository, or false to disable them (required)</param>
        public CheckSuitePreferenceAutoTrigger(long appId, bool setting)
        {
            AppId = appId;
            Setting = setting;
        }

        /// <summary>
        /// The Id of the GitHub App
        /// </summary>
        public long AppId { get; protected set; }

        /// <summary>
        /// Set to true to enable automatic creation of CheckSuite events upon pushes to the repository, or false to disable them
        /// </summary>
        public bool Setting { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "{0}: {1}", AppId, Setting);
    }
}