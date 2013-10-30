using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableOrganizationsClient : IObservableOrganizationsClient
    {
        readonly IOrganizationsClient _client;
        readonly IConnection _connection;

        public ObservableOrganizationsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Organization;
            _connection = client.Connection;
        }

        public IObservable<Organization> Get(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            return _client.Get(org).ToObservable();
        }

        public IObservable<Organization> GetAllForCurrent()
        {
            return _connection.GetAndFlattenAllPages<Organization>(ApiUrls.Organizations());
        }

        public IObservable<Organization> GetAll(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return _connection.GetAndFlattenAllPages<Organization>(ApiUrls.Organizations(user));
        }
    }
}
