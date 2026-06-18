using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Clients;
using Octokit.Reactive.Internal;
using System.Threading;

namespace Octokit.Reactive
{
    public class ObservableRepositoriesClient : IObservableRepositoriesClient
    {
        readonly IRepositoriesClient _client;
        readonly IConnection _connection;

        public ObservableRepositoriesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Repository;
            _connection = client.Connection;
            Status = new ObservableCommitStatusClient(client);
            Hooks = new ObservableRepositoryHooksClient(client);
            Forks = new ObservableRepositoryForksClient(client);
            Collaborator = new ObservableRepoCollaboratorsClient(client);
            Deployment = new ObservableDeploymentsClient(client);
            Environment = new ObservableEnvironmentsClient(client);
            Statistics = new ObservableStatisticsClient(client);
            PullRequest = new ObservablePullRequestsClient(client);
            Branch = new ObservableRepositoryBranchesClient(client);
            Comment = new ObservableRepositoryCommentsClient(client);
            Commit = new ObservableRepositoryCommitsClient(client);
            CustomProperty = new ObservableRepositoryCustomPropertiesClient(client);
            Release = new ObservableReleasesClient(client);
            DeployKeys = new ObservableRepositoryDeployKeysClient(client);
            Content = new ObservableRepositoryContentsClient(client);
            Merging = new ObservableMergingClient(client);
            Page = new ObservableRepositoryPagesClient(client);
            Invitation = new ObservableRepositoryInvitationsClient(client);
            Traffic = new ObservableRepositoryTrafficClient(client);
            Project = new ObservableProjectsClient(client);
            Actions = new ObservableRepositoryActionsClient(client);
            Autolinks = new ObservableAutolinksClient(client);
        }

