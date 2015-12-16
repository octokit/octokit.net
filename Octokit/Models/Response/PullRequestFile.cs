using System;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestFile
    {
        public PullRequestFile() { }

        public PullRequestFile(string sha, string fileName, string status, int additions, int deletions, int changes, Uri blobUrl, Uri rawUrl, Uri contentsUrl, string patch)
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
        }

        public string Sha { get; protected set; }
        [Parameter(Key = "filename")]
        public string FileName { get; protected set; }
        public string Status { get; protected set; }
        public int Additions { get; protected set; }
        public int Deletions { get; protected set; }
        public int Changes { get; protected set; }
        public Uri BlobUrl { get; protected set; }
        public Uri RawUrl { get; protected set; }
        public Uri ContentsUrl { get; protected set; }
        public string Patch { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Sha: {0} FileName: {1} Additions: {2} Deletions: {3} Changes: {4}", Sha, FileName, Additions, Deletions, Changes); }
        }
    }
}
