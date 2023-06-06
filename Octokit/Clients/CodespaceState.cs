namespace Octokit
{
    public enum CodespaceState
    {
        Unknown,
        Created,
        Queued,
        Provisioning,
        Available,
        Awaiting,
        Unavailable,
        Deleted,
        Moved,
        Shutdown,
        Archived,
        Starting,
        ShuttingDown,
        Failed,
        Exporting,
        Updating,
        Rebuilding,
    }
}