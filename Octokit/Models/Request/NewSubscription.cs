using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewSubscription
    {
        /// <summary>
        /// Determines if notifications should be received from this repository.
        /// </summary>
        public bool Subscribed { get; set; }

        /// <summary>
        /// Determines if all notifications should be blocked from this repository.
        /// </summary>
        public bool Ignored { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Subscribed: {0} Ignored: {1}", Subscribed, Ignored);
            }
        }
    }
}
