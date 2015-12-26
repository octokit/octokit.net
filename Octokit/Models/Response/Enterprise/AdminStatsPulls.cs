using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    public class AdminStatsPulls
    {
        public AdminStatsPulls() { }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "unmergeable")]
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

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Unmergeable")]
        public int UnmergeablePulls
        {
            get;
            private set;
        }
    }
}