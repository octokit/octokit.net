using System.Collections.Generic;

namespace Octokit
{
    public class WeeklyCommitCounts
    {
        public IEnumerable<int> All { get; set; }

        public IEnumerable<int> Owner { get; set; }
    }
}