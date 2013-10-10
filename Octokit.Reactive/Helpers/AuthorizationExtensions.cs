using System;
using System.Reactive.Linq;
using Octokit.Reactive;

namespace Octokit
{
    public static class AuthorizationExtensions
    {
        /// <summary>
        /// This method will create a new authorization for the specified OAuth application, only if an authorization 
        /// for that application doesn’t already exist for the user. It returns the user’s token for the application
        /// if one exists. Otherwise, it creates a new one.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method allows the caller to provide a callback which is used to retrieve the two-factor code from
        /// the user. Typically the callback is used to show some user interface to the user.
        /// </para>
        /// <para>
        /// See <a href="http://developer.github.com/v3/oauth/#list-your-authorizations">API documentation</a> 
        /// for more details.
        /// </para>
        /// </remarks>
        /// <param name="authorizationsClient">The <see cref="IAuthorizationsClient" /> this method extends</param>
        /// <param name="clientId">Client ID for the OAuth application that is requesting the token</param>
        /// <param name="clientSecret">The client secret</param>
        /// <param name="newAuthorization">Defines the scopes and metadata for the token</param>
        /// <param name="twoFactorChallengeHandler">Callback used to retrieve the two-factor authentication code
        /// from the user</param>
        /// <returns></returns>
        public static IObservable<Authorization> GetOrCreateApplicationAuthentication(
            this IObservableAuthorizationsClient authorizationsClient,
            string clientId,
            string clientSecret,
            NewAuthorization newAuthorization,
            Func<TwoFactorRequiredException, IObservable<TwoFactorChallengeResult>> twoFactorChallengeHandler
            )
        {
            Ensure.ArgumentNotNull(authorizationsClient, "authorizationsClient");
            Ensure.ArgumentNotNullOrEmptyString(clientId, "clientId");
            Ensure.ArgumentNotNullOrEmptyString(clientSecret, "clientSecret");
            Ensure.ArgumentNotNull(newAuthorization, "authorization");

            return authorizationsClient.GetOrCreateApplicationAuthentication(clientId, clientSecret, newAuthorization)
                .Catch<Authorization, TwoFactorRequiredException>(exception => twoFactorChallengeHandler(exception)
                    .SelectMany(result =>
                        result.ResendCodeRequested
                            ? authorizationsClient.GetOrCreateApplicationAuthentication(
                                clientId,
                                clientSecret,
                                newAuthorization,
                                twoFactorChallengeHandler)
                            : authorizationsClient.GetOrCreateApplicationAuthentication(clientId,
                                clientSecret,
                                newAuthorization,
                                result.AuthenticationCode)));
        }
    }
}
