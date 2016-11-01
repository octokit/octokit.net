using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
#if NET_45
using System.Collections.Generic;
#endif

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
        public Task<Repository> Create(NewRepository newRepository)
        {
            Ensure.ArgumentNotNull(newRepository, "newRepository");

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
        public Task<Repository> Create(string organizationLogin, NewRepository newRepository)
        {
            Ensure.ArgumentNotNull(organizationLogin, "organizationLogin");
            Ensure.ArgumentNotNull(newRepository, "newRepository");
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
        public Task Delete(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

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
        public Task Delete(long repositoryId)
        {
            return ApiConnection.Delete(ApiUrls.Repository(repositoryId));
        }

        /// <summary>
        /// Gets the specified branch.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branchName">The name of the branch</param>
        [Obsolete("Please use RepositoriesClient.Branch.Get() instead.  This method will be removed in a future version")]
        public Task<Branch> GetBranch(long repositoryId, string branchName)
        {
            Ensure.ArgumentNotNullOrEmptyString(branchName, "branchName");

            return Branch.Get(repositoryId, branchName);
        }

        /// <summary>
        /// Updates the specified repository with the values given in <paramref name="update"/>
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="update">New values to update the repository with</param>
        /// <returns>The updated <see cref="T:Octokit.Repository"/></returns>
        public Task<Repository> Edit(string owner, string name, RepositoryUpdate update)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(update, "update");
            Ensure.ArgumentNotNull(update.Name, "update.Name");

            return ApiConnection.Patch<Repository>(ApiUrls.Repository(owner, name), update, AcceptHeaders.SquashCommitPreview);
        }

        /// <summary>
        /// Updates the specified repository with the values given in <paramref name="update"/>
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="update">New values to update the repository with</param>
        /// <returns>The updated <see cref="T:Octokit.Repository"/></returns>
        public Task<Repository> Edit(long repositoryId, RepositoryUpdate update)
        {
            Ensure.ArgumentNotNull(update, "update");

            return ApiConnection.Patch<Repository>(ApiUrls.Repository(repositoryId), update, AcceptHeaders.SquashCommitPreview);
        }

        /// <summary>
        /// Edit the specified branch with the values given in <paramref name="update"/>
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="update">New values to update the branch with</param>
        /// <returns>The updated <see cref="T:Octokit.Branch"/></returns>
        [Obsolete("This existing implementation will cease to work when the Branch Protection API preview period ends.  Please use the RepositoryBranchesClient methods instead.")]
        public Task<Branch> EditBranch(string owner, string name, string branch, BranchUpdate update)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(update, "update");

            return Branch.Edit(owner, name, branch, update);
        }

        /// <summary>
        /// Edit the specified branch with the values given in <paramref name="update"/>
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="update">New values to update the branch with</param>
        /// <returns>The updated <see cref="T:Octokit.Branch"/></returns>
        [Obsolete("This existing implementation will cease to work when the Branch Protection API preview period ends.  Please use the RepositoryBranchesClient methods instead.")]
        public Task<Branch> EditBranch(long repositoryId, string branch, BranchUpdate update)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(update, "update");

            return Branch.Edit(repositoryId, branch, update);
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
        public Task<Repository> Get(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Get<Repository>(ApiUrls.Repository(owner, name), null, AcceptHeaders.SquashCommitPreview);
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
        public Task<Repository> Get(long repositoryId)
        {
            return ApiConnection.Get<Repository>(ApiUrls.Repository(repositoryId), null, AcceptHeaders.SquashCommitPreview);
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
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
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
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        public Task<IReadOnlyList<Repository>> GetAllPublic(PublicRepositoryRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

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
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
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
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        public Task<IReadOnlyList<Repository>> GetAllForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

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
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        public Task<IReadOnlyList<Repository>> GetAllForCurrent(RepositoryRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            return GetAllForCurrent(request, ApiOptions.None);
        }

        public Task<IReadOnlyList<Repository>> GetAllForCurrent(RepositoryRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, "request");
            Ensure.ArgumentNotNull(options, "options");

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
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        public Task<IReadOnlyList<Repository>> GetAllForUser(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

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
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        public Task<IReadOnlyList<Repository>> GetAllForUser(string login, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");
            Ensure.ArgumentNotNull(options, "options");

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
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        public Task<IReadOnlyList<Repository>> GetAllForOrg(string organization)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");

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
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        public Task<IReadOnlyList<Repository>> GetAllForOrg(string organization, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");
            Ensure.ArgumentNotNull(options, "options");

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
        /// Gets all the branches for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-branches">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        [Obsolete("Please use RepositoriesClient.Branch.GetAll() instead.  This method will be removed in a future version")]
        public Task<IReadOnlyList<Branch>> GetAllBranches(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return Branch.GetAll(owner, name);
        }

        /// <summary>
        /// Gets all the branches for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-branches">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        [Obsolete("Please use RepositoriesClient.Branch.GetAll() instead.  This method will be removed in a future version")]
        public Task<IReadOnlyList<Branch>> GetAllBranches(long repositoryId)
        {
            return Branch.GetAll(repositoryId);
        }

        /// <summary>
        /// Gets all the branches for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-branches">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        [Obsolete("Please use RepositoriesClient.Branch.GetAll() instead.  This method will be removed in a future version")]
        public Task<IReadOnlyList<Branch>> GetAllBranches(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return Branch.GetAll(owner, name, options);
        }

        /// <summary>
        /// Gets all the branches for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-branches">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        [Obsolete("Please use RepositoriesClient.Branch.GetAll() instead.  This method will be removed in a future version")]
        public Task<IReadOnlyList<Branch>> GetAllBranches(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return Branch.GetAll(repositoryId, options);
        }

        /// <summary>
        /// Gets all contributors for the specified repository. Does not include anonymous contributors.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-contributors">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All contributors of the repository.</returns>
        public Task<IReadOnlyList<RepositoryContributor>> GetAllContributors(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

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
        public Task<IReadOnlyList<RepositoryContributor>> GetAllContributors(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

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
        public Task<IReadOnlyList<RepositoryContributor>> GetAllContributors(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

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
        public Task<IReadOnlyList<RepositoryContributor>> GetAllContributors(string owner, string name, bool includeAnonymous)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

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
        public Task<IReadOnlyList<RepositoryContributor>> GetAllContributors(string owner, string name, bool includeAnonymous, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

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
        public Task<IReadOnlyList<RepositoryContributor>> GetAllContributors(long repositoryId, bool includeAnonymous, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

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
        public async Task<IReadOnlyList<RepositoryLanguage>> GetAllLanguages(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = ApiUrls.RepositoryLanguages(owner, name);
            var data = await ApiConnection.Get<Dictionary<string, long>>(endpoint).ConfigureAwait(false);

            return new ReadOnlyCollection<RepositoryLanguage>(
                data.Select(kvp => new RepositoryLanguage(kvp.Key, kvp.Value)).ToList());
        }

        /// <summary>
        /// Gets all languages for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-languages">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>All languages used in the repository and the number of bytes of each language.</returns>
        public async Task<IReadOnlyList<RepositoryLanguage>> GetAllLanguages(long repositoryId)
        {
            var endpoint = ApiUrls.RepositoryLanguages(repositoryId);
            var data = await ApiConnection.Get<Dictionary<string, long>>(endpoint).ConfigureAwait(false);

            return new ReadOnlyCollection<RepositoryLanguage>(
                data.Select(kvp => new RepositoryLanguage(kvp.Key, kvp.Value)).ToList());
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
        public Task<IReadOnlyList<Team>> GetAllTeams(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

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
        public Task<IReadOnlyList<Team>> GetAllTeams(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

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
        public Task<IReadOnlyList<Team>> GetAllTeams(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

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
        public Task<IReadOnlyList<RepositoryTag>> GetAllTags(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

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
        public Task<IReadOnlyList<RepositoryTag>> GetAllTags(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

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
        public Task<IReadOnlyList<RepositoryTag>> GetAllTags(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<RepositoryTag>(ApiUrls.RepositoryTags(repositoryId), options);
        }

        /// <summary>
        /// Gets the specified branch.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branchName">The name of the branch</param>
        [Obsolete("Please use RepositoriesClient.Branch.Get() instead.  This method will be removed in a future version")]
        public Task<Branch> GetBranch(string owner, string name, string branchName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branchName, "branchName");

            return Branch.Get(owner, name, branchName);
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
    }
}
