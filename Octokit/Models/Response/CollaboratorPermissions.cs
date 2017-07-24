using Octokit.Internal;

namespace Octokit
{
    public enum CollaboratorPermissions
    {
        [Parameter(Value = "admin")]
        Admin,
        [Parameter(Value = "write")]
        Write,
        [Parameter(Value = "read")]
        Read,
        [Parameter(Value = "none")]
        None
    }
}
