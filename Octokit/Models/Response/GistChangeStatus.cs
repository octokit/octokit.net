using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used by <see cref="GistHistory"/> to indicate the level of change.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GistChangeStatus
    {
        public GistChangeStatus() { }

        public GistChangeStatus(int deletions, int additions, int total)
        {
            Deletions = deletions;
            Additions = additions;
            Total = total;
        }

        /// <summary>
        /// The number of deletions that occurred as part of this change.
        /// </summary>
        public int Deletions { get; protected set; }

        /// <summary>
        /// The number of additions that occurred as part of this change.
        /// </summary>
        public int Additions { get; protected set; }

        /// <summary>
        /// The total number of changes.
        /// </summary>
        public int Total { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Deletions: {0}, Additions: {1}, Total: {2}", Deletions, Additions, Total); }
        }
    }
}