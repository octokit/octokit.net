namespace Octokit.Reactive
{
    public class ObservableActionsArtifactsClient : IObservableActionsArtifactsClient
    {
        readonly IActionsArtifactsClient _client;

        /// <summary>
        /// Instantiate a new GitHub Actions Artifacts API client.
        /// </summary>
        /// <param name="client">A GitHub client.</param>
        public ObservableActionsArtifactsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Actions.Artifacts;
        }
    }
}
