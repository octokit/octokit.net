using System.Collections.Generic;
using System.Diagnostics;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SearchCommitsResult : SearchResult<SearchCommit>
    {
        public SearchCommitsResult() { }
        public SearchCommitsResult(int totalCount, bool incompleteResults, IReadOnlyList<SearchCommit> items)
         : base(totalCount, incompleteResults, items)
        {
        }
    }
}
