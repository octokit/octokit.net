using System;
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
        public RepositoryTag(string name, GitReference commit, string zipballUrl, string tarballUrl)
        {
            Name = name;
            Commit = commit;
            ZipballUrl = zipballUrl;
            TarballUrl = tarballUrl;
        }

        public string Name { get; protected set; }

        public GitReference Commit { get; protected set; }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Zipball")]
        public string ZipballUrl { get; protected set; }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Tarball")]
        public string TarballUrl { get; protected set; }

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
