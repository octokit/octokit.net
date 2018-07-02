using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckSuitePreferences
    {
        public CheckSuitePreferences()
        {
        }

        public CheckSuitePreferences(IReadOnlyList<CheckSuitePreferenceBool> autoTriggerChecks)
        {
            AutoTriggerChecks = autoTriggerChecks;
        }

        public IReadOnlyList<CheckSuitePreferenceBool> AutoTriggerChecks { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "AutoTriggerChecks: {0}", string.Join(", ", AutoTriggerChecks.Select(x => x.DebuggerDisplay)));
    }
}
