using Octokit.Internal;

namespace Octokit
{
    public enum CheckRunStatusFilter
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
        [Parameter(Value = "requested")]
        Requested,
        [Parameter(Value = "in_progress")]
        InProgress,
        [Parameter(Value = "completed")]
        Completed,
        [Parameter(Value = "queued")]
        Queued,
        [Parameter(Value = "waiting")]
        Waiting,
        [Parameter(Value = "skipped")]
        Skipped,
    }
}
