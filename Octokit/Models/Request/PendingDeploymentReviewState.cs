using Octokit.Internal;

namespace Octokit
{
    public enum PendingDeploymentReviewState
    {
        [Parameter(Value = "approved")]
        Approved,
        [Parameter(Value = "rejected")]
        Rejected,
    }
}
