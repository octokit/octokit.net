using System;
using System.Reactive.Linq;
using Octokit.Reactive;

namespace Octokit
{
    public static class AuthorizationExtensions
    {
        public static IObservable<Authorization> GetOrCreateApplicationAuthentication(
            this IObservableAuthorizationsClient authorizationsClient,
            string clientId,
            string clientSecret,
            AuthorizationUpdate authorization,
            Func<TwoFactorRequiredException, IObservable<TwoFactorChallengeResult>> twoFactorChallengeHandler
            )
        {
            Ensure.ArgumentNotNull(authorizationsClient, "authorizationsClient");
            Ensure.ArgumentNotNullOrEmptyString(clientId, "clientId");
            Ensure.ArgumentNotNullOrEmptyString(clientSecret, "clientSecret");
            Ensure.ArgumentNotNull(authorization, "authorization");

            return authorizationsClient.GetOrCreateApplicationAuthentication(clientId, clientSecret, authorization)
                .Catch<Authorization, TwoFactorRequiredException>(exception => twoFactorChallengeHandler(exception)
                    .SelectMany(result =>
                        result.ResendCodeRequested
                            ? authorizationsClient.GetOrCreateApplicationAuthentication(
                                clientId,
                                clientSecret,
                                authorization,
                                twoFactorChallengeHandler)
                            : authorizationsClient.GetOrCreateApplicationAuthentication(clientId,
                                clientSecret,
                                authorization,
                                result.AuthenticationCode)));
        }
    }
}
