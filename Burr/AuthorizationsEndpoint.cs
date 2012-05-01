using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Burr.Helpers;

namespace Burr
{
    public class AuthorizationsEndpoint : IAuthorizationsEndpoint
    {
        IGitHubClient client;

        public AuthorizationsEndpoint(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            this.client = client;
        }

        /// <summary>
        /// Get all <see cref="Authorizations"/> for the authenticated user. This method requires basic auth.
        /// </summary>
        /// <returns>An <see cref="Authorization"/></returns>
        public async Task<IEnumerable<Authorization>> GetAllAsync()
        {
            Ensure.IsUsingBasicAuth(client.AuthenticationType);

            var res = await client.Connection.GetAsync<IEnumerable<Authorization>>("/authorizations");

            return res.BodyAsObject;
        }

        /// <summary>
        /// Get a specific <see cref="Authorization"/> for the authenticated user. This method requires basic auth.
        /// </summary>
        /// <param name="id">The id of the <see cref="Authorization"/>.</param>
        /// <returns>An <see cref="Authorization"/></returns>
        public async Task<Authorization> GetAsync(int id)
        {
            Ensure.IsUsingBasicAuth(client.AuthenticationType);

            var endpoint = string.Format("/authorizations/{0}", id);
            var res = await client.Connection.GetAsync<Authorization>(endpoint);

            return res.BodyAsObject;
        }
    }
}
