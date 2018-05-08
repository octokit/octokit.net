using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckSuiteRequest
    {
        public long AppId { get; set; }
        public string CheckName { get; set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "AppId: {0}, CheckName: {1}", AppId, CheckName);
    }
}
