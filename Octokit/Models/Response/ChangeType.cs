using Octokit.Internal;

namespace Octokit
{
    public enum ChangeType
    {
        [Parameter(Value = "Added")]
        Added,

        [Parameter(Value = "Removed")]
        Removed
    }
}