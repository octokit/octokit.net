using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GistFork
    {
        public GistFork() { }

        public GistFork(User user, string url, DateTimeOffset createdAt)
        {
            User = user;
            Url = url;
            CreatedAt = createdAt;
        }

        /// <summary>
        /// The <see cref="User"/> that created this <see cref="GistFork"/>
        /// </summary>
        public User User { get; protected set; }

        /// <summary>
        /// The API URL for this <see cref="GistFork"/>.
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// The <see cref="DateTimeOffset"/> for when this <see cref="Gist"/> was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "User: {0}, Url: {1}", User.DebuggerDisplay, Url); }
        }
    }
}