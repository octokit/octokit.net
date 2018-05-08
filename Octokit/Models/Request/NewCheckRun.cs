using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewCheckRun : CheckRunUpdate
    {
        public string HeadBranch { get; set; }
        public string HeadSha { get; set; }

        internal override string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "HeadBranch: {0}, HeadSha{1}, {2}", HeadBranch, HeadSha, base.DebuggerDisplay);
    }
}
