﻿using System.Collections.Generic;
using System.Net;
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
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{User}"/> of <see cref="User"/>s starring the passed repository.</returns>
        public Task<IReadOnlyList<User>> GetAllStargazers(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAllStargazers(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the stargazers for the passed repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{User}"/> of <see cref="User"/>s starring the passed repository.</returns>
        public Task<IReadOnlyList<User>> GetAllStargazers(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<User>(ApiUrls.Stargazers(owner, name), options);
        }

        /// <summary>
        /// Retrieves all of the stargazers for the passed repository with star creation timestamps.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{UserStar}"/> of <see cref="User"/>s starring the passed repository with star creation timestamps.</returns>
        public Task<IReadOnlyList<UserStar>> GetAllStargazersWithTimestamps(string owner, string name)
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
        /// <returns>A <see cref="IReadOnlyPagedCollection{UserStar}"/> of <see cref="User"/>s starring the passed repository with star creation timestamps.</returns>
        public Task<IReadOnlyList<UserStar>> GetAllStargazersWithTimestamps(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<UserStar>(ApiUrls.Stargazers(owner, name), null, AcceptHeaders.StarCreationTimestamps, options);
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>(ies) starred by the current authenticated user.
        /// </returns>
        public Task<IReadOnlyList<Repository>> GetAllForCurrent()
        {
            return GetAllForCurrent(ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>(ies) starred by the current authenticated user.
        /// </returns>
        public Task<IReadOnlyList<Repository>> GetAllForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<Repository>(ApiUrls.Starred(), options);
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user with star creation timestamps.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IReadOnlyPagedCollection{RepoStar}"/> of <see cref="Repository"/>(ies) starred by the current authenticated user with star creation timestamps.
        /// </returns>
        public Task<IReadOnlyList<RepositoryStar>> GetAllForCurrentWithTimestamps()
        {
            return GetAllForCurrentWithTimestamps(ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user with star creation timestamps.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IReadOnlyPagedCollection{RepoStar}"/> of <see cref="Repository"/>(ies) starred by the current authenticated user with star creation timestamps.
        /// </returns>
        public Task<IReadOnlyList<RepositoryStar>> GetAllForCurrentWithTimestamps(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<RepositoryStar>(ApiUrls.Starred(), null, AcceptHeaders.StarCreationTimestamps, options);
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user.
        /// </summary>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>(ies) starred by the current user,
        /// sorted according to the passed request parameters.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters",
            Justification = "But i think i do need star-specific request parameters")]
        public Task<IReadOnlyList<Repository>> GetAllForCurrent(StarredRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            return GetAllForCurrent(request, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user.
        /// </summary>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>(ies) starred by the current user,
        /// sorted according to the passed request parameters.
        /// </returns>
        public Task<IReadOnlyList<Repository>> GetAllForCurrent(StarredRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, "request");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<Repository>(ApiUrls.Starred(), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the current user with star creation timestamps.
        /// </summary>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IReadOnlyPagedCollection{RepoStar}"/> of <see cref="Repository"/>(ies) starred by the current user,
        /// sorted according to the passed request parameters and with star creation timestamps.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters",
            Justification = "But i think i do need star-specific request parameters")]
        public Task<IReadOnlyList<RepositoryStar>> GetAllForCurrentWithTimestamps(StarredRequest request)
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
        /// A <see cref="IReadOnlyPagedCollection{RepoStar}"/> of <see cref="Repository"/>(ies) starred by the current user,
        /// sorted according to the passed request parameters and with star creation timestamps.
        /// </returns>
        public Task<IReadOnlyList<RepositoryStar>> GetAllForCurrentWithTimestamps(StarredRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, "request");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<RepositoryStar>(ApiUrls.Starred(), request.ToParametersDictionary(), AcceptHeaders.StarCreationTimestamps, options);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IReadOnlyPagedCollection{Repository}"/>(ies) starred by the specified user.
        /// </returns>
        public Task<IReadOnlyList<Repository>> GetAllForUser(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return GetAllForUser(user, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IReadOnlyPagedCollection{Repository}"/>(ies) starred by the specified user.
        /// </returns>
        public Task<IReadOnlyList<Repository>> GetAllForUser(string user, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<Repository>(ApiUrls.StarredByUser(user), options);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user with star creation timestamps.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IReadOnlyPagedCollection{RepoStar}"/>(ies) starred by the specified user with star creation timestamps.
        /// </returns>
        public Task<IReadOnlyList<RepositoryStar>> GetAllForUserWithTimestamps(string user)
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
        /// A <see cref="IReadOnlyPagedCollection{RepoStar}"/>(ies) starred by the specified user with star creation timestamps.
        /// </returns>
        public Task<IReadOnlyList<RepositoryStar>> GetAllForUserWithTimestamps(string user, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<RepositoryStar>(ApiUrls.StarredByUser(user), null, AcceptHeaders.StarCreationTimestamps, options);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> starred by the specified user.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public Task<IReadOnlyList<Repository>> GetAllForUser(string user, StarredRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");
            Ensure.ArgumentNotNull(request, "request");

            return GetAllForUser(user, request, ApiOptions.None);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> starred by the specified user.</returns>
        public Task<IReadOnlyList<Repository>> GetAllForUser(string user, StarredRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");
            Ensure.ArgumentNotNull(request, "request");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<Repository>(ApiUrls.StarredByUser(user), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the specified user with star creation timestamps.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="request">Star-specific request parameters that sort the results</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>
        /// A <see cref="IReadOnlyPagedCollection{RepoStar}"/> of <see cref="Repository"/>(ies) starred by the specified user, 
        /// sorted according to the passed request parameters and with star creation timestamps.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public Task<IReadOnlyList<RepositoryStar>> GetAllForUserWithTimestamps(string user, StarredRequest request)
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
        /// A <see cref="IReadOnlyPagedCollection{RepoStar}"/> of <see cref="Repository"/>(ies) starred by the specified user, 
        /// sorted according to the passed request parameters and with star creation timestamps.
        /// </returns>
        public Task<IReadOnlyList<RepositoryStar>> GetAllForUserWithTimestamps(string user, StarredRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");
            Ensure.ArgumentNotNull(request, "request");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<RepositoryStar>(ApiUrls.StarredByUser(user), request.ToParametersDictionary(), AcceptHeaders.StarCreationTimestamps, options);
        }

        /// <summary>
        /// Check if a repository is starred by the current authenticated user.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <c>bool</c> representing the success of the operation</returns>
        public async Task<bool> CheckStarred(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            try
            {
                var response = await Connection.Get<object>(ApiUrls.Starred(owner, name), null, null).ConfigureAwait(false);
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
        /// <returns>A <c>bool</c> representing the success of starring</returns>
        public async Task<bool> StarRepo(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            try
            {
                var response = await Connection.Put<object>(ApiUrls.Starred(owner, name), null, null).ConfigureAwait(false);
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
        /// <returns>A <c>bool</c> representing the success of the operation</returns>
        public async Task<bool> RemoveStarFromRepo(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            try
            {
                var statusCode = await Connection.Delete(ApiUrls.Starred(owner, name)).ConfigureAwait(false);
                return statusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }
    }
}
