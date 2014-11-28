using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// The affected files in a <see cref="GitHubCommit"/>.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GitHubCommitFile
    {
        /// <summary>
        /// The name of the file
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public string Filename { get; set; }

        /// <summary>
        /// Number of additions performed on the file.
        /// </summary>
        public int Additions { get; set; }

        /// <summary>
        /// Number of deletions performed on the file.
        /// </summary>
        public int Deletions { get; set; }

        /// <summary>
        /// Number of changes performed on the file.
        /// </summary>
        public int Changes { get; set; }

        /// <summary>
        /// File status, like modified, added, deleted.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The url to the file blob.
        /// </summary>
        public string BlobUrl { get; set; }

        /// <summary>
        /// The url to file contents API.
        /// </summary>
        public string ContentsUrl { get; set; }

        /// <summary>
        /// The raw url to download the file.
        /// </summary>
        public string RawUrl { get; set; }

        /// <summary>
        /// The SHA of the file.
        /// </summary>
        public string Sha { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Filename: {0} ({1})", Filename, Status);
            }
        }
    }
}