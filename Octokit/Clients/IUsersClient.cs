using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Users API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/users/">Users API documentation</a> for more information.
    /// </remarks>
    public interface IUsersClient
    {
        /// <summary>
        /// A client for GitHub's User Emails API
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/emails/">Emails API documentation</a> for more information.
        ///</remarks>
        IUserEmailsClient Email { get; }

        /// <summary>
        /// Returns the user specified by the login.
        /// </summary>
        /// <param name="login">The login name for the user</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        [SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "login")]
        Task<User> Get(string login);

        /// <summary>
        /// Returns a <see cref="User"/> for the current authenticated user.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        Task<User> Current();

        /// <summary>
        /// Update the specified <see cref="UserUpdate"/>.
        /// </summary>
        /// <param name="user">The login for the user</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        Task<User> Update(UserUpdate user);

        /// <summary>
        /// A client for GitHub's User Followers API
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/">Followers API documentation</a> for more information.
        ///</remarks>
        IFollowersClient Followers { get; }
    }
}
