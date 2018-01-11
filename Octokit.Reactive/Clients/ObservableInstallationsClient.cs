using System;
using Octokit.Reactive.Internal;

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

        public IObservable<Installation> GetAll()
        {
            return _connection.GetAndFlattenAllPages<Installation>(ApiUrls.Installations(), null, AcceptHeaders.MachineManPreview);
        }

        public IObservable<Installation> GetAll(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<Installation>(ApiUrls.Installations(), null, AcceptHeaders.MachineManPreview, options);
        }
    }
}
