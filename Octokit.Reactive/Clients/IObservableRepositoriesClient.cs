using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;
using System.Threading.Tasks;

namespace Octokit.Reactive
{
    public interface IObservableRepositoriesClient
    {
        /// <summary>
        /// Creates a new repository for the current user.
        /// </summary>
        /// <param name="newRepository">A <see cref="NewRepository"/> instance describing the new repository to create</param>
        /// <returns>An <see cref="IObservable{Repository}"/> instance for the created repository</returns>
        IObservable<Repository> Create(NewRepository newRepository);

        /// <summary>
        /// Creates a new repository in the specified organization.
        /// </summary>
        /// <param name="organizationLogin">The login of the organization in which to create the repository</param>
        /// <param name="newRepository">A <see cref="NewRepository"/> instance describing the new repository to create</param>
        /// <returns>An <see cref="IObservable{Repository}"/> instance for the created repository</returns>
        IObservable<Repository> Create(string organizationLogin, NewRepository newRepository);

        /// <summary>
        /// Creates a new repository using a repository template.
        /// </summary>
        /// <param name="templateOwner">The owner of the template</param>
        /// <param name="templateRepo">The name of the template</param>
        /// <param name="newRepository">A <see cref="NewRepositoryFromTemplate"/> instance describing the new repository to create from a template</param>
        /// <returns>An <see cref="IObservable{Repository}"/> instance for the created repository</returns>
        IObservable<Repository> Generate(string templateOwner, string templateRepo, NewRepositoryFromTemplate newRepository);

        /// <summary>
        /// Deletes a repository for the specified owner and name.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <remarks>Deleting a repository requires admin access. If OAuth is used, the `delete_repo` scope is required.</remarks>
        /// <returns>An <see cref="IObservable{Unit}"/> for the operation</returns>
        IObservable<Unit> Delete(string owner, string name);

        /// <summary>
        /// Deletes a repository for the specified owner and name.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <remarks>Deleting a repository requires admin access. If OAuth is used, the `delete_repo` scope is required.</remarks>
        /// <returns>An <see cref="IObservable{Unit}"/> for the operation</returns>
        IObservable<Unit> Delete(long repositoryId);

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
        IObservable<Repository> Transfer(string owner, string name, RepositoryTransfer repositoryTransfer);

        /// <summary>
        /// Transfers the ownership of the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/#transfer-a-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="repositoryTransfer">Repository transfer information</param>
        /// <returns>A <see cref="Repository"/></returns>
        IObservable<Repository> Transfer(long repositoryId, RepositoryTransfer repositoryTransfer);

        /// <summary>
        /// Checks if vulnerability alerts are enabled for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/reference/repos#check-if-vulnerability-alerts-are-enabled-for-a-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The current owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>A <c>bool</c> indicating if alerts are turned on or not.</returns>
        IObservable<bool> AreVulnerabilityAlertsEnabled(string owner, string name);

        /// <summary>
        /// Retrieves the <see cref="Repository"/> for the specified owner and name.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>A <see cref="Repository"/></returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        IObservable<Repository> Get(string owner, string name);

        /// <summary>
        /// Retrieves the <see cref="Repository"/> for the specified owner and name.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>A <see cref="Repository"/></returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        IObservable<Repository> Get(long repositoryId);

        /// <summary>
        /// Retrieves every public <see cref="Repository"/>.
        /// </summary>
        /// <remarks>
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Makes a network request")]
        IObservable<Repository> GetAllPublic();

        /// <summary>
        /// Retrieves every public <see cref="Repository"/> since the last repository seen.
        /// </summary>
        /// <remarks>
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <param name="request">Search parameters of the last repository seen</param>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Makes a network request")]
        IObservable<Repository> GetAllPublic(PublicRepositoryRequest request);

        /// <summary>
        /// Retrieves every <see cref="Repository"/> that belongs to the current user.
        /// </summary>
        /// <remarks>
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Makes a network request")]
        IObservable<Repository> GetAllForCurrent();

