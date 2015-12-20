using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Information about a file in a repository. It does not include the contents of the file.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryContentInfo
    {
        public RepositoryContentInfo() { }

        public RepositoryContentInfo(string name, string path, string sha, int size, ContentType type, Uri downloadUrl, Uri url, Uri gitUrl, Uri htmlUrl)
        {
            Name = name;
            Path = path;
            Sha = sha;
            Size = size;
            Type = type;
            DownloadUrl = downloadUrl;
            Url = url;
            GitUrl = gitUrl;
            HtmlUrl = htmlUrl;
        }

        /// <summary>
        /// Name of the content.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Path to this content.
        /// </summary>
        public string Path { get; protected set; }

        /// <summary>
        /// SHA of the last commit that modified this content.
        /// </summary>
        public string Sha { get; protected set; }

        /// <summary>
        /// Size of the content.
        /// </summary>
        public int Size { get; protected set; }

        /// <summary>
        /// The type of this content. It might be a File, Directory, Submodule, or Symlink
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Matches the property name used by the API")]
        public ContentType Type { get; protected set; }

        /// <summary>
        /// URL to the raw content
        /// </summary>
        public Uri DownloadUrl { get; protected set; }

        /// <summary>
        /// URL to this content
        /// </summary>
        public Uri Url { get; protected set; }

        /// <summary>
        /// The GIT URL to this content.
        /// </summary>
        public Uri GitUrl { get; protected set; }

        /// <summary>
        /// The URL to view this content on GitHub.
        /// </summary>
        public Uri HtmlUrl { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Name: {0} Path: {1} Type:{2}", Name, Path, Type);
            }
        }
    }
}
