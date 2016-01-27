using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// An enhanced git commit containing links to additional resources
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GitHubCommitStats
    {
        public GitHubCommitStats() { }

        public GitHubCommitStats(int additions, int deletions, int total)
        {
            Additions = additions;
            Deletions = deletions;
            Total = total;
        }

        /// <summary>
        /// The number of additions made within the commit
        /// </summary>
        public int Additions { get; protected set; }

        /// <summary>
        /// The number of deletions made within the commit
        /// </summary>
        public int Deletions { get; protected set; }

        /// <summary>
        /// The total number of modifications within the commit
        /// </summary>
        public int Total { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Stats: +{0} -{1} ={2}", Additions, Deletions, Total); }
        }
    }
}
