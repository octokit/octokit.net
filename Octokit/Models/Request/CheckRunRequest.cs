using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Details to filter a check suite request, such as by App Id or check run name
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CheckRunRequest : RequestParameters
    {
        /// <summary>
        /// Returns check runs with the specified name.
        /// </summary>
        [Parameter(Key = "check_name")]
        public string CheckName { get; set; }

        /// <summary>
        /// Returns check runs with the specified status. Can be one of queued, in_progress, or completed.
        /// </summary>
        [Parameter(Key = "status")]
        public StringEnum<CheckStatusFilter>? Status { get; set; }

        /// <summary>
        /// Filters check runs by their completed_at timestamp. Can be one of latest (returning the most recent check runs) or all. Default: latest
        /// </summary>
        [Parameter(Key = "filter")]
        public StringEnum<CheckRunCompletedAtFilter>? Filter { get; set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "CheckName: {0}, Status: {1}", CheckName, Status);
    }

    public enum CheckStatusFilter
    {
        [Parameter(Value = "queued")]
        Queued,

        [Parameter(Value = "in_progress")]
        InProgress,

        [Parameter(Value = "completed")]
        Completed,
    }

    public enum CheckRunCompletedAtFilter
    {
        [Parameter(Value = "latest")]
        Latest,

        [Parameter(Value = "all")]
        All
    }
}
