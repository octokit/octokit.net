using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Issue
    {
        public Issue() { }

        public Issue(Uri url, Uri htmlUrl, Uri commentsUrl, Uri eventsUrl, int number, ItemState state, string title, string body, User user, IReadOnlyList<Label> labels, User assignee, Milestone milestone, int comments, PullRequest pullRequest, DateTimeOffset? closedAt, DateTimeOffset createdAt, DateTimeOffset? updatedAt, int id, bool locked)
        {
            Id = id;
            Url = url;
            HtmlUrl = htmlUrl;
            CommentsUrl = commentsUrl;
            EventsUrl = eventsUrl;
            Number = number;
            State = state;
            Title = title;
            Body = body;
            User = user;
            Labels = labels;
            Assignee = assignee;
            Milestone = milestone;
            Comments = comments;
            PullRequest = pullRequest;
            ClosedAt = closedAt;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Locked = locked;
        }

        /// <summary>
        /// The Id for this issue
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The URL for this issue.
        /// </summary>
        public Uri Url { get; protected set; }

        /// <summary>
        /// The URL for the HTML view of this issue.
        /// </summary>
        public Uri HtmlUrl { get; protected set; }

        /// <summary>
        /// The Comments URL of this issue.
        /// </summary>
        public Uri CommentsUrl { get; protected set; }

        /// <summary>
        /// The Events URL of this issue.
        /// </summary>
        public Uri EventsUrl { get; protected set; }

        /// <summary>
        /// The issue number.
        /// </summary>
        public int Number { get; protected set; }

        /// <summary>
        /// Whether the issue is open or closed.
        /// </summary>
        public ItemState State { get; protected set; }

        /// <summary>
        /// Title of the issue
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// Details about the issue.
        /// </summary>
        public string Body { get; protected set; }

        /// <summary>
        /// The user that created the issue.
        /// </summary>
        public User User { get; protected set; }

        /// <summary>
        /// The set of labels applied to the issue
        /// </summary>
        public IReadOnlyList<Label> Labels { get; protected set; }

        /// <summary>
        /// The user this issue is assigned to.
        /// </summary>
        public User Assignee { get; protected set; }

        /// <summary>
        /// The milestone, if any, that this issue is assigned to.
        /// </summary>
        public Milestone Milestone { get; protected set; }

        /// <summary>
        /// The number of comments on the issue.
        /// </summary>
        public int Comments { get; protected set; }

        public PullRequest PullRequest { get; protected set; }

        /// <summary>
        /// The date the issue was closed if closed.
        /// </summary>
        public DateTimeOffset? ClosedAt { get; protected set; }

        /// <summary>
        /// The date the issue was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; protected set; }

        /// <summary>
        /// The date the issue was last updated.
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; protected set; }

        /// <summary>
        /// If the issue is locked or not
        /// </summary>
        public bool Locked { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Number: {0} State: {1}", Number, State);
            }
        }

        public IssueUpdate ToUpdate()
        {
            var milestoneId = Milestone == null
                ? new int?()
                : Milestone.Number;

            var assignee = Assignee == null
                ? null
                : Assignee.Login;

            var issueUpdate = new IssueUpdate
            {
                Assignee = assignee,
                Body = Body,
                Milestone = milestoneId,
                State = State,
                Title = Title
            };

            return issueUpdate;
        }
    }
}