        /// <summary>
        /// Creates a new repository for the current user.
        /// </summary>
        /// <param name="newRepository">A <see cref="NewRepository"/> instance describing the new repository to create</param>
        /// <returns>An <see cref="IObservable{Repository}"/> instance for the created repository</returns>
        public IObservable<Repository> Create(NewRepository newRepository, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(newRepository, nameof(newRepository));
            if (string.IsNullOrEmpty(newRepository.Name))
                throw new ArgumentException("The new repository's name must not be null.");

            return _client.Create(newRepository, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Creates a new repository in the specified organization.
        /// </summary>
        /// <param name="organizationLogin">The login of the organization in which to create the repository</param>
        /// <param name="newRepository">A <see cref="NewRepository"/> instance describing the new repository to create</param>
        /// <returns>An <see cref="IObservable{Repository}"/> instance for the created repository</returns>
        public IObservable<Repository> Create(string organizationLogin, NewRepository newRepository, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(organizationLogin, nameof(organizationLogin));
            Ensure.ArgumentNotNull(newRepository, nameof(newRepository));
            if (string.IsNullOrEmpty(newRepository.Name))
                throw new ArgumentException("The new repository's name must not be null.");

            return _client.Create(organizationLogin, newRepository, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Creates a new repository from a template
        /// </summary>
        /// <param name="templateOwner">The organization or person who will owns the template</param>
        /// <param name="templateRepo">The name of template repository to work from</param>
        /// <param name="newRepository">A <see cref="NewRepositoryFromTemplate"/> instance describing the new repository to create from a template</param>
        /// <returns></returns>
        public IObservable<Repository> Generate(string templateOwner, string templateRepo, NewRepositoryFromTemplate newRepository, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(templateOwner, nameof(templateOwner));
            Ensure.ArgumentNotNull(templateRepo, nameof(templateRepo));
            Ensure.ArgumentNotNull(newRepository, nameof(newRepository));
            if (string.IsNullOrEmpty(newRepository.Name))
                throw new ArgumentException("The new repository's name must not be null.");

            return _client.Generate(templateOwner, templateRepo, newRepository, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Deletes a repository for the specified owner and name.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <remarks>Deleting a repository requires admin access. If OAuth is used, the `delete_repo` scope is required.</remarks>
        /// <returns>An <see cref="IObservable{Unit}"/> for the operation</returns>
        public IObservable<Unit> Delete(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Delete(owner, name, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Deletes a repository for the specified owner and name.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <remarks>Deleting a repository requires admin access. If OAuth is used, the `delete_repo` scope is required.</remarks>
        /// <returns>An <see cref="IObservable{Unit}"/> for the operation</returns>
        public IObservable<Unit> Delete(long repositoryId, CancellationToken cancellationToken = default)
        {
            return _client.Delete(repositoryId, cancellationToken).ToObservable();
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
        public IObservable<Repository> Transfer(string owner, string name, RepositoryTransfer repositoryTransfer, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(repositoryTransfer, nameof(repositoryTransfer));

            return _client.Transfer(owner, name, repositoryTransfer, cancellationToken).ToObservable();
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
        public IObservable<Repository> Transfer(long repositoryId, RepositoryTransfer repositoryTransfer, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(repositoryTransfer, nameof(repositoryTransfer));

            return _client.Transfer(repositoryId, repositoryTransfer, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Checks if vulnerability alerts are enabled for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/reference/repos#check-if-vulnerability-alerts-are-enabled-for-a-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The current owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>A <c>bool</c> indicating if alerts are turned on or not.</returns>
        public IObservable<bool> AreVulnerabilityAlertsEnabled(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.AreVulnerabilityAlertsEnabled(owner, name, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Retrieves the <see cref="Repository"/> for the specified owner and name.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>A <see cref="Repository"/></returns>
        public IObservable<Repository> Get(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Get(owner, name, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Retrieves the <see cref="Repository"/> for the specified owner and name.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>A <see cref="Repository"/></returns>
        public IObservable<Repository> Get(long repositoryId, CancellationToken cancellationToken = default)
        {
            return _client.Get(repositoryId, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Retrieves every public <see cref="Repository"/>.
        /// </summary>
        /// <remarks>
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        public IObservable<Repository> GetAllPublic(CancellationToken cancellationToken = default)
        {
            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.AllPublicRepositories(), cancellationToken);
        }

        /// <summary>
        /// Retrieves every public <see cref="Repository"/> since the last repository seen.
        /// </summary>
        /// <remarks>
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <param name="request">Search parameters of the last repository seen</param>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        public IObservable<Repository> GetAllPublic(PublicRepositoryRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            var url = ApiUrls.AllPublicRepositories(request.Since);

            return _connection.GetAndFlattenAllPages<Repository>(url, cancellationToken);
        }

        /// <summary>
        /// Retrieves every <see cref="Repository"/> that belongs to the current user.
        /// </summary>
        /// <remarks>
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        public IObservable<Repository> GetAllForCurrent(CancellationToken cancellationToken = default)
        {
            return GetAllForCurrent(ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Retrieves every <see cref="Repository"/> that belongs to the current user.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        public IObservable<Repository> GetAllForCurrent(ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.Repositories(), options, cancellationToken);
        }

        /// <summary>
        /// Retrieves every <see cref="Repository"/> that belongs to the current user.
        /// </summary>
        /// <remarks>
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <param name="request">Search parameters to filter results on</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        public IObservable<Repository> GetAllForCurrent(RepositoryRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForCurrent(request, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Retrieves every <see cref="Repository"/> that belongs to the current user.
        /// </summary>
        /// <param name="request">Search parameters to filter results on</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        public IObservable<Repository> GetAllForCurrent(RepositoryRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.Repositories(), request.ToParametersDictionary(), cancellationToken);
        }

        /// <summary>
        /// Retrieves every <see cref="Repository"/> that belongs to the specified user.
        /// </summary>
        /// <param name="login">The account name to search for</param>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        public IObservable<Repository> GetAllForUser(string login, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, nameof(login));

            return GetAllForUser(login, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Retrieves every <see cref="Repository"/> that belongs to the specified user.
        /// </summary>
        /// <param name="login">The account name to search for</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        public IObservable<Repository> GetAllForUser(string login, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, nameof(login));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.Repositories(login), null, options, cancellationToken);
        }

        /// <summary>
        /// Retrieves every <see cref="Repository"/> that belongs to the specified organization.
        /// </summary>
        /// <remarks>
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        public IObservable<Repository> GetAllForOrg(string organization, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));

            return GetAllForOrg(organization, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Retrieves every <see cref="Repository"/> that belongs to the specified organization.
        /// </summary>
        /// <param name="organization">The organization name to search for</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        public IObservable<Repository> GetAllForOrg(string organization, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.OrganizationRepositories(organization), options, cancellationToken);
        }

        /// <summary>
        /// A client for GitHub's Commit Status API.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/statuses/">Commit Status API documentation</a> for more
        /// details. Also check out the <a href="https://github.com/blog/1227-commit-status-api">blog post</a>
        /// that announced this feature.
        /// </remarks>
        public IObservableCommitStatusClient Status { get; private set; }

        /// <summary>
        /// Client for GitHub's Repository Deployments API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/deployments/deployments">Deployments API documentation</a> for more details
        /// </remarks>
        public IObservableDeploymentsClient Deployment { get; private set; }


        /// <summary>
        /// Client for GitHub's Repository Environments API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/deployments/environments#about-the-deployment-environments-api/">Environments API documentation</a> for more details
        /// </remarks>
        public IObservableRepositoryDeployEnvironmentsClient Environment { get; private set; }

        /// <summary>
        /// Client for GitHub's Repository Statistics API
        /// Note that the GitHub API uses caching on these endpoints,
        /// see <a href="https://developer.github.com/v3/repos/statistics/#a-word-about-caching">a word about caching</a> for more details.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/statistics/">Statistics API documentation</a> for more details
        ///</remarks>
        public IObservableStatisticsClient Statistics { get; private set; }

        /// <summary>
        /// Client for GitHub's Repository Comments API.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/comments/">Repository Comments API documentation</a> for more information.
        /// </remarks>
        public IObservableRepositoryCommentsClient Comment { get; private set; }

        /// <summary>
        /// Client for GitHub's Repository Custom Property Values API.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/custom-properties/">Repository Custom Property API documentation</a> for more information.
        /// </remarks>
        public IObservableRepositoryCustomPropertiesClient CustomProperty { get; private set; }

        /// <summary>
        /// A client for GitHub's Repository Hooks API.
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/hooks/">Hooks API documentation</a> for more information.</remarks>
        public IObservableRepositoryHooksClient Hooks { get; private set; }

        /// <summary>
        /// A client for GitHub's Repository Forks API.
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/forks/">Forks API documentation</a> for more information.</remarks>
        public IObservableRepositoryForksClient Forks { get; private set; }

        /// <summary>
        /// Client for GitHub's Repository Contents API.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/contents/">Repository Contents API documentation</a> for more information.
        /// </remarks>
        public IObservableRepositoryContentsClient Content { get; private set; }


        /// <summary>
        /// Client for GitHub's Repository Merging API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/merging/">Merging API documentation</a> for more details
        ///</remarks>
        public IObservableMergingClient Merging { get; private set; }

        /// <summary>
        /// Gets all contributors for the specified repository. Does not include anonymous contributors.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-contributors">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All contributors of the repository.</returns>
        public IObservable<RepositoryContributor> GetAllContributors(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllContributors(owner, name, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all contributors for the specified repository. Does not include anonymous contributors.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-contributors">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>All contributors of the repository.</returns>
        public IObservable<RepositoryContributor> GetAllContributors(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAllContributors(repositoryId, ApiOptions.None, cancellationToken);
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
        public IObservable<RepositoryContributor> GetAllContributors(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAllContributors(owner, name, false, options, cancellationToken);
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
        public IObservable<RepositoryContributor> GetAllContributors(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAllContributors(repositoryId, false, options, cancellationToken);
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
        public IObservable<RepositoryContributor> GetAllContributors(string owner, string name, bool includeAnonymous, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllContributors(owner, name, includeAnonymous, ApiOptions.None, cancellationToken);
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
        public IObservable<RepositoryContributor> GetAllContributors(long repositoryId, bool includeAnonymous, CancellationToken cancellationToken = default)
        {
            return GetAllContributors(repositoryId, includeAnonymous, ApiOptions.None, cancellationToken);
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
        public IObservable<RepositoryContributor> GetAllContributors(string owner, string name, bool includeAnonymous, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.RepositoryContributors(owner, name);
            var parameters = new Dictionary<string, string>();
            if (includeAnonymous)
                parameters.Add("anon", "1");

            return _connection.GetAndFlattenAllPages<RepositoryContributor>(endpoint, parameters, options, cancellationToken);
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
        public IObservable<RepositoryContributor> GetAllContributors(long repositoryId, bool includeAnonymous, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.RepositoryContributors(repositoryId);
            var parameters = new Dictionary<string, string>();
            if (includeAnonymous)
                parameters.Add("anon", "1");

            return _connection.GetAndFlattenAllPages<RepositoryContributor>(endpoint, parameters, options, cancellationToken);
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
        public IObservable<RepositoryLanguage> GetAllLanguages(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            var endpoint = ApiUrls.RepositoryLanguages(owner, name);
            return _connection
                .GetAndFlattenAllPages<Tuple<string, long>>(endpoint, cancellationToken)
                .Select(t => new RepositoryLanguage(t.Item1, t.Item2));
        }

        /// <summary>
        /// Gets all languages for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-languages">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>All languages used in the repository and the number of bytes of each language.</returns>
        public IObservable<RepositoryLanguage> GetAllLanguages(long repositoryId, CancellationToken cancellationToken = default)
        {
            var endpoint = ApiUrls.RepositoryLanguages(repositoryId);
            return _connection
                .GetAndFlattenAllPages<Tuple<string, long>>(endpoint, cancellationToken)
                .Select(t => new RepositoryLanguage(t.Item1, t.Item2));
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
        public IObservable<Team> GetAllTeams(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllTeams(owner, name, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all teams for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-teams">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>All <see cref="T:Octokit.Team"/>s associated with the repository</returns>
        public IObservable<Team> GetAllTeams(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAllTeams(repositoryId, ApiOptions.None, cancellationToken);
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
        public IObservable<Team> GetAllTeams(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Team>(ApiUrls.RepositoryTeams(owner, name), options, cancellationToken);
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
        public IObservable<Team> GetAllTeams(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Team>(ApiUrls.RepositoryTeams(repositoryId), options, cancellationToken);
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
        public IObservable<RepositoryTag> GetAllTags(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllTags(owner, name, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all tags for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-tags">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>All of the repositories tags.</returns>
        public IObservable<RepositoryTag> GetAllTags(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAllTags(repositoryId, ApiOptions.None, cancellationToken);
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
        public IObservable<RepositoryTag> GetAllTags(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<RepositoryTag>(ApiUrls.RepositoryTags(owner, name), options, cancellationToken);
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
        public IObservable<RepositoryTag> GetAllTags(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<RepositoryTag>(ApiUrls.RepositoryTags(repositoryId), options, cancellationToken);
        }

        /// <summary>
        /// Updates the specified repository with the values given in <paramref name="update"/>
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="update">New values to update the repository with</param>
        /// <returns>The updated <see cref="T:Octokit.Repository"/></returns>
        public IObservable<Repository> Edit(string owner, string name, RepositoryUpdate update, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(update, nameof(update));

            return _client.Edit(owner, name, update, cancellationToken).ToObservable();
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
        public IObservable<RepositoryContentLicense> GetLicenseContents(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.GetLicenseContents(owner, name, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Get the contents of a repository's license
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/licenses/#get-the-contents-of-a-repositorys-license">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>Returns the contents of the repository's license file, if one is detected.</returns>
        public IObservable<RepositoryContentLicense> GetLicenseContents(long repositoryId, CancellationToken cancellationToken = default)
        {
            return _client.GetLicenseContents(repositoryId, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets the list of errors in the codeowners file
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>Returns the list of errors in the codeowners files</returns>
        [ManualRoute("GET", "/repos/{owner}/{repo}/codeowners/errors")]
        public IObservable<RepositoryCodeOwnersErrors> GetAllCodeOwnersErrors(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.GetAllCodeOwnersErrors(owner, name, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets the list of errors in the codeowners file
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns>Returns the list of errors in the codeowners files</returns>
        [ManualRoute("GET", "/repositories/{id}/codeowners/errors")]
        public IObservable<RepositoryCodeOwnersErrors> GetAllCodeOwnersErrors(long repositoryId, CancellationToken cancellationToken = default)
        {
            return _client.GetAllCodeOwnersErrors(repositoryId, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Updates the specified repository with the values given in <paramref name="update"/>
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="update">New values to update the repository with</param>
        /// <returns>The updated <see cref="T:Octokit.Repository"/></returns>
        public IObservable<Repository> Edit(long repositoryId, RepositoryUpdate update, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(update, nameof(update));

            return _client.Edit(repositoryId, update, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Compare two references in a repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="base">The reference to use as the base commit</param>
        /// <param name="head">The reference to use as the head commit</param>
        /// <returns></returns>
        public IObservable<CompareResult> Compare(string owner, string name, string @base, string head, CancellationToken cancellationToken = default)
        {
            return _client.Commit.Compare(owner, name, @base, head, cancellationToken).ToObservable();
        }

        /// <summary>
        /// A client for GitHub's Repository Actions API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions">Actions API documentation</a> for more details
        /// </remarks>
        public IObservableRepositoryActionsClient Actions { get; private set; }

        /// <summary>
        /// A client for GitHub's Repository Autolinks API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/repos/autolinks">API documentation</a> for more information.
        /// </remarks>
        public IObservableAutolinksClient Autolinks { get; private set; }

        /// <summary>
        /// A client for GitHub's Repository Branches API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/">Branches API documentation</a> for more details
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public IObservableRepositoryBranchesClient Branch { get; private set; }

        /// <summary>
        /// A client for GitHub's Repo Collaborators.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/">Collaborators API documentation</a> for more details
        /// </remarks>
        public IObservableRepoCollaboratorsClient Collaborator { get; private set; }

        /// <summary>
        /// Client for GitHub's Repository Commits API
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/commits/">Commits API documentation</a> for more details
        ///</remarks>
        public IObservableRepositoryCommitsClient Commit { get; private set; }

        /// <summary>
        /// Access GitHub's Releases API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/repos/releases/
        /// </remarks>
        public IObservableReleasesClient Release { get; private set; }

        /// <summary>
        /// Client for managing pull requests.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/pulls/">Pull Requests API documentation</a> for more details
        /// </remarks>
        public IObservablePullRequestsClient PullRequest { get; private set; }

        /// <summary>
        /// Client for managing deploy keys
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/">Repository Deploy Keys API documentation</a> for more information.
        /// </remarks>
        public IObservableRepositoryDeployKeysClient DeployKeys { get; private set; }
        /// <summary>
        /// A client for GitHub's Repository Pages API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/">Repository Pages API documentation</a> for more information.
        /// </remarks>
        public IObservableRepositoryPagesClient Page { get; private set; }

        /// <summary>
        /// A client for GitHub's Repository Invitations API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/">Repository Invitations API documentation</a> for more information.
        /// </remarks>
        public IObservableRepositoryInvitationsClient Invitation { get; private set; }

        /// <summary>
        /// Access GitHub's Repository Traffic API
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/repos/traffic/
        /// </remarks>
        public IObservableRepositoryTrafficClient Traffic { get; private set; }

        /// <summary>
        /// Access GitHub's Repository Projects API
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/repos/projects/
        /// </remarks>
        public IObservableProjectsClient Project { get; private set; }

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
        public IObservable<RepositoryTopics> GetAllTopics(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default)
        {
            return _client.GetAllTopics(owner, name, options, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets all topics for the specified owner and repository name.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/reference/repos#get-all-repository-topics">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All topics associated with the repository.</returns>
        public IObservable<RepositoryTopics> GetAllTopics(string owner, string name, CancellationToken cancellationToken = default)
        {
            return GetAllTopics(owner, name, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all topics for the specified repository ID.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/reference/repos#get-all-repository-topics">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>All topics associated with the repository.</returns>
        public IObservable<RepositoryTopics> GetAllTopics(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            return _client.GetAllTopics(repositoryId, options, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets all topics for the specified repository ID.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/reference/repos#get-all-repository-topics">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <returns>All topics associated with the repository.</returns>
        public IObservable<RepositoryTopics> GetAllTopics(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAllTopics(repositoryId, ApiOptions.None, cancellationToken);
        }

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
        public IObservable<RepositoryTopics> ReplaceAllTopics(long repositoryId, RepositoryTopics topics, CancellationToken cancellationToken = default)
        {
            return _client.ReplaceAllTopics(repositoryId, topics, cancellationToken).ToObservable();
        }

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
        public IObservable<RepositoryTopics> ReplaceAllTopics(string owner, string name, RepositoryTopics topics, CancellationToken cancellationToken = default)
        {
            return _client.ReplaceAllTopics(owner, name, topics, cancellationToken).ToObservable();
        }
    }
}
