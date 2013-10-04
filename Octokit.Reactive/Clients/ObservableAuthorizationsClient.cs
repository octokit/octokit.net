using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive.Clients
{
    public class ObservableAuthorizationsClient : IObservableAuthorizationsClient
    {
        readonly IAuthorizationsClient _client;

        public ObservableAuthorizationsClient(IAuthorizationsClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client;
        }

        public IObservable<IReadOnlyCollection<Authorization>> GetAll()
        {
            return _client.GetAll().ToObservable();
        }

        public IObservable<Authorization> Get(int id)
        {
            return _client.Get(id).ToObservable();
        }

        public IObservable<Authorization> Update(int id, AuthorizationUpdate authorization)
        {
            Ensure.ArgumentNotNull(authorization, "authorization");

            return _client.Update(id, authorization).ToObservable();
        }

        public IObservable<Authorization> Create(AuthorizationUpdate authorization)
        {
            Ensure.ArgumentNotNull(authorization, "authorization");

            return _client.Create(authorization).ToObservable();
        }

        public IObservable<Unit> Delete(int id)
        {
            return _client.Delete(id).ToObservable();
        }
    }
}
