using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckSuiteTriggerRequest
    {
        public string HeadSha { get; set; }

        internal virtual string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "HeadSha: {0}", HeadSha);
    }
}