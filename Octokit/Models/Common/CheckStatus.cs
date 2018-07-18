using Octokit.Internal;

namespace Octokit
{
    public enum CheckStatus
    {
        [Parameter(Value = "queued")]
        Queued,

        [Parameter(Value = "in_progress")]
        InProgress,

        [Parameter(Value = "completed")]
        Completed,
    }

    public enum CheckConclusion
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
    }

    public enum CheckWarningLevel
    {
        [Parameter(Value = "notice")]
        Notice,

        [Parameter(Value = "warning")]
        Warning,

        [Parameter(Value = "failure")]
        Failure,
    }
}
