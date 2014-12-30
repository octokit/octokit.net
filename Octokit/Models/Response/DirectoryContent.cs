using System;
using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    /// <summary>
    /// Represents a piece of content in the repository. This could be a submodule, a symlink, a directory, or a file.
    /// Look at the Type property to figure out which one it is.
    /// </summary>
    public class RepositoryContent
    {
        /// <summary>
        /// Name of the content.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Path to this content.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// SHA of the last commit that modified this content.
        /// </summary>
        public string Sha { get; set; }

        /// <summary>
        /// Size of the content.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// The encoding of the content if this is a file. Typically "base64". Otherwise it's null.
        /// </summary>
        public string Encoding { get; set; }

        /// <summary>
        /// The encoded content if this is a file. Otherwise it's null.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The type of this content. It might be a File, Directory, Submodule, or Symlink
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Matches the property name used by the API")]
        public ContentType Type { get; set; }

        /// <summary>
        /// Path to the target file in the repository if this is a symlink. Otherwise it's null.
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// The location of the submodule repository if this is a submodule. Otherwise it's null.
        /// </summary>
        public Uri SubmoduleGitUrl { get; set; }

        /// <summary>
        /// URL to this content
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// The GIT URL to this content.
        /// </summary>
        public Uri GitUrl { get; set; }

        /// <summary>
        /// The URL to view this content on GitHub.
        /// </summary>
        public Uri HtmlUrl { get; set; }
    }
    
    /// <summary>
    /// The possible repository content types.
    /// </summary>
    public enum ContentType
    {
        File,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Dir", Justification = "Matches the value returned by the API")]
        Dir,
        Symlink,
        Submodule
    }

    /// <summary>
    /// The response from the Repository Contents API. The API assumes a dynamic client type so we need
    /// to model that.
    /// </summary>
    /// <remarks>https://developer.github.com/v3/repos/contents/</remarks>
    public class RepositoryContentChangeSet
    {
        /// <summary>
        /// The directory content of the response.
        /// </summary>
        public RepositoryContent Content { get; set; }

        /// <summary>
        /// The commit information for the content change.
        /// </summary>
        public Commit Commit { get; set; }
    }
}