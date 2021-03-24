using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CommitPullRequest
    {
        public CommitPullRequest() { }

        public CommitPullRequest(int number)
        {
            Number = number;
        }

        public CommitPullRequest(long id, string nodeId, string url, string htmlUrl, string diffUrl, string patchUrl, string issueUrl, string statusesUrl, int number, ItemState state, string title, string body, DateTimeOffset createdAt, DateTimeOffset updatedAt, DateTimeOffset? closedAt, DateTimeOffset? mergedAt, GitReference head, GitReference @base, User user, User assignee, IReadOnlyList<User> assignees, bool draft, bool? mergeable, MergeableState? mergeableState, User mergedBy, string mergeCommitSha, Milestone milestone, IReadOnlyList<User> requestedReviewers, IReadOnlyList<Team> requestedTeams, IReadOnlyList<Label> labels)
        {
            Id = id;
            NodeId = nodeId;
            Url = url;
            HtmlUrl = htmlUrl;
            DiffUrl = diffUrl;
            PatchUrl = patchUrl;
            IssueUrl = issueUrl;
            StatusesUrl = statusesUrl;
            Number = number;
            State = state;
            Title = title;
            Body = body;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            ClosedAt = closedAt;
            MergedAt = mergedAt;
            Head = head;
            Base = @base;
            User = user;
            Assignee = assignee;
            Assignees = assignees;
            Draft = draft;
            MergeCommitSha = mergeCommitSha;
            Milestone = milestone;
            RequestedReviewers = requestedReviewers;
            RequestedTeams = requestedTeams;
            Labels = labels;
        }

        /// <summary>
        /// The internal Id for this pull request (not the pull request number)
        /// </summary>
        public long Id { get; protected set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; protected set; }

        /// <summary>
        /// The URL for this pull request.
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// The URL for the pull request page.
        /// </summary>
        public string HtmlUrl { get; protected set; }

        /// <summary>
        /// The URL for the pull request's diff (.diff) file.
        /// </summary>
        public string DiffUrl { get; protected set; }

        /// <summary>
        /// The URL for the pull request's patch (.patch) file.
        /// </summary>
        public string PatchUrl { get; protected set; }

        /// <summary>
        /// The URL for the specific pull request issue.
        /// </summary>
        public string IssueUrl { get; protected set; }

        /// <summary>
        /// The URL for the pull request statuses.
        /// </summary>
        public string StatusesUrl { get; protected set; }

        /// <summary>
        /// The pull request number.
        /// </summary>
        public int Number { get; protected set; }

        /// <summary>
        /// Whether the pull request is open or closed. The default is <see cref="ItemState.Open"/>.
        /// </summary>
        public StringEnum<ItemState> State { get; protected set; }

        /// <summary>
        /// Title of the pull request.
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// The body (content) contained within the pull request.
        /// </summary>
        public string Body { get; protected set; }

        /// <summary>
        /// When the pull request was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; protected set; }

        /// <summary>
        /// When the pull request was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; protected set; }

        /// <summary>
        /// When the pull request was closed.
        /// </summary>
        public DateTimeOffset? ClosedAt { get; protected set; }

        /// <summary>
        /// When the pull request was merged.
        /// </summary>
        public DateTimeOffset? MergedAt { get; protected set; }

        /// <summary>
        /// The HEAD reference for the pull request.
        /// </summary>
        public GitReference Head { get; protected set; }

        /// <summary>
        /// The BASE reference for the pull request.
        /// </summary>
        public GitReference Base { get; protected set; }

        /// <summary>
        /// The user who created the pull request.
        /// </summary>
        public User User { get; protected set; }

        /// <summary>
        /// The user who is assigned the pull request.
        /// </summary>
        public User Assignee { get; protected set; }

        /// <summary>
        ///The multiple users this pull request is assigned to.
        /// </summary>
        public IReadOnlyList<User> Assignees { get; protected set; }

        /// <summary>
        /// The milestone, if any, that this pull request is assigned to.
        /// </summary>
        public Milestone Milestone { get; protected set; }

        /// <summary>
        /// Whether or not the pull request is in a draft state, and cannot be merged.
        /// </summary>
        public bool Draft { get; protected set; }

        /// <summary>
        /// Whether or not the pull request has been merged.
        /// </summary>
        public bool Merged
        {
            get { return MergedAt.HasValue; }
        }

        /// <summary>
        /// The value of this field changes depending on the state of the pull request.
        /// Not Merged - the hash of the test commit used to determine mergeability.
        /// Merged with merge commit - the hash of said merge commit.
        /// Merged via squashing - the hash of the squashed commit added to the base branch.
        /// Merged via rebase - the hash of the commit that the base branch was updated to.
        /// </summary>
        public string MergeCommitSha { get; protected set; }

        /// <summary>
        /// Users requested for review
        /// </summary>
        public IReadOnlyList<User> RequestedReviewers { get; protected set; }

        /// <summary>
        /// Teams requested for review
        /// </summary>
        public IReadOnlyList<Team> RequestedTeams { get; protected set; }

        public IReadOnlyList<Label> Labels { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Number: {0} State: {1}", Number, State); }
        }
    }
}
