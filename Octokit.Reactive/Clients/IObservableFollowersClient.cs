using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;
using System.Threading;

namespace Octokit.Reactive
{
    public interface IObservableFollowersClient
    {
        /// <summary>
        /// List the authenticated user’s followers
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#list-followers-of-a-user">API documentation</a> for more information.
        /// </remarks>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>A <see cref="IObservable{User}"/> of <see cref="User"/>s that follow the authenticated user.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<User> GetAllForCurrent(CancellationToken cancellationToken = default);

        /// <summary>
        /// List the authenticated user’s followers
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#list-followers-of-a-user">API documentation</a> for more information.
        /// </remarks>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>A <see cref="IObservable{User}"/> of <see cref="User"/>s that follow the authenticated user.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<User> GetAllForCurrent(ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// List a user’s followers
        /// </summary>
        /// <param name="login">The login name for the user</param>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#list-followers-of-a-user">API documentation</a> for more information.
        /// </remarks>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>A <see cref="IObservable{User}"/> of <see cref="User"/>s that follow the passed user.</returns>
        IObservable<User> GetAll(string login, CancellationToken cancellationToken = default);

        /// <summary>
        /// List a user’s followers
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="login">The login name for the user</param>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#list-followers-of-a-user">API documentation</a> for more information.
        /// </remarks>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>A <see cref="IObservable{User}"/> of <see cref="User"/>s that follow the passed user.</returns>
        IObservable<User> GetAll(string login, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// List who the authenticated user is following
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#list-users-followed-by-another-user">API documentation</a> for more information.
        /// </remarks>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>A <see cref="IObservable{User}"/> of <see cref="User"/>s that the authenticated user follows.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<User> GetAllFollowingForCurrent(CancellationToken cancellationToken = default);

        /// <summary>
        /// List who the authenticated user is following
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#list-users-followed-by-another-user">API documentation</a> for more information.
        /// </remarks>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>A <see cref="IObservable{User}"/> of <see cref="User"/>s that the authenticated user follows.</returns>
        IObservable<User> GetAllFollowingForCurrent(ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// List who a user is following
        /// </summary>
        /// <param name="login">The login name of the user</param>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#list-users-followed-by-another-user">API documentation</a> for more information.
        /// </remarks>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>A <see cref="IObservable{User}"/> of <see cref="User"/>s that the passed user follows.</returns>
        IObservable<User> GetAllFollowing(string login, CancellationToken cancellationToken = default);

        /// <summary>
        /// List who a user is following
        /// </summary>
        /// <param name="login">The login name of the user</param>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#list-users-followed-by-another-user">API documentation</a> for more information.
        /// </remarks>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>A <see cref="IObservable{User}"/> of <see cref="User"/>s that the passed user follows.</returns>
        IObservable<User> GetAllFollowing(string login, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Check if the authenticated user follows another user
        /// </summary>
        /// <param name="following">The login name of the other user</param>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#check-if-you-are-following-a-user">API documentation</a> for more information.
        /// </remarks>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>A <c>bool</c> representing the success of the operation.</returns>
        IObservable<bool> IsFollowingForCurrent(string following, CancellationToken cancellationToken = default);

        /// <summary>
        /// Check if one user follows another user
        /// </summary>
        /// <param name="login">The login name of the user</param>
        /// <param name="following">The login name of the other user</param>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#check-if-one-user-follows-another">API documentation</a> for more information.
        /// </remarks>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>A <c>bool</c> representing the success of the operation.</returns>
        IObservable<bool> IsFollowing(string login, string following, CancellationToken cancellationToken = default);

        /// <summary>
        /// Follow a user
        /// </summary>
        /// <param name="login">The login name of the user to follow</param>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#follow-a-user">API documentation</a> for more information.
        /// </remarks>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>A <c>bool</c> representing the success of the operation.</returns>
        IObservable<bool> Follow(string login, CancellationToken cancellationToken = default);

        /// <summary>
        /// Unfollow a user
        /// </summary>
        /// <param name="login">The login name of the user to unfollow</param>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/users/followers/#unfollow-a-user">API documentation</a> for more information.
        /// </remarks>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Unfollow",
            Justification = "Unfollow is consistent with the GitHub website")]
        IObservable<Unit> Unfollow(string login, CancellationToken cancellationToken = default);
    }
}
