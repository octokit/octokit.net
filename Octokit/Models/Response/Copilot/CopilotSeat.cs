using System;
using Octokit;

namespace Octokit
{
    public class CopilotSeat
    {
        public CopilotSeat()
        {
        }

        public CopilotSeat(DateTimeOffset? createdAt, DateTimeOffset? updatedAt, string pendingCancellationDate, DateTimeOffset? lastActivityAt, string lastActivityEditor, User assignee)
        {
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            PendingCancellationDate = pendingCancellationDate;
            LastActivityAt = lastActivityAt;
            LastActivityEditor = lastActivityEditor;
            Assignee = assignee;
        }

        public DateTimeOffset? CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public string PendingCancellationDate { get; set; }

        public DateTimeOffset? LastActivityAt { get; set; }

        public string LastActivityEditor { get; set; }

        public User Assignee { get; set; }
    }
}