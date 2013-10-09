using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;

namespace Octokit.Reactive
{
    public interface IObservableAuthorizationsClient
    {
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", 
            Justification = "It's an API call, so it's not a property.")]
        IObservable<IReadOnlyList<Authorization>> GetAll();
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "It's fiiiine. It's fine. Trust us.")]
        IObservable<Authorization> Get(int id);

        /// <summary>
        /// This method will create a new authorization for the specified OAuth application, only if an authorization 
        /// for that application doesn’t already exist for the user. It returns the user’s token for the application
        /// if one exists. Otherwise, it creates one.
        /// </summary>
        /// <param name="clientId">Client ID for the OAuth application that is requesting the token.</param>
        /// <param name="clientSecret">The client secret</param>
        /// <param name="authorization">Defines the scopes and metadata for the token</param>
        /// <exception cref="AuthorizationException">Thrown when the user does not have permission to make 
        /// this request. Check </exception>
        /// <exception cref="TwoFactorRequiredException">Thrown when the current account has two-factor
        /// authentication enabled.</exception>
        /// <returns></returns>
        IObservable<Authorization> GetOrCreateApplicationAuthentication(
            string clientId,
            string clientSecret,
            AuthorizationUpdate authorization);

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
        IObservable<Authorization> GetOrCreateApplicationAuthentication(
            string clientId,
            string clientSecret,
            AuthorizationUpdate authorization,
            string twoFactorAuthenticationCode);
        IObservable<Authorization> Update(int id, AuthorizationUpdate authorization);
        IObservable<Authorization> Create(AuthorizationUpdate authorization);
        IObservable<Unit> Delete(int id);
    }
}
