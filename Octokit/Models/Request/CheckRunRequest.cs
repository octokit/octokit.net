using Octokit.Internal;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckRunRequest : RequestParameters
    {
        public string CheckName { get; set; }
        public CheckStatus? Status { get; set; }
        public CheckRunRequestFilter? Filter { get; set; }
        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "CheckName: {0}", CheckName);
    }

    public enum CheckRunRequestFilter
    {
        [Parameter(Value = "latest")]
        Latest,
        [Parameter(Value = "all")]
        All
    }
}
