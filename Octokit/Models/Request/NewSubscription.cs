using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to watch a repository (subscribe to repository's notifications). Called by the 
    /// <see cref="IWatchedClient.WatchRepo"/> method.
    /// </summary>
    /// <remarks>
    /// API: https://developer.github.com/v3/activity/watching/#set-a-repository-subscription
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewSubscription
    {
        /// <summary>
        /// Determines if notifications should be received from this repository.
        /// </summary>
        /// <remarks>
        /// If you would like to watch a repository, set subscribed to true. If you would like to ignore notifications
        /// made within a repository, set ignored to true. If you would like to stop watching a repository, delete the 
        /// repository’s subscription completely using the <see cref="IWatchedClient.UnwatchRepo"/> method.
        /// </remarks>
        public bool Subscribed { get; set; }

        /// <summary>
        /// Determines if all notifications should be blocked from this repository.
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
