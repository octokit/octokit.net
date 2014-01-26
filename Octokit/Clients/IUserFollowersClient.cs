using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's User Followers API
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/users/followers/">Followers API documentation</a> for more information.
    ///</remarks>
    public interface IUserFollowersClient
    {
        /// <summary>
        /// List the authenticated user’s followers
        /// </summary>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<User>> GetAllForCurrent();

        /// <summary>
        /// List a user’s followers
        /// </summary>
        /// <param name="login">The login name for the user</param>
        /// <returns></returns>
        Task<IReadOnlyList<User>> GetAll(string login);

        /// <summary>
        /// List who the authenticated user is following
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyList<User>> GetFollowingForCurrent();

        /// <summary>
        /// List who a user is following
        /// </summary>
        /// <param name="login">The login name of the user</param>
        /// <returns></returns>
        Task<IReadOnlyList<User>> GetFollowing(string login);

        /// <summary>
        /// Check if the authenticated user follows another user
        /// </summary>
        /// <param name="following">The login name of the other user</param>
        /// <returns></returns>
        Task<bool> CheckFollowingForCurrent(string following);

        /// <summary>
        /// Check if one user follows another user
        /// </summary>
        /// <param name="login">The login name of the user</param>
        /// <param name="following">The login name of the other user</param>
        /// <returns></returns>
        Task<bool> CheckFollowing(string login, string following);

        /// <summary>
        /// Check if the authenticated user is followed by another user
        /// </summary>
        /// <param name="following">The login name of the other user</param>
        /// <returns></returns>
        Task<bool> CheckFollowerForCurrent(string following);

        /// <summary>
        /// Check if a user is followed by another user
        /// </summary>
        /// <param name="login">The login name of the user</param>
        /// <param name="following">The login name of the other user</param>
        /// <returns></returns>
        Task<bool> CheckFollower(string login, string following);

        /// <summary>
        /// Follow a user
        /// </summary>
        /// <param name="login">The login name of the user to follow</param>
        /// <returns></returns>
        Task<bool> Follow(string login);

        /// <summary>
        /// Unfollow a user
        /// </summary>
        /// <param name="login">The login name of the user to unfollow</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Unfollow")]
        Task<bool> Unfollow(string login);
    }
}