        /// <summary>
        /// Retrieves every <see cref="Repository"/> that belongs to the current user.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        IObservable<Repository> GetAllForCurrent(ApiOptions options);

        /// <summary>
        /// Retrieves every <see cref="Repository"/> that belongs to the current user.
        /// </summary>
        /// <remarks>
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <param name="request">Search parameters to filter results on</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        IObservable<Repository> GetAllForCurrent(RepositoryRequest request);

        /// <summary>
        /// Retrieves every <see cref="Repository"/> that belongs to the current user.
        /// </summary>
        /// <param name="request">Search parameters to filter results on</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        IObservable<Repository> GetAllForCurrent(RepositoryRequest request, ApiOptions options);

        /// <summary>
        /// Retrieves every <see cref="Repository"/> that belongs to the specified user.
        /// </summary>
        /// <param name="login">The account name to search for</param>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        IObservable<Repository> GetAllForUser(string login);

        /// <summary>
        /// Retrieves every <see cref="Repository"/> that belongs to the specified user.
        /// </summary>
        /// <param name="login">The account name to search for</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        IObservable<Repository> GetAllForUser(string login, ApiOptions options);

        /// <summary>
        /// Retrieves every <see cref="Repository"/> that belongs to the specified organization.
        /// </summary>
        /// <remarks>
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        IObservable<Repository> GetAllForOrg(string organization);

        /// <summary>
        /// Retrieves every <see cref="Repository"/> that belongs to the specified organization.
        /// </summary>
        /// <param name="organization">The organization name to search for</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        IObservable<Repository> GetAllForOrg(string organization, ApiOptions options);

        /// <summary>
        /// Access GitHub's Repository Actions API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions">API documentation</a> for more details.
        /// </remarks>
        IObservableRepositoryActionsClient Actions { get; }

        /// <summary>
        /// Client for managing branches in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/">Branches API documentation</a> for more details
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        IObservableRepositoryBranchesClient Branch { get; }

        /// <summary>
        /// A client for GitHub's Commit Status API.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/statuses/">Commit Status API documentation</a> for more
        /// details. Also check out the <a href="https://github.com/blog/1227-commit-status-api">blog post</a>
        /// that announced this feature.
        /// </remarks>
        IObservableCommitStatusClient Status { get; }

        /// <summary>
        /// Client for GitHub's Repository Deployments API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/deployments/deployments">Deployments API documentation</a> for more details
        /// </remarks>
        IObservableDeploymentsClient Deployment { get; }

        /// <summary>
        /// Client for GitHub's Repository Envirinments API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/deployments/environments/">Envirinments API documentation</a> for more details
        /// </remarks>
        IObservableRepositoryDeployEnvironmentsClient Environment { get; }

        /// <summary>
        /// Client for GitHub's Repository Statistics API.
        /// Note that the GitHub API uses caching on these endpoints,
        /// see <a href="https://developer.github.com/v3/repos/statistics/#a-word-about-caching">a word about caching</a> for more details.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/statistics/">Statistics API documentation</a> for more details
        ///</remarks>
        IObservableStatisticsClient Statistics { get; }

        /// <summary>
        /// Client for GitHub's Repository Comments API.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/comments/">Repository Comments API documentation</a> for more information.
        /// </remarks>
        IObservableRepositoryCommentsClient Comment { get; }

        /// <summary>
        /// A client for GitHub's Repository Hooks API.
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/">Hooks API documentation</a> for more information.</remarks>
        IObservableRepositoryHooksClient Hooks { get; }

        /// <summary>
        /// A client for GitHub's Repository Forks API.
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/forks/">Forks API documentation</a> for more information.</remarks>
        IObservableRepositoryForksClient Forks { get; }

        /// <summary>
        /// Client for GitHub's Repository Contents API.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/contents/">Repository Contents API documentation</a> for more information.
        /// </remarks>
        IObservableRepositoryContentsClient Content { get; }

