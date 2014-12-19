using System;
using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    public class DirectoryContent
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public string Sha { get; set; }

        public int? Size { get; set; }

        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Matches the property name used by the API")]
        public ContentType Type { get; set; }

        public Uri Url { get; set; }

        public Uri GitUrl { get; set; }

        public Uri HtmlUrl { get; set; }
    }

    public class FileContent : DirectoryContent
    {
        public string Encoding { get; set; }

        public string Content { get; set; }
    }

    public class SymlinkContent : DirectoryContent
    {
        public string Target { get; set; }
    }

    public class SubmoduleContent : DirectoryContent
    {
        public Uri SubmoduleGitUrl { get; set; }
    }

    public enum ContentType
    {
        File,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Dir", Justification = "Matches the value returned by the API")]
        Dir,
        Symlink,
        Submodule
    }

    public class CreatedContent
    {
        public DirectoryContent Content { get; set; }
        public Commit Commit { get; set; }
    }
}