using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive.Clients
{
    public class ObservableAuthorizationsClient : IObservableAuthorizationsClient
    {
        readonly IAuthorizationsClient _client;

        public ObservableAuthorizationsClient(IAuthorizationsClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client;
        }

        public IObservable<IReadOnlyList<Authorization>> GetAll()
        {
            return _client.GetAll().ToObservable();
        }

        public IObservable<Authorization> Get(int id)
        {
            return _client.Get(id).ToObservable();
        }
        /// <summary>
        /// This method will create a new authorization for the specified OAuth application, only if an authorization 
        /// for that application doesn’t already exist for the user. It returns the user’s token for the application
        /// if one exists. Otherwise, it creates one.
        /// </summary>
        /// <param name="clientId">Client ID for the OAuth application that is requesting the token.</param>
        /// <param name="clientSecret">The client secret</param>
        /// <param name="authorization">Definse the scopes and metadata for the token</param>
        /// <exception cref="AuthorizationException">Thrown when the user does not have permission to make 
        /// this request. Check </exception>
        /// <returns></returns>
        public IObservable<Authorization> GetOrCreateApplicationAuthentication(
            string clientId,
            string clientSecret,
            AuthorizationUpdate authorization)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, "clientId");
            Ensure.ArgumentNotNullOrEmptyString(clientSecret, "clientSecret");
            Ensure.ArgumentNotNull(authorization, "authorization");

            return _client.GetOrCreateApplicationAuthentication(clientId, clientSecret, authorization)
                .ToObservable();
        }

        /// <summary>
        /// This method will create a new authorization for the specified OAuth application, only if an authorization 
        /// for that application doesn’t already exist for the user. It returns the user’s token for the application
        /// if one exists. Otherwise, it creates one.
        /// </summary>
        /// <param name="clientId">Client ID for the OAuth application that is requesting the token.</param>
        /// <param name="clientSecret">The client secret</param>
        /// <param name="authorization">Defines the scopes and metadata for the token</param>
        /// <param name="twoFactorAuthenticationCode"></param>
        /// <exception cref="AuthorizationException">Thrown when the user does not have permission to make 
        /// this request. Check </exception>
        /// <exception cref="TwoFactorChallengeFailedException">Thrown when the two-factor code is not
        /// valid.</exception>
        /// <returns></returns>

        public IObservable<Authorization> GetOrCreateApplicationAuthentication(
            string clientId,
            string clientSecret,
            AuthorizationUpdate authorization,
            string twoFactorAuthenticationCode)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, "clientId");
            Ensure.ArgumentNotNullOrEmptyString(clientSecret, "clientSecret");
            Ensure.ArgumentNotNull(authorization, "authorization");
            Ensure.ArgumentNotNullOrEmptyString(twoFactorAuthenticationCode, "twoFactorAuthenticationCode");

            return _client.GetOrCreateApplicationAuthentication(clientId, clientSecret, authorization, twoFactorAuthenticationCode)
                .ToObservable();
        }

        public IObservable<Authorization> Update(int id, AuthorizationUpdate authorization)
        {
            Ensure.ArgumentNotNull(authorization, "authorization");

            return _client.Update(id, authorization).ToObservable();
        }

        public IObservable<Authorization> Create(AuthorizationUpdate authorization)
        {
            Ensure.ArgumentNotNull(authorization, "authorization");

            return _client.Create(authorization).ToObservable();
        }

        public IObservable<Unit> Delete(int id)
        {
            return _client.Delete(id).ToObservable();
        }
    }
}
