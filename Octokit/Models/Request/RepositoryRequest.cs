using Octokit.Internal;

namespace Octokit
{
    public class RepositoryRequest : RequestParameters
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public RepositoryType Type { get; set; }

        public RepositorySort Sort { get; set; }

        public SortDirection Direction { get; set; }
    }

    public enum RepositoryType
    {
        All,
        Owner,
        Public,
        Private,
        Member
    }

    public enum RepositorySort
    {
        Created,
        Updated,
        Pushed,

        [Parameter(Value = "full_name")]
        FullName
    }
}
