namespace Octokit.Reactive
{
    class ObservableInstallationsClient : IObservableInstallationsClient
    {
        readonly IInstallationsClient _client;
        readonly IConnection _connection;

        public ObservableInstallationsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Installations;
            _connection = client.Connection;
            AccessTokens = new ObservableAccessTokensClient(client);
        }
        
        public IObservableAccessTokensClient AccessTokens { get; }
    }
}
