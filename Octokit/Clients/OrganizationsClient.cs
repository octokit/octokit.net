using System;
#if NET_45
using System.Collections.Generic;
#endif
using System.Threading.Tasks;

namespace Octokit
{
    public class OrganizationsClient : ApiClient, IOrganizationsClient
    {
        public OrganizationsClient(IApiConnection client) : base(client)
        {
        }

        public async Task<Organization> Get(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            var endpoint = "/orgs/{0}".FormatUri(org);
            return await Client.Get<Organization>(endpoint);
        }

        public async Task<IReadOnlyList<Organization>> GetAllForCurrent()
        {
            return await Client.GetAll<Organization>(ApiUrls.Organizations());
        }

        public async Task<IReadOnlyList<Organization>> GetAll(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return await Client.GetAll<Organization>(ApiUrls.Organizations(user));
        }
    }
}
