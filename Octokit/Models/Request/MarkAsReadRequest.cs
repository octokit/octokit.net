using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class MarkAsReadRequest : RequestParameters
    {
        public MarkAsReadRequest()
        {
            LastReadAt = null;
        }

        /// <summary>
        /// Describes the last point that notifications were checked. Anything updated since this time will not be updated.
        /// </summary>
        public DateTimeOffset? LastReadAt { get; set; }

        internal string DebuggerDisplay
        {
            get { return String.Format(CultureInfo.InvariantCulture, "LastReadAt: {0}", LastReadAt); }
        }
    }
}
