using System.Collections.Generic;
using System.Diagnostics;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SearchLabelsResult : SearchResult<Label>
    {
        public SearchLabelsResult() { }

        public SearchLabelsResult(int totalCount, bool incompleteResults, IReadOnlyList<Label> items)
            : base(totalCount, incompleteResults, items)
        {
        }
    }
}