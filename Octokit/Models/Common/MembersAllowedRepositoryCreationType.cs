using Octokit.Internal;

namespace Octokit
{
    public enum MembersAllowedRepositoryCreationType
    {
        [Parameter(Value = "all")]
        All,
        [Parameter(Value = "private")]
        Private,
        [Parameter(Value = "none")]
        None
    }
}