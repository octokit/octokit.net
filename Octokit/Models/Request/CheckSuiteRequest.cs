using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Details to filter a check suite request, such as by App Id or check run name
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckSuiteRequest : RequestParameters
    {
        /// <summary>
        /// Filters check suites by GitHub App Id
        /// </summary>
        [Parameter(Key = "app_id")]
        public long? AppId { get; set; }

        /// <summary>
        /// Filters check suites by the name of the check run
        /// </summary>
        [Parameter(Key = "check_name")]
        public string CheckName { get; set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "AppId: {0}, CheckName: {1}", AppId, CheckName);
    }
}
