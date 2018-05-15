using System.Collections.Generic;

namespace Octokit
{
    public class CheckSuiteList
    {
        public CheckSuiteList()
        {
        }

        public CheckSuiteList(int totalCount, IReadOnlyList<CheckSuite> checkSuites)
        {
            TotalCount = totalCount;
            CheckSuites = checkSuites;
        }

        public int TotalCount { get; protected set; }

        public IReadOnlyList<CheckSuite> CheckSuites { get; protected set; }
    }
}
