using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// A historical version of a <see cref="Gist"/>
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GistHistory
    {
        public GistHistory() { }

        public GistHistory(string url, string version, User user, GistChangeStatus changeStatus, DateTimeOffset committedAt)
        {
            Url = url;
            Version = version;
            User = user;
            ChangeStatus = changeStatus;
            CommittedAt = committedAt;
        }

        /// <summary>
        /// The url that can be used by the API to retrieve this version of the <see cref="Gist"/>.
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// A git sha representing the version.
        /// </summary>
        public string Version { get; protected set; }

        /// <summary>
        /// The <see cref="User"/> who create this version.
        /// </summary>
        public User User { get; protected set; }

        /// <summary>
        /// A <see cref="GistHistory"/> that represents the level of change for this <see cref="GistHistory"/>.
        /// </summary>
        public GistChangeStatus ChangeStatus { get; protected set; }

        /// <summary>
        /// The <see cref="DateTimeOffset"/> the version was created.
        /// </summary>
        public DateTimeOffset CommittedAt { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "User: {0}, Url: {1}, Version: {2}, ChangeStatus: {3}", User.DebuggerDisplay, Url, Version, ChangeStatus); }
        }
    }
}