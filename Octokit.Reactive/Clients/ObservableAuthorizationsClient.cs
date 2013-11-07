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
            Ensure.ArgumentNotNull(client, "client");

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
        /// <returns>An <see cref="Authorization"/></returns>
        public IObservable<Authorization> GetAll()
        {
            return _connection.GetAndFlattenAllPages<Authorization>(ApiUrls.Authorizations());
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
        public IObservable<Authorization> Get(int id)
        {
            return _client.Get(id).ToObservable();
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
        public IObservable<Authorization> GetOrCreateApplicationAuthentication(
            string clientId,
            string clientSecret,
            NewAuthorization newAuthorization)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, "clientId");
            Ensure.ArgumentNotNullOrEmptyString(clientSecret, "clientSecret");
            Ensure.ArgumentNotNull(newAuthorization, "authorization");

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
        /// <param name="clientId">Client ID for the OAuth application that is requesting the token</param>
        /// <param name="clientSecret">The client secret</param>
        /// <param name="newAuthorization">Defines the scopes and metadata for the token</param>
        /// <param name="twoFactorAuthenticationCode">The two-factor authentication code provided by the user</param>
        /// <exception cref="AuthorizationException">Thrown when the user does not have permission to make 
        /// this request. Check </exception>
        /// <exception cref="TwoFactorChallengeFailedException">Thrown when the two-factor code is not
        /// valid.</exception>
        /// <returns></returns>
        public IObservable<Authorization> GetOrCreateApplicationAuthentication(
            string clientId,
            string clientSecret,
            NewAuthorization newAuthorization,
            string twoFactorAuthenticationCode)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, "clientId");
            Ensure.ArgumentNotNullOrEmptyString(clientSecret, "clientSecret");
            Ensure.ArgumentNotNull(newAuthorization, "authorization");
            Ensure.ArgumentNotNullOrEmptyString(twoFactorAuthenticationCode, "twoFactorAuthenticationCode");

            return _client.GetOrCreateApplicationAuthentication(
                clientId,
                clientSecret,
                newAuthorization,
                twoFactorAuthenticationCode)
                .ToObservable();
        }

        /// <summary>
        /// Create a new <see cref="Authorization"/>.
        /// </summary>
        /// <param name="newAuthorization">Information about the new authorization to create</param>
        /// <returns></returns>
        public IObservable<Authorization> Create(NewAuthorization newAuthorization)
        {
            Ensure.ArgumentNotNull(newAuthorization, "authorization");

            return _client.Create(newAuthorization).ToObservable();
        }

        /// <summary>
        /// Update the <see cref="Authorization"/> specified by the id.
        /// </summary>
        /// <param name="id">The id of the <see cref="Authorization"/></param>
        /// <param name="authorizationUpdate">The changes to make to the authorization</param>
        /// <returns></returns>
        public IObservable<Authorization> Update(int id, AuthorizationUpdate authorizationUpdate)
        {
            Ensure.ArgumentNotNull(authorizationUpdate, "authorizationUpdate");

            return _client.Update(id, authorizationUpdate).ToObservable();
        }

        /// <summary>
        /// Deletes an <see cref="Authorization"/>.
        /// </summary>
        /// <param name="id">The systemwide id of the authorization</param>
        /// <returns></returns>
        public IObservable<Unit> Delete(int id)
        {
            return _client.Delete(id).ToObservable();
        }
    }
}
