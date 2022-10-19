using System;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class IssueComment
    {
        public IssueComment() { }

        public IssueComment(int id, string nodeId, string url, string htmlUrl, string body, DateTimeOffset createdAt, DateTimeOffset? updatedAt, User user, ReactionSummary reactions, AuthorAssociation authorAssociation)
        {
            Id = id;
            NodeId = nodeId;
            Url = url;
            HtmlUrl = htmlUrl;
            Body = body;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            User = user;
            Reactions = reactions;
            AuthorAssociation = authorAssociation;
        }

        /// <summary>
        /// The issue comment Id.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; private set; }

        /// <summary>
        /// The URL for this issue comment.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// The html URL for this issue comment.
        /// </summary>
        public string HtmlUrl { get; private set; }

        /// <summary>
        /// Details about the issue comment.
        /// </summary>
        public string Body { get; private set; }

        /// <summary>
        /// The date the issue comment was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// The date the issue comment was last updated.
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; private set; }

        /// <summary>
        /// The user that created the issue comment.
        /// </summary>
        public User User { get; private set; }

        /// <summary>
        /// The comment author association with repository.
        /// </summary>
        public StringEnum<AuthorAssociation> AuthorAssociation { get; private set; }

        /// <summary>
        /// The reaction summary for this comment.
        /// </summary>
        public ReactionSummary Reactions { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Id: {0} CreatedAt: {1}", Id, CreatedAt); }
        }
    }

    public enum IssueCommentSort
    {
        /// <summary>
        /// Sort by create date (default)
        /// </summary>
        [Parameter(Value = "created")]
        Created,

        /// <summary>
        /// Sort by the date of the last update
        /// </summary>
        [Parameter(Value = "updated")]
        Updated
    }
}
