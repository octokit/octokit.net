using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequest
    {
        public PullRequest() { }

        public PullRequest(int number)
        {
            Number = number;
        }

        public PullRequest(long id, string nodeId, string url, string htmlUrl, string diffUrl, string patchUrl, string issueUrl, string statusesUrl, int number, ItemState state, string title, string body, DateTimeOffset createdAt, DateTimeOffset updatedAt, DateTimeOffset? closedAt, DateTimeOffset? mergedAt, GitReference head, GitReference @base, User user, User assignee, IReadOnlyList<User> assignees, bool draft, bool? mergeable, MergeableState? mergeableState, User mergedBy, string mergeCommitSha, int comments, int commits, int additions, int deletions, int changedFiles, Milestone milestone, bool locked, bool? maintainerCanModify, IReadOnlyList<User> requestedReviewers, IReadOnlyList<Team> requestedTeams, IReadOnlyList<Label> labels)
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
            Mergeable = mergeable;
            MergeableState = mergeableState;
            MergedBy = mergedBy;
            MergeCommitSha = mergeCommitSha;
            Comments = comments;
            Commits = commits;
            Additions = additions;
            Deletions = deletions;
            ChangedFiles = changedFiles;
            Milestone = milestone;
            Locked = locked;
            MaintainerCanModify = maintainerCanModify;
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
        /// Whether or not the pull request can be merged.
        /// </summary>
        public bool? Mergeable { get; protected set; }

        /// <summary>
        /// Provides extra information regarding the mergeability of the pull request.
        /// </summary>
        public StringEnum<MergeableState>? MergeableState { get; protected set; }

        /// <summary>
        /// The user who merged the pull request.
        /// </summary>
        public User MergedBy { get; protected set; }

        /// <summary>
        /// The value of this field changes depending on the state of the pull request.
        /// Not Merged - the hash of the test commit used to determine mergeability.
        /// Merged with merge commit - the hash of said merge commit.
        /// Merged via squashing - the hash of the squashed commit added to the base branch.
        /// Merged via rebase - the hash of the commit that the base branch was updated to.
        /// </summary>
        public string MergeCommitSha { get; protected set; }

        /// <summary>
        /// Total number of comments contained in the pull request.
        /// </summary>
        public int Comments { get; protected set; }

        /// <summary>
        /// Total number of commits contained in the pull request.
        /// </summary>
        public int Commits { get; protected set; }

        /// <summary>
        /// Total number of additions contained in the pull request.
        /// </summary>
        public int Additions { get; protected set; }

        /// <summary>
        /// Total number of deletions contained in the pull request.
        /// </summary>
        public int Deletions { get; protected set; }

        /// <summary>
        /// Total number of files changed in the pull request.
        /// </summary>
        public int ChangedFiles { get; protected set; }

        /// <summary>
        /// If the issue is locked or not
        /// </summary>
        public bool Locked { get; protected set; }

        /// <summary>
        /// Whether maintainers of the base repository can push to the HEAD branch
        /// </summary>
        public bool? MaintainerCanModify { get; protected set; }

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

    /// <summary>
    /// Provides extra information regarding the mergeability of a pull request
    /// </summary>
    public enum MergeableState
    {
        /// <summary>
        /// Merge conflict. Merging is blocked.
        /// </summary>
        [Parameter(Value = "dirty")]
        Dirty,

        /// <summary>
        /// Mergeability was not checked yet. Merging is blocked.
        /// </summary>
        [Parameter(Value = "unknown")]
        Unknown,

        /// <summary>
        /// Failing/missing required status check.  Merging is blocked.
        /// </summary>
        [Parameter(Value = "blocked")]
        Blocked,

        /// <summary>
        /// Head branch is behind the base branch. Only if required status checks is enabled but loose policy is not. Merging is blocked.
        /// </summary>
        [Parameter(Value = "behind")]
        Behind,

        /// <summary>
        /// Failing/pending commit status that is not part of the required status checks. Merging is still allowed.
        /// </summary>
        [Parameter(Value = "unstable")]
        Unstable,

        /// <summary>
        /// GitHub Enterprise only, if a repo has custom pre-receive hooks. Merging is allowed.
        /// </summary>
        [Parameter(Value = "has_hooks")]
        HasHooks,

        /// <summary>
        /// No conflicts, everything good. Merging is allowed.
        /// </summary>
        [Parameter(Value = "clean")]
        Clean
    }
}
