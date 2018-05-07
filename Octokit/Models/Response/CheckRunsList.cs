using System.Collections.Generic;

namespace Octokit
{
    public class CheckRunsList
    {
        public int TotalCount { get; protected set; }
        public IReadOnlyList<CheckRun> CheckRuns { get; protected set; }
    }
}
