using System;
#if NET_45
using System.Collections.Generic;
#endif
using System.Collections;
using System.Threading.Tasks;
using Octokit.Internal;

namespace Octokit
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
        public async Task<IReadOnlyList<Authorization>> GetAll()
        {
            return await Client.GetAll(authorizationsEndpoint);
        }

        /// <summary>
        /// Get a specific <see cref="Authorization"/> for the authenticated user. This method requires basic auth.
        /// </summary>
        /// <param name="id">The id of the <see cref="Authorization"/>.</param>
        /// <returns>An <see cref="Authorization"/></returns>
        public async Task<Authorization> Get(int id)
        {
            var endpoint = "/authorizations/{0}".FormatUri(id);
            return await Client.Get(endpoint);
        }

        /// <summary>
        /// This method will create a new authorization for the specified OAuth application, only if an authorization 
        /// for that application doesn’t already exist for the user. It returns the user’s token for the application
        /// if one exists. Otherwise, it creates one.
        /// </summary>
        /// <param name="clientId">Client ID for the OAuth application that is requesting the token.</param>
        /// <param name="clientSecret">The client secret</param>
        /// <param name="authorization">Definse the scopes and metadata for the token</param>
        /// <returns></returns>
        public async Task<Authorization> GetOrCreateApplicationAuthentication(
            string clientId,
            string clientSecret,
            AuthorizationUpdate authorization)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, "clientId");
            Ensure.ArgumentNotNullOrEmptyString(clientSecret, "clientSecret");
            Ensure.ArgumentNotNull(authorization, "authorization");

            var endpoint = "/authorizations/clients/{0}".FormatUri(clientId);
            var requestData = new
            {
                client_secret = clientSecret,
                scopes = authorization.Scopes,
                note = authorization.Note,
                note_url = authorization.NoteUrl
            };

            return await Client.GetOrCreate(endpoint, requestData);
        }

        public async Task<Authorization> GetOrCreateApplicationAuthentication(
            string clientId,
            string clientSecret,
            AuthorizationUpdate authorization,
            string twoFactorAuthenticationCode)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, "clientId");
            Ensure.ArgumentNotNullOrEmptyString(clientSecret, "clientSecret");
            Ensure.ArgumentNotNull(authorization, "authorization");
            Ensure.ArgumentNotNullOrEmptyString(twoFactorAuthenticationCode, "twoFactorAuthenticationCode");

            var endpoint = "/authorizations/clients/{0}".FormatUri(clientId);
            var requestData = new
            {
                client_secret = clientSecret,
                scopes = authorization.Scopes,
                note = authorization.Note,
                note_url = authorization.NoteUrl
            };

            try
            {
                return await Client.GetOrCreate(
                    endpoint,
                    requestData,
                    twoFactorAuthenticationCode);
            }
            catch (AuthorizationException e)
            {
                throw new TwoFactorChallengeFailedException("Two-Factor Authentication code is not valid", e);
            }
        }

        /// <summary>
        /// Update the specified <see cref="Authorization"/>.
        /// </summary>
        /// <param name="id">The id of the <see cref="Authorization"/>.</param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public async Task<Authorization> Update(int id, AuthorizationUpdate authorization)
        {
            Ensure.ArgumentNotNull(authorization, "authorization");

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
            Ensure.ArgumentNotNull(authorization, "authorization");

            return await Client.Create(authorizationsEndpoint, authorization);
        }

        /// <summary>
        /// Deletes an <see cref="Authorization"/>.
        /// </summary>
        /// <param name="id">The systemwide id of the authorization</param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            var endpoint = "/authorizations/{0}".FormatUri(id);
            await Client.Delete(endpoint);
        }
    }
}
