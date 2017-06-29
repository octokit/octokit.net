using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class TimelineEventInfo
    {
        public TimelineEventInfo() { }

        public TimelineEventInfo(int id, string url, User actor, string commitId, EventInfoState @event, DateTimeOffset createdAt, Label label, User assignee, Milestone milestone, SourceInfo source, RenameInfo rename)
        {
            Id = id;
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
        }

        public int Id { get; protected set; }
        public string Url { get; protected set; }
        public User Actor { get; protected set; }
        public string CommitId { get; protected set; }
        public StringEnum<EventInfoState> Event { get; protected set; }
        public DateTimeOffset CreatedAt { get; protected set; }
        public Label Label { get; protected set; }
        public User Assignee { get; protected set; }
        public Milestone Milestone { get; protected set; }
        public SourceInfo Source { get; protected set; }
        public RenameInfo Rename { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Id: {0} CreatedAt: {1} Event: {2}", Id, CreatedAt, Event); }
        }
    }
}
