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

        public TeamDiscussion(string url, int number, string nodeId, string title, string body, string bodyHtml, string bodyVersion, bool @private, bool pinned, string teamUrl, string commentsUrl, int commentsCount, ReactionSummary reactions)
        {
            Url = url;
            Number = number;
            NodeId = nodeId;
            Title = title;
            Body = body;
            BodyHtml = bodyHtml;
            BodyVersion = bodyVersion;
            Private = @private;
            Pinned = pinned;
            TeamUrl = teamUrl;
            CommentsUrl = commentsUrl;
            CommentsCount = commentsCount;
            Reactions = reactions;
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
        /// team discussion body in HTML format
        /// </summary>
        public string BodyHtml { get; protected set; }

        /// <summary>
        /// team discussion body version
        /// </summary>
        public string BodyVersion { get; protected set; }

        /// <summary>
        /// is team discussion private
        /// </summary>
        public bool Private { get; protected set; }

        /// <summary>
        /// is team discussion pinned
        /// </summary>
        public bool Pinned { get; protected set; }

        /// <summary>
        /// URL of the team of this discussion
        /// </summary>
        public string TeamUrl { get; protected set; }

        /// <summary>
        /// URL of comments of this discussion
        /// </summary>
        public string CommentsUrl { get; protected set; }

        /// <summary>
        /// how many comments in this discussion
        /// </summary>
        public int CommentsCount { get; protected set; }

        /// <summary>
        /// reaction summary for this discussion
        /// </summary>
        public ReactionSummary Reactions { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Title: {0} ", Title); }
        }
    }
}
