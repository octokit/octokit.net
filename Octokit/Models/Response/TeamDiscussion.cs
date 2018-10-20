using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Team discussion.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class TeamDiscussion
    {
        public TeamDiscussion() { }

        public TeamDiscussion(string url, int number, string nodeId, string title, string body, bool @private, int commentsCount)
        {
            Url = url;
            Number = number;
            NodeId = nodeId;
            Title = title;
            Body = body;
            Private = @private;
            CommentsCount = commentsCount;
        }

        /// <summary>
        /// url for this team discussion
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// team discussion id
        /// </summary>
        public int Number { get; protected set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; protected set; }

        /// <summary>
        /// team discussion title
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// team discussion body
        /// </summary>
        public string Body { get; protected set; }

        /// <summary>
        /// is team discussion private
        /// </summary>
        public bool Private { get; protected set; }

        /// <summary>
        /// permission attached to this team
        /// </summary>
        public StringEnum<Permission> Permission { get; protected set; }

        /// <summary>
        /// how many members in this team
        /// </summary>
        public int CommentsCount { get; protected set; }

        /////// <summary>
        /////// The parent team
        /////// </summary>
        ////public Team Parent { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Title: {0} ", Title); }
        }
    }
}
