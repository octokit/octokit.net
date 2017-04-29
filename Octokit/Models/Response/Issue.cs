using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Issue
    {
        public Issue() { }

        public Issue(string url, string htmlUrl, string commentsUrl, string eventsUrl, int number, ItemState state, string title, string body, User closedBy, User user, IReadOnlyList<Label> labels, User assignee, IReadOnlyList<User> assignees, Milestone milestone, int comments, PullRequest pullRequest, DateTimeOffset? closedAt, DateTimeOffset createdAt, DateTimeOffset? updatedAt, int id, bool locked, Repository repository)
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
            ClosedBy = closedBy;
            User = user;
            Labels = labels;
            Assignee = assignee;
            Assignees = assignees;
            Milestone = milestone;
            Comments = comments;
            PullRequest = pullRequest;
            ClosedAt = closedAt;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Locked = locked;
            Repository = repository;
        }

        /// <summary>
        /// The internal Id for this issue (not the issue number)
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The URL for this issue.
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// The URL for the HTML view of this issue.
        /// </summary>
        public string HtmlUrl { get; protected set; }

        /// <summary>
        /// The Comments URL of this issue.
        /// </summary>
        public string CommentsUrl { get; protected set; }

        /// <summary>
        /// The Events URL of this issue.
        /// </summary>
        public string EventsUrl { get; protected set; }

        /// <summary>
        /// The issue number.
        /// </summary>
        public int Number { get; protected set; }

        /// <summary>
        /// Whether the issue is open or closed.
        /// </summary>
        public StringEnum<ItemState> State { get; protected set; }

        /// <summary>
        /// Title of the issue
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// Details about the issue.
        /// </summary>
        public string Body { get; protected set; }

        /// <summary>
        /// Details about the user who has closed this issue.
        /// </summary>
        public User ClosedBy { get; protected set; }

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
        ///The multiple users this issue is assigned to.
        /// </summary>
        public IReadOnlyList<User> Assignees { get; protected set; }

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
        /// If the issue is locked or not.
        /// </summary>
        public bool Locked { get; protected set; }

        /// <summary>
        /// The repository the issue comes from.
        /// </summary>
        public Repository Repository { get; protected set; }

        public ReactionSummary Reactions { get; protected set; }

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

            var assignees = Assignees == null
                ? null
                : Assignees.Select(x => x.Login);

            var labels = Labels == null
                ? null
                : Labels.Select(x => x.Name);

            ItemState state;
            var issueUpdate = new IssueUpdate
            {
                Body = Body,
                Milestone = milestoneId,
                State = (State.TryParse(out state) ? (ItemState?)state : null),
                Title = Title
            };

            if (assignees != null)
            {
                foreach (var assignee in assignees)
                {
                    issueUpdate.AddAssignee(assignee);
                }
            }

            if (labels != null)
            {
                foreach (var label in labels)
                {
                    issueUpdate.AddLabel(label);
                }
            }

            return issueUpdate;
        }
    }
}