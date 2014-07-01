using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's User Followers API
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/users/followers/">Followers API documentation</a> for more information.
    ///</remarks>
    public class FollowersClient : ApiClient, IFollowersClient
    {
        /// <summary>
        /// Initializes a new GitHub User Followers API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public FollowersClient(IApiConnection apiConnection) : base(apiConnection) 
        { 
        }

        /// <summary>
        /// List the authenticated user’s followers
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#list-followers-of-a-user">API documentation</a> for more information.
        /// </remarks>
        /// <returns>A <see cref="IReadOnlyList{User}"/> of <see cref="User"/>s that follow the authenticated user.</returns>
        public Task<IReadOnlyList<User>> GetAllForCurrent()
        {
            return ApiConnection.GetAll<User>(ApiUrls.Followers());
        }

        /// <summary>
        /// List a user’s followers
        /// </summary>
        /// <param name="login">The login name for the user</param>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#list-followers-of-a-user">API documentation</a> for more information.
        /// </remarks>
        /// <returns>A <see cref="IReadOnlyList{User}"/> of <see cref="User"/>s that follow the passed user.</returns>
        public Task<IReadOnlyList<User>> GetAll(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            return ApiConnection.GetAll<User>(ApiUrls.Followers(login));
        }

        /// <summary>
        /// List who the authenticated user is following
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#list-users-followed-by-another-user">API documentation</a> for more information.
        /// </remarks>
        /// <returns>A <see cref="IReadOnlyList{User}"/> of <see cref="User"/>s that the authenticated user follows.</returns>
        public Task<IReadOnlyList<User>> GetFollowingForCurrent()
        {
            return ApiConnection.GetAll<User>(ApiUrls.Following());
        }

        /// <summary>
        /// List who a user is following
        /// </summary>
        /// <param name="login">The login name of the user</param>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#list-users-followed-by-another-user">API documentation</a> for more information.
        /// </remarks>
        /// <returns>A <see cref="IReadOnlyList{User}"/> of <see cref="User"/>s that the passed user follows.</returns>
        public Task<IReadOnlyList<User>> GetFollowing(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            return ApiConnection.GetAll<User>(ApiUrls.Following(login));
        }

        /// <summary>
        /// Check if the authenticated user follows another user
        /// </summary>
        /// <param name="following">The login name of the other user</param>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#check-if-you-are-following-a-user">API documentation</a> for more information.
        /// </remarks>
        /// <returns>A <c>bool</c> representing the success of the operation.</returns>
        public async Task<bool> IsFollowingForCurrent(string following)
        {
            Ensure.ArgumentNotNullOrEmptyString(following, "following");

            try
            {
                var response = await Connection.Get<object>(ApiUrls.IsFollowing(following), null, null)
                                                .ConfigureAwait(false);
                if(response.StatusCode != HttpStatusCode.NotFound && response.StatusCode != HttpStatusCode.NoContent)
                {
                    throw new ApiException("Invalid Status Code returned. Expected a 204 or a 404", response.StatusCode);
                }
                return response.StatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Check if one user follows another user
        /// </summary>
        /// <param name="login">The login name of the user</param>
        /// <param name="following">The login name of the other user</param>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#check-if-one-user-follows-another">API documentation</a> for more information.
        /// </remarks>
        /// <returns>A <c>bool</c> representing the success of the operation.</returns>
        public async Task<bool> IsFollowing(string login, string following)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");
            Ensure.ArgumentNotNullOrEmptyString(following, "following");

            try
            {
                var response = await Connection.Get<object>(ApiUrls.IsFollowing(login, following), null, null)
                                                .ConfigureAwait(false);
                if (response.StatusCode != HttpStatusCode.NotFound && response.StatusCode != HttpStatusCode.NoContent)
                {
                    throw new ApiException("Invalid Status Code returned. Expected a 204 or a 404", response.StatusCode);
                }
                return response.StatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Follow a user
        /// </summary>
        /// <param name="login">The login name of the user to follow</param>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#follow-a-user">API documentation</a> for more information.
        /// </remarks>
        /// <returns>A <c>bool</c> representing the success of the operation.</returns>
        public async Task<bool> Follow(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            try
            {
                var requestData = new { };
                var response = await Connection.Put<object>(ApiUrls.IsFollowing(login), requestData)
                                                .ConfigureAwait(false);
                if (response.StatusCode != HttpStatusCode.NoContent)
                {
                    throw new ApiException("Invalid Status Code returned. Expected a 204", response.StatusCode);
                }
                return response.StatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Unfollow a user
        /// </summary>
        /// <param name="login">The login name of the user to unfollow</param>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#unfollow-a-user">API documentation</a> for more information.
        /// </remarks>
        /// <returns></returns>
        public Task Unfollow(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            return ApiConnection.Delete(ApiUrls.IsFollowing(login));
        }
    }
}
