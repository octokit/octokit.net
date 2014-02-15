using System.Collections.Generic;
using System.Linq;

namespace Octokit
{
    /// <summary>
    /// Returns the total commit counts for the owner and total commit counts in total in the last 52 weeks
    /// </summary>
    public class Participation
    {
        /// <summary>
        /// Returns the commit counts made each week, for the last 52 weeks
        /// </summary>
        public IEnumerable<int> All { get; set; }

        /// <summary>
        /// Returns the commit counts made by the owner each week, for the last 52 weeks
        /// </summary>
        public IEnumerable<int> Owner { get; set; }

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
    }
}