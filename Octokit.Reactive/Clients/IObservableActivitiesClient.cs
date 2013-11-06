namespace Octokit.Reactive
{
    public interface IObservableActivitiesClient
    {
        IObservableEventsClient Events { get; }
    }
}