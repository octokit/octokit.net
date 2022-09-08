using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryTag
    {
        public RepositoryTag() { }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "tarball")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "zipball")]
        public RepositoryTag(string name, string nodeId, GitReference commit, string zipballUrl, string tarballUrl)
        {
            Name = name;
            NodeId = nodeId;
            Commit = commit;
            ZipballUrl = zipballUrl;
            TarballUrl = tarballUrl;
        }

        public string Name { get; private set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; private set; }

        public GitReference Commit { get; private set; }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Zipball")]
        public string ZipballUrl { get; private set; }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Tarball")]
        public string TarballUrl { get; private set; }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "RepositoryTag: Name: {0} Commit: {1}", Name, Commit);
            }
        }
    }
}
