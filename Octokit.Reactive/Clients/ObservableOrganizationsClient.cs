using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive.Clients
{
    public class ObservableOrganizationsClient : IObservableOrganizationsClient
    {
        readonly IOrganizationsClient _client;

        public ObservableOrganizationsClient(IOrganizationsClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client;
        }

        public IObservable<Organization> Get(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            return _client.Get(org).ToObservable();
        }

        public IObservable<IReadOnlyList<Organization>> GetAllForCurrent()
        {
            return _client.GetAllForCurrent().ToObservable();
        }

        public IObservable<IReadOnlyList<Organization>> GetAll(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return _client.GetAll(user).ToObservable();
        }
    }
}
