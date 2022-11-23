using Octokit.Internal;

namespace Octokit
{
    public enum EnvironmentApprovalState
    {
        [Parameter(Value = "approved")]
        Approved,
        [Parameter(Value = "rejected")]
        Rejected,
    }
}
