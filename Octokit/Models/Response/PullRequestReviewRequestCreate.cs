using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestReviewRequestCreate
    {
        public PullRequestReviewRequestCreate() { }

        public PullRequestReviewRequestCreate(int number)
        {
            Number = number;
        }

        public PullRequestReviewRequestCreate(long id, Uri url, Uri htmlUrl, Uri diffUrl, Uri patchUrl, Uri issueUrl, Uri statusesUrl, int number, ItemState state, string title, string body, DateTimeOffset createdAt, DateTimeOffset updatedAt, DateTimeOffset? closedAt, DateTimeOffset? mergedAt, GitReference head, GitReference @base, User user, User assignee, IReadOnlyList<User> assignees, Milestone milestone, bool locked, IReadOnlyList<User> requestedReviewers)
        {
            Id = id;
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
            Milestone = milestone;
            Locked = locked;
            RequestedReviewers = requestedReviewers;
        }

        /// <summary>
        /// The internal Id for this pull request (not the pull request number)
        /// </summary>
        public long Id { get; protected set; }

        /// <summary>
        /// The URL for this pull request.
        /// </summary>
        public Uri Url { get; protected set; }

        /// <summary>
        /// The URL for the pull request page.
        /// </summary>
        public Uri HtmlUrl { get; protected set; }

        /// <summary>
        /// The URL for the pull request's diff (.diff) file.
        /// </summary>
        public Uri DiffUrl { get; protected set; }

        /// <summary>
        /// The URL for the pull request's patch (.patch) file.
        /// </summary>
        public Uri PatchUrl { get; protected set; }

        /// <summary>
        /// The URL for the specific pull request issue.
        /// </summary>
        public Uri IssueUrl { get; protected set; }

        /// <summary>
        /// The URL for the pull request statuses.
        /// </summary>
        public Uri StatusesUrl { get; protected set; }

        /// <summary>
        /// The pull request number.
        /// </summary>
        public int Number { get; protected set; }

        /// <summary>
        /// Whether the pull request is open or closed. The default is <see cref="ItemState.Open"/>.
        /// </summary>
        public ItemState State { get; protected set; }

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
        /// If the issue is locked or not
        /// </summary>
        public bool Locked { get; protected set; }

        public IReadOnlyList<User> RequestedReviewers { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Number: {0} State: {1} Reviewers: {2}", Number, State, RequestedReviewers); }
        }
    }
}