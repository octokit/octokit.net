using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    /// <summary>
    /// Returns the total commit counts for the owner and total commit counts in total in the last 52 weeks
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Participation
    {
        public Participation() { }

        public Participation(IEnumerable<int> all, IEnumerable<int> owner)
        {
            Ensure.ArgumentNotNull(all, "all");
            Ensure.ArgumentNotNull(owner, "owner");

            All = new ReadOnlyCollection<int>(all.ToList());
            Owner = new ReadOnlyCollection<int>(owner.ToList());
        }

        /// <summary>
        /// Returns the commit counts made each week, for the last 52 weeks
        /// </summary>
        public IReadOnlyList<int> All { get; protected set; }

        /// <summary>
        /// Returns the commit counts made by the owner each week, for the last 52 weeks
        /// </summary>
        public IReadOnlyList<int> Owner { get; protected set; }

        /// <summary>
        /// The total number of commits made by the owner in the last 52 weeks.
        /// </summary>
        /// <returns></returns>
        public int TotalCommitsByOwner()
        {
            return Owner.Sum();
        }

        /// <summary>
        /// The total number of commits made by contributors in the last 52 weeks.
        /// </summary>
        /// <returns></returns>
        public int TotalCommitsByContributors()
        {
            return All.Sum() - Owner.Sum();
        }

        /// <summary>
        /// The total number of commits made in the last 52 weeks.
        /// </summary>
        /// <returns></returns>
        public int TotalCommits()
        {
            return All.Sum();
        }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "Owner: {0} Total: {1}", TotalCommitsByOwner(), TotalCommits());
            }
        }
    }
}