        /// <summary>
        /// Client for GitHub's Repository Merging API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/merging/">Merging API documentation</a> for more details
        ///</remarks>
        IObservableMergingClient Merging { get; }

        /// <summary>
        /// Gets all contributors for the specified repository. Does not include anonymous contributors.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-contributors">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All contributors of the repository.</returns>
        IObservable<RepositoryContributor> GetAllContributors(string owner, string name);

        /// <summary>
        /// Gets all contributors for the specified repository. Does not include anonymous contributors.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-contributors">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>All contributors of the repository.</returns>
        IObservable<RepositoryContributor> GetAllContributors(long repositoryId);

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
        IObservable<RepositoryContributor> GetAllContributors(string owner, string name, ApiOptions options);

        /// <summary>
        /// Gets all contributors for the specified repository. Does not include anonymous contributors.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-contributors">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>All contributors of the repository.</returns>
        IObservable<RepositoryContributor> GetAllContributors(long repositoryId, ApiOptions options);

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
        IObservable<RepositoryContributor> GetAllContributors(string owner, string name, bool includeAnonymous);

        /// <summary>
        /// Gets all contributors for the specified repository. With the option to include anonymous contributors.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-contributors">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="includeAnonymous">True if anonymous contributors should be included in result; Otherwise false</param>
        /// <returns>All contributors of the repository.</returns>
        IObservable<RepositoryContributor> GetAllContributors(long repositoryId, bool includeAnonymous);

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
        IObservable<RepositoryContributor> GetAllContributors(string owner, string name, bool includeAnonymous, ApiOptions options);

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
        IObservable<RepositoryContributor> GetAllContributors(long repositoryId, bool includeAnonymous, ApiOptions options);

        /// <summary>
        /// Gets all languages for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-languages">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All languages used in the repository and the number of bytes of each language.</returns>
        IObservable<RepositoryLanguage> GetAllLanguages(string owner, string name);

        /// <summary>
        /// Gets all languages for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-languages">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>All languages used in the repository and the number of bytes of each language.</returns>
        IObservable<RepositoryLanguage> GetAllLanguages(long repositoryId);

        /// <summary>
        /// Gets all teams for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-teams">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All <see cref="T:Octokit.Team"/>s associated with the repository</returns>
        IObservable<Team> GetAllTeams(string owner, string name);

        /// <summary>
        /// Gets all teams for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-teams">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>All <see cref="T:Octokit.Team"/>s associated with the repository</returns>
        IObservable<Team> GetAllTeams(long repositoryId);

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
        IObservable<Team> GetAllTeams(string owner, string name, ApiOptions options);

        /// <summary>
        /// Gets all teams for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-teams">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>All <see cref="T:Octokit.Team"/>s associated with the repository</returns>
        IObservable<Team> GetAllTeams(long repositoryId, ApiOptions options);

        /// <summary>
        /// Gets all tags for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-tags">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All of the repositories tags.</returns>
        IObservable<RepositoryTag> GetAllTags(string owner, string name);

        /// <summary>
        /// Gets all tags for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-tags">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>All of the repositories tags.</returns>
        IObservable<RepositoryTag> GetAllTags(long repositoryId);

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
        IObservable<RepositoryTag> GetAllTags(string owner, string name, ApiOptions options);

        /// <summary>
        /// Gets all tags for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-tags">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>All of the repositories tags.</returns>
        IObservable<RepositoryTag> GetAllTags(long repositoryId, ApiOptions options);

        /// <summary>
        /// Get the contents of a repository's license
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/licenses/#get-the-contents-of-a-repositorys-license">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>Returns the contents of the repository's license file, if one is detected.</returns>
        IObservable<RepositoryContentLicense> GetLicenseContents(string owner, string name);

        /// <summary>
        /// Get the contents of a repository's license
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/licenses/#get-the-contents-of-a-repositorys-license">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>Returns the contents of the repository's license file, if one is detected.</returns>
        IObservable<RepositoryContentLicense> GetLicenseContents(long repositoryId);

        /// <summary>
        /// Updates the specified repository with the values given in <paramref name="update"/>
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="update">New values to update the repository with</param>
        /// <returns>The updated <see cref="T:Octokit.Repository"/></returns>
        IObservable<Repository> Edit(string owner, string name, RepositoryUpdate update);

        /// <summary>
        /// Updates the specified repository with the values given in <paramref name="update"/>
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="update">New values to update the repository with</param>
        /// <returns>The updated <see cref="T:Octokit.Repository"/></returns>
        IObservable<Repository> Edit(long repositoryId, RepositoryUpdate update);

        /// <summary>
        /// A client for GitHub's Repo Collaborators.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/">Collaborators API documentation</a> for more details
        /// </remarks>
        IObservableRepoCollaboratorsClient Collaborator { get; }

        /// <summary>
        /// Client for GitHub's Repository Commits API
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/commits/">Commits API documentation</a> for more details
        ///</remarks>
        IObservableRepositoryCommitsClient Commit { get; }

        /// <summary>
        /// Access GitHub's Releases API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/repos/releases/
        /// </remarks>
        IObservableReleasesClient Release { get; }

        /// <summary>
        /// Client for managing pull requests.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/pulls/">Pull Requests API documentation</a> for more details
        /// </remarks>
        IObservablePullRequestsClient PullRequest { get; }

        /// <summary>
        /// Client for managing deploy keys
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/">Repository Deploy Keys API documentation</a> for more information.
        /// </remarks>
        IObservableRepositoryDeployKeysClient DeployKeys { get; }

        /// <summary>
        /// A client for GitHub's Repository Pages API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/">Repository Pages API documentation</a> for more information.
        /// </remarks>
        IObservableRepositoryPagesClient Page { get; }

        /// <summary>
        /// A client for GitHub's Repository Invitations API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/">Repository Invitations API documentation</a> for more information.
        /// </remarks>
        IObservableRepositoryInvitationsClient Invitation { get; }

        /// <summary>
        /// Access GitHub's Repository Traffic API
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/repos/traffic/
        /// </remarks>
        IObservableRepositoryTrafficClient Traffic { get; }

        /// <summary>
        /// Access GitHub's Repository Projects API
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/repos/projects/
        /// </remarks>
        IObservableProjectsClient Project { get; }

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
        IObservable<RepositoryTopics> GetAllTopics(string owner, string name, ApiOptions options);

        /// <summary>
        /// Gets all topics for the specified owner and repository name.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/reference/repos#get-all-repository-topics">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All topics associated with the repository.</returns>
        IObservable<RepositoryTopics> GetAllTopics(string owner, string name);

        /// <summary>
        /// Gets all topics for the specified repository ID.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/reference/repos#get-all-repository-topics">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>All topics associated with the repository.</returns>
        IObservable<RepositoryTopics> GetAllTopics(long repositoryId, ApiOptions options);

        /// <summary>
        /// Gets all topics for the specified repository ID.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/reference/repos#get-all-repository-topics">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <returns>All topics associated with the repository.</returns>
        IObservable<RepositoryTopics> GetAllTopics(long repositoryId);

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
        IObservable<RepositoryTopics> ReplaceAllTopics(long repositoryId, RepositoryTopics topics);

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
        IObservable<RepositoryTopics> ReplaceAllTopics(string owner, string name, RepositoryTopics topics);

        /// <summary>
        /// Gets the list of errors in the codeowners file
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>Returns the list of errors in the codeowners files</returns>
        [ManualRoute("GET", "/repos/{owner}/{repo}/codeowners/errors")]
        IObservable<RepositoryCodeOwnersErrors> GetAllCodeOwnersErrors(string owner, string name);

        /// <summary>
        /// Gets the list of errors in the codeowners file
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>Returns the list of errors in the codeowners files</returns>
        [ManualRoute("GET", "/repositories/{id}/codeowners/errors")]
        IObservable<RepositoryCodeOwnersErrors> GetAllCodeOwnersErrors(long repositoryId);
    }
}
