namespace Octokit
{
    public interface IActivitiesClient
    {
        IEventsClient Events { get; }
    }
}