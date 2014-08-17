using System;
using System.Diagnostics;
using Octokit.Internal;

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
    }
}
