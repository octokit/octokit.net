namespace Octokit
{
    public interface IActivitiesClient
    {
        IEventsClient Event { get; }
    }
}