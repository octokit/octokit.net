using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewCheckRun : CheckRunUpdate
    {
        public NewCheckRun(string name, string headBranch, string headSha) : base(name)
        {
            HeadBranch = headBranch;
            HeadSha = headSha;
        }

        public string HeadBranch { get; private set; }
        public string HeadSha { get; private set; }

        internal override string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "HeadBranch: {0}, HeadSha{1}, {2}", HeadBranch, HeadSha, base.DebuggerDisplay);
    }
}
