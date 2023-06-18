using Octokit.Internal;

namespace Octokit
{
    public enum CodespaceState
    {
        [Parameter(Value = "Unknown")]
        Unknown,
        [Parameter(Value = "Created")]
        Created,
        [Parameter(Value = "Queued")]
        Queued,
        [Parameter(Value = "Provisioning")]
        Provisioning,
        [Parameter(Value = "Available")]
        Available,
        [Parameter(Value = "Awaiting")]
        Awaiting,
        [Parameter(Value = "Unavailable")]
        Unavailable,
        [Parameter(Value = "Deleted")]
        Deleted,
        [Parameter(Value = "Moved")]
        Moved,
        [Parameter(Value = "Shutdown")]
        Shutdown,
        [Parameter(Value = "Archived")]
        Archived,
        [Parameter(Value = "Starting")]
        Starting,
        [Parameter(Value = "ShuttingDown")]
        ShuttingDown,
        [Parameter(Value = "Failed")]
        Failed,
        [Parameter(Value = "Exporting")]
        Exporting,
        [Parameter(Value = "Updating")]
        Updating,
        [Parameter(Value = "Rebuilding")]
        Rebuilding,
    }
}