using System.Collections.Generic;

namespace Octokit
{
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
    }
}
