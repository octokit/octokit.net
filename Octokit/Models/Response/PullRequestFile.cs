using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit.Internal;

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

        public string Sha { get; protected set; }
        [Parameter(Key = "filename")]
        public string FileName { get; protected set; }
        public string Status { get; protected set; }
        public int Additions { get; protected set; }
        public int Deletions { get; protected set; }
        public int Changes { get; protected set; }
        public Uri BlobUri { get; protected set; }
        public Uri RawUri { get; protected set; }
        public Uri ContentsUri { get; protected set; }
        public string Patch { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return String.Format(CultureInfo.InvariantCulture, "Sha: {0} FileName: {1} Additions: {2} Deletions: {3} Changes: {4}", Sha, FileName, Additions, Deletions, Changes); }
        }
    }
}
