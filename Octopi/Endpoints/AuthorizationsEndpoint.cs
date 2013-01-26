using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octopi.Http;

namespace Octopi
{
    public class AuthorizationsEndpoint : IAuthorizationsEndpoint
    {
        static readonly Uri authorizationsEndpoint = new Uri("/authorizations", UriKind.Relative);

        readonly IConnection connection;
        
        public AuthorizationsEndpoint(IConnection connection)
        {
            Ensure.ArgumentNotNull(connection, "connection");

            this.connection = connection;
        }

        /// <summary>
        /// Get all <see cref="Authorization"/>s for the authenticated user. This method requires basic auth.
        /// </summary>
        /// <returns>An <see cref="Authorization"/></returns>
        public async Task<List<Authorization>> GetAll()
        {
            var res = await connection.GetAsync<List<Authorization>>(authorizationsEndpoint);

            return res.BodyAsObject;
        }

        /// <summary>
        /// Get a specific <see cref="Authorization"/> for the authenticated user. This method requires basic auth.
        /// </summary>
        /// <param name="id">The id of the <see cref="Authorization"/>.</param>
        /// <returns>An <see cref="Authorization"/></returns>
        public async Task<Authorization> GetAsync(int id)
        {
            var endpoint = new Uri(string.Format("/authorizations/{0}", id), UriKind.Relative);
            var res = await connection.GetAsync<Authorization>(endpoint);

            return res.BodyAsObject;
        }

        /// <summary>
        /// Update the specified <see cref="Authorization"/>.
        /// </summary>
        /// <param name="id">The id of the <see cref="Authorization"/>.</param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<Authorization> UpdateAsync(int id, AuthorizationUpdate authorization)
        {
            var endpoint = new Uri(string.Format("/authorizations/{0}", id), UriKind.Relative);
            var res = await connection.PatchAsync<Authorization>(endpoint, authorization);

            return res.BodyAsObject;
        }

        /// <summary>
        /// Create a new <see cref="Authorization"/>.
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<Authorization> CreateAsync(AuthorizationUpdate authorization)
        {
            var res = await connection.PostAsync<Authorization>(authorizationsEndpoint, authorization);

            return res.BodyAsObject;
        }

        /// <summary>
        /// Deletes an <see cref="Authorization"/>.
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            var endpoint = new Uri(string.Format("/authorizations/{0}", id), UriKind.Relative);
            await connection.DeleteAsync<Authorization>(endpoint);
        }
    }
}
