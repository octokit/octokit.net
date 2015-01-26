using System.Collections.Generic;
using System.Diagnostics;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SearchIssuesResult : SearchResult<Issue>
    {
        public SearchIssuesResult() { }

        public SearchIssuesResult(int totalCount, bool incompleteResults, IReadOnlyList<Issue> items)
            : base(totalCount, incompleteResults, items)
        {
        }
    }
}