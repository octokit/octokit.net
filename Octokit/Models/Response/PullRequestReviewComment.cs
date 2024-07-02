using System;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestReviewComment
    {
        public PullRequestReviewComment() { }

        public PullRequestReviewComment(long id)
        {
            Id = id;
        }

        public PullRequestReviewComment(string url, long id, string nodeId, string diffHunk, string path, int? position, int? originalPosition, string commitId, string originalCommitId, User user, string body, DateTimeOffset createdAt, DateTimeOffset updatedAt, string htmlUrl, string pullRequestUrl, ReactionSummary reactions, long? inReplyToId, long? pullRequestReviewId, AuthorAssociation authorAssociation)
        {
            PullRequestReviewId = pullRequestReviewId;
            Url = url;
            Id = id;
            NodeId = nodeId;
            DiffHunk = diffHunk;
            Path = path;
            Position = position;
            OriginalPosition = originalPosition;
            CommitId = commitId;
            OriginalCommitId = originalCommitId;
            User = user;
            Body = body;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            HtmlUrl = htmlUrl;
            PullRequestUrl = pullRequestUrl;
            Reactions = reactions;
            InReplyToId = inReplyToId;
            AuthorAssociation = authorAssociation;
        }

        /// <summary>
        /// URL of the comment via the API.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// The comment Id.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; private set; }

        /// <summary>
        /// The diff hunk the comment is about.
        /// </summary>
        public string DiffHunk { get; private set; }

        /// <summary>
        /// The relative path of the file the comment is about.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// The line index in the diff.
        /// </summary>
        public int? Position { get; private set; }

        /// <summary>
        /// The comment original position.
        /// </summary>
        public int? OriginalPosition { get; private set; }

        /// <summary>
        /// The commit Id the comment is associated with.
        /// </summary>
        public string CommitId { get; private set; }

        /// <summary>
        /// The original commit Id the comment is associated with.
        /// </summary>
        public string OriginalCommitId { get; private set; }

        /// <summary>
        /// The user that created the comment.
        /// </summary>
        public User User { get; private set; }

        /// <summary>
        /// The text of the comment.
        /// </summary>
        public string Body { get; private set; }

        /// <summary>
        /// The date the comment was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// The date the comment was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; private set; }

        /// <summary>
        /// The URL for this comment on GitHub.com
        /// </summary>
        public string HtmlUrl { get; private set; }

        /// <summary>
        /// The URL for the pull request via the API.
        /// </summary>
        public string PullRequestUrl { get; private set; }

        /// <summary>
        /// The reaction summary for this comment.
        /// </summary>
        public ReactionSummary Reactions { get; private set; }

        /// <summary>
        /// The Id of the comment this comment replys to.
        /// </summary>
        public long? InReplyToId { get; private set; }

        /// <summary>
        /// The Id of the pull request this comment belongs to.
        /// </summary>
        public long? PullRequestReviewId { get; private set; }

        /// <summary>
        /// The comment author association with repository.
        /// </summary>
        public StringEnum<AuthorAssociation> AuthorAssociation { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Id: {0}, Path: {1}, User: {2}, Url: {3}", Id, Path, User.DebuggerDisplay, Url); }
        }
    }

    public enum PullRequestReviewCommentSort
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
