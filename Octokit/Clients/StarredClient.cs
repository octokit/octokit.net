using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Activity Starring API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/activity/starring/">Activity Starring API documentation</a> for more information.
    /// </remarks>
    public class StarredClient : ApiClient, IStarredClient
    {
        /// <summary>
        /// Instantiates a new GitHub Activity Starring API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public StarredClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Retrieves all of the stargazers for the passed repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repos/{owner}/{repo}/stargazers")]
        public Task<IReadOnlyList<User>> GetAllStargazers(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllStargazers(owner, name, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the stargazers for the passed repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repositories/{id}/stargazers")]
        public Task<IReadOnlyList<User>> GetAllStargazers(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAllStargazers(repositoryId, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the stargazers for the passed repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repos/{owner}/{repo}/stargazers")]
        public Task<IReadOnlyList<User>> GetAllStargazers(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<User>(ApiUrls.Stargazers(owner, name), options, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the stargazers for the passed repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repositories/{id}/stargazers")]
        public Task<IReadOnlyList<User>> GetAllStargazers(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<User>(ApiUrls.Stargazers(repositoryId), options, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the stargazers for the passed repository with star creation timestamps.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repos/{owner}/{repo}/stargazers")]
        public Task<IReadOnlyList<UserStar>> GetAllStargazersWithTimestamps(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllStargazersWithTimestamps(owner, name, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the stargazers for the passed repository with star creation timestamps.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repositories/{id}/stargazers")]
        public Task<IReadOnlyList<UserStar>> GetAllStargazersWithTimestamps(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAllStargazersWithTimestamps(repositoryId, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the stargazers for the passed repository with star creation timestamps.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repos/{owner}/{repo}/stargazers")]
        public Task<IReadOnlyList<UserStar>> GetAllStargazersWithTimestamps(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<UserStar>(ApiUrls.Stargazers(owner, name), null, AcceptHeaders.StarJson, options, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the stargazers for the passed repository with star creation timestamps.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/repositories/{id}/stargazers")]
        public Task<IReadOnlyList<UserStar>> GetAllStargazersWithTimestamps(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<UserStar>(ApiUrls.Stargazers(repositoryId), null, AcceptHeaders.StarJson, options, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user.
        /// </summary>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/user/starred")]
        public Task<IReadOnlyList<Repository>> GetAllForCurrent(CancellationToken cancellationToken = default)
        {
            return GetAllForCurrent(ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/user/starred")]
        public Task<IReadOnlyList<Repository>> GetAllForCurrent(ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Repository>(ApiUrls.Starred(), options, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user with star creation timestamps.
        /// </summary>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/user/starred")]
        public Task<IReadOnlyList<RepositoryStar>> GetAllForCurrentWithTimestamps(CancellationToken cancellationToken = default)
        {
            return GetAllForCurrentWithTimestamps(ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user with star creation timestamps.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/user/starred")]
        public Task<IReadOnlyList<RepositoryStar>> GetAllForCurrentWithTimestamps(ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<RepositoryStar>(ApiUrls.Starred(), null, AcceptHeaders.StarJson, options, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user.
        /// </summary>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters",
            Justification = "But i think i do need star-specific request parameters")]
        [ManualRoute("GET", "/user/starred")]
        public Task<IReadOnlyList<Repository>> GetAllForCurrent(StarredRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForCurrent(request, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user.
        /// </summary>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/user/starred")]
        public Task<IReadOnlyList<Repository>> GetAllForCurrent(StarredRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Repository>(ApiUrls.Starred(), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user with star creation timestamps.
        /// </summary>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters",
            Justification = "But i think i do need star-specific request parameters")]
        [ManualRoute("GET", "/user/starred")]
        public Task<IReadOnlyList<RepositoryStar>> GetAllForCurrentWithTimestamps(StarredRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForCurrentWithTimestamps(request, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user with star creation timestamps.
        /// </summary>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/user/starred")]
        public Task<IReadOnlyList<RepositoryStar>> GetAllForCurrentWithTimestamps(StarredRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<RepositoryStar>(ApiUrls.Starred(), request.ToParametersDictionary(), AcceptHeaders.StarJson, options, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/users/{username}/starred")]
        public Task<IReadOnlyList<Repository>> GetAllForUser(string user, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return GetAllForUser(user, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/users/{username}/starred")]
        public Task<IReadOnlyList<Repository>> GetAllForUser(string user, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Repository>(ApiUrls.StarredByUser(user), options, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user with star creation timestamps.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/users/{username}/starred")]
        public Task<IReadOnlyList<RepositoryStar>> GetAllForUserWithTimestamps(string user, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return GetAllForUserWithTimestamps(user, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user with star creation timestamps.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/users/{username}/starred")]
        public Task<IReadOnlyList<RepositoryStar>> GetAllForUserWithTimestamps(string user, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<RepositoryStar>(ApiUrls.StarredByUser(user), null, AcceptHeaders.StarJson, options, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        [ManualRoute("GET", "/users/{username}/starred")]
        public Task<IReadOnlyList<Repository>> GetAllForUser(string user, StarredRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForUser(user, request, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/users/{username}/starred")]
        public Task<IReadOnlyList<Repository>> GetAllForUser(string user, StarredRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Repository>(ApiUrls.StarredByUser(user), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user with star creation timestamps.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        [ManualRoute("GET", "/users/{username}/starred")]
        public Task<IReadOnlyList<RepositoryStar>> GetAllForUserWithTimestamps(string user, StarredRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForUserWithTimestamps(user, request, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user with star creation timestamps.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/users/{username}/starred")]
        public Task<IReadOnlyList<RepositoryStar>> GetAllForUserWithTimestamps(string user, StarredRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<RepositoryStar>(ApiUrls.StarredByUser(user), request.ToParametersDictionary(), AcceptHeaders.StarJson, options, cancellationToken);
        }

        /// <summary>
        /// Check if a repository is starred by the current authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        [ManualRoute("GET", "/user/starred/{owner}/{repo}")]
        public async Task<bool> CheckStarred(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            try
            {
                var response = await Connection.Get<object>(ApiUrls.Starred(owner, name), null, null, cancellationToken).ConfigureAwait(false);
                return response.HttpResponse.StatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Stars a repository for the authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository to star</param>
        /// <param name="name">The name of the repository to star</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("PUT", "/user/starred/{owner}/{repo}")]
        public async Task<bool> StarRepo(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            try
            {
                var response = await Connection.Put<object>(ApiUrls.Starred(owner, name), null, null, cancellationToken).ConfigureAwait(false);
                return response.HttpResponse.StatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Unstars a repository for the authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository to unstar</param>
        /// <param name="name">The name of the repository to unstar</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("DELETE", "/user/starred/{owner}/{repo}")]
        public async Task<bool> RemoveStarFromRepo(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            try
            {
                var statusCode = await Connection.Delete(ApiUrls.Starred(owner, name), cancellationToken).ConfigureAwait(false);
                return statusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }
    }
}
