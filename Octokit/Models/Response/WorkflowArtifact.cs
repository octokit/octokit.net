using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkflowArtifact
    {
        public WorkflowArtifact()
        {
        }

        public WorkflowArtifact(long id, string name, long sizeInBytes, string archiveDownloadUrl, bool expired, DateTimeOffset createdAt, DateTimeOffset expiresAt)
        {
            Id = id;
            Name = name;
            SizeInBytes = sizeInBytes;
            ArchiveDownloadUrl = archiveDownloadUrl;
            Expired = expired;
            CreatedAt = createdAt;
            ExpiresAt = expiresAt;
        }

        public long Id { get; protected set; }
        public string Name { get; protected set; }

        public long SizeInBytes { get; protected set; }
        public string ArchiveDownloadUrl { get; protected set; }
        public bool Expired { get; protected set; }
        public DateTimeOffset CreatedAt { get; protected set; }
        public DateTimeOffset ExpiresAt { get; protected set; }
        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "Id: {0}, Name: {1}, Size: {2}", Id, Name, SizeInBytes);

    }
}