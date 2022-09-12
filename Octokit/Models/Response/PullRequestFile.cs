using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestFile
    {
        public PullRequestFile() { }

        public PullRequestFile(string sha, string fileName, string status, int additions, int deletions, int changes, string blobUrl, string rawUrl, string contentsUrl, string patch, string previousFileName)
        {
            Sha = sha;
            FileName = fileName;
            Status = status;
            Additions = additions;
            Deletions = deletions;
            Changes = changes;
            BlobUrl = blobUrl;
            RawUrl = rawUrl;
            ContentsUrl = contentsUrl;
            Patch = patch;
            PreviousFileName = previousFileName;
        }

        public string Sha { get; private set; }
        [Parameter(Key = "filename")]
        public string FileName { get; private set; }
        public string Status { get; private set; }
        public int Additions { get; private set; }
        public int Deletions { get; private set; }
        public int Changes { get; private set; }
        public string BlobUrl { get; private set; }
        public string RawUrl { get; private set; }
        public string ContentsUrl { get; private set; }
        public string Patch { get; private set; }
        [Parameter(Key = "previous_filename")]
        public string PreviousFileName { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Sha: {0} FileName: {1} Additions: {2} Deletions: {3} Changes: {4}", Sha, FileName, Additions, Deletions, Changes); }
        }
    }
}
