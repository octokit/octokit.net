using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Represents an environment for a deployment approval.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class EnvironmentApproval
    {
        public EnvironmentApproval() { }

        public EnvironmentApproval(long id, string nodeId, string name, string url, string htmlUrl, DateTimeOffset createdAt, DateTimeOffset updatedAt)
        {
            Id = id;
            NodeId = nodeId;
            Name = name;
            Url = url;
            HtmlUrl = htmlUrl;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        /// <summary>
        /// The Id of the environment.
        /// </summary>
        public long Id { get; protected set; }

        /// <summary>
        /// GraphQL Node Id.
        /// </summary>
        public string NodeId { get; protected set; }

        /// <summary>
        /// The name of the environment.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The URL for this environment.
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// The URL for the HTML view of this environment.
        /// </summary>
        public string HtmlUrl { get; protected set; }

        /// <summary>
        /// The time that the environment was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; protected set; }

        /// <summary>
        /// The time that the environment was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Id: {0}, Name: {1}", Id, Name);
            }
        }
    }
}
