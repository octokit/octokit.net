using System;

namespace Octokit
{
    public class IssueEvent
    {
        /// <summary>
        /// The id of the issue/pull request event.
        /// </summary>
        public int Id { get; set; }

        public Uri Url { get; set; }

        /// <summary>
        /// Always the User that generated the event
        /// </summary>
        public Actor Actor { get; set; }

        /// <summary>
        /// Identifies the actual type of Event that occurred
        /// </summary>
        public EventInfoState InfoState { get; set; }

        /// <summary>
        /// The String SHA of a commit that referenced this Issue
        /// </summary>
        public string CommitId { get; set; }

        /// <summary>
        /// Date the event occurred for the issue/pull request.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        public Issue Issue { get; set; }
    }
}
