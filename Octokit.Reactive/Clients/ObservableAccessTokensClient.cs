using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    class ObservableAccessTokensClient : IObservableAccessTokensClient
    {
        readonly IAccessTokensClient _client;
        readonly IConnection _connection;

        public ObservableAccessTokensClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Installations.AccessTokens;
            _connection = client.Connection;
        }

        public IObservable<AccessToken> Create(int installationId)
        {
            return _client.Create(installationId).ToObservable();
        }
    }
}
