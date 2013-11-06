namespace Octokit.Reactive
{
    public interface IObservableActivitiesClient
    {
        IObservableEventsClient Event{ get; }
    }
}