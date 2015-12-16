using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Notification
    {
        public Notification() { }

        public Notification(string id, Repository repository, NotificationInfo subject, string reason, bool unread, string updatedAt, string lastReadAt, string url)
        {
            Id = id;
            Repository = repository;
            Subject = subject;
            Reason = reason;
            Unread = unread;
            UpdatedAt = updatedAt;
            LastReadAt = lastReadAt;
            Url = url;
        }

        public string Id { get; protected set; } // NB: API currently returns this as string which is Weird

        public Repository Repository { get; protected set; }

        public NotificationInfo Subject { get; protected set; }

        public string Reason { get; protected set; }

        public bool Unread { get; protected set; }

        public string UpdatedAt { get; protected set; }

        public string LastReadAt { get; protected set; }

        public string Url { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Repository: {0} UpdatedAt: {1}", Repository, UpdatedAt); }
        }
    }
}
