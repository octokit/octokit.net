using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestFile
    {
        public PullRequestFile() { }

        public PullRequestFile(string sha, string fileName, string status, int additions, int deletions, int changes, Uri blobUri, Uri rawUri, Uri contentsUri, string patch)
        {
            Sha = sha;
            FileName = fileName;
            Status = status;
            Additions = additions;
            Deletions = deletions;
            Changes = changes;
            BlobUri = blobUri;
            RawUri = rawUri;
            ContentsUri = contentsUri;
            Patch = patch;
        }

        public string Sha { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
        public int Additions { get; set; }
        public int Deletions { get; set; }
        public int Changes { get; set; }
        public Uri BlobUri { get; set; }
        public Uri RawUri { get; set; }
        public Uri ContentsUri { get; set; }
        public string Patch { get; set; }

        internal string DebuggerDisplay
        {
            get { return String.Format(CultureInfo.InvariantCulture, "Sha: {0} Filename: {1} Additions: {2} Deletions: {3} Changes: {4}", Sha, FileName, Additions, Deletions, Changes); }
        }
    }
}
