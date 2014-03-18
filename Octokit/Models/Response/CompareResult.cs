using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CompareResult
    {
        public string Url { get; set; }
        public string HtmlUrl { get; set; }
        public string PermalinkUrl { get; set; }
        public string DiffUrl { get; set; }
        public string PatchUrl { get; set; }
        public GitHubCommit BaseCommit { get; set; }
        public GitHubCommit MergedBaseCommit { get; set; }
        public string Status { get; set; }
        public int AheadBy { get; set; }
        public int BehindBy { get; set; }
        public int TotalCommits { get; set; }
        public IReadOnlyCollection<GitHubCommit> Commits { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Status: {0} Ahead By: {1}, Behind By: {2}", Status, AheadBy, BehindBy);
            }
        }
    }
}
