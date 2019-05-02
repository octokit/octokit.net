using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    /// <summary>
    /// Check Suite preferences
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckSuitePreferences
    {
        public CheckSuitePreferences()
        {
        }

        /// <summary>
        /// Check Suite preferences
        /// </summary>
        /// <param name="autoTriggerChecks">Enables or disables automatic creation of CheckSuite events upon pushes to the repository. Enabled by default</param>
        public CheckSuitePreferences(IReadOnlyList<CheckSuitePreferenceAutoTrigger> autoTriggerChecks)
        {
            AutoTriggerChecks = autoTriggerChecks;
        }

        /// <summary>
        /// Enables or disables automatic creation of CheckSuite events upon pushes to the repository. Enabled by default
        /// </summary>
        public IReadOnlyList<CheckSuitePreferenceAutoTrigger> AutoTriggerChecks { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "AutoTriggerChecks: {0}", string.Join(", ", AutoTriggerChecks.Select(x => x.DebuggerDisplay)));
    }
}
