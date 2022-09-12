using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GistFork
    {
        public GistFork() { }

        public GistFork(string nodeId, User user, string url, DateTimeOffset createdAt)
        {
            NodeId = nodeId;
            User = user;
            Url = url;
            CreatedAt = createdAt;
        }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; private set; }

        /// <summary>
        /// The <see cref="User"/> that created this <see cref="GistFork"/>
        /// </summary>
        public User User { get; private set; }

        /// <summary>
        /// The API URL for this <see cref="GistFork"/>.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// The <see cref="DateTimeOffset"/> for when this <see cref="Gist"/> was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "User: {0}, Url: {1}", User.DebuggerDisplay, Url); }
        }
    }
}
