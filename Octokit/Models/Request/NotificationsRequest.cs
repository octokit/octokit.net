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
        /// Only show notifications updated after the given time. Defaults to the everything if unspecified.
        /// </summary>
        public DateTimeOffset? Since { get; set; }

        /// <summary>
        /// Only show notifications updated before the given time.
        /// </summary>
        /// <remarks>
        /// This is sent as a timestamp in ISO 8601 format: YYYY-MM-DDTHH:MM:SSZ.
        /// </remarks>
        /// <value>
        /// The before.
        /// </value>
        public DateTimeOffset? Before { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "All: {0}, Participating: {1}, Since: {2}", All, Participating, Since);
            }
        }
    }
}