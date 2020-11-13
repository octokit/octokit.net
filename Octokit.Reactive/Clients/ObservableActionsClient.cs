using Octokit.Reactive.Clients;

namespace Octokit.Reactive
{
    /// <summary>
    /// Used to maintain api structure therefore contains no methods
    /// </summary>
    public class ObservableActionsClient : IObservableActionsClient
    {
        public ObservableActionsClient(IGitHubClient client)
        {
            Workflows = new ObservableWorkflowClient(client);
        }
        public IObservableWorkflowsClient Workflows { get; set; }
    }

}