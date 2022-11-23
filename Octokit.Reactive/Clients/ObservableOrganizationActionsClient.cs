namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Org Actions API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/reference/actions"> Actions API documentation</a> for more information.
    /// </remarks>
    public class ObservableOrganizationActionsClient : IObservableOrganizationActionsClient
    {
        readonly IOrganizationActionsClient _client;
        readonly IConnection _connection;

        /// <summary>
        /// Initializes a new Organization API client.
        /// </summary>
        /// <param name="client">An <see cref="IGitHubClient" /> used to make the requests</param>
        public ObservableOrganizationActionsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            Secrets = new ObservableOrganizationSecretsClient(client);

            _client = client.Organization.Actions;
            _connection = client.Connection;
        }

        /// <summary>
        /// Returns a client to manage organization secrets.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#secrets"> Secrets API documentation</a> for more information.
        /// </remarks>
        public IObservableOrganizationSecretsClient Secrets { get; private set; }
    }
}
