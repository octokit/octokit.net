using System.Collections.Generic;

namespace Octokit
{
    public class CheckSuitesResponse
    {
        public CheckSuitesResponse()
        {
        }

        public CheckSuitesResponse(int totalCount, IReadOnlyList<CheckSuite> checkSuites)
        {
            TotalCount = totalCount;
            CheckSuites = checkSuites;
        }

        public int TotalCount { get; protected set; }

        public IReadOnlyList<CheckSuite> CheckSuites { get; protected set; }
    }
}
