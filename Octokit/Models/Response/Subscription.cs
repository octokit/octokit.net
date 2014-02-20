using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Subscription
    {
        /// <summary>
        /// Determines if notifications should be received from this repository.
        /// </summary>
        public bool Subscribed { get; set; }

        /// <summary>
        /// Determines if all notifications should be blocked from this repository.
        /// </summary>
        public bool Ignored { get; set; }

        /// <summary>
        /// Url of the label
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// The <see cref="DateTimeOffset"/> for when this <see cref="Subscription"/> was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The API URL for this <see cref="Subscription"/>.
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// The API URL for this <see cref="Repository"/>.
        /// </summary>
        public Uri RepositoryUrl { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Subscribed: {0}", Subscribed);
            }
        }
    }
}
