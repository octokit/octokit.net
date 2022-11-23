using Octokit.Internal;

namespace Octokit
{
    public enum WorkflowRunConclusion
    {
        [Parameter(Value = "success")]
        Success,
        [Parameter(Value = "failure")]
        Failure,
        [Parameter(Value = "neutral")]
        Neutral,
        [Parameter(Value = "cancelled")]
        Cancelled,
        [Parameter(Value = "timed_out")]
        TimedOut,
        [Parameter(Value = "action_required")]
        ActionRequired,
        [Parameter(Value = "stale")]
        Stale,
    }
}
