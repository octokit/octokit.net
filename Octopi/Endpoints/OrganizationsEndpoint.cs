using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Octopi.Http;

namespace Octopi.Endpoints
{
    public class OrganizationsEndpoint : ApiEndpoint<Organization>, IOrganizationsEndpoint
    {
        public OrganizationsEndpoint(IConnection connection) : base(connection)
        {
        }

        public async Task<Organization> Get(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            var endpoint = new Uri(string.Format("/orgs/{0}", org), UriKind.Relative);
            return await Get(endpoint);
        }

        public async Task<IReadOnlyCollection<Organization>> GetAllForCurrent()
        {
            var endpoint = new Uri("user/orgs", UriKind.Relative);

            return await GetAll(endpoint);
        }

        public async Task<IReadOnlyCollection<Organization>> GetAll(string user)
        {
            Ensure.ArgumentNotNull(user, "user");

            var endpoint = new Uri(string.Format(CultureInfo.InvariantCulture, "/users/{0}/orgs", user),
                UriKind.Relative);
            
            return await GetAll(endpoint);
        }
    }
}
