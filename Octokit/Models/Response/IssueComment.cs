using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class IssueComment
    {
        public IssueComment() { }

        public IssueComment(int id, Uri url, Uri htmlUrl, string body, DateTimeOffset createdAt, DateTimeOffset? updatedAt, User user)
        {
            Id = id;
            Url = url;
            HtmlUrl = htmlUrl;
            Body = body;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            User = user;
        }

        /// <summary>
        /// The issue comment Id.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The URL for this issue comment.
        /// </summary>
        public Uri Url { get; protected set; }

        /// <summary>
        /// The html URL for this issue comment.
        /// </summary>
        public Uri HtmlUrl { get; protected set; }

        /// <summary>
        /// Details about the issue comment.
        /// </summary>
        public string Body { get; protected set; }

        /// <summary>
        /// The date the issue comment was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; protected set; }

        /// <summary>
        /// The date the issue comment was last updated.
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; protected set; }

        /// <summary>
        /// The user that created the issue comment.
        /// </summary>
        public User User { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Id: {0} CreatedAt: {1}", Id, CreatedAt); }
        }
    }
}
