using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repositories API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/reference/repos">Repositories API documentation</a> for more details.
    /// </remarks>
    public interface IRepositoriesClient
    {
        /// <summary>
        /// Client for managing pull requests.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/pulls/">Pull Requests API documentation</a> for more details
        /// </remarks>
        IPullRequestsClient PullRequest { get; }

        /// <summary>
        /// Client for managing Actions in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions">Repository Actions API documentation</a> for more information.
        /// </remarks>
        IRepositoryActionsClient Actions { get; }

        /// <summary>
        /// Client for managing branches in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/">Branches API documentation</a> for more details
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        IRepositoryBranchesClient Branch { get; }

        /// <summary>
        /// Client for managing commit comments in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/comments/">Repository Comments API documentation</a> for more information.
        /// </remarks>
        IRepositoryCommentsClient Comment { get; }

        /// <summary>
        /// Client for managing deploy keys in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/">Repository Deploy Keys API documentation</a> for more information.
        /// </remarks>
        IRepositoryDeployKeysClient DeployKeys { get; }

        /// <summary>
        /// Client for managing the contents of a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/contents/">Repository Contents API documentation</a> for more information.
        /// </remarks>
        IRepositoryContentsClient Content { get; }

        /// <summary>
        /// Creates a new repository for the current user.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#create">API documentation</a> for more information.
        /// </remarks>
        /// <param name="newRepository">A <see cref="NewRepository"/> instance describing the new repository to create</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="Repository"/> instance for the created repository.</returns>
        Task<Repository> Create(NewRepository newRepository);

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
        Task<Repository> Create(string organizationLogin, NewRepository newRepository);

        /// <summary>
        /// Creates a new repository from a template
        /// </summary>
        /// <param name="templateOwner">The organization or person who will owns the template</param>
        /// <param name="templateRepo">The name of template repository to work from</param>
        /// <param name="newRepository"></param>
        /// <returns></returns>
        Task<Repository> Generate(string templateOwner, string templateRepo, NewRepositoryFromTemplate newRepository);


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
        Task Delete(string owner, string name);

        /// <summary>
        /// Deletes the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#delete-a-repository">API documentation</a> for more information.
        /// Deleting a repository requires admin access. If OAuth is used, the `delete_repo` scope is required.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task Delete(long repositoryId);

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
        Task<Repository> Transfer(string owner, string name, RepositoryTransfer repositoryTransfer);

        /// <summary>
        /// Transfers the ownership of the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/#transfer-a-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="repositoryTransfer">Repository transfer information</param>
        /// <returns>A <see cref="Repository"/></returns>
        Task<Repository> Transfer(long repositoryId, RepositoryTransfer repositoryTransfer);

        /// <summary>
        /// Checks if vulnerability alerts are enabled for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/reference/repos#check-if-vulnerability-alerts-are-enabled-for-a-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The current owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>A <c>bool</c> indicating if alerts are turned on or not.</returns>
        Task<bool> AreVulnerabilityAlertsEnabled(string owner, string name);

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
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        Task<Repository> Get(string owner, string name);

        /// <summary>
        /// Gets the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#get">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="Repository"/></returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        Task<Repository> Get(long repositoryId);

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
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
             Justification = "Makes a network request")]
        [ExcludeFromPaginationApiOptionsConventionTest("This API call uses the PublicRepositoryRequest.Since parameter for pagination")]
        Task<IReadOnlyList<Repository>> GetAllPublic();

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
        [ExcludeFromPaginationApiOptionsConventionTest("This API call uses the PublicRepositoryRequest.Since parameter for pagination")]
        Task<IReadOnlyList<Repository>> GetAllPublic(PublicRepositoryRequest request);

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
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
             Justification = "Makes a network request")]
        Task<IReadOnlyList<Repository>> GetAllForCurrent();

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
        Task<IReadOnlyList<Repository>> GetAllForCurrent(ApiOptions options);

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
        Task<IReadOnlyList<Repository>> GetAllForCurrent(RepositoryRequest request);

        /// <summary>
        /// Gets all repositories owned by the current user.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-your-repositories">API documentation</a> for more information.
        /// </remarks>
        /// <param name="request">Search parameters to filter results on</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        Task<IReadOnlyList<Repository>> GetAllForCurrent(RepositoryRequest request, ApiOptions options);

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
        Task<IReadOnlyList<Repository>> GetAllForUser(string login);

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
        Task<IReadOnlyList<Repository>> GetAllForUser(string login, ApiOptions options);

        /// <summary>
        /// Gets all repositories owned by the specified organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-organization-repositories">API documentation</a> for more information.
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <param name="organization">The organization name to search for</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        Task<IReadOnlyList<Repository>> GetAllForOrg(string organization);

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
        Task<IReadOnlyList<Repository>> GetAllForOrg(string organization, ApiOptions options);

        /// <summary>
        /// A client for GitHub's Commit Status API.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/statuses/">Commit Status API documentation</a> for more
        /// details. Also check out the <a href="https://github.com/blog/1227-commit-status-api">blog post</a>
        /// that announced this feature.
        /// </remarks>
        ICommitStatusClient Status { get; }

        /// <summary>
        /// A client for GitHub's Repository Hooks API.
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/">Hooks API documentation</a> for more information.</remarks>
        IRepositoryHooksClient Hooks { get; }

        /// <summary>
        /// A client for GitHub's Repository Forks API.
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/forks/">Forks API documentation</a> for more information.</remarks>
        IRepositoryForksClient Forks { get; }

        /// <summary>
        /// A client for GitHub's Repo Collaborators.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/">Collaborators API documentation</a> for more details
        /// </remarks>
        IRepoCollaboratorsClient Collaborator { get; }

        /// <summary>
        /// Client for GitHub's Repository Deployments API
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/deployments/">Deployments API documentation</a> for more details
        /// </remarks>
        IDeploymentsClient Deployment { get; }

        /// <summary>
        /// Client for GitHub's Repository Environments API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/deployments/environments/">Environments API documentation</a> for more details
        /// </remarks>
        IRepositoryDeployEnvironmentsClient Environment { get;  }

        /// <summary>
        /// Client for GitHub's Repository Statistics API.
        /// Note that the GitHub API uses caching on these endpoints,
        /// see <a href="https://developer.github.com/v3/repos/statistics/#a-word-about-caching">a word about caching</a> for more details.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/statistics/">Statistics API documentation</a> for more details
        ///</remarks>
        IStatisticsClient Statistics { get; }

        /// <summary>
        /// Client for GitHub's Repository Commits API
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/commits/">Commits API documentation</a> for more details
        ///</remarks>
        IRepositoryCommitsClient Commit { get; }

        /// <summary>
        /// Access GitHub's Releases API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/repos/releases/
        /// </remarks>
        IReleasesClient Release { get; }

        /// <summary>
        /// Client for GitHub's Repository Merging API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/merging/">Merging API documentation</a> for more details
        ///</remarks>
        IMergingClient Merging { get; }

        /// <summary>
        /// Gets all contributors for the specified repository. Does not include anonymous contributors.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-contributors">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All contributors of the repository.</returns>
        Task<IReadOnlyList<RepositoryContributor>> GetAllContributors(string owner, string name);

        /// <summary>
        /// Gets all contributors for the specified repository. Does not include anonymous contributors.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-contributors">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>All contributors of the repository.</returns>
        Task<IReadOnlyList<RepositoryContributor>> GetAllContributors(long repositoryId);

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
        Task<IReadOnlyList<RepositoryContributor>> GetAllContributors(string owner, string name, ApiOptions options);

        /// <summary>
        /// Gets all contributors for the specified repository. Does not include anonymous contributors.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-contributors">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>All contributors of the repository.</returns>
        Task<IReadOnlyList<RepositoryContributor>> GetAllContributors(long repositoryId, ApiOptions options);

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
        Task<IReadOnlyList<RepositoryContributor>> GetAllContributors(string owner, string name, bool includeAnonymous);

        /// <summary>
        /// Gets all contributors for the specified repository. With the option to include anonymous contributors.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-contributors">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="includeAnonymous">True if anonymous contributors should be included in result; Otherwise false</param>
        /// <returns>All contributors of the repository.</returns>
        Task<IReadOnlyList<RepositoryContributor>> GetAllContributors(long repositoryId, bool includeAnonymous);

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
        Task<IReadOnlyList<RepositoryContributor>> GetAllContributors(string owner, string name, bool includeAnonymous, ApiOptions options);

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
        Task<IReadOnlyList<RepositoryContributor>> GetAllContributors(long repositoryId, bool includeAnonymous, ApiOptions options);

        /// <summary>
        /// Gets all languages for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-languages">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All languages used in the repository and the number of bytes of each language.</returns>
        [ExcludeFromPaginationApiOptionsConventionTest("Pagination not supported by GitHub API (tested 29/08/2017)")]
        Task<IReadOnlyList<RepositoryLanguage>> GetAllLanguages(string owner, string name);

        /// <summary>
        /// Gets all languages for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-languages">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>All languages used in the repository and the number of bytes of each language.</returns>
        [ExcludeFromPaginationApiOptionsConventionTest("Pagination not supported by GitHub API (tested 29/08/2017)")]
        Task<IReadOnlyList<RepositoryLanguage>> GetAllLanguages(long repositoryId);

        /// <summary>
        /// Gets all teams for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-teams">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All <see cref="T:Octokit.Team"/>s associated with the repository</returns>
        Task<IReadOnlyList<Team>> GetAllTeams(string owner, string name);

        /// <summary>
        /// Gets all teams for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-teams">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>All <see cref="T:Octokit.Team"/>s associated with the repository</returns>
        Task<IReadOnlyList<Team>> GetAllTeams(long repositoryId);

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
        Task<IReadOnlyList<Team>> GetAllTeams(string owner, string name, ApiOptions options);

        /// <summary>
        /// Gets all teams for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-teams">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>All <see cref="T:Octokit.Team"/>s associated with the repository</returns>
        Task<IReadOnlyList<Team>> GetAllTeams(long repositoryId, ApiOptions options);

        /// <summary>
        /// Gets all tags for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-tags">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All of the repositories tags.</returns>
        Task<IReadOnlyList<RepositoryTag>> GetAllTags(string owner, string name);

        /// <summary>
        /// Gets all tags for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-tags">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>All of the repositories tags.</returns>
        Task<IReadOnlyList<RepositoryTag>> GetAllTags(long repositoryId);

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
        Task<IReadOnlyList<RepositoryTag>> GetAllTags(string owner, string name, ApiOptions options);

        /// <summary>
        /// Gets all tags for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-tags">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>All of the repositories tags.</returns>
        Task<IReadOnlyList<RepositoryTag>> GetAllTags(long repositoryId, ApiOptions options);

        /// <summary>
        /// Get the contents of a repository's license
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/licenses/#get-the-contents-of-a-repositorys-license">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>Returns the contents of the repository's license file, if one is detected.</returns>
        Task<RepositoryContentLicense> GetLicenseContents(string owner, string name);

        /// <summary>
        /// Get the contents of a repository's license
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/licenses/#get-the-contents-of-a-repositorys-license">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>Returns the contents of the repository's license file, if one is detected.</returns>
        Task<RepositoryContentLicense> GetLicenseContents(long repositoryId);

        /// <summary>
        /// Updates the specified repository with the values given in <paramref name="update"/>
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="update">New values to update the repository with</param>
        /// <returns>The updated <see cref="T:Octokit.Repository"/></returns>
        Task<Repository> Edit(string owner, string name, RepositoryUpdate update);

        /// <summary>
        /// Updates the specified repository with the values given in <paramref name="update"/>
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="update">New values to update the repository with</param>
        /// <returns>The updated <see cref="T:Octokit.Repository"/></returns>
        Task<Repository> Edit(long repositoryId, RepositoryUpdate update);

        /// <summary>
        /// A client for GitHub's Repository Pages API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/">Repository Pages API documentation</a> for more information.
        /// </remarks>
        IRepositoryPagesClient Page { get; }

        /// <summary>
        /// A client for GitHub's Repository Invitations API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/">Repository Invitations API documentation</a> for more information.
        /// </remarks>
        IRepositoryInvitationsClient Invitation { get; }

        /// <summary>
        /// Access GitHub's Repository Traffic API
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/repos/traffic/
        /// </remarks>
        IRepositoryTrafficClient Traffic { get; }

        /// <summary>
        /// Access GitHub's Repository Projects API
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/repos/projects/
        /// </remarks>
        IProjectsClient Project { get; }

        /// <summary>
        /// Gets all topics for the specified owner and repository name.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/reference/repos#get-all-repository-topics">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>All topics associated with the repository.</returns>
        Task<RepositoryTopics> GetAllTopics(string owner, string name, ApiOptions options);

        /// <summary>
        /// Gets all topics for the specified owner and repository name.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/reference/repos#get-all-repository-topics">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All topics associated with the repository.</returns>
        Task<RepositoryTopics> GetAllTopics(string owner, string name);

        /// <summary>
        /// Gets all topics for the specified repository ID.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/reference/repos#get-all-repository-topics">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>All topics associated with the repository.</returns>
        Task<RepositoryTopics> GetAllTopics(long repositoryId, ApiOptions options);

        /// <summary>
        /// Gets all topics for the specified repository ID.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/reference/repos#get-all-repository-topics">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <returns>All topics associated with the repository.</returns>
        Task<RepositoryTopics> GetAllTopics(long repositoryId);

        /// <summary>
        /// Replaces all topics for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/reference/repos#replace-all-repository-topics">API documentation</a> for more details
        ///
        /// This is a replacement operation; it is not additive. To clear repository topics, for example, you could specify an empty list of topics here.
        /// </remarks>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <param name="topics">The list of topics to associate with the repository</param>
        /// <returns>All topics now associated with the repository.</returns>
        Task<RepositoryTopics> ReplaceAllTopics(long repositoryId, RepositoryTopics topics);

        /// <summary>
        /// Replaces all topics for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/reference/repos#replace-all-repository-topics">API documentation</a> for more details
        ///
        /// This is a replacement operation; it is not additive. To clear repository topics, for example, you could specify an empty list of topics here.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="topics">The list of topics to associate with the repository</param>
        /// <returns>All topics now associated with the repository.</returns>
        Task<RepositoryTopics> ReplaceAllTopics(string owner, string name, RepositoryTopics topics);

        /// <summary>
        /// Gets the list of errors in the codeowners file
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>Returns the list of errors in the codeowners files</returns>
        [ManualRoute("GET", "/repos/{owner}/{repo}/codeowners/errors")]
        Task<RepositoryCodeOwnersErrors> GetAllCodeOwnersErrors(string owner, string name);

        /// <summary>
        /// Gets the list of errors in the codeowners file
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>Returns the list of errors in the codeowners files</returns>
        [ManualRoute("GET", "/repositories/{id}/codeowners/errors")]
        Task<RepositoryCodeOwnersErrors> GetAllCodeOwnersErrors(long repositoryId);
    }
}