namespace Octokit
{
    public interface IActivitiesClient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Event")]
        IEventsClient Event { get; }
    }
}