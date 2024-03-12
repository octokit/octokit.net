using Octokit.Internal;

namespace Octokit
{
    public enum Status
    {
        [Parameter(Value = "enabled")]
        Enabled,
        [Parameter(Value = "disabled")]
        Disabled
    }
}
