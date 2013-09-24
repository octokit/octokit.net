using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive.Clients
{
    public class ObservableSshKeysClient : IObservableSshKeysClient
    {
        readonly ISshKeysClient client;

        public ObservableSshKeysClient(ISshKeysClient client)
        {
            Ensure.ArgumentNotNull(client, "client");
            
            this.client = client;
        }

        public IObservable<SshKey> Get(int id)
        {
            return client.Get(id).ToObservable();
        }

        public IObservable<IReadOnlyCollection<SshKey>> GetAll(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return client.GetAll(user).ToObservable();
        }

        public IObservable<IReadOnlyCollection<SshKey>> GetAllForCurrent()
        {
            return client.GetAllForCurrent().ToObservable();
        }

        public IObservable<SshKey> Create(SshKeyUpdate key)
        {
            Ensure.ArgumentNotNull(key, "key");

            return client.Create(key).ToObservable();
        }

        public IObservable<SshKey> Update(int id, SshKeyUpdate key)
        {
            Ensure.ArgumentNotNull(key, "key");

            return client.Update(id, key).ToObservable();
        }

        public IObservable<Unit> Delete(int id)
        {
            return client.Delete(id).ToObservable();
        }
    }
}
