using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewCheckSuite : CheckSuiteTriggerRequest
    {
        public string HeadBranch { get; set; }
        internal override string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "HeadBranch: {0}, {1}", HeadBranch, base.DebuggerDisplay);
    }
}
