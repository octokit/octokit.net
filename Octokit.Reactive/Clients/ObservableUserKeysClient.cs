using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableUserKeysClient : IObservableUserKeysClient
    {
        readonly IUserKeysClient _client;

        public ObservableUserKeysClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.User.Keys;
        }

        public IObservable<PublicKey> GetAll()
        {
            return _client.GetAll().ToObservable().SelectMany(k => k);
        }

        public IObservable<PublicKey> GetAll(string userName)
        {
            return _client.GetAll(userName).ToObservable().SelectMany(k => k);
        }
    }
}