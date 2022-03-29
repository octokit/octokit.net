namespace Octokit.Reactive
{
    public class ObservableActionsSelfHostedRunnersClient : IObservableActionsSelfHostedRunnersClient
    {
        readonly IActionsSelfHostedRunnersClient _client;

        /// <summary>
        /// Instantiate a new GitHub Actions Self-hosted runners API client.
        /// </summary>
        /// <param name="client">A GitHub client.</param>
        public ObservableActionsSelfHostedRunnersClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Actions.SelfHostedRunners;
        }
    }
}
