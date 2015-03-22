using System;
#if NET_45
using System.Collections.Generic;
#endif
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's OAuth API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/oauth/">OAuth API documentation</a> for more details.
    /// </remarks>
    public class AuthorizationsClient : ApiClient, IAuthorizationsClient
    {
        const string previewAcceptsHeader = "application/vnd.github.mirage-preview+json";

        /// <summary>
        /// Initializes a new GitHub OAuth API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public AuthorizationsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets all <see cref="Authorization"/>s for the authenticated user.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://developer.github.com/v3/oauth/#list-your-authorizations">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of <see cref="Authorization"/>s.</returns>
        public Task<IReadOnlyList<Authorization>> GetAll()
        {
            return GetAll(new ApiOptions());
        }

        /// <summary>
        /// Gets all <see cref="Authorization"/>s for the authenticated user.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://developer.github.com/v3/oauth/#list-your-authorizations">API documentation</a> for more information.
        /// </remarks>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of <see cref="Authorization"/>s.</returns>
        public Task<IReadOnlyList<Authorization>> GetAll(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<Authorization>(ApiUrls.Authorizations(), null, previewAcceptsHeader, options);
        }

        /// <summary>
        /// Gets a specific <see cref="Authorization"/> for the authenticated user.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://developer.github.com/v3/oauth/#get-a-single-authorization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The ID of the <see cref="Authorization"/> to get</param>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make this request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The specified <see cref="Authorization"/>.</returns>
        public Task<Authorization> Get(int id)
        {
            return ApiConnection.Get<Authorization>(ApiUrls.Authorizations(id), null, previewAcceptsHeader);
        }

        /// <summary>
        /// Creates a new authorization for the specified OAuth application if an authorization for that application doesn’t already 
        /// exist for the user; otherwise, returns the user’s existing authorization for that application.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://developer.github.com/v3/oauth/#get-or-create-an-authorization-for-a-specific-app">API documentation</a> for more information.
        /// </remarks>
        /// <param name="clientId">Client ID of the OAuth application for the token</param>
        /// <param name="clientSecret">The client secret</param>
        /// <param name="newAuthorization">Describes the new authorization to create</param>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make this request.
        /// </exception>
        /// <exception cref="TwoFactorRequiredException">
        /// Thrown when the current account has two-factor authentication enabled and an authentication code is required.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The created <see cref="Authorization"/>.</returns>
        public Task<ApplicationAuthorization> GetOrCreateApplicationAuthentication(
            string clientId,
            string clientSecret,
            NewAuthorization newAuthorization)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, "clientId");
            Ensure.ArgumentNotNullOrEmptyString(clientSecret, "clientSecret");
            Ensure.ArgumentNotNull(newAuthorization, "authorization");

            var requestData = new
            {
                client_secret = clientSecret,
                scopes = newAuthorization.Scopes,
                note = newAuthorization.Note,
                note_url = newAuthorization.NoteUrl
            };

            if (String.IsNullOrWhiteSpace(newAuthorization.Fingerprint))
            {
                // use classic API
                var endpoint = ApiUrls.AuthorizationsForClient(clientId);
                return ApiConnection.Put<ApplicationAuthorization>(endpoint, requestData);
            }
            else
            {
                // use new API
                var endpoint = ApiUrls.AuthorizationsForClient(clientId, newAuthorization.Fingerprint);
                return ApiConnection.Put<ApplicationAuthorization>(endpoint, requestData, null, previewAcceptsHeader);
            }
        }

        /// <summary>
        /// Creates a new authorization for the specified OAuth application if an authorization for that application doesn’t already 
        /// exist for the user; otherwise, returns the user’s existing authorization for that application.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://developer.github.com/v3/oauth/#get-or-create-an-authorization-for-a-specific-app">API documentation</a> for more information.
        /// </remarks>
        /// <param name="clientId">Client ID of the OAuth application for the token</param>
        /// <param name="clientSecret">The client secret</param>
        /// <param name="newAuthorization">Describes the new authorization to create</param>
        /// <param name="twoFactorAuthenticationCode">The two-factor authentication code in response to the current user's previous challenge</param>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make this request.
        /// </exception>
        /// <exception cref="TwoFactorRequiredException">
        /// Thrown when the current account has two-factor authentication enabled and an authentication code is required.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The created <see cref="Authorization"/>.</returns>
        public async Task<ApplicationAuthorization> GetOrCreateApplicationAuthentication(
            string clientId,
            string clientSecret,
            NewAuthorization newAuthorization,
            string twoFactorAuthenticationCode)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, "clientId");
            Ensure.ArgumentNotNullOrEmptyString(clientSecret, "clientSecret");
            Ensure.ArgumentNotNull(newAuthorization, "authorization");
            Ensure.ArgumentNotNullOrEmptyString(twoFactorAuthenticationCode, "twoFactorAuthenticationCode");

            var requestData = new
            {
                client_secret = clientSecret,
                scopes = newAuthorization.Scopes,
                note = newAuthorization.Note,
                note_url = newAuthorization.NoteUrl
            };

            try
            {
                if (String.IsNullOrWhiteSpace(newAuthorization.Fingerprint))
                {
                    // use classic API
                    var endpoint = ApiUrls.AuthorizationsForClient(clientId);
                    return await ApiConnection.Put<ApplicationAuthorization>(
                        endpoint,
                        requestData,
                        twoFactorAuthenticationCode);
                }
                else
                {
                    // use new API
                    var endpoint = ApiUrls.AuthorizationsForClient(clientId, newAuthorization.Fingerprint);
                    return await ApiConnection.Put<ApplicationAuthorization>(
                        endpoint,
                        requestData,
                        twoFactorAuthenticationCode,
                        previewAcceptsHeader);
                }
            }
            catch (AuthorizationException e)
            {
                throw new TwoFactorChallengeFailedException(e);
            }
        }

        /// <summary>
        /// Checks the validity of an OAuth token without running afoul of normal rate limits for failed login attempts.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="https://developer.github.com/v3/oauth_authorizations/#check-an-authorization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="clientId">Client ID of the OAuth application for the token</param>
        /// <param name="accessToken">The OAuth token to check</param>
        /// <returns>The valid <see cref="ApplicationAuthorization"/>.</returns>
        public async Task<ApplicationAuthorization> CheckApplicationAuthentication(string clientId, string accessToken)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, "clientId");
            Ensure.ArgumentNotNullOrEmptyString(accessToken, "accessToken");

            var endpoint = ApiUrls.ApplicationAuthorization(clientId, accessToken);
            return await ApiConnection.Get<ApplicationAuthorization>(
                endpoint,
                null,
                previewAcceptsHeader);
        }

        /// <summary>
        /// Resets a valid OAuth token for an OAuth application without end user involvment.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="https://developer.github.com/v3/oauth_authorizations/#reset-an-authorization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="clientId">ClientID of the OAuth application for the token</param>
        /// <param name="accessToken">The OAuth token to reset</param>
        /// <returns>The valid <see cref="ApplicationAuthorization"/> with a new OAuth token</returns>
        public async Task<ApplicationAuthorization> ResetApplicationAuthentication(string clientId, string accessToken)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, "clientId");
            Ensure.ArgumentNotNullOrEmptyString(accessToken, "accessToken");

            var requestData = new { };

            return await ApiConnection.Post<ApplicationAuthorization>(
                ApiUrls.ApplicationAuthorization(clientId, accessToken), requestData);
        }

        /// <summary>
        /// Revokes a single OAuth token for an OAuth application.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="https://developer.github.com/v3/oauth_authorizations/#revoke-an-authorization-for-an-application">API documentation for more information.</a>
        /// </remarks>
        /// <param name="clientId">ClientID of the OAuth application for the token</param>
        /// <param name="accessToken">The OAuth token to revoke</param>
        /// <returns>A <see cref="Task"/> for the request's execution.</returns>
        public Task RevokeApplicationAuthentication(string clientId, string accessToken)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, "clientId");
            Ensure.ArgumentNotNullOrEmptyString(accessToken, "accessToken");

            return ApiConnection.Delete(
                ApiUrls.ApplicationAuthorization(clientId, accessToken));
        }

        /// <summary>
        /// Revokes every OAuth token for an OAuth application.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="https://developer.github.com/v3/oauth_authorizations/#revoke-all-authorizations-for-an-application">API documentation for more information.</a>
        /// </remarks>
        /// <param name="clientId">ClientID of the OAuth application for the token</param>
        /// <returns>A <see cref="Task"/> for the request's execution.</returns>
        public Task RevokeAllApplicationAuthentications(string clientId)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, "clientId");

            return ApiConnection.Delete(
                ApiUrls.ApplicationAuthorization(clientId));
        }

        /// <summary>
        /// Updates the specified <see cref="Authorization"/>.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://developer.github.com/v3/oauth/#update-an-existing-authorization">API 
        /// documentation</a> for more details.
        /// </remarks>
        /// <param name="id">ID of the <see cref="Authorization"/> to update</param>
        /// <param name="authorizationUpdate">Describes the changes to make to the authorization</param>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The updated <see cref="Authorization"/>.</returns>
        public Task<Authorization> Update(int id, AuthorizationUpdate authorizationUpdate)
        {
            Ensure.ArgumentNotNull(authorizationUpdate, "authorizationUpdate");

            if (String.IsNullOrWhiteSpace(authorizationUpdate.Fingerprint))
            {
                return ApiConnection.Patch<Authorization>(
                    ApiUrls.Authorizations(id),
                    authorizationUpdate);
            }
            else
            {
                return ApiConnection.Patch<Authorization>(
                    ApiUrls.Authorizations(id),
                    authorizationUpdate,
                    previewAcceptsHeader);
            }
        }

        /// <summary>
        /// Deletes the specified <see cref="Authorization"/>.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://developer.github.com/v3/oauth/#delete-an-authorization">API 
        /// documentation</a> for more details.
        /// </remarks>
        /// <param name="id">The system-wide ID of the authorization to delete</param>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="Task"/> for the request's execution.</returns>
        public Task Delete(int id)
        {
            return ApiConnection.Delete(ApiUrls.Authorizations(id));
        }
    }
}
