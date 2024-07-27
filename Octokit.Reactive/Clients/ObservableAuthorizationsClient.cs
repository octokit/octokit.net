using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableAuthorizationsClient : IObservableAuthorizationsClient
    {
        readonly IAuthorizationsClient _client;
        readonly IConnection _connection;

        public ObservableAuthorizationsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Authorization;
            _connection = client.Connection;
        }

        /// <summary>
        /// Get all <see cref="Authorization"/>s for the authenticated user. This method requires basic auth.
        /// </summary>
        /// <remarks>
        /// See <a href="http://developer.github.com/v3/oauth/#list-your-authorizations">API documentation</a> for more
        /// details.
        /// </remarks>
        /// <returns>A list of <see cref="Authorization"/>s for the authenticated user.</returns>
        public IObservable<Authorization> GetAll()
        {
            return GetAll(ApiOptions.None);
        }

        /// <summary>
        /// Get all <see cref="Authorization"/>s for the authenticated user. This method requires basic auth.
        /// </summary>
        /// <remarks>
        /// See <a href="http://developer.github.com/v3/oauth/#list-your-authorizations">API documentation</a> for more
        /// details.
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>A list of <see cref="Authorization"/>s for the authenticated user.</returns>
        public IObservable<Authorization> GetAll(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Authorization>(ApiUrls.Authorizations(), options);
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
        public IObservable<Authorization> Get(long id)
        {
            return _client.Get(id).ToObservable();
        }

        /// <summary>
        /// Creates a new personal token for the authenticated user.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="https://developer.github.com/v3/oauth_authorizations/#create-a-new-authorization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="newAuthorization">Describes the new authorization to create</param>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make this request.
        /// </exception>
        /// <exception cref="TwoFactorRequiredException">
        /// Thrown when the current account has two-factor authentication enabled and an authentication code is required.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The created <see cref="Authorization"/>.</returns>
        public IObservable<ApplicationAuthorization> Create(NewAuthorization newAuthorization)
        {
            Ensure.ArgumentNotNull(newAuthorization, nameof(newAuthorization));

            return _client.Create(newAuthorization).ToObservable();
        }

        /// <summary>
        /// Creates a new personal token for the authenticated user.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="https://developer.github.com/v3/oauth_authorizations/#create-a-new-authorization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="twoFactorAuthenticationCode">The two-factor authentication code in response to the current user's previous challenge</param>
        /// <param name="newAuthorization">Describes the new authorization to create</param>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make this request.
        /// </exception>
        /// <exception cref="TwoFactorRequiredException">
        /// Thrown when the current account has two-factor authentication enabled and an authentication code is required.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The created <see cref="Authorization"/>.</returns>
        public IObservable<ApplicationAuthorization> Create(
            NewAuthorization newAuthorization,
            string twoFactorAuthenticationCode)
        {
            Ensure.ArgumentNotNull(newAuthorization, nameof(newAuthorization));
            Ensure.ArgumentNotNullOrEmptyString(twoFactorAuthenticationCode, nameof(twoFactorAuthenticationCode));

            return _client.Create(newAuthorization, twoFactorAuthenticationCode).ToObservable();
        }

        /// <summary>
        /// Creates a new authorization for the specified OAuth application if an authorization for that application
        /// doesn’t already exist for the user; otherwise, it fails.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://developer.github.com/v3/oauth/#get-or-create-an-authorization-for-a-specific-app">API documentation</a> for more information.
        /// </remarks>
        /// <param name="clientId">Client Id of the OAuth application for the token</param>
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
        public IObservable<ApplicationAuthorization> Create(
            string clientId,
            string clientSecret,
            NewAuthorization newAuthorization)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, nameof(clientId));
            Ensure.ArgumentNotNullOrEmptyString(clientSecret, nameof(clientSecret));
            Ensure.ArgumentNotNull(newAuthorization, nameof(newAuthorization));

            return _client.Create(clientId, clientSecret, newAuthorization).ToObservable();
        }

        /// <summary>
        /// Creates a new authorization for the specified OAuth application if an authorization for that application
        /// doesn’t already exist for the user; otherwise, it fails.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://developer.github.com/v3/oauth/#get-or-create-an-authorization-for-a-specific-app">API documentation</a> for more information.
        /// </remarks>
        /// <param name="clientId">Client Id of the OAuth application for the token</param>
        /// <param name="clientSecret">The client secret</param>
        /// <param name="twoFactorAuthenticationCode">The two-factor authentication code in response to the current user's previous challenge</param>
        /// <param name="newAuthorization">Describes the new authorization to create</param>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make this request.
        /// </exception>
        /// <exception cref="TwoFactorRequiredException">
        /// Thrown when the current account has two-factor authentication enabled and an authentication code is required.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The created <see cref="Authorization"/>.</returns>
        public IObservable<ApplicationAuthorization> Create(
            string clientId,
            string clientSecret,
            NewAuthorization newAuthorization,
            string twoFactorAuthenticationCode)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, nameof(clientId));
            Ensure.ArgumentNotNullOrEmptyString(clientSecret, nameof(clientSecret));
            Ensure.ArgumentNotNull(newAuthorization, nameof(newAuthorization));
            Ensure.ArgumentNotNullOrEmptyString(twoFactorAuthenticationCode, nameof(twoFactorAuthenticationCode));

            return _client.Create(clientId, clientSecret, newAuthorization, twoFactorAuthenticationCode).ToObservable();
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
        /// <param name="clientId">Client Id for the OAuth application that is requesting the token</param>
        /// <param name="clientSecret">The client secret</param>
        /// <param name="newAuthorization">Defines the scopes and metadata for the token</param>
        /// <exception cref="AuthorizationException">Thrown when the user does not have permission to make
        /// this request. Check </exception>
        /// <exception cref="TwoFactorRequiredException">Thrown when the current account has two-factor
        /// authentication enabled.</exception>
        /// <returns></returns>
        public IObservable<ApplicationAuthorization> GetOrCreateApplicationAuthentication(
            string clientId,
            string clientSecret,
            NewAuthorization newAuthorization)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, nameof(clientId));
            Ensure.ArgumentNotNullOrEmptyString(clientSecret, nameof(clientSecret));
            Ensure.ArgumentNotNull(newAuthorization, nameof(newAuthorization));

            return _client.GetOrCreateApplicationAuthentication(clientId, clientSecret, newAuthorization)
                .ToObservable();
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
        /// <param name="clientId">Client Id for the OAuth application that is requesting the token</param>
        /// <param name="clientSecret">The client secret</param>
        /// <param name="newAuthorization">Defines the scopes and metadata for the token</param>
        /// <param name="twoFactorAuthenticationCode">The two-factor authentication code provided by the user</param>
        /// <exception cref="AuthorizationException">Thrown when the user does not have permission to make
        /// this request. Check </exception>
        /// <exception cref="TwoFactorChallengeFailedException">Thrown when the two-factor code is not
        /// valid.</exception>
        /// <returns></returns>
        public IObservable<ApplicationAuthorization> GetOrCreateApplicationAuthentication(
            string clientId,
            string clientSecret,
            NewAuthorization newAuthorization,
            string twoFactorAuthenticationCode)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, nameof(clientId));
            Ensure.ArgumentNotNullOrEmptyString(clientSecret, nameof(clientSecret));
            Ensure.ArgumentNotNull(newAuthorization, nameof(newAuthorization));
            Ensure.ArgumentNotNullOrEmptyString(twoFactorAuthenticationCode, nameof(twoFactorAuthenticationCode));

            return _client.GetOrCreateApplicationAuthentication(
                clientId,
                clientSecret,
                newAuthorization,
                twoFactorAuthenticationCode)
                .ToObservable();
        }


        /// <summary>
        /// Checks the validity of an OAuth token without running afoul of normal rate limits for failed login attempts.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="https://developer.github.com/v3/oauth_authorizations/#check-an-authorization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="clientId">Client Id of the OAuth application for the token</param>
        /// <param name="accessToken">The OAuth token to check</param>
        /// <returns>The valid <see cref="ApplicationAuthorization"/>.</returns>
        public IObservable<ApplicationAuthorization> CheckApplicationAuthentication(string clientId, string accessToken)
        {
            Ensure.ArgumentNotNullOrEmptyString("clientId", clientId);
            Ensure.ArgumentNotNullOrEmptyString("accessToken", accessToken);

            return _client.CheckApplicationAuthentication(clientId, accessToken)
                .ToObservable();
        }

        /// <summary>
        /// Resets a valid OAuth token for an OAuth application without end user involvement.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="https://developer.github.com/v3/oauth_authorizations/#reset-an-authorization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="clientId">ClientID of the OAuth application for the token</param>
        /// <param name="accessToken">The OAuth token to reset</param>
        /// <returns>The valid <see cref="ApplicationAuthorization"/> with a new OAuth token</returns>
        public IObservable<ApplicationAuthorization> ResetApplicationAuthentication(string clientId, string accessToken)
        {
            Ensure.ArgumentNotNullOrEmptyString("clientId", clientId);
            Ensure.ArgumentNotNullOrEmptyString("accessToken", accessToken);

            return _client.ResetApplicationAuthentication(clientId, accessToken)
                .ToObservable();
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
        /// <returns></returns>
        public IObservable<Unit> RevokeApplicationAuthentication(string clientId, string accessToken)
        {
            Ensure.ArgumentNotNullOrEmptyString("clientId", clientId);
            Ensure.ArgumentNotNullOrEmptyString("accessToken", accessToken);

            return _client.RevokeApplicationAuthentication(clientId, accessToken)
                .ToObservable();
        }

        /// <summary>
        /// Update the <see cref="Authorization"/> specified by the id.
        /// </summary>
        /// <param name="id">The id of the <see cref="Authorization"/></param>
        /// <param name="authorizationUpdate">The changes to make to the authorization</param>
        /// <returns></returns>
        public IObservable<Authorization> Update(long id, AuthorizationUpdate authorizationUpdate)
        {
            Ensure.ArgumentNotNull(authorizationUpdate, nameof(authorizationUpdate));

            return _client.Update(id, authorizationUpdate).ToObservable();
        }

        /// <summary>
        /// Deletes the specified <see cref="Authorization"/>.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://developer.github.com/v3/oauth/#delete-an-authorization">API
        /// documentation</a> for more details.
        /// </remarks>
        /// <param name="id">The system-wide Id of the authorization to delete</param>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<Unit> Delete(long id)
        {
            return _client.Delete(id).ToObservable();
        }

        /// <summary>
        /// Deletes the specified <see cref="Authorization"/>.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://developer.github.com/v3/oauth/#delete-an-authorization">API
        /// documentation</a> for more details.
        /// </remarks>
        /// <param name="id">The system-wide Id of the authorization to delete</param>
        /// <param name="twoFactorAuthenticationCode">Two factor authorization code</param>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<Unit> Delete(long id, string twoFactorAuthenticationCode)
        {
            return _client.Delete(id, twoFactorAuthenticationCode).ToObservable();
        }
    }
}
