using Octokit.Internal;
using System;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Package
    {
        public Package() { }

        public Package(long id, string name, PackageType packageType, Author owner, int versionCount, PackageVisibility visibility, string url, DateTime createdAt, DateTime updatedAt, string htmlUrl)
        {
            Id = id;
            Name = name;
            PackageType = packageType;
            Owner = owner;
            VersionCount = versionCount;
            Visibility = visibility;
            Url = url;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            HtmlUrl = htmlUrl;
        }

        /// <summary>
        /// The Id of the package
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The Name of the package
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The Type of the package
        /// </summary>
        public StringEnum<PackageType> PackageType { get; private set; }

        /// <summary>
        /// The Owner of the package
        /// </summary>
        public Author Owner { get; private set; }

        /// <summary>
        /// The Version Count of the package
        /// </summary>
        public int VersionCount { get; private set; }

        /// <summary>
        /// The Visibility of the package
        /// </summary>
        public StringEnum<PackageVisibility> Visibility { get; private set; }

        /// <summary>
        /// The Url of the package
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// The Date the package was first created
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// The Date the package was last updated
        /// </summary>
        public DateTime UpdatedAt { get; private set; }

        /// <summary>
        /// The Url of the package
        /// </summary>
        public string HtmlUrl { get; private set; }

        internal string DebuggerDisplay => new SimpleJsonSerializer().Serialize(this);
    }
}