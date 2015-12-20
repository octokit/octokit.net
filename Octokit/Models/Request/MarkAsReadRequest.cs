using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to mark a notification as "read" which removes it from the default view on GitHub.com.
    /// </summary>
    /// <remarks>
    /// https://developer.github.com/v3/activity/notifications/#mark-as-read
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class MarkAsReadRequest : RequestParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarkAsReadRequest"/> class.
        /// </summary>
        public MarkAsReadRequest()
        {
            LastReadAt = null;
        }

        /// <summary>
        /// Describes the last point that notifications were checked. Anything updated since this time will not be
        /// updated.
        /// </summary>
        /// <remarks>
        /// This is sent as a timestamp in ISO 8601 format: YYYY-MM-DDTHH:MM:SSZ. Default: the current time.
        /// </remarks>
        public DateTimeOffset? LastReadAt { get; set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "LastReadAt: {0}", LastReadAt); }
        }
    }
}
