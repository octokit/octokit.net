using Octokit.Internal;

namespace Octokit
{
    public enum WorkflowRunStatus
    {
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
        [Parameter(Value = "pending")]
        Pending
    }
}
