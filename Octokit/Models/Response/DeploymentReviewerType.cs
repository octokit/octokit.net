using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// The reviewer types for a pending deployment.
    /// </summary>
    public enum DeploymentReviewerType
    {
        [Parameter(Value = "User")]
        User,
        [Parameter(Value = "Team")]
        Team,
    }
}
