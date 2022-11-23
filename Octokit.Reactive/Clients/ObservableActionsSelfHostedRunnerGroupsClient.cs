namespace Octokit.Reactive
{
    public class ObservableActionsSelfHostedRunnerGroupsClient : IObservableActionsSelfHostedRunnerGroupsClient
    {
        readonly IActionsSelfHostedRunnerGroupsClient _client;

        /// <summary>
        /// Instantiate a new GitHub Actions Self-hosted runner groups API client.
        /// </summary>
        /// <param name="client">A GitHub client.</param>
        public ObservableActionsSelfHostedRunnerGroupsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Actions.SelfHostedRunnerGroups;
        }
    }
}
