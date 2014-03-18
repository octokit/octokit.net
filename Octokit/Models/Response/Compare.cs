using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Compare
    {
        public string Url { get; set; }
        public string PermalinkUrl { get; set; }
        public string DiffUrl { get; set; }
        public string PatchUrl { get; set; }
        public CommitExtendedInfo BaseCommit { get; set; }
        public CommitExtendedInfo MergedBaseCommit { get; set; }
        public string Status { get; set; }
        public int AheadBy { get; set; }
        public int BehindBy { get; set; }
        public string TotalCommits { get; set; }
        public IReadOnlyList<CommitExtendedInfo> Commits { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Url: {0} Status: {1}", Url, Status);
            }
        }
    }
}
