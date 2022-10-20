using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Workflow
    {
        public Workflow() { }

        public Workflow(long id, string nodeId, string name, string path, WorkflowState state, DateTimeOffset createdAt, DateTimeOffset updatedAt, string url, string htmlUrl, string badgeUrl, DateTimeOffset? deletedAt)
        {
            Id = id;
            NodeId = nodeId;
            Name = name;
            Path = path;
            State = state;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Url = url;
            HtmlUrl = htmlUrl;
            BadgeUrl = badgeUrl;
            DeletedAt = deletedAt;
        }

        /// <summary>
        /// The Id for this workflow.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// GraphQL Node Id.
        /// </summary>
        public string NodeId { get; private set; }

        /// <summary>
        /// Name of the workflow.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The path of the workflow file.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// The state of the workflow.
        /// </summary>
        public StringEnum<WorkflowState> State { get; private set; }

        /// <summary>
        /// The time that the workflow was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// The time that the workflow was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; private set; }

        /// <summary>
        /// The URL for this workflow.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// The URL for the HTML view of this workflow.
        /// </summary>
        public string HtmlUrl { get; private set; }

        /// <summary>
        /// The URL of the badge image for this workflow.
        /// </summary>
        public string BadgeUrl { get; private set; }

        /// <summary>
        /// The time that the workflow was deleted.
        /// </summary>
        public DateTimeOffset? DeletedAt { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Id: {0} Name: {1}", Id, Name);
            }
        }
    }
}
