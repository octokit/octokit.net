using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CompareResult
    {
        public CompareResult() { }

        public CompareResult(string url, string htmlUrl, string permalinkUrl, string diffUrl, string patchUrl, GitHubCommit baseCommit, GitHubCommit mergedBaseCommit, string status, int aheadBy, int behindBy, int totalCommits, IReadOnlyList<GitHubCommit> commits, IReadOnlyList<GitHubCommitFile> files)
        {
            Url = url;
            HtmlUrl = htmlUrl;
            PermalinkUrl = permalinkUrl;
            DiffUrl = diffUrl;
            PatchUrl = patchUrl;
            BaseCommit = baseCommit;
            MergedBaseCommit = mergedBaseCommit;
            Status = status;
            AheadBy = aheadBy;
            BehindBy = behindBy;
            TotalCommits = totalCommits;
            Commits = commits;
            Files = files;
        }

        public string Url { get; protected set; }
        public string HtmlUrl { get; protected set; }
        public string PermalinkUrl { get; protected set; }
        public string DiffUrl { get; protected set; }
        public string PatchUrl { get; protected set; }
        public GitHubCommit BaseCommit { get; protected set; }
        public GitHubCommit MergedBaseCommit { get; protected set; }
        public string Status { get; protected set; }
        public int AheadBy { get; protected set; }
        public int BehindBy { get; protected set; }
        public int TotalCommits { get; protected set; }
        public IReadOnlyList<GitHubCommit> Commits { get; protected set; }
        public IReadOnlyList<GitHubCommitFile> Files { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Status: {0} Ahead By: {1}, Behind By: {2}", Status, AheadBy, BehindBy);
            }
        }
    }
}
