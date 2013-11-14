using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;

namespace Octokit.Reactive
{
    public interface IObservableAuthorizationsClient
    {
        /// <summary>
        /// Get all <see cref="Authorization"/>s for the authenticated user. This method requires basic auth.
        /// </summary>
        /// <remarks>
        /// See <a href="http://developer.github.com/v3/oauth/#list-your-authorizations">API documentation</a> for more
        /// details.
        /// </remarks>
        /// <returns>An <see cref="Authorization"/></returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "It's an API call, so it's not a property.")]
        IObservable<Authorization> GetAll();

        /// <summary>
        /// Get a specific <see cref="Authorization"/> for the authenticated user. This method requires basic auth.
        /// </summary>
        /// <remarks>
        /// See <a href="http://developer.github.com/v3/oauth/#get-a-single-authorization">API documentation</a> for
        /// more details.
        /// </remarks>
        /// <param name="id">The id of the <see cref="Authorization"/></param>
        /// <returns>An <see cref="Authorization"/></returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "It's fiiiine. It's fine. Trust us.")]
        IObservable<Authorization> Get(int id);

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
        IObservable<Authorization> GetOrCreateApplicationAuthentication(
            string clientId,
            string clientSecret,
            NewAuthorization newAuthorization);

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
        IObservable<Authorization> GetOrCreateApplicationAuthentication(
            string clientId,
            string clientSecret,
            NewAuthorization newAuthorization,
            string twoFactorAuthenticationCode);

        /// <summary>
        /// Create a new <see cref="Authorization"/>.
        /// </summary>
        /// <param name="newAuthorization">Information about the new authorization to create</param>
        /// <returns></returns>
        IObservable<Authorization> Create(NewAuthorization newAuthorization);

        /// <summary>
        /// Update the <see cref="Authorization"/> specified by the id.
        /// </summary>
        /// <param name="id">The id of the <see cref="Authorization"/></param>
        /// <param name="authorizationUpdate">The changes to make to the authorization</param>
        /// <returns></returns>
        IObservable<Authorization> Update(int id, AuthorizationUpdate authorizationUpdate);

        /// <summary>
        /// Deletes an <see cref="Authorization"/>.
        /// </summary>
        /// <param name="id">The systemwide id of the authorization</param>
        /// <returns></returns>
        IObservable<Unit> Delete(int id);
    }
}
