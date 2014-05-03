using System;
using System.Reactive.Threading.Tasks;

using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableStarredClient : IObservableStarredClient
    {
        private IStarredClient _client;
        private IConnection _connection;

        public ObservableStarredClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Activity.Starring;
            _connection = client.Connection;
        }

        /// <summary>
        /// Retrieves all of the stargazers for the passed repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <returns>A <see cref="IObservable{User}"/> of <see cref="User"/>s starring the passed repository</returns>
        public IObservable<User> GetAllStargazers(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _connection.GetAndFlattenAllPages<User>(ApiUrls.Stargazers(owner, name));
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <returns>
        /// A <see cref="IObservable{Repository}"/> of <see cref="Repository"/>(ies) starred by the current user
        /// </returns>
        public IObservable<Repository> GetAllForCurrent()
        {
            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.Starred());
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user
        /// </summary>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <returns>
        /// A <see cref="IObservable{Repository}"/> of <see cref="Repository"/>(ies) starred by the current user,
        /// sorted according to the passed request parameters
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public IObservable<Repository> GetAllForCurrent(StarredRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.Starred(), request.ToParametersDictionary());
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <returns>A <see cref="IObservable{Repository}"/> starred by the specified user</returns>
        public IObservable<Repository> GetAllForUser(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.StarredByUser(user));
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <returns>A <see cref="IObservable{Repository}"/> starred by the specified user</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public IObservable<Repository> GetAllForUser(string user, StarredRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");
            Ensure.ArgumentNotNull(request, "request");

            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.StarredByUser(user), request.ToParametersDictionary());
        }

        /// <summary>
        /// Check if a repository is starred by the current authenticated user
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <c>bool</c> representing the success of the operation</returns>
        public IObservable<bool> CheckStarred(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.CheckStarred(owner, name).ToObservable();
        }

        /// <summary>
        /// Stars a repository for the authenticated user
        /// </summary>
        /// <param name="owner">The owner of the repository to star</param>
        /// <param name="name">The name of the repository to star</param>
        /// <returns>A <c>bool</c> representing the success of starring</returns>
        public IObservable<bool> StarRepo(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.StarRepo(owner, name).ToObservable();
        }

        /// <summary>
        /// Unstars a repository for the authenticated user
        /// </summary>
        /// <param name="owner">The owner of the repository to unstar</param>
        /// <param name="name">The name of the repository to unstar</param>
        /// <returns>A <c>bool</c> representing the success of the operation</returns>
        public IObservable<bool> RemoveStarFromRepo(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.RemoveStarFromRepo(owner, name).ToObservable();
        }
    }
}
