using System.Collections.Generic;
using System.Diagnostics;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SearchCommitsResult : SearchResult<SearchCode>
    {
        public SearchCommitsResult() { }
        public SearchCommitsResult(int totalCount, bool incompleteResults, IReadOnlyList<SearchCode> items)
         : base(totalCount, incompleteResults, items)
        {
        }
    }
}
