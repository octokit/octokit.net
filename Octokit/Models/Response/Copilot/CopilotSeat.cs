using System;

namespace Octokit
{
    /// <summary>
    /// Details about a Copilot seat allocated to an organization member.
    /// </summary>
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

        /// <summary>
        /// Timestamp of when the assignee was last granted access to GitHub Copilot, in ISO 8601 format
        /// </summary>
        public DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// Timestamp of when the assignee's GitHub Copilot access was last updated, in ISO 8601 format.
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; set; }

        /// <summary>
        /// The pending cancellation date for the seat, in `YYYY-MM-DD` format. This will be null unless
        /// the assignee's Copilot access has been canceled during the current billing cycle.
        /// If the seat has been cancelled, this corresponds to the start of the organization's next billing cycle.
        /// </summary>
        public string PendingCancellationDate { get; set; }

        /// <summary>
        /// Timestamp of user's last GitHub Copilot activity, in ISO 8601 format.
        /// </summary>
        public DateTimeOffset? LastActivityAt { get; set; }

        /// <summary>
        /// Last editor that was used by the user for a GitHub Copilot completion.
        /// </summary>
        public string LastActivityEditor { get; set; }

        /// <summary>
        /// The assignee that has been granted access to GitHub Copilot
        /// </summary>
        public User Assignee { get; set; }
        
        /// <summary>
        /// The team that granted access to GitHub Copilot to the assignee. This will be null if the
        /// user was assigned a seat individually.
        /// </summary>
        public Team AssigningTeam { get; set; }
    }
}