using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Artifact
    {
        public Artifact()
        {
        }

        public Artifact(long id, string nodeId, string name, int sizeInBytes, string url, string archiveDownloadUrl, bool expired, DateTime createdAt, DateTime expiresAt, DateTime updatedAt, ArtifactWorkflowRun workflowRun)
        {
            Id = id;
            NodeId = nodeId;
            Name = name;
            SizeInBytes = sizeInBytes;
            Url = url;
            ArchiveDownloadUrl = archiveDownloadUrl;
            Expired = expired;
            CreatedAt = createdAt;
            ExpiresAt = expiresAt;
            UpdatedAt = updatedAt;
            WorkflowRun = workflowRun;
        }

        /// <summary>
        /// The artifact Id
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The artifact node Id
        /// </summary>
        public string NodeId { get; private set; }

        /// <summary>
        /// The name of the artifact
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The size of the artifact in bytes
        /// </summary>
        public int SizeInBytes { get; private set; }

        /// <summary>
        /// The url for retrieving the artifact information
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// The url for downloading the artifact contents
        /// </summary>
        public string ArchiveDownloadUrl { get; private set; }

        /// <summary>
        /// True if the artifact has expired
        /// </summary>
        public bool Expired { get; private set; }

        /// <summary>
        /// The date and time when the artifact was created
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// The date and time when the artifact expires
        /// </summary>
        public DateTime ExpiresAt { get; private set; }

        /// <summary>
        /// The date and time when the artifact was last updated
        /// </summary>
        public DateTime UpdatedAt { get; private set; }

        /// <summary>
        /// The workflow from where the artifact was created
        /// </summary>
        public ArtifactWorkflowRun WorkflowRun { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Id: {0}", Id);
    }
}