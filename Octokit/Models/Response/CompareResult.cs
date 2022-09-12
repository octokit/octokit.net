using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CompareResult
    {
        public CompareResult() { }

        public CompareResult(string url, string htmlUrl, string permalinkUrl, string diffUrl, string patchUrl, GitHubCommit baseCommit, GitHubCommit mergeBaseCommit, string status, int aheadBy, int behindBy, int totalCommits, IReadOnlyList<GitHubCommit> commits, IReadOnlyList<GitHubCommitFile> files)
        {
            Url = url;
            HtmlUrl = htmlUrl;
            PermalinkUrl = permalinkUrl;
            DiffUrl = diffUrl;
            PatchUrl = patchUrl;
            BaseCommit = baseCommit;
            MergeBaseCommit = mergeBaseCommit;
            Status = status;
            AheadBy = aheadBy;
            BehindBy = behindBy;
            TotalCommits = totalCommits;
            Commits = commits;
            Files = files;
        }

        public string Url { get; private set; }
        public string HtmlUrl { get; private set; }
        public string PermalinkUrl { get; private set; }
        public string DiffUrl { get; private set; }
        public string PatchUrl { get; private set; }
        public GitHubCommit BaseCommit { get; private set; }
        public GitHubCommit MergeBaseCommit { get; private set; }
        public string Status { get; private set; }
        public int AheadBy { get; private set; }
        public int BehindBy { get; private set; }
        public int TotalCommits { get; private set; }
        public IReadOnlyList<GitHubCommit> Commits { get; private set; }
        public IReadOnlyList<GitHubCommitFile> Files { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Status: {0} Ahead By: {1}, Behind By: {2}", Status, AheadBy, BehindBy);
            }
        }
    }
}
