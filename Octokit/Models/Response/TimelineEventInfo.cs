using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class TimelineEventInfo
    {
        public TimelineEventInfo() { }

        public TimelineEventInfo(long id, string nodeId, string url, User actor, string commitId, EventInfoState @event, DateTimeOffset createdAt, Label label, User assignee, Milestone milestone, SourceInfo source, RenameInfo rename, IssueEventProjectCard projectCard)
        {
            Id = id;
            NodeId = nodeId;
            Url = url;
            Actor = actor;
            CommitId = commitId;
            Event = @event;
            CreatedAt = createdAt;
            Label = label;
            Assignee = assignee;
            Milestone = milestone;
            Source = source;
            Rename = rename;
            ProjectCard = projectCard;
        }

        public long Id { get; private set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; private set; }

        public string Url { get; private set; }
        public User Actor { get; private set; }
        public string CommitId { get; private set; }
        public StringEnum<EventInfoState> Event { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public Label Label { get; private set; }
        public User Assignee { get; private set; }
        public Milestone Milestone { get; private set; }

        /// <summary>
        /// The source of reference from another issue
        /// Only provided for cross-referenced events
        /// </summary>
        public SourceInfo Source { get; private set; }

        /// <summary>
        /// An object containing rename details
        /// Only provided for renamed events
        /// </summary>
        public RenameInfo Rename { get; private set; }

        /// <summary>
        /// The name of the column that the card was listed in prior to column_name.
        /// Only returned for moved_columns_in_project events
        /// </summary>
        public IssueEventProjectCard ProjectCard { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Id: {0} CreatedAt: {1} Event: {2}", Id, CreatedAt, Event); }
        }
    }
}
