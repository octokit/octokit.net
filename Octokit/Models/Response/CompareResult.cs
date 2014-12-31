using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CompareResult
    {
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
        public IReadOnlyCollection<GitHubCommit> Commits { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Status: {0} Ahead By: {1}, Behind By: {2}", Status, AheadBy, BehindBy);
            }
        }
    }
}
