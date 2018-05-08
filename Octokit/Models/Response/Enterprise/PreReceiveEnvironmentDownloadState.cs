using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// The state of the most recent download.
    /// </summary>
    public enum PreReceiveEnvironmentDownloadState
    {
        [Parameter(Value = "not_started")]
        NotStarted,

        [Parameter(Value = "in_progress")]
        InProgress,

        [Parameter(Value = "success")]
        Success,

        [Parameter(Value = "failed")]
        Failed
    }
}