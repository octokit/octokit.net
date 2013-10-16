using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Helpers;

namespace Octokit.Reactive.Clients
{
    public class ObservableSshKeysClient : IObservableSshKeysClient
    {
        readonly ISshKeysClient _client;
        readonly IConnection _connection; 

        public ObservableSshKeysClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");
            
            _client = client.SshKey;
            _connection = client.Connection;
        }

        public IObservable<SshKey> Get(int id)
        {
            return _client.Get(id).ToObservable();
        }

        public IObservable<SshKey> GetAll(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return _connection.GetAndFlattenAllPages<SshKey>(ApiUrls.SshKeys(user));
        }

        public IObservable<SshKey> GetAllForCurrent()
        {
            return _connection.GetAndFlattenAllPages<SshKey>(ApiUrls.SshKeys());
        }

        public IObservable<SshKey> Create(SshKeyUpdate key)
        {
            Ensure.ArgumentNotNull(key, "key");

            return _client.Create(key).ToObservable();
        }

        public IObservable<SshKey> Update(int id, SshKeyUpdate key)
        {
            Ensure.ArgumentNotNull(key, "key");

            return _client.Update(id, key).ToObservable();
        }

        public IObservable<Unit> Delete(int id)
        {
            return _client.Delete(id).ToObservable();
        }
    }
}
