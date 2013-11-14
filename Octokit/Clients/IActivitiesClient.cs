namespace Octokit
{
    public interface IActivitiesClient
    {
        IEventsClient Events { get; }
        IStarredClient Starring { get; }
    }
}