using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive.Clients
{
    public class ObservableOrganizationsClient : IObservableOrganizationsClient
    {
        readonly IOrganizationsClient client;

        public ObservableOrganizationsClient(IOrganizationsClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            this.client = client;
        }

        public IObservable<Organization> Get(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            return client.Get(org).ToObservable();
        }

        public IObservable<IReadOnlyCollection<Organization>> GetAllForCurrent()
        {
            return client.GetAllForCurrent().ToObservable();
        }

        public IObservable<IReadOnlyCollection<Organization>> GetAll(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return client.GetAll(user).ToObservable();
        }
    }
}
