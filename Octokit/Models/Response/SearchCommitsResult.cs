using System.Collections.Generic;
using System.Diagnostics;
using Octokit.Internal;


namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SearchCommitsResult : SearchResult<SearchCommits>
    {
        public SearchCommitsResult() { }

        public SearchCommitsResult(int totalCount, bool incompleteResults, IReadOnlyList<SearchCommits> items)
            : base(totalCount, incompleteResults, items)
        {
        }
    }
}
