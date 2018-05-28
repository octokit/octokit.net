using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewCheckSuite
    {
        public NewCheckSuite(string headSha)
        {
            HeadSha = headSha;
        }

        public string HeadSha { get; private set; }

        public string HeadBranch { get; set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "HeadSha: {0} HeadBranch: {1}", HeadSha, HeadBranch);
    }
}
