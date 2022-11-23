using Octokit.Internal;

namespace Octokit
{
    public enum WorkflowState
    {
        [Parameter(Value = "active")]
        Active,
        [Parameter(Value = "deleted")]
        Deleted,
        [Parameter(Value = "disabled_fork")]
        DisabledFork,
        [Parameter(Value = "disabled_inactivity")]
        DisabledInactivity,
        [Parameter(Value = "disabled_manually")]
        DisabledManually
    }
}
