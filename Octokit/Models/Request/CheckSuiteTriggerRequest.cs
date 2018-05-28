using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckSuiteTriggerRequest
    {
        public CheckSuiteTriggerRequest(string headSha)
        {
            HeadSha = headSha;
        }

        public string HeadSha { get; private set; }

        internal virtual string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "HeadSha: {0}", HeadSha);
    }
}