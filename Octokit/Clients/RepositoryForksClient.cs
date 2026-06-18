using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Forks API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/forks/">Forks API documentation</a> for more information.
    /// </remarks>
    public class RepositoryForksClient : ApiClient, IRepositoryForksClient
    {
        /// <summary>
        /// Initializes a new GitHub Repos Fork API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public RepositoryForksClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets the list of forks defined for a repository
        /// </summary>
        /// <remarks>
        /// See <a href="http://developer.github.com/v3/repos/forks/#list-forks">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/forks")]
        public Task<IReadOnlyList<Repository>> GetAll(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets the list of forks defined for a repository
        /// </summary>
        /// <remarks>
        /// See <a href="http://developer.github.com/v3/repos/forks/#list-forks">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        [ManualRoute("GET", "/repositories/{id}/forks")]
        public Task<IReadOnlyList<Repository>> GetAll(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAll(repositoryId, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets the list of forks defined for a repository
        /// </summary>
        /// <remarks>
        /// See <a href="http://developer.github.com/v3/repos/forks/#list-forks">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/forks")]
        public Task<IReadOnlyList<Repository>> GetAll(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Repository>(ApiUrls.RepositoryForks(owner, name), options, cancellationToken);
        }

        /// <summary>
        /// Gets the list of forks defined for a repository
        /// </summary>
        /// <remarks>
        /// See <a href="http://developer.github.com/v3/repos/forks/#list-forks">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{id}/forks")]
        public Task<IReadOnlyList<Repository>> GetAll(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Repository>(ApiUrls.RepositoryForks(repositoryId), options, cancellationToken);
        }

        /// <summary>
        /// Gets the list of forks defined for a repository
        /// </summary>
        /// <remarks>
        /// See <a href="http://developer.github.com/v3/repos/forks/#list-forks">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to request and filter a list of repository forks</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/forks")]
        public Task<IReadOnlyList<Repository>> GetAll(string owner, string name, RepositoryForksListRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAll(owner, name, request, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets the list of forks defined for a repository
        /// </summary>
        /// <remarks>
        /// See <a href="http://developer.github.com/v3/repos/forks/#list-forks">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to request and filter a list of repository forks</param>
        [ManualRoute("GET", "/repositories/{id}/forks")]
        public Task<IReadOnlyList<Repository>> GetAll(long repositoryId, RepositoryForksListRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAll(repositoryId, request, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets the list of forks defined for a repository
        /// </summary>
        /// <remarks>
        /// See <a href="http://developer.github.com/v3/repos/forks/#list-forks">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to request and filter a list of repository forks</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/forks")]
        public Task<IReadOnlyList<Repository>> GetAll(string owner, string name, RepositoryForksListRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Repository>(ApiUrls.RepositoryForks(owner, name), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Gets the list of forks defined for a repository
        /// </summary>
        /// <remarks>
        /// See <a href="http://developer.github.com/v3/repos/forks/#list-forks">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to request and filter a list of repository forks</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{id}/forks")]
        public Task<IReadOnlyList<Repository>> GetAll(long repositoryId, RepositoryForksListRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Repository>(ApiUrls.RepositoryForks(repositoryId), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Creates a fork for a repository. Specify organization in the fork parameter to create for an organization.
        /// </summary>
        /// <remarks>
        /// See <a href="http://developer.github.com/v3/repos/forks/#create-a-fork">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="fork">Used to fork a repository</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/forks")]
        public Task<Repository> Create(string owner, string name, NewRepositoryFork fork, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(fork, nameof(fork));

            return ApiConnection.Post<Repository>(ApiUrls.RepositoryForks(owner, name), fork, cancellationToken);
        }

        /// <summary>
        /// Creates a fork for a repository. Specify organization in the fork parameter to create for an organization.
        /// </summary>
        /// <remarks>
        /// See <a href="http://developer.github.com/v3/repos/forks/#create-a-fork">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="fork">Used to fork a repository</param>
        [ManualRoute("POST", "/repositories/{id}/forks")]
        public Task<Repository> Create(long repositoryId, NewRepositoryFork fork, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(fork, nameof(fork));

            return ApiConnection.Post<Repository>(ApiUrls.RepositoryForks(repositoryId), fork, cancellationToken);
        }
    }
}
