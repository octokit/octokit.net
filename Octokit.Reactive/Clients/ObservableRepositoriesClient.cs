﻿using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Clients;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableRepositoriesClient : IObservableRepositoriesClient
    {
        readonly IRepositoriesClient _client;
        readonly IConnection _connection;

        public ObservableRepositoriesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Repository;
            _connection = client.Connection;
            Status = new ObservableCommitStatusClient(client);
            Hooks = new ObservableRepositoryHooksClient(client);
            Forks = new ObservableRepositoryForksClient(client);
#pragma warning disable CS0618 // Type or member is obsolete
            RepoCollaborators = new ObservableRepoCollaboratorsClient(client);
#pragma warning restore CS0618 // Type or member is obsolete
            Collaborator = new ObservableRepoCollaboratorsClient(client);
            Deployment = new ObservableDeploymentsClient(client);
            Statistics = new ObservableStatisticsClient(client);
            PullRequest = new ObservablePullRequestsClient(client);
#pragma warning disable CS0618 // Type or member is obsolete
            RepositoryComments = new ObservableRepositoryCommentsClient(client);
#pragma warning restore CS0618 // Type or member is obsolete
            Comment = new ObservableRepositoryCommentsClient(client);
#pragma warning disable CS0618 // Type or member is obsolete
            Commits = new ObservableRepositoryCommitsClient(client);
#pragma warning restore CS0618 // Type or member is obsolete
            Commit = new ObservableRepositoryCommitsClient(client);
            Release = new ObservableReleasesClient(client);
            DeployKeys = new ObservableRepositoryDeployKeysClient(client);
            Content = new ObservableRepositoryContentsClient(client);
            Merging = new ObservableMergingClient(client);
            Page = new ObservableRepositoryPagesClient(client);
        }

        /// <summary>
        /// Creates a new repository for the current user.
        /// </summary>
        /// <param name="newRepository">A <see cref="NewRepository"/> instance describing the new repository to create</param>
        /// <returns>An <see cref="IObservable{Repository}"/> instance for the created repository</returns>
        public IObservable<Repository> Create(NewRepository newRepository)
        {
            Ensure.ArgumentNotNull(newRepository, "newRepository");
            if (string.IsNullOrEmpty(newRepository.Name))
                throw new ArgumentException("The new repository's name must not be null.");

            return _client.Create(newRepository).ToObservable();
        }

        /// <summary>
        /// Creates a new repository in the specified organization.
        /// </summary>
        /// <param name="organizationLogin">The login of the organization in which to create the repostiory</param>
        /// <param name="newRepository">A <see cref="NewRepository"/> instance describing the new repository to create</param>
        /// <returns>An <see cref="IObservable{Repository}"/> instance for the created repository</returns>
        public IObservable<Repository> Create(string organizationLogin, NewRepository newRepository)
        {
            Ensure.ArgumentNotNull(organizationLogin, "organizationLogin");
            Ensure.ArgumentNotNull(newRepository, "newRepository");
            if (string.IsNullOrEmpty(newRepository.Name))
                throw new ArgumentException("The new repository's name must not be null.");

            return _client.Create(organizationLogin, newRepository).ToObservable();
        }

        /// <summary>
        /// Deletes a repository for the specified owner and name.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <remarks>Deleting a repository requires admin access. If OAuth is used, the `delete_repo` scope is required.</remarks>
        /// <returns>An <see cref="IObservable{Unit}"/> for the operation</returns>
        public IObservable<Unit> Delete(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.Delete(owner, name).ToObservable();
        }

        /// <summary>
        /// Retrieves the <see cref="Repository"/> for the specified owner and name.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>A <see cref="Repository"/></returns>
        public IObservable<Repository> Get(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.Get(owner, name).ToObservable();
        }

        /// <summary>
        /// Retrieves every public <see cref="Repository"/>.
        /// </summary>
        /// <remarks>
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        public IObservable<Repository> GetAllPublic()
        {
            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.AllPublicRepositories());
        }

        /// <summary>
        /// Retrieves every public <see cref="Repository"/> since the last repository seen.
        /// </summary>
        /// <remarks>
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <param name="request">Search parameters of the last repository seen</param>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        public IObservable<Repository> GetAllPublic(PublicRepositoryRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            var url = ApiUrls.AllPublicRepositories(request.Since);

            return _connection.GetAndFlattenAllPages<Repository>(url);
        }

        /// <summary>
        /// Retrieves every <see cref="Repository"/> that belongs to the current user.
        /// </summary>
        /// <remarks>
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        public IObservable<Repository> GetAllForCurrent()
        {
            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.Repositories());
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
        public IObservable<Repository> GetAllForCurrent(RepositoryRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.Repositories(), request.ToParametersDictionary());
        }

        /// <summary>
        /// Retrieves every <see cref="Repository"/> that belongs to the specified user.
        /// </summary>
        /// <remarks>
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        public IObservable<Repository> GetAllForUser(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.Repositories(login));
        }

        /// <summary>
        /// Retrieves every <see cref="Repository"/> that belongs to the specified organization.
        /// </summary>
        /// <remarks>
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        public IObservable<Repository> GetAllForOrg(string organization)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");

            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.OrganizationRepositories(organization));
        }

        /// <summary>
        /// A client for GitHub's Commit Status API.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/statuses/">Commit Status API documentation</a> for more
        /// details. Also check out the <a href="https://github.com/blog/1227-commit-status-api">blog post</a> 
        /// that announced this feature.
        /// </remarks>
        [Obsolete("Use Status instead")]
        public IObservableCommitStatusClient CommitStatus { get { return Status; } }

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
        /// See the <a href="http://developer.github.com/v3/repos/deployment/">Collaborators API documentation</a> for more details
        /// </remarks>
        public IObservableDeploymentsClient Deployment { get; private set; }

        /// <summary>
        /// Client for GitHub's Repository Statistics API
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
        [Obsolete("Comment information is now available under the Comment property. This will be removed in a future update.")]
        public IObservableRepositoryCommentsClient RepositoryComments { get; private set; }

        /// <summary>
        /// Client for GitHub's Repository Comments API.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/comments/">Repository Comments API documentation</a> for more information.
        /// </remarks>
        public IObservableRepositoryCommentsClient Comment { get; private set; }

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
        /// Gets all the branches for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-branches">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>All <see cref="T:Octokit.Branch"/>es of the repository</returns>
        public IObservable<Branch> GetAllBranches(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = ApiUrls.RepoBranches(owner, name);
            return _connection.GetAndFlattenAllPages<Branch>(endpoint);
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
        public IObservable<RepositoryContributor> GetAllContributors(string owner, string name)
        {
            return GetAllContributors(owner, name, false);
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
        public IObservable<RepositoryContributor> GetAllContributors(string owner, string name, bool includeAnonymous)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = ApiUrls.RepositoryContributors(owner, name);
            var parameters = new Dictionary<string, string>();
            if (includeAnonymous)
                parameters.Add("anon", "1");

            return _connection.GetAndFlattenAllPages<RepositoryContributor>(endpoint, parameters);
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
        public IObservable<RepositoryLanguage> GetAllLanguages(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = ApiUrls.RepositoryLanguages(owner, name);
            return _connection
                .GetAndFlattenAllPages<Tuple<string, long>>(endpoint)
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
        public IObservable<Team> GetAllTeams(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = ApiUrls.RepositoryTeams(owner, name);
            return _connection.GetAndFlattenAllPages<Team>(endpoint);
        }

        /// <summary>
        /// Gets all tags for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#list-tags">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All of the repositorys tags.</returns>
        public IObservable<RepositoryTag> GetAllTags(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = ApiUrls.RepositoryTags(owner, name);
            return _connection.GetAndFlattenAllPages<RepositoryTag>(endpoint);
        }

        /// <summary>
        /// Gets the specified branch.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/#get-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <param name="branchName">The name of the branch</param>
        /// <returns>The specified <see cref="T:Octokit.Branch"/></returns>
        public IObservable<Branch> GetBranch(string owner, string repositoryName, string branchName)
        {
            return _client.GetBranch(owner, repositoryName, branchName).ToObservable();
        }

        /// <summary>
        /// Updates the specified repository with the values given in <paramref name="update"/>
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="update">New values to update the repository with</param>
        /// <returns>The updated <see cref="T:Octokit.Repository"/></returns>
        public IObservable<Repository> Edit(string owner, string name, RepositoryUpdate update)
        {
            return _client.Edit(owner, name, update).ToObservable();
        }

        /// <summary>
        /// Edit the specified branch with the values given in <paramref name="update"/>
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="update">New values to update the branch with</param>
        /// <returns>The updated <see cref="T:Octokit.Branch"/></returns>
        public IObservable<Branch> EditBranch(string owner, string name, string branch, BranchUpdate update)
        {
            return _client.EditBranch(owner, name, branch, update).ToObservable();
        }

        /// <summary>
        /// Compare two references in a repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="base">The reference to use as the base commit</param>
        /// <param name="head">The reference to use as the head commit</param>
        /// <returns></returns>
        public IObservable<CompareResult> Compare(string owner, string name, string @base, string head)
        {
            return _client.Commit.Compare(owner, name, @base, head).ToObservable();
        }

        /// <summary>
        /// A client for GitHub's Repo Collaborators.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/">Collaborators API documentation</a> for more details
        /// </remarks>
        [Obsolete("Collaborator information is now available under the Collaborator property. This will be removed in a future update.")]
        public IObservableRepoCollaboratorsClient RepoCollaborators { get; private set; }

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
        [Obsolete("Commit information is now available under the Commit property. This will be removed in a future update.")]
        public IObservableRepositoryCommitsClient Commits { get; private set; }

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
    }
}
