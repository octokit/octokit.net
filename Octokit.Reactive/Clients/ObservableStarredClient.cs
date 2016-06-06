using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableStarredClient : IObservableStarredClient
    {
        private readonly IStarredClient _client;
        private readonly IConnection _connection;

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

            return GetAllStargazers(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the stargazers for the passed repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <returns>A <see cref="IObservable{User}"/> of <see cref="User"/>s starring the passed repository</returns>
        public IObservable<User> GetAllStargazers(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<User>(ApiUrls.Stargazers(owner, name), options);
        }

        /// <summary>
        /// Retrieves all of the stargazers for the passed repository with star creation timestamps.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IObservable{UserStar}"/> of <see cref="User"/>s starring the passed repository with star creation timestamps.</returns>
        public IObservable<UserStar> GetAllStargazersWithTimestamps(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAllStargazersWithTimestamps(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the stargazers for the passed repository with star creation timestamps.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IObservable{UserStar}"/> of <see cref="User"/>s starring the passed repository with star creation timestamps.</returns>
        public IObservable<UserStar> GetAllStargazersWithTimestamps(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<UserStar>(ApiUrls.Stargazers(owner, name), null, AcceptHeaders.StarCreationTimestamps, options);
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
            return GetAllForCurrent(ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <returns>
        /// A <see cref="IObservable{Repository}"/> of <see cref="Repository"/>(ies) starred by the current user
        /// </returns>
        public IObservable<Repository> GetAllForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.Starred(), options);
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user with star creation timestamps.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IObservable{RepoStar}"/> of <see cref="Repository"/>(ies) starred by the current authenticated user with star creation timestamps.
        /// </returns>
        public IObservable<RepositoryStar> GetAllForCurrentWithTimestamps()
        {
            return GetAllForCurrentWithTimestamps(ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user with star creation timestamps.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IObservable{RepoStar}"/> of <see cref="Repository"/>(ies) starred by the current authenticated user with star creation timestamps.
        /// </returns>
        public IObservable<RepositoryStar> GetAllForCurrentWithTimestamps(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<RepositoryStar>(ApiUrls.Starred(), null, AcceptHeaders.StarCreationTimestamps, options);
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

            return GetAllForCurrent(request, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user
        /// </summary>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <returns>
        /// A <see cref="IObservable{Repository}"/> of <see cref="Repository"/>(ies) starred by the current user,
        /// sorted according to the passed request parameters
        /// </returns>
        public IObservable<Repository> GetAllForCurrent(StarredRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, "request");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.Starred(), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user with star creation timestamps.
        /// </summary>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IObservable{RepoStar}"/> of <see cref="Repository"/>(ies) starred by the current user,
        /// sorted according to the passed request parameters and with star creation timestamps.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public IObservable<RepositoryStar> GetAllForCurrentWithTimestamps(StarredRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            return GetAllForCurrentWithTimestamps(request, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user with star creation timestamps.
        /// </summary>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IObservable{RepoStar}"/> of <see cref="Repository"/>(ies) starred by the current user,
        /// sorted according to the passed request parameters and with star creation timestamps.
        /// </returns>
        public IObservable<RepositoryStar> GetAllForCurrentWithTimestamps(StarredRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, "request");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<RepositoryStar>(ApiUrls.Starred(), request.ToParametersDictionary(), AcceptHeaders.StarCreationTimestamps, options);
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

            return GetAllForUser(user, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <returns>A <see cref="IObservable{Repository}"/> starred by the specified user</returns>
        public IObservable<Repository> GetAllForUser(string user, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.StarredByUser(user), options);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user with star creation timestamps.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IObservable{RepoStar}"/>(ies) starred by the specified user with star creation timestamps.
        /// </returns>
        public IObservable<RepositoryStar> GetAllForUserWithTimestamps(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return GetAllForUserWithTimestamps(user, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user with star creation timestamps.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IObservable{RepoStar}"/>(ies) starred by the specified user with star creation timestamps.
        /// </returns>
        public IObservable<RepositoryStar> GetAllForUserWithTimestamps(string user, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<RepositoryStar>(ApiUrls.StarredByUser(user), null, AcceptHeaders.StarCreationTimestamps, options);
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

            return GetAllForUser(user, request, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated</exception>
        /// <returns>A <see cref="IObservable{Repository}"/> starred by the specified user</returns>
        public IObservable<Repository> GetAllForUser(string user, StarredRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");
            Ensure.ArgumentNotNull(request, "request");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.StarredByUser(user), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user with star creation timestamps.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IObservable{RepoStar}"/> of <see cref="Repository"/>(ies) starred by the specified user, 
        /// sorted according to the passed request parameters and with star creation timestamps.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public IObservable<RepositoryStar> GetAllForUserWithTimestamps(string user, StarredRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");
            Ensure.ArgumentNotNull(request, "request");

            return GetAllForUserWithTimestamps(user, request, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user with star creation timestamps.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IObservable{RepoStar}"/> of <see cref="Repository"/>(ies) starred by the specified user, 
        /// sorted according to the passed request parameters and with star creation timestamps.
        /// </returns>
        public IObservable<RepositoryStar> GetAllForUserWithTimestamps(string user, StarredRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");
            Ensure.ArgumentNotNull(request, "request");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<RepositoryStar>(ApiUrls.StarredByUser(user), request.ToParametersDictionary(), AcceptHeaders.StarCreationTimestamps, options);
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
