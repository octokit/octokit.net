using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Threading.Tasks;

namespace Octopi.Reactive.Clients
{
    public class ObservableAuthorizationsClient : IObservableAuthorizationsClient
    {
        readonly IAuthorizationsClient client;

        public ObservableAuthorizationsClient(IAuthorizationsClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            this.client = client;
        }

        public IObservable<IReadOnlyCollection<Authorization>> GetAll()
        {
            return client.GetAll().ToObservable();
        }

        public IObservable<Authorization> Get(long id)
        {
            return client.Get(id).ToObservable();
        }

        public IObservable<Authorization> Update(long id, AuthorizationUpdate authorization)
        {
            return client.Update(id, authorization).ToObservable();
        }

        public IObservable<Authorization> Create(AuthorizationUpdate authorization)
        {
            return client.Create(authorization).ToObservable();
        }

        public IObservable<Unit> Delete(long id)
        {
            return client.Delete(id).ToObservable();
        }
    }
}
