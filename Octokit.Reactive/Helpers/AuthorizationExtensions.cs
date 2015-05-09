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
        public static IObservable<ApplicationAuthorization> GetOrCreateApplicationAuthentication(
            this IObservableAuthorizationsClient authorizationsClient,
            string clientId,
            string clientSecret,
            NewAuthorization newAuthorization,
            Func<TwoFactorRequiredException, IObservable<TwoFactorChallengeResult>> twoFactorChallengeHandler)
        {
            Ensure.ArgumentNotNull(authorizationsClient, "authorizationsClient");
            Ensure.ArgumentNotNullOrEmptyString(clientId, "clientId");
            Ensure.ArgumentNotNullOrEmptyString(clientSecret, "clientSecret");
            Ensure.ArgumentNotNull(newAuthorization, "authorization");

            return authorizationsClient.GetOrCreateApplicationAuthentication(clientId, clientSecret, newAuthorization)
                .Catch<ApplicationAuthorization, TwoFactorRequiredException>(exception => twoFactorChallengeHandler(exception)
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

        /// <summary>
        /// This method will create a new authorization for the specified OAuth application. If an authorization 
        /// for that application already exists for the user and fingerprint, it'll delete the existing one and 
        /// recreate it.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is typically used to initiate an application authentication flow.
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
        /// <param name="retryInvalidTwoFactorCode">If true, instead of completing when the two factor code supplied
        /// is invalid, we go through the whole cycle again and prompt the two factor dialog.</param>
        /// <returns></returns>
        public static IObservable<ApplicationAuthorization> CreateAndDeleteExistingApplicationAuthorization(
            this IObservableAuthorizationsClient authorizationsClient,
            string clientId,
            string clientSecret,
            NewAuthorization newAuthorization,
            Func<TwoFactorAuthorizationException, IObservable<TwoFactorChallengeResult>> twoFactorChallengeHandler,
            bool retryInvalidTwoFactorCode)
        {
            return authorizationsClient.CreateAndDeleteExistingApplicationAuthorization(
                clientId,
                clientSecret,
                newAuthorization,
                twoFactorChallengeHandler,
                null,
                retryInvalidTwoFactorCode);
        }

        public static IObservable<ApplicationAuthorization> CreateAndDeleteExistingApplicationAuthorization(
            this IObservableAuthorizationsClient authorizationsClient,
            string clientId,
            string clientSecret,
            NewAuthorization newAuthorization,
            Func<TwoFactorAuthorizationException, IObservable<TwoFactorChallengeResult>> twoFactorChallengeHandler,
            string twoFactorAuthenticationCode,
            bool retryInvalidTwoFactorCode)
        {
            Ensure.ArgumentNotNull(authorizationsClient, "authorizationsClient");
            Ensure.ArgumentNotNullOrEmptyString(clientId, "clientId");
            Ensure.ArgumentNotNullOrEmptyString(clientSecret, "clientSecret");
            Ensure.ArgumentNotNull(newAuthorization, "newAuthorization");

            // If retryInvalidTwoFactorCode is false, then we only show the TwoFactorDialog when we catch 
            // a TwoFactorRequiredException. If it's true, we show it for TwoFactorRequiredException and 
            // TwoFactorChallengeFailedException
            Func<TwoFactorAuthorizationException, IObservable<TwoFactorChallengeResult>> twoFactorHandler = ex =>
                retryInvalidTwoFactorCode || ex is TwoFactorRequiredException
                    ? twoFactorChallengeHandler(ex)
                    : Observable.Throw<TwoFactorChallengeResult>(ex);

            return authorizationsClient.CreateAuthorizationAndDeleteExisting(
                clientId,
                clientSecret,
                newAuthorization,
                twoFactorAuthenticationCode)
                .Catch<ApplicationAuthorization, TwoFactorAuthorizationException>(
                    exception => twoFactorHandler(exception)
                    .SelectMany(result =>
                        result.ResendCodeRequested
                            ? authorizationsClient.CreateAndDeleteExistingApplicationAuthorization(
                                clientId,
                                clientSecret,
                                newAuthorization,
                                twoFactorHandler,
                                null, // twoFactorAuthenticationCode
                                retryInvalidTwoFactorCode)
                            : authorizationsClient.CreateAndDeleteExistingApplicationAuthorization(
                                    clientId,
                                    clientSecret,
                                    newAuthorization,
                                    twoFactorHandler,
                                    result.AuthenticationCode,
                                    retryInvalidTwoFactorCode)));
        }

        // If the Application Authorization already exists, the result might have an empty string as the token. This is
        // because GitHub.com no longer stores the token, but stores a hashed version of it. It is assumed that clients
        // will store the token locally.
        // The only reason to be calling GetOrCreateApplicationAuthentication is pretty much the case when you are
        // logging in and thus don't have the token already. So if the token returned is an empty string, we'll go
        // ahead and delete it for you and then recreate it.
        static IObservable<ApplicationAuthorization> CreateAuthorizationAndDeleteExisting(
            this IObservableAuthorizationsClient authorizationsClient,
            string clientId,
            string clientSecret,
            NewAuthorization newAuthorization,
            string twoFactorAuthenticationCode = null)
        {
            return authorizationsClient.GetOrCreateAuthorizationUnified(
                clientId,
                clientSecret,
                newAuthorization,
                twoFactorAuthenticationCode)
                .SelectMany(authorization => string.IsNullOrEmpty(authorization.Token)
                    ? authorizationsClient.Delete(authorization.Id, twoFactorAuthenticationCode)
                        .SelectMany(_ =>
                            authorizationsClient.CreateNewAuthorization(
                                clientId,
                                clientSecret,
                                newAuthorization,
                                twoFactorAuthenticationCode))
                    : Observable.Return(authorization));
        }

        static IObservable<ApplicationAuthorization> GetOrCreateAuthorizationUnified(
            this IObservableAuthorizationsClient authorizationsClient,
            string clientId,
            string clientSecret,
            NewAuthorization newAuthorization,
            string twoFactorAuthenticationCode = null)
        {
            return string.IsNullOrEmpty(twoFactorAuthenticationCode)
                ? authorizationsClient.GetOrCreateApplicationAuthentication(clientId, clientSecret, newAuthorization)
                : authorizationsClient.GetOrCreateApplicationAuthentication(clientId, clientSecret, newAuthorization, twoFactorAuthenticationCode);
        }

        static IObservable<ApplicationAuthorization> CreateNewAuthorization(
            this IObservableAuthorizationsClient authorizationsClient,
            string clientId,
            string clientSecret,
            NewAuthorization newAuthorization,
            string twoFactorAuthenticationCode = null)
        {
            return string.IsNullOrEmpty(twoFactorAuthenticationCode)
                ? authorizationsClient.Create(clientId, clientSecret, newAuthorization)
                : authorizationsClient.Create(clientId, clientSecret, newAuthorization, twoFactorAuthenticationCode);
        }
    }
}
