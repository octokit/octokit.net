namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Repository Actions API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/reference/actions">Repository Actions API documentation</a> for more details.
    /// </remarks>
    public class ObservableRepositoryActionsClient : IObservableRepositoryActionsClient
    {
        readonly IRepositoryActionsClient _client;
        readonly IConnection _connection;

        public ObservableRepositoryActionsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Repository.Actions;
            _connection = client.Connection;

            Secrets = new ObservableRepositorySecretsClient(client);
        }

        /// <summary>
        /// Client for GitHub's Repository Actions API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions">Deployments API documentation</a> for more details
        /// </remarks>
        public IObservableRepositorySecretsClient Secrets { get; private set; }
    }
}
