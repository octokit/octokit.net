using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    public class AdminStatsPulls
    {
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "unmergeablePulls")]
        public AdminStatsPulls(int totalPulls, int mergedPulls, int mergeablePulls, int unmergeablePulls)
        {
            TotalPulls = totalPulls;
            MergedPulls = mergedPulls;
            MergeablePulls = mergeablePulls;
            UnmergeablePulls = unmergeablePulls;
        }

        public int TotalPulls
        {
            get;
            private set;
        }

        public int MergedPulls
        {
            get;
            private set;
        }

        public int MergeablePulls
        {
            get;
            private set;
        }

        public int UnmergeablePulls
        {
            get;
            private set;
        }
    }
}