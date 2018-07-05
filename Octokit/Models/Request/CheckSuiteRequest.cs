using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckSuiteRequest : RequestParameters
    {
        [Parameter(Key = "app_id")]
        public long? AppId { get; set; }

        [Parameter(Key = "check_name")]
        public string CheckName { get; set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "AppId: {0}, CheckName: {1}", AppId, CheckName);
    }
}
