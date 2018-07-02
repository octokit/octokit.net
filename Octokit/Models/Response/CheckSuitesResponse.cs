using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
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

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "TotalCount: {0}, CheckSuites: {1}", TotalCount, CheckSuites.Count);
    }
}
