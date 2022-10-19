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

        /// <summary>
        /// The total number of check suites that match the request filter
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// The retrieved check suites
        /// </summary>
        public IReadOnlyList<CheckSuite> CheckSuites { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "TotalCount: {0}, CheckSuites: {1}", TotalCount, CheckSuites.Count);
    }
}
