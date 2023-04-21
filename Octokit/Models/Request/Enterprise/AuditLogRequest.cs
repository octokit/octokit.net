using Octokit.Internal;
using System.Diagnostics;
using System.Globalization;

namespace Octokit.Models.Request.Enterprise
{
    /// <summary>
    /// Used to filter a audit log request.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AuditLogRequest : RequestParameters
    {
        public AuditLogRequest()
        {
            After = null;
            Before = null;
            Phrase = null;
            Filter = IncludeFilter.Web;
            SortDirection = AuditLogSortDirection.Descending;
        }

        [Parameter(Key = "after")]
        public string After { get; set; }

        [Parameter(Key = "before")]
        public string Before { get; set; }

        [Parameter(Key = "include")]
        public IncludeFilter Filter { get; set; }

        [Parameter(Key = "phrase")]
        public string Phrase { get; set; }

        [Parameter(Key = "order")]
        public AuditLogSortDirection SortDirection { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Filter: {0} SortDirection: {1}", Filter, SortDirection);
            }
        }
    }

    /// <summary>
    /// The range of filters available for event types.
    /// </summary>
    /// <remarks>https://docs.github.com/en/enterprise-cloud@latest/rest/enterprise-admin/audit-log</remarks>
    public enum IncludeFilter
    {
        /// <summary>
        /// Web (non-git) events. (Default)
        /// </summary>
        [Parameter(Value = "web")]
        Web,

        /// <summary>
        /// Git events.
        /// </summary>
        [Parameter(Value = "git")]
        Git,

        /// <summary>
        /// Both web and Git events.
        /// </summary>
        [Parameter(Value = "all")]
        All
    }

    /// <summary>
    /// The two possible sort directions.
    /// </summary>
    public enum AuditLogSortDirection
    {
        /// <summary>
        /// Sort ascending
        /// </summary>
        [Parameter(Value = "asc")]
        Ascending,

        /// <summary>
        /// Sort descending
        /// </summary>
        [Parameter(Value = "desc")]
        Descending
    }
}
