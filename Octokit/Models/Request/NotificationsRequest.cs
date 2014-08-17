using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Specifies the parameters to filter notifications by
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NotificationsRequest : RequestParameters
    {
        /// <summary>
        /// If true, show notifications marked as read. Default: false
        /// </summary>
        public bool All { get; set; }

        /// <summary>
        /// If true, only shows notifications in which the user is directly participating or mentioned. Default: false
        /// </summary>
        public bool Participating { get; set; }

        /// <summary>
        /// Filters out any notifications updated before the given time. Default: Time.now
        /// </summary>
        public DateTimeOffset Since { get; set; }

        /// <summary>
        /// Construct a <see cref="NotificationsRequest"/> object
        /// </summary>
        public NotificationsRequest()
        {
            All = false;
            Participating = false;
            Since = DateTimeOffset.Now;
        }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "All: {0}, Participating: {1}, Since: {2}", All, Participating, Since);
            }
        }
    }
}