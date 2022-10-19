using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PackageVersion
    {
        public PackageVersion() { }

        public PackageVersion(long id, string name, string url, string packageHtmlUrl, DateTime createdAt, DateTime updatedAt, string htmlUrl, PackageVersionMetadata metadata)
        {
            Id = id;
            Name = name;
            Url = url;
            PackageHtmlUrl = packageHtmlUrl;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            HtmlUrl = htmlUrl;
            Metadata = metadata;
        }

        public long Id { get; private set; }

        public string Name { get; private set; }

        public string Url { get; private set; }

        public string PackageHtmlUrl { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public string HtmlUrl { get; private set; }

        public PackageVersionMetadata Metadata { get; private set; }

        internal string DebuggerDisplay => new SimpleJsonSerializer().Serialize(this);
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PackageVersionMetadata
    {
        public PackageVersionMetadata() { }

        public PackageVersionMetadata(string packageType)
        {
            PackageType = packageType;
        }

        public string PackageType { get; private set; }

        internal string DebuggerDisplay => new SimpleJsonSerializer().Serialize(this);
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PackageVersionMetadataContainer
    {
        public PackageVersionMetadataContainer() { }

        public PackageVersionMetadataContainer(IReadOnlyList<string> tags)
        {
            Tags = tags;
        }

        public IReadOnlyList<string> Tags { get; private set; }

        internal string DebuggerDisplay => new SimpleJsonSerializer().Serialize(this);
    }
}