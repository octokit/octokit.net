using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AutoTriggerChecksObject
    {
        public AutoTriggerChecksObject()
        {
        }

        public AutoTriggerChecksObject(IReadOnlyList<CheckSuitePreference> autoTriggerChecks)
        {
            AutoTriggerChecks = autoTriggerChecks;
        }

        public IReadOnlyList<CheckSuitePreference> AutoTriggerChecks { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "AutoTriggerChecks: {0}", string.Join(", ", AutoTriggerChecks.Select(x => x.DebuggerDisplay)));
    }
}
