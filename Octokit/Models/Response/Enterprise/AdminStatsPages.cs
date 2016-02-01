using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AdminStatsPages
    {
        public AdminStatsPages() { }

        public AdminStatsPages(int totalPages)
        {
            TotalPages = totalPages;
        }

        public int TotalPages
        {
            get;
            private set;
        }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "TotalPages: {0}", TotalPages);
            }
        }
    }
}