using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckSuiteEventPayload : ActivityPayload
    {
        public string Action { get; private set; }
        public CheckSuite CheckSuite { get; private set; }
    }
}
