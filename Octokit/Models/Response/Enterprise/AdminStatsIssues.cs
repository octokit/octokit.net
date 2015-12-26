using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AdminStatsIssues
    {
        public AdminStatsIssues() { }

        public AdminStatsIssues(int totalIssues, int openIssues, int closedIssues)
        {
            TotalIssues = totalIssues;
            OpenIssues = openIssues;
            ClosedIssues = closedIssues;
        }

        public int TotalIssues
        {
            get;
            private set;
        }

        public int OpenIssues
        {
            get;
            private set;
        }

        public int ClosedIssues
        {
            get;
            private set;
        }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "TotalIssues: {0} OpenIssues: {1} ClosedIssues: {2}", TotalIssues, OpenIssues, ClosedIssues);
            }
        }
    }
}