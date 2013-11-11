using Octokit.Internal;

namespace Octokit
{
    public class StarredRequest : RequestParameters
    {
        public StarredRequest()
        {
            SortProperty = StarredSort.Created;
            SortDirection = SortDirection.Ascending;
        }

        [Parameter(Key = "sort")]
        public StarredSort SortProperty { get; set; }

        [Parameter(Key = "direction")]
        public SortDirection SortDirection { get; set; }
    }

    public enum StarredSort
    {
        Created,
        Updated
    }
}
