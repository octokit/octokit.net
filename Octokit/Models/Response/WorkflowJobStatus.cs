using Octokit.Internal;

namespace Octokit
{
    public enum WorkflowJobStatus
    {
        [Parameter(Value = "queued")]
        Queued,
        [Parameter(Value = "in_progress")]
        InProgress,
        [Parameter(Value = "completed")]
        Completed,
        [Parameter(Value = "waiting")]
        Waiting,
        [Parameter(Value = "pending")]
        Pending,
    }
}
