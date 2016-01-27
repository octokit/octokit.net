using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// The affected files in a <see cref="GitHubCommit"/>.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GitHubCommitFile
    {
        public GitHubCommitFile() { }

        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public GitHubCommitFile(string filename, int additions, int deletions, int changes, string status, string blobUrl, string contentsUrl, string rawUrl, string sha, string patch, string previousFileName)
        {
            Filename = filename;
            Additions = additions;
            Deletions = deletions;
            Changes = changes;
            Status = status;
            BlobUrl = blobUrl;
            ContentsUrl = contentsUrl;
            RawUrl = rawUrl;
            Sha = sha;
            Patch = patch;
            PreviousFileName = previousFileName;
        }

        /// <summary>
        /// The name of the file
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public string Filename { get; protected set; }

        /// <summary>
        /// Number of additions performed on the file.
        /// </summary>
        public int Additions { get; protected set; }

        /// <summary>
        /// Number of deletions performed on the file.
        /// </summary>
        public int Deletions { get; protected set; }

        /// <summary>
        /// Number of changes performed on the file.
        /// </summary>
        public int Changes { get; protected set; }

        /// <summary>
        /// File status, like modified, added, deleted.
        /// </summary>
        public string Status { get; protected set; }

        /// <summary>
        /// The url to the file blob.
        /// </summary>
        public string BlobUrl { get; protected set; }

        /// <summary>
        /// The url to file contents API.
        /// </summary>
        public string ContentsUrl { get; protected set; }

        /// <summary>
        /// The raw url to download the file.
        /// </summary>
        public string RawUrl { get; protected set; }

        /// <summary>
        /// The SHA of the file.
        /// </summary>
        public string Sha { get; protected set; }

        /// <summary>
        /// The patch associated with the commit
        /// </summary>
        public string Patch { get; protected set; }

        /// <summary>
        /// The previous filename for a renamed file.
        /// </summary>
        [Parameter(Key = "previous_filename")]
        public string PreviousFileName { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Filename: {0} ({1})", Filename, Status); }
        }
    }
}
