using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Represents updatable fields for a users subscription to a given thread
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewThreadSubscription
    {
        /// <summary>
        /// Determines if notifications should be received from this thread
        /// </summary>
        public bool Subscribed { get; set; }

        /// <summary>
        /// Determines if all notifications should be blocked from this thread
        /// </summary>
        public bool Ignored { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Subscribed: {0} Ignored: {1}", Subscribed, Ignored);
            }
        }
    }
}