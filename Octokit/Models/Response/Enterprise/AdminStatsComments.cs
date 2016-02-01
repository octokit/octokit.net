using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AdminStatsComments
    {
        public AdminStatsComments() { }

        public AdminStatsComments(int totalCommitComments, int totalGistComments, int totalIssueComments, int totalPullRequestComments)
        {
            TotalCommitComments = totalCommitComments;
            TotalGistComments = totalGistComments;
            TotalIssueComments = totalIssueComments;
            TotalPullRequestComments = totalPullRequestComments;
        }

        public int TotalCommitComments
        {
            get;
            private set;
        }

        public int TotalGistComments
        {
            get;
            private set;
        }

        public int TotalIssueComments
        {
            get;
            private set;
        }

        public int TotalPullRequestComments
        {
            get;
            private set;
        }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "TotalCommitComments: {0} TotalGistComments: {1} TotalIssueComments: {2} TotalPullRequestComments: {3}", TotalCommitComments, TotalGistComments, TotalIssueComments, TotalPullRequestComments);
            }
        }
    }
}