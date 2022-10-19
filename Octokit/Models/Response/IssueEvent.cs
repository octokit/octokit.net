using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class IssueEvent
    {
        public IssueEvent() { }

        public IssueEvent(long id, string nodeId, string url, User actor, User assignee, Label label, EventInfoState @event, string commitId, DateTimeOffset createdAt, Issue issue, string commitUrl, RenameInfo rename, IssueEventProjectCard projectCard, User reviewRequester, User requestedReviewer, User assigner, LockReason lockReason, DismissedReviewInfo dismissedReview, Milestone milestone)
        {
            Id = id;
            NodeId = nodeId;
            Url = url;
            Actor = actor;
            Assignee = assignee;
            Label = label;
            Event = @event;
            CommitId = commitId;
            CreatedAt = createdAt;
            Issue = issue;
            CommitUrl = commitUrl;
            Rename = rename;
            ProjectCard = projectCard;
            RequestedReviewer = requestedReviewer;
            ReviewRequester = reviewRequester;
            Assigner = assigner;
            LockReason = lockReason;
            DismissedReview = dismissedReview;
            Milestone = milestone;
        }

        /// <summary>
        /// The id of the issue/pull request event.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; private set; }

        /// <summary>
        /// The URL for this issue/pull request event.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Always the User that generated the event.
        /// </summary>
        public User Actor { get; private set; }

        /// <summary>
        /// The user that was assigned, if the event was 'Assigned'.
        /// </summary>
        public User Assignee { get; private set; }

        /// <summary>
        /// The label that was assigned, if the event was 'Labeled'
        /// </summary>
        public Label Label { get; private set; }

        /// <summary>
        /// Identifies the actual type of Event that occurred.
        /// </summary>
        public StringEnum<EventInfoState> Event { get; private set; }

        /// <summary>
        /// The String SHA of a commit that referenced this Issue.
        /// </summary>
        public string CommitId { get; private set; }

        /// <summary>
        /// The commit URL of a commit that referenced this issue.
        /// </summary>
        public string CommitUrl { get; private set; }

        /// <summary>
        /// Date the event occurred for the issue/pull request.
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// The issue associated to this event.
        /// </summary>
        public Issue Issue { get; private set; }

        /// <summary>
        /// An object containing rename details
        /// Only provided for renamed events
        /// </summary>
        public RenameInfo Rename { get; private set; }

        /// <summary>
        /// Information about the project card that triggered the event.
        /// The project_card attribute is not returned if someone deletes the project board, or if you do not have permission to view it.
        /// </summary>
        public IssueEventProjectCard ProjectCard { get; private set; }

        /// <summary>
        /// The user that was requested to review the pull request.
        /// Only provided for "review_requested" and "review_request_removed" events.
        /// </summary>
        public User RequestedReviewer { get; private set; }

        /// <summary>
        /// The user that requested the review for the pull request.
        /// Only provided for "review_requested" and "review_request_removed" events.
        /// </summary>
        public User ReviewRequester { get; private set; }

        /// <summary>
        /// The user who performed the (un)assignment for the issue, if the event is assigned or unassigned.
        /// </summary>
        public User Assigner { get; private set; }

        /// <summary>
        /// The reason an issue or pull request conversation was locked, if one was provided.
        /// Only provided for "locked" and "unlocked" events.
        /// </summary>
        public StringEnum<LockReason> LockReason { get; private set; }

        /// <summary>
        /// Object containing dismissed review details.
        /// Only provided for "review_dismissed" events.
        /// </summary>
        public DismissedReviewInfo DismissedReview { get; private set; }

        /// <summary>
        /// Milestone object.
        /// Only provided for "milestoned" and "demilestoned" events.
        /// </summary>
        public Milestone Milestone { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Id: {0} CreatedAt: {1}", Id, CreatedAt); }
        }
    }
}
