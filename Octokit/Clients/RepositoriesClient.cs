﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repositories API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/repos/">Repositories API documentation</a> for more details.
    /// </remarks>
    public class RepositoriesClient : ApiClient, IRepositoriesClient
    {
        /// <summary>
        /// Initializes a new GitHub Repos API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public RepositoriesClient(IApiConnection apiConnection) : base(apiConnection)
        {
            Status = new CommitStatusClient(apiConnection);
            Hooks = new RepositoryHooksClient(apiConnection);
            Forks = new RepositoryForksClient(apiConnection);
            Collaborator = new RepoCollaboratorsClient(apiConnection);
            Statistics = new StatisticsClient(apiConnection);
            Deployment = new DeploymentsClient(apiConnection);
            PullRequest = new PullRequestsClient(apiConnection);
            Comment = new RepositoryCommentsClient(apiConnection);
            Commit = new RepositoryCommitsClient(apiConnection);
            Release = new ReleasesClient(apiConnection);
            DeployKeys = new RepositoryDeployKeysClient(apiConnection);
            Merging = new MergingClient(apiConnection);
            Content = new RepositoryContentsClient(apiConnection);
            Page = new RepositoryPagesClient(apiConnection);
            Invitation = new RepositoryInvitationsClient(apiConnection);
            Branch = new RepositoryBranchesClient(apiConnection);
            Traffic = new RepositoryTrafficClient(apiConnection);
            Project = new ProjectsClient(apiConnection);
        }

        /// <summary>
        /// Creates a new repository for the current user.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#create">API documentation</a> for more information.
        /// </remarks>
        /// <param name="newRepository">A <see cref="NewRepository"/> instance describing the new repository to create</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="Repository"/> instance for the created repository.</returns>
        [ManualRoute("POST", "/user/repos")]
        public Task<Repository> Create(NewRepository newRepository)
        {
            Ensure.ArgumentNotNull(newRepository, nameof(newRepository));

            return Create(ApiUrls.Repositories(), null, newRepository);
        }

        /// <summary>
        /// Creates a new repository in the specified organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#create">API documentation</a> for more information.
        /// </remarks>
        /// <param name="organizationLogin">Login of the organization in which to create the repository</param>
        /// <param name="newRepository">A <see cref="NewRepository"/> instance describing the new repository to create</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="Repository"/> instance for the created repository</returns>
        [ManualRoute("POST", "/orgs/{org}/repos")]
        public Task<Repository> Create(string organizationLogin, NewRepository newRepository)
        {
            Ensure.ArgumentNotNull(organizationLogin, nameof(organizationLogin));
            Ensure.ArgumentNotNull(newRepository, nameof(newRepository));
            if (string.IsNullOrEmpty(newRepository.Name))
                throw new ArgumentException("The new repository's name must not be null.");

            return Create(ApiUrls.OrganizationRepositories(organizationLogin), organizationLogin, newRepository);
        }

        async Task<Repository> Create(Uri url, string organizationLogin, NewRepository newRepository)
        {
            try
            {
                return await ApiConnection.Post<Repository>(url, newRepository).ConfigureAwait(false);
            }
            catch (ApiValidationException e)
            {
                string errorMessage = e.ApiError.FirstErrorMessageSafe();

                if (string.Equals(
                    "name already exists on this account",
                    errorMessage,
                    StringComparison.OrdinalIgnoreCase))
                {
                    if (string.IsNullOrEmpty(organizationLogin))
                    {
                        throw new RepositoryExistsException(newRepository.Name, e);
                    }

                    var baseAddress = Connection.BaseAddress.Host != GitHubClient.GitHubApiUrl.Host
                        ? Connection.BaseAddress
                        : new Uri("https://github.com/");
                    throw new RepositoryExistsException(
                        organizationLogin,
                        newRepository.Name,
                        baseAddress, e);
                }

                if (string.Equals(
                    "please upgrade your plan to create a new private repository.",
                    errorMessage,
                    StringComparison.OrdinalIgnoreCase))
                {
                    throw new PrivateRepositoryQuotaExceededException(e);
                }

                if (string.Equals(
                    "name can't be private. You are over your quota.",
                    errorMessage,
                    StringComparison.OrdinalIgnoreCase))
                {
                    throw new PrivateRepositoryQuotaExceededException(e);
                }

                if (errorMessage != null && errorMessage.EndsWith("is an unknown gitignore template.", StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidGitIgnoreTemplateException(e);
                }

                throw;
            }
        }

        /// <summary>
        /// Deletes the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#delete-a-repository">API documentation</a> for more information.
        /// Deleting a repository requires admin access. If OAuth is used, the `delete_repo` scope is required.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("DELETE", "/repos/{owner}/{name}")]
        public Task Delete(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Delete(ApiUrls.Repository(owner, name));
        }

        /// <summary>
        /// Deletes the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#delete-a-repository">API documentation</a> for more information.
        /// Deleting a repository requires admin access. If OAuth is used, the `delete_repo` scope is required.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("DELETE", "/repositories/{id}")]
        public Task Delete(long repositoryId)
        {
            return ApiConnection.Delete(ApiUrls.Repository(repositoryId));
        }

        /// <summary>
        /// Transfers the ownership of the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/#transfer-a-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The current owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="repositoryTransfer">Repository transfer information</param>
        /// <returns>A <see cref="Repository"/></returns>
        [ManualRoute("POST", "/repos/{owner}/{name}/transfer")]
        public Task<Repository> Transfer(string owner, string name, RepositoryTransfer repositoryTransfer)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(repositoryTransfer, nameof(repositoryTransfer));

            return ApiConnection.Post<Repository>(ApiUrls.RepositoryTransfer(owner, name), repositoryTransfer);
        }

        /// <summary>
        /// Transfers the ownership of the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/#transfer-a-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="repositoryTransfer">Repository transfer information</param>
        /// <returns>A <see cref="Repository"/></returns>
        [ManualRoute("POST", "/repositories/{id}/transfer")]
        public Task<Repository> Transfer(long repositoryId, RepositoryTransfer repositoryTransfer)
        {
            Ensure.ArgumentNotNull(repositoryTransfer, nameof(repositoryTransfer));

            return ApiConnection.Post<Repository>(ApiUrls.RepositoryTransfer(repositoryId), repositoryTransfer);
        }

        /// <summary>
        /// Updates the specified repository with the values given in <paramref name="update"/>
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="update">New values to update the repository with</param>
        /// <returns>The updated <see cref="T:Octokit.Repository"/></returns>
        [ManualRoute("PATCH", "/repos/{owner}/{name}")]
        public Task<Repository> Edit(string owner, string name, RepositoryUpdate update)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(update, nameof(update));
            Ensure.ArgumentNotNull(update.Name, nameof(update.Name));

            return ApiConnection.Patch<Repository>(ApiUrls.Repository(owner, name), update);
        }

        /// <summary>
        /// Updates the specified repository with the values given in <paramref name="update"/>
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="update">New values to update the repository with</param>
        /// <returns>The updated <see cref="T:Octokit.Repository"/></returns>
        [ManualRoute("PATCH", "/repositories/{id}")]
        public Task<Repository> Edit(long repositoryId, RepositoryUpdate update)
        {
            Ensure.ArgumentNotNull(update, nameof(update));

            return ApiConnection.Patch<Repository>(ApiUrls.Repository(repositoryId), update);
        }

        /// <summary>
        /// Gets the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#get">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="Repository"/></returns>
        [ManualRoute("GET", "/repos/{owner}/{name}")]
        public Task<Repository> Get(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<Repository>(ApiUrls.Repository(owner, name));
        }

        /// <summary>
        /// Gets the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#get">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="Repository"/></returns>
        [ManualRoute("GET", "/repositories/{id}")]
        public Task<Repository> Get(long repositoryId)
        {
            return ApiConnection.Get<Repository>(ApiUrls.Repository(repositoryId));
        }

        /// <summary>
        /// Gets all public repositories.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/#list-all-public-repositories">API documentation</a> for more information.
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="IReadOnlyList{Repository}"/> of <see cref="Repository"/>.</returns>
        [ManualRoute("GET", "/repositories")]
        public Task<IReadOnlyList<Repository>> GetAllPublic()
        {
            return ApiConnection.GetAll<Repository>(ApiUrls.AllPublicRepositories());
        }

        /// <summary>
        /// Gets all public repositories since the integer Id of the last Repository that you've seen.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/#list-all-public-repositories">API documentation</a> for more information.
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <param name="request">Search parameters of the last repository seen</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="IReadOnlyList{Repository}"/> of <see cref="Repository"/>.</returns>
        [ManualRoute("GET", "/repositories")]
        public Task<IReadOnlyList<Repository>> GetAllPublic(PublicRepositoryRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            var url = ApiUrls.AllPublicRepositories(request.Since);

            return ApiConnection.GetAll<Repository>(url);
        }

        /// <summary>
        /// Gets all repositories owned by the current user.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-your-repositories">API documentation</a> for more information.
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="IReadOnlyList{Repository}"/> of <see cref="Repository"/>.</returns>
        [ManualRoute("GET", "/user/repos")]
        public Task<IReadOnlyList<Repository>> GetAllForCurrent()
        {
            return GetAllForCurrent(ApiOptions.None);
        }

        /// <summary>
        /// Gets all repositories owned by the current user.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-your-repositories">API documentation</a> for more information.
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="IReadOnlyList{Repository}"/> of <see cref="Repository"/>.</returns>
        [ManualRoute("GET", "/user/repos")]
        public Task<IReadOnlyList<Repository>> GetAllForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Repository>(ApiUrls.Repositories(), options);
        }

        /// <summary>
        /// Gets all repositories owned by the current user.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-your-repositories">API documentation</a> for more information.
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <param name="request">Search parameters to filter results on</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="IReadOnlyList{Repository}"/> of <see cref="Repository"/>.</returns>
        [ManualRoute("GET", "/user/repos")]
        public Task<IReadOnlyList<Repository>> GetAllForCurrent(RepositoryRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForCurrent(request, ApiOptions.None);
        }

        /// <summary>
        /// Gets all repositories owned by the current user.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-your-repositories">API documentation</a> for more information.
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <param name="request">Search parameters to filter results on</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="IReadOnlyList{Repository}"/> of <see cref="Repository"/>.</returns>
        [ManualRoute("GET", "/user/repos")]
        public Task<IReadOnlyList<Repository>> GetAllForCurrent(RepositoryRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Repository>(ApiUrls.Repositories(), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Gets all repositories owned by the specified user.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-user-repositories">API documentation</a> for more information.
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <param name="login">The account name to search for</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="IReadOnlyList{Repository}"/> of <see cref="Repository"/>.</returns>
        [ManualRoute("GET", "/user/{username}/repos")]
        public Task<IReadOnlyList<Repository>> GetAllForUser(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, nameof(login));

            return GetAllForUser(login, ApiOptions.None);
        }

        /// <summary>
        /// Gets all repositories owned by the specified user.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-user-repositories">API documentation</a> for more information.
        /// </remarks>
        /// <param name="login">The account name to search for</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="IReadOnlyList{Repository}"/> of <see cref="Repository"/>.</returns>
        [ManualRoute("GET", "/user/{username}/repos")]
        public Task<IReadOnlyList<Repository>> GetAllForUser(string login, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, nameof(login));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Repository>(ApiUrls.Repositories(login), options);
        }

        /// <summary>
        /// Gets all repositories owned by the specified organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-organization-repositories">API documentation</a> for more information.
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="IReadOnlyList{Repository}"/> of <see cref="Repository"/>.</returns>
        [ManualRoute("GET", "/orgs/{org}/repos")]
        public Task<IReadOnlyList<Repository>> GetAllForOrg(string organization)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));

            return GetAllForOrg(organization, ApiOptions.None);
        }

        /// <summary>
        /// Gets all repositories owned by the specified organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-organization-repositories">API documentation</a> for more information.
        /// </remarks>
        /// <param name="organization">The organization name to search for</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="IReadOnlyList{Repository}"/> of <see cref="Repository"/>.</returns>
        [ManualRoute("GET", "/orgs/{org}/repos")]
        public Task<IReadOnlyList<Repository>> GetAllForOrg(string organization, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Repository>(ApiUrls.OrganizationRepositories(organization), options);
        }

        /// <summary>
        /// A client for GitHub's Repository Branches API.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/branches/">Branches API documentation</a> for more details
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public IRepositoryBranchesClient Branch { get; private set; }

        /// <summary>
        /// A client for GitHub's Commit Status API.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/statuses/">Commit Status API documentation</a> for more
        /// details. Also check out the <a href="https://github.com/blog/1227-commit-status-api">blog post</a>
        /// that announced this feature.
        /// </remarks>
        public ICommitStatusClient Status { get; private set; }

        /// <summary>
        /// A client for GitHub's Repository Hooks API.
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/">Hooks API documentation</a> for more information.</remarks>
        public IRepositoryHooksClient Hooks { get; private set; }

        /// <summary>
        /// A client for GitHub's Repository Forks API.
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/forks/">Forks API documentation</a> for more information.</remarks>
        public IRepositoryForksClient Forks { get; private set; }

        /// <summary>
        /// A client for GitHub's Repo Collaborators.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/">Collaborators API documentation</a> for more details
        /// </remarks>
        public IRepoCollaboratorsClient Collaborator { get; private set; }

        /// <summary>
        /// Client for GitHub's Repository Deployments API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/deployments/">Deployments API documentation</a> for more details
        /// </remarks>
        public IDeploymentsClient Deployment { get; private set; }

        /// <summary>
        /// Client for GitHub's Repository Statistics API
        /// Note that the GitHub API uses caching on these endpoints,
        /// see <a href="https://developer.github.com/v3/repos/statistics/#a-word-about-caching">a word about caching</a> for more details.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/statistics/">Statistics API documentation</a> for more details
        ///</remarks>
        public IStatisticsClient Statistics { get; private set; }

        /// <summary>
        /// Client for GitHub's Repository Commits API
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/commits/">Commits API documentation</a> for more details
        ///</remarks>
        public IRepositoryCommitsClient Commit { get; private set; }

        /// <summary>
        /// Access GitHub's Releases API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/repos/releases/
        /// </remarks>
        public IReleasesClient Release { get; private set; }

        /// <summary>
        /// Client for GitHub's Repository Merging API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/merging/">Merging API documentation</a> for more details
        ///</remarks>
        public IMergingClient Merging { get; private set; }

        /// <summary>
        /// Client for managing pull requests.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/pulls/">Pull Requests API documentation</a> for more details
        /// </remarks>
        public IPullRequestsClient PullRequest { get; private set; }

        /// <summary>
        /// Client for managing commit comments in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/comments/">Repository Comments API documentation</a> for more information.
        /// </remarks>
        public IRepositoryCommentsClient Comment { get; private set; }

        /// <summary>
        /// Client for managing deploy keys in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/">Repository Deploy Keys API documentation</a> for more information.
        /// </remarks>
        public IRepositoryDeployKeysClient DeployKeys { get; private set; }

        /// <summary>
        /// Client for managing the contents of a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/contents/">Repository Contents API documentation</a> for more information.
        /// </remarks>
        public IRepositoryContentsClient Content { get; private set; }

        /// <summary>
        /// Gets all contributors for the specified repository. Does not include anonymous contributors.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-contributors">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All contributors of the repository.</returns>
        [ManualRoute("GET", "/repos/{owner}/{name}/contributors")]
        public Task<IReadOnlyList<RepositoryContributor>> GetAllContributors(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllContributors(owner, name, false);
        }

        /// <summary>
        /// Gets all contributors for the specified repository. Does not include anonymous contributors.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-contributors">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>All contributors of the repository.</returns>
        [ManualRoute("GET", "/repositories/{id}/contributors")]
        public Task<IReadOnlyList<RepositoryContributor>> GetAllContributors(long repositoryId)
        {
            return GetAllContributors(repositoryId, false);
        }

        /// <summary>
        /// Gets all contributors for the specified repository. Does not include anonymous contributors.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-contributors">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>All contributors of the repository.</returns>
        [ManualRoute("GET", "/repos/{owner}/{name}/contributors")]
        public Task<IReadOnlyList<RepositoryContributor>> GetAllContributors(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAllContributors(owner, name, false, options);
        }

        /// <summary>
        /// Gets all contributors for the specified repository. Does not include anonymous contributors.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-contributors">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>All contributors of the repository.</returns>
        [ManualRoute("GET", "/repositories/{id}/contributors")]
        public Task<IReadOnlyList<RepositoryContributor>> GetAllContributors(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAllContributors(repositoryId, false, options);
        }

        /// <summary>
        /// Gets all contributors for the specified repository. With the option to include anonymous contributors.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-contributors">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="includeAnonymous">True if anonymous contributors should be included in result; Otherwise false</param>
        /// <returns>All contributors of the repository.</returns>
        [ManualRoute("GET", "/repos/{owner}/{name}/contributors")]
        public Task<IReadOnlyList<RepositoryContributor>> GetAllContributors(string owner, string name, bool includeAnonymous)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllContributors(owner, name, includeAnonymous, ApiOptions.None);
        }

        /// <summary>
        /// Gets all contributors for the specified repository. With the option to include anonymous contributors.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-contributors">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="includeAnonymous">True if anonymous contributors should be included in result; Otherwise false</param>
        /// <returns>All contributors of the repository.</returns>
        [ManualRoute("GET", "/repositories/{id}/contributors")]
        public Task<IReadOnlyList<RepositoryContributor>> GetAllContributors(long repositoryId, bool includeAnonymous)
        {
            return GetAllContributors(repositoryId, includeAnonymous, ApiOptions.None);
        }

        /// <summary>
        /// Gets all contributors for the specified repository. With the option to include anonymous contributors.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-contributors">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="includeAnonymous">True if anonymous contributors should be included in result; Otherwise false</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>All contributors of the repository.</returns>
        [ManualRoute("GET", "/repos/{owner}/{name}/contributors")]
        public Task<IReadOnlyList<RepositoryContributor>> GetAllContributors(string owner, string name, bool includeAnonymous, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            var parameters = new Dictionary<string, string>();
            if (includeAnonymous)
                parameters.Add("anon", "1");

            return ApiConnection.GetAll<RepositoryContributor>(ApiUrls.RepositoryContributors(owner, name), parameters, options);
        }

        /// <summary>
        /// Gets all contributors for the specified repository. With the option to include anonymous contributors.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-contributors">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="includeAnonymous">True if anonymous contributors should be included in result; Otherwise false</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>All contributors of the repository.</returns>
        [ManualRoute("GET", "/repositories/{id}/contributors")]
        public Task<IReadOnlyList<RepositoryContributor>> GetAllContributors(long repositoryId, bool includeAnonymous, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var parameters = new Dictionary<string, string>();
            if (includeAnonymous)
                parameters.Add("anon", "1");

            return ApiConnection.GetAll<RepositoryContributor>(ApiUrls.RepositoryContributors(repositoryId), parameters, options);
        }

        /// <summary>
        /// Gets all languages for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-languages">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All languages used in the repository and the number of bytes of each language.</returns>
        [ManualRoute("GET", "/repos/{owner}/{name}/languages")]
        public async Task<IReadOnlyList<RepositoryLanguage>> GetAllLanguages(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            var endpoint = ApiUrls.RepositoryLanguages(owner, name);
            var data = await ApiConnection.Get<Dictionary<string, long>>(endpoint).ConfigureAwait(false);

            return new ReadOnlyCollection<RepositoryLanguage>(
                (data ?? new Dictionary<string, long>())
                .Select(kvp => new RepositoryLanguage(kvp.Key, kvp.Value)).ToList());
        }

        /// <summary>
        /// Gets all languages for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-languages">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>All languages used in the repository and the number of bytes of each language.</returns>
        [ManualRoute("GET", "/repositories/{id}/languages")]
        public async Task<IReadOnlyList<RepositoryLanguage>> GetAllLanguages(long repositoryId)
        {
            var endpoint = ApiUrls.RepositoryLanguages(repositoryId);
            var data = await ApiConnection.Get<Dictionary<string, long>>(endpoint).ConfigureAwait(false);

            return new ReadOnlyCollection<RepositoryLanguage>(
                (data ?? new Dictionary<string, long>())
                .Select(kvp => new RepositoryLanguage(kvp.Key, kvp.Value)).ToList());
        }

        /// <summary>
        /// Gets all teams for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-teams">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All <see cref="T:Octokit.Team"/>s associated with the repository</returns>
        [ManualRoute("GET", "/repos/{owner}/{name}/teams")]
        public Task<IReadOnlyList<Team>> GetAllTeams(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllTeams(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets all teams for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-teams">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>All <see cref="T:Octokit.Team"/>s associated with the repository</returns>
        [ManualRoute("GET", "/repositories/{id}/teams")]
        public Task<IReadOnlyList<Team>> GetAllTeams(long repositoryId)
        {
            return GetAllTeams(repositoryId, ApiOptions.None);
        }

        /// <summary>
        /// Gets all teams for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-teams">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>All <see cref="T:Octokit.Team"/>s associated with the repository</returns>
        [ManualRoute("GET", "/repos/{owner}/{name}/teams")]
        public Task<IReadOnlyList<Team>> GetAllTeams(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Team>(ApiUrls.RepositoryTeams(owner, name), options);
        }

        /// <summary>
        /// Gets all teams for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-teams">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>All <see cref="T:Octokit.Team"/>s associated with the repository</returns>
        [ManualRoute("GET", "/repositories/{id}/teams")]
        public Task<IReadOnlyList<Team>> GetAllTeams(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Team>(ApiUrls.RepositoryTeams(repositoryId), options);
        }

        /// <summary>
        /// Gets all tags for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-tags">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All of the repositories tags.</returns>
        [ManualRoute("GET", "/repos/{owner}/{name}/tags")]
        public Task<IReadOnlyList<RepositoryTag>> GetAllTags(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllTags(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets all tags for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-tags">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>All of the repositories tags.</returns>
        [ManualRoute("GET", "/repositories/{id}/tags")]
        public Task<IReadOnlyList<RepositoryTag>> GetAllTags(long repositoryId)
        {
            return GetAllTags(repositoryId, ApiOptions.None);
        }

        /// <summary>
        /// Gets all tags for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-tags">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>All of the repositories tags.</returns>
        [ManualRoute("GET", "/repos/{owner}/{name}/tags")]
        public Task<IReadOnlyList<RepositoryTag>> GetAllTags(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<RepositoryTag>(ApiUrls.RepositoryTags(owner, name), options);
        }

        /// <summary>
        /// Gets all tags for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-tags">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>All of the repositories tags.</returns>
        [ManualRoute("GET", "/repositories/{id}/tags")]
        public Task<IReadOnlyList<RepositoryTag>> GetAllTags(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<RepositoryTag>(ApiUrls.RepositoryTags(repositoryId), options);
        }

        /// <summary>
        /// Get the contents of a repository's license
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/licenses/#get-the-contents-of-a-repositorys-license">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>Returns the contents of the repository's license file, if one is detected.</returns>
        [ManualRoute("GET", "/repos/{owner}/{name}/license")]
        public Task<RepositoryContentLicense> GetLicenseContents(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<RepositoryContentLicense>(ApiUrls.RepositoryLicense(owner, name));
        }

        /// <summary>
        /// Get the contents of a repository's license
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/licenses/#get-the-contents-of-a-repositorys-license">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>Returns the contents of the repository's license file, if one is detected.</returns>
        [ManualRoute("GET", "/repositories/{id}/license")]
        public Task<RepositoryContentLicense> GetLicenseContents(long repositoryId)
        {
            return ApiConnection.Get<RepositoryContentLicense>(ApiUrls.RepositoryLicense(repositoryId));
        }

        /// <summary>
        /// A client for GitHub's Repository Pages API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/">Repository Pages API documentation</a> for more information.
        /// </remarks>
        public IRepositoryPagesClient Page { get; private set; }

        /// <summary>
        /// A client for GitHub's Repository Invitations API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/">Repository Invitations API documentation</a> for more information.
        /// </remarks>
        public IRepositoryInvitationsClient Invitation { get; private set; }

        /// <summary>
        /// Access GitHub's Repository Traffic API
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/repos/traffic/
        /// </remarks>
        public IRepositoryTrafficClient Traffic { get; private set; }

        /// <summary>
        /// Access GitHub's Repository Projects API
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/repos/projects/
        /// </remarks>
        public IProjectsClient Project { get; private set; }
    }
}
