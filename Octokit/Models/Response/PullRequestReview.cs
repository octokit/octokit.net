using System;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestReview
    {
        public PullRequestReview() { }

        public PullRequestReview(long id)
        {
            Id = id;
        }

        public PullRequestReview(long id, string nodeId, string commitId, User user, string body, string htmlUrl, string pullRequestUrl, PullRequestReviewState state, AuthorAssociation authorAssociation, DateTimeOffset submittedAt)
        {
            Id = id;
            NodeId = nodeId;
            CommitId = commitId;
            User = user;
            Body = body;
            HtmlUrl = htmlUrl;
            PullRequestUrl = pullRequestUrl;
            State = state;
            AuthorAssociation = authorAssociation;
            SubmittedAt = submittedAt;
        }

        /// <summary>
        /// The review Id.
        /// </summary>
        public long Id { get; protected set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; protected set; }

        /// <summary>
        /// The state of the review
        /// </summary>
        public StringEnum<PullRequestReviewState> State { get; protected set; }

        /// <summary>
        /// The commit Id the review is associated with.
        /// </summary>
        public string CommitId { get; protected set; }

        /// <summary>
        /// The user that created the review.
        /// </summary>
        public User User { get; protected set; }

        /// <summary>
        /// The text of the review.
        /// </summary>
        public string Body { get; protected set; }

        /// <summary>
        /// The URL for this review on Github.com
        /// </summary>
        public string HtmlUrl { get; protected set; }

        /// <summary>
        /// The URL for the pull request via the API.
        /// </summary>
        public string PullRequestUrl { get; protected set; }

        /// <summary>
        /// The comment author association with repository.
        /// </summary>
        public StringEnum<AuthorAssociation> AuthorAssociation { get; protected set; }

        /// <summary>
        /// The time the review was submitted
        /// </summary>
        public DateTimeOffset SubmittedAt { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Id: {0}, State: {1}, User: {2}", Id, State.DebuggerDisplay, User.DebuggerDisplay); }
        }
    }

    public enum PullRequestReviewState
    {
        [Parameter(Value = "APPROVED")]
        Approved,

        [Parameter(Value = "CHANGES_REQUESTED")]
        ChangesRequested,

        [Parameter(Value = "COMMENTED")]
        Commented,

        [Parameter(Value = "DISMISSED")]
        Dismissed,

        [Parameter(Value = "PENDING")]
        Pending
    }
}
