using Octokit.Internal;

namespace Octokit
{
    public enum CustomPropertyValuesEditableBy
    {
        [Parameter(Value = "org_actors")]
        OrgActors,
        [Parameter(Value = "org_and_repo_actors")]
        OrgAndRepoActors,
    }
}
