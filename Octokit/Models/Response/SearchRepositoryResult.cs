using System.Collections.Generic;

namespace Octokit
{
    public class SearchRepositoryResult
    {
        public int TotalCount { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public IList<Repository> Items { get; set; }
    }
}
