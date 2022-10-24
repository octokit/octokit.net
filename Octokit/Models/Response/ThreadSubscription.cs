﻿using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ThreadSubscription
    {
        public ThreadSubscription() { }

        public ThreadSubscription(bool subscribed, bool ignored, string reason, DateTimeOffset createdAt, string url, string threadUrl)
        {
            Subscribed = subscribed;
            Ignored = ignored;
            Reason = reason;
            CreatedAt = createdAt;
            Url = url;
            ThreadUrl = threadUrl;
        }

        /// <summary>
        /// Determines if notifications should be received from this repository.
        /// </summary>
        public bool Subscribed { get; private set; }

        /// <summary>
        /// Determines if all notifications should be blocked from this repository.
        /// </summary>
        public bool Ignored { get; private set; }

        /// <summary>
        /// Url of the label
        /// </summary>
        public string Reason { get; private set; }

        /// <summary>
        /// The <see cref="DateTimeOffset"/> for when this <see cref="Subscription"/> was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// The API URL for this <see cref="Subscription"/>.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// The API URL for this thread.
        /// </summary>
        public string ThreadUrl { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Subscribed: {0}", Subscribed); }
        }
    }
}
