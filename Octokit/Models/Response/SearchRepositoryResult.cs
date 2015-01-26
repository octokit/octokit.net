using System.Collections.Generic;
using System.Diagnostics;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SearchRepositoryResult : SearchResult<Repository>
    {
        public SearchRepositoryResult() { }

        public SearchRepositoryResult(int totalCount, bool incompleteResults, IReadOnlyList<Repository> items)
            : base(totalCount, incompleteResults, items)
        {
        }
    }
}
