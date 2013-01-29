﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octopi.Http;

namespace Octopi.Clients
{
    public class AuthorizationsClient : ApiClient<Authorization>, IAuthorizationsClient
    {
        static readonly Uri authorizationsEndpoint = new Uri("/authorizations", UriKind.Relative);

        public AuthorizationsClient(IApiConnection<Authorization> client) : base(client)
        {
        }

        /// <summary>
        /// Get all <see cref="Authorization"/>s for the authenticated user. This method requires basic auth.
        /// </summary>
        /// <returns>An <see cref="Authorization"/></returns>
        public async Task<IReadOnlyCollection<Authorization>> GetAll()
        {
            return await Client.GetAll(authorizationsEndpoint);
        }

        /// <summary>
        /// Get a specific <see cref="Authorization"/> for the authenticated user. This method requires basic auth.
        /// </summary>
        /// <param name="id">The id of the <see cref="Authorization"/>.</param>
        /// <returns>An <see cref="Authorization"/></returns>
        public async Task<Authorization> Get(long id)
        {
            var endpoint = "/authorizations/{0}".FormatUri(id);
            return await Client.Get(endpoint);
        }

        /// <summary>
        /// Update the specified <see cref="Authorization"/>.
        /// </summary>
        /// <param name="id">The id of the <see cref="Authorization"/>.</param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<Authorization> Update(long id, AuthorizationUpdate authorization)
        {
            var endpoint = "/authorizations/{0}".FormatUri(id);
            return await Client.Update(endpoint, authorization);
        }

        /// <summary>
        /// Create a new <see cref="Authorization"/>.
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<Authorization> Create(AuthorizationUpdate authorization)
        {
            return await Client.Create(authorizationsEndpoint, authorization);
        }

        /// <summary>
        /// Deletes an <see cref="Authorization"/>.
        /// </summary>
        /// <param name="id">The systemwide id of the authorization</param>
        /// <returns></returns>
        public async Task Delete(long id)
        {
            var endpoint = "/authorizations/{0}".FormatUri(id);
            await Client.Delete(endpoint);
        }
    }
}
