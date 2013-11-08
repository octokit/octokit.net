#if NET_45
using System.Collections.Generic;
#endif
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's OAuth API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/oauth/">OAuth API documentation</a> for more details.
    /// </remarks>
    public interface IAuthorizationsClient
    {
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
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", 
            Justification = "It's an API call, so it's not a property.")]
        Task<IReadOnlyList<Authorization>> GetAll();
        
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
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "It's fiiiine. It's fine. Trust us.")]
        Task<Authorization> Get(int id);

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
        Task<Authorization> GetOrCreateApplicationAuthentication(
            string clientId,
            string clientSecret,
            NewAuthorization newAuthorization);
        
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
        Task<Authorization> GetOrCreateApplicationAuthentication(
            string clientId,
            string clientSecret,
            NewAuthorization newAuthorization,
            string twoFactorAuthenticationCode);
        
        /// <summary>
        /// Creates a new <see cref="Authorization"/>.
        /// </summary>
        /// <remarks>
        /// This method requires authentication.
        /// See the <a href="http://developer.github.com/v3/oauth/#create-a-new-authorization">API documentation</a> for more information.
        /// </remarks>
        /// <param name="newAuthorization">Describes the new authorization to create</param>
        /// <exception cref="AuthorizationException">
        /// Thrown when the current user does not have permission to make the request.
        /// </exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The created <see cref="Authorization"/>.</returns>
        Task<Authorization> Create(NewAuthorization newAuthorization);

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
        Task<Authorization> Update(int id, AuthorizationUpdate authorizationUpdate);

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
        Task Delete(int id);
    }
}
