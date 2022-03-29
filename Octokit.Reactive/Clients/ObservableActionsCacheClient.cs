namespace Octokit.Reactive
{
    public class ObservableActionsCacheClient : IObservableActionsCacheClient
    {
        readonly IActionsCacheClient _client;

        /// <summary>
        /// Instantiate a new GitHub Actions Cache API client.
        /// </summary>
        /// <param name="client">A GitHub client.</param>
        public ObservableActionsCacheClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Actions.Cache;
        }
    }
}
