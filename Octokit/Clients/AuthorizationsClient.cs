using System;
#if NET_45
using System.Collections.Generic;
#endif
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Client for the OAuth2 API.
    /// </summary>
    /// <remarks>
    /// See <a href="http://developer.github.com/v3/oauth/">OAuth API documentation</a> for more details.
    /// </remarks>
    public class AuthorizationsClient : ApiClient<Authorization>, IAuthorizationsClient
    {
        static readonly Uri _authorizationsEndpoint = new Uri("/authorizations", UriKind.Relative);

        public AuthorizationsClient(IApiConnection<Authorization> client) : base(client)
        {
        }

        /// <summary>
        /// Get all <see cref="Authorization"/>s for the authenticated user. This method requires basic auth.
        /// </summary>
        /// <remarks>
        /// See <a href="http://developer.github.com/v3/oauth/#list-your-authorizations">API documentation</a> for more
        /// details.
        /// </remarks>
        /// <returns>An <see cref="Authorization"/></returns>
        public async Task<IReadOnlyList<Authorization>> GetAll()
        {
            return await Client.GetAll(_authorizationsEndpoint);
        }

        /// <summary>
        /// Get a specific <see cref="Authorization"/> for the authenticated user. This method requires basic auth.
        /// </summary>
        /// <remarks>
        /// See <a href="http://developer.github.com/v3/oauth/#get-a-single-authorization">API documentation</a> for
        /// more details.
        /// </remarks>
        /// <param name="id">The id of the <see cref="Authorization"/></param>
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
        /// <remarks>
        /// See <a href="http://developer.github.com/v3/oauth/#get-or-create-an-authorization-for-a-specific-app">API
        /// documentation</a> for more details.
        /// </remarks>
        /// <param name="clientId">Client ID for the OAuth application that is requesting the token</param>
        /// <param name="clientSecret">The client secret</param>
        /// <param name="newAuthorization">Defines the scopes and metadata for the token</param>
        /// <exception cref="AuthorizationException">Thrown when the user does not have permission to make 
        /// this request. Check </exception>
        /// <exception cref="TwoFactorRequiredException">Thrown when the current account has two-factor
        /// authentication enabled.</exception>
        /// <returns></returns>
        public async Task<Authorization> GetOrCreateApplicationAuthentication(
            string clientId,
            string clientSecret,
            NewAuthorization newAuthorization)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, "clientId");
            Ensure.ArgumentNotNullOrEmptyString(clientSecret, "clientSecret");
            Ensure.ArgumentNotNull(newAuthorization, "authorization");

            var endpoint = "/authorizations/clients/{0}".FormatUri(clientId);
            var requestData = new
            {
                client_secret = clientSecret,
                scopes = newAuthorization.Scopes,
                note = newAuthorization.Note,
                note_url = newAuthorization.NoteUrl
            };

            return await Client.GetOrCreate(endpoint, requestData);
        }

        /// <summary>
        /// This method will create a new authorization for the specified OAuth application, only if an authorization 
        /// for that application doesn’t already exist for the user. It returns the user’s token for the application
        /// if one exists. Otherwise, it creates one.
        /// </summary>
        /// <remarks>
        /// See <a href="http://developer.github.com/v3/oauth/#get-or-create-an-authorization-for-a-specific-app">API 
        /// documentation</a> for more details.
        /// </remarks>
        /// <param name="clientId">Client ID for the OAuth application that is requesting the token</param>
        /// <param name="clientSecret">The client secret</param>
        /// <param name="newAuthorization">Defines the scopes and metadata for the token</param>
        /// <param name="twoFactorAuthenticationCode"></param>
        /// <exception cref="AuthorizationException">Thrown when the user does not have permission to make 
        /// this request. Check </exception>
        /// <exception cref="TwoFactorChallengeFailedException">Thrown when the two-factor code is not
        /// valid.</exception>
        /// <returns></returns>
        public async Task<Authorization> GetOrCreateApplicationAuthentication(
            string clientId,
            string clientSecret,
            NewAuthorization newAuthorization,
            string twoFactorAuthenticationCode)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, "clientId");
            Ensure.ArgumentNotNullOrEmptyString(clientSecret, "clientSecret");
            Ensure.ArgumentNotNull(newAuthorization, "authorization");
            Ensure.ArgumentNotNullOrEmptyString(twoFactorAuthenticationCode, "twoFactorAuthenticationCode");

            var endpoint = "/authorizations/clients/{0}".FormatUri(clientId);
            var requestData = new
            {
                client_secret = clientSecret,
                scopes = newAuthorization.Scopes,
                note = newAuthorization.Note,
                note_url = newAuthorization.NoteUrl
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
        /// <param name="id">The id of the <see cref="Authorization"/></param>
        /// <param name="authorizationUpdate">The changes to make to the authorization</param>
        /// <returns></returns>
        public async Task<Authorization> Update(int id, AuthorizationUpdate authorizationUpdate)
        {
            Ensure.ArgumentNotNull(authorizationUpdate, "authorizationUpdate");

            var endpoint = "/authorizations/{0}".FormatUri(id);
            return await Client.Update(endpoint, authorizationUpdate);
        }

        /// <summary>
        /// Create a new <see cref="Authorization"/>.
        /// </summary>
        /// <param name="newAuthorization"></param>
        /// <returns></returns>
        public async Task<Authorization> Create(NewAuthorization newAuthorization)
        {
            Ensure.ArgumentNotNull(newAuthorization, "newAuthorization");

            return await Client.Create(_authorizationsEndpoint, newAuthorization);
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
