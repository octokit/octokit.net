using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octopi.Http;

namespace Octopi.Clients
{
    public class OrganizationsClient : ApiClient<Organization>, IOrganizationsClient
    {
        public OrganizationsClient(IApiConnection<Organization> client) : base(client)
        {
        }

        public async Task<Organization> Get(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            var endpoint = "/orgs/{0}".FormatUri(org);
            return await Client.Get(endpoint);
        }

        public async Task<IReadOnlyCollection<Organization>> GetAllForCurrent()
        {
            var endpoint = new Uri("/user/orgs", UriKind.Relative);

            return await Client.GetAll(endpoint);
        }

        public async Task<IReadOnlyCollection<Organization>> GetAll(string user)
        {
            Ensure.ArgumentNotNull(user, "user");

            var endpoint = "/users/{0}/orgs".FormatUri(user);

            return await Client.GetAll(endpoint);
        }
    }
}
