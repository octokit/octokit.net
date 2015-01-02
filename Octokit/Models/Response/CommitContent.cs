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
        /// The type of this content. It might be a File, Directory, Submodule, or Symlink
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Matches the property name used by the API")]
        public ContentType Type { get; set; }

        /// <summary>
        /// URL to the raw content
        /// </summary>
        public Uri DownloadUrl { get; set; }

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

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Name: {0} Path: {1} Type:{2}", Name, Path, Type);
            }
        }
    }
}
