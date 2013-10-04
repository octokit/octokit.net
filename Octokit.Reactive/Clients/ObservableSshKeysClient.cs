using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive.Clients
{
    public class ObservableSshKeysClient : IObservableSshKeysClient
    {
        readonly ISshKeysClient _client;

        public ObservableSshKeysClient(ISshKeysClient client)
        {
            Ensure.ArgumentNotNull(client, "client");
            
            _client = client;
        }

        public IObservable<SshKey> Get(int id)
        {
            return _client.Get(id).ToObservable();
        }

        public IObservable<IReadOnlyCollection<SshKey>> GetAll(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return _client.GetAll(user).ToObservable();
        }

        public IObservable<IReadOnlyCollection<SshKey>> GetAllForCurrent()
        {
            return _client.GetAllForCurrent().ToObservable();
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
