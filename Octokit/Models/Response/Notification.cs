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

        public string Id { get; private set; } // NB: API currently returns this as string which is Weird

        public Repository Repository { get; private set; }

        public NotificationInfo Subject { get; private set; }

        public string Reason { get; private set; }

        public bool Unread { get; private set; }

        public string UpdatedAt { get; private set; }

        public string LastReadAt { get; private set; }

        public string Url { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Repository: {0} UpdatedAt: {1}", Repository, UpdatedAt); }
        }
    }
}
