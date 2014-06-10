using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableFollowersClient : IObservableFollowersClient
    {
        readonly IFollowersClient _client;
        readonly IConnection _connection;

        /// <summary>
        /// Initializes a new User Followers API client.
        /// </summary>
        /// <param name="client">An <see cref="IGitHubClient" /> used to make the requests</param>
        public ObservableFollowersClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.User.Followers;
            _connection = client.Connection;
        }

        /// <summary>
        /// List the authenticated user’s followers
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#list-followers-of-a-user">API documentation</a> for more information.
        /// </remarks>
        /// <returns>A <see cref="System.Collections.Generic.IReadOnlyList{User}"/> of <see cref="User"/>s that follow the authenticated user.</returns>
        public IObservable<User> GetAllForCurrent()
        {
            return _connection.GetAndFlattenAllPages<User>(ApiUrls.Followers());
        }

        /// <summary>
        /// List a user’s followers
        /// </summary>
        /// <param name="login">The login name for the user</param>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#list-followers-of-a-user">API documentation</a> for more information.
        /// </remarks>
        /// <returns>A <see cref="System.Collections.Generic.IReadOnlyList{User}"/> of <see cref="User"/>s that follow the passed user.</returns>
        public IObservable<User> GetAll(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            return _connection.GetAndFlattenAllPages<User>(ApiUrls.Followers(login));
        }

        /// <summary>
        /// List who the authenticated user is following
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#list-users-followed-by-another-user">API documentation</a> for more information.
        /// </remarks>
        /// <returns>A <see cref="System.Collections.Generic.IReadOnlyList{User}"/> of <see cref="User"/>s that the authenticated user follows.</returns>
        public IObservable<User> GetFollowingForCurrent()
        {
            return _connection.GetAndFlattenAllPages<User>(ApiUrls.Following());
        }

        /// <summary>
        /// List who a user is following
        /// </summary>
        /// <param name="login">The login name of the user</param>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#list-users-followed-by-another-user">API documentation</a> for more information.
        /// </remarks>
        /// <returns>A <see cref="System.Collections.Generic.IReadOnlyList{User}"/> of <see cref="User"/>s that the passed user follows.</returns>
        public IObservable<User> GetFollowing(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            return _connection.GetAndFlattenAllPages<User>(ApiUrls.Following(login));
        }

        /// <summary>
        /// Check if the authenticated user follows another user
        /// </summary>
        /// <param name="following">The login name of the other user</param>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#check-if-you-are-following-a-user">API documentation</a> for more information.
        /// </remarks>
        /// <returns>A <c>bool</c> representing the success of the operation.</returns>
        public IObservable<bool> IsFollowingForCurrent(string following)
        {
            Ensure.ArgumentNotNullOrEmptyString(following, "following");

            return _client.IsFollowingForCurrent(following).ToObservable();
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
        public IObservable<bool> IsFollowing(string login, string following)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");
            Ensure.ArgumentNotNullOrEmptyString(following, "following");

            return _client.IsFollowing(login, following).ToObservable();
        }

        /// <summary>
        /// Follow a user
        /// </summary>
        /// <param name="login">The login name of the user to follow</param>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#follow-a-user">API documentation</a> for more information.
        /// </remarks>
        /// <returns>A <c>bool</c> representing the success of the operation.</returns>
        public IObservable<bool> Follow(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            return _client.Follow(login).ToObservable();
        }

        /// <summary>
        /// Unfollow a user
        /// </summary>
        /// <param name="login">The login name of the user to unfollow</param>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#unfollow-a-user">API documentation</a> for more information.
        /// </remarks>
        /// <returns></returns>
        public IObservable<Unit> Unfollow(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            return _client.Unfollow(login).ToObservable();
        }
    }
}
