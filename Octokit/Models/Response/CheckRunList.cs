using System.Collections.Generic;

namespace Octokit
{
    public class CheckRunList
    {
        public CheckRunList()
        {
        }

        public CheckRunList(int totalCount, IReadOnlyList<CheckRun> checkRuns)
        {
            TotalCount = totalCount;
            CheckRuns = checkRuns;
        }

        public int TotalCount { get; protected set; }

        public IReadOnlyList<CheckRun> CheckRuns { get; protected set; }
    }
}
