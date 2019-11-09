using Octokit.Internal;

namespace Octokit
{
    public enum DefaultRepositoryPermission
    {
        [Parameter(Value = "read")]
        Read,
        [Parameter(Value = "write")]
        Write,
        [Parameter(Value = "admin")]
        Admin,
        [Parameter(Value = "none")]
        None
    }
}
