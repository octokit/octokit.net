using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Nocto.Helpers;

namespace Nocto
{
    public class AuthorizationsEndpoint : IAuthorizationsEndpoint
    {
        readonly IGitHubClient client;

        public AuthorizationsEndpoint(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            this.client = client;
        }

        /// <summary>
        /// Get all <see cref="Authorization"/>s for the authenticated user. This method requires basic auth.
        /// </summary>
        /// <returns>An <see cref="Authorization"/></returns>
        public async Task<List<Authorization>> GetAllAsync()
        {
            Ensure.IsUsingBasicAuth(client.AuthenticationType);

            var res = await client.Connection.GetAsync<List<Authorization>>("/authorizations");

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

        /// <summary>
        /// Update the specified <see cref="Authorization"/>.
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public async Task<Authorization> UpdateAsync(int id, AuthorizationUpdate auth)
        {
            Ensure.IsUsingBasicAuth(client.AuthenticationType);

            var endpoint = string.Format("/authorizations/{0}", id);
            var res = await client.Connection.PatchAsync<Authorization>(endpoint, auth);

            return res.BodyAsObject;
        }

        /// <summary>
        /// Create a new <see cref="Authorization"/>.
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public async Task<Authorization> CreateAsync(AuthorizationUpdate auth)
        {
            Ensure.IsUsingBasicAuth(client.AuthenticationType);

            var res = await client.Connection.PostAsync<Authorization>("/authorizations", auth);

            return res.BodyAsObject;
        }

        /// <summary>
        /// Deletes an <see cref="Authorization"/>.
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            Ensure.IsUsingBasicAuth(client.AuthenticationType);

            var endpoint = string.Format("/authorizations/{0}", id);
            await client.Connection.DeleteAsync<Authorization>(endpoint);
        }
    }
}
