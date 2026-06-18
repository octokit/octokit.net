using Octokit.Reactive.Internal;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Repository Projects API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/projects/">Repository Projects API documentation</a> for more information.
    /// </remarks>
    public class ObservableProjectsClient : IObservableProjectsClient
    {
        readonly IProjectsClient _client;
        readonly IConnection _connection;

        public ObservableProjectsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Repository.Project;
            _connection = client.Connection;
            Card = new ObservableProjectCardsClient(client);
            Column = new ObservableProjectColumnsClient(client);
        }

        /// <summary>
        /// Get all projects for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public IObservable<Project> GetAllForRepository(string owner, string name, CancellationToken cancellationToken = default)
        {
            return GetAllForRepository(owner, name, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Get all projects for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Project> GetAllForRepository(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            var url = ApiUrls.RepositoryProjects(owner, name);

            return _connection.GetAndFlattenAllPages<Project>(url, new Dictionary<string, string>(), options);
        }

        /// <summary>
        /// Get all projects for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter the list of projects returned</param>
        public IObservable<Project> GetAllForRepository(string owner, string name, ProjectRequest request, CancellationToken cancellationToken = default)
        {
            return GetAllForRepository(owner, name, request, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Get all projects for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter the list of projects returned</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Project> GetAllForRepository(string owner, string name, ProjectRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            var url = ApiUrls.RepositoryProjects(owner, name);

            return _connection.GetAndFlattenAllPages<Project>(url, request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Get all projects for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        public IObservable<Project> GetAllForRepository(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAllForRepository(repositoryId, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Get all projects for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Project> GetAllForRepository(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var url = ApiUrls.RepositoryProjects(repositoryId);

            return _connection.GetAndFlattenAllPages<Project>(url, new Dictionary<string, string>(), options);
        }

        /// <summary>
        /// Get all projects for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter the list of projects returned</param>
        public IObservable<Project> GetAllForRepository(long repositoryId, ProjectRequest request, CancellationToken cancellationToken = default)
        {
            return GetAllForRepository(repositoryId, request, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Get all projects for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter the list of projects returned</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Project> GetAllForRepository(long repositoryId, ProjectRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            var url = ApiUrls.RepositoryProjects(repositoryId);

            return _connection.GetAndFlattenAllPages<Project>(url, request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Get all projects for the specified organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-organization-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        public IObservable<Project> GetAllForOrganization(string organization, CancellationToken cancellationToken = default)
        {
            return GetAllForOrganization(organization, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Get all projects for the specified organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-organization-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Project> GetAllForOrganization(string organization, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNull(options, nameof(options));

            var url = ApiUrls.OrganizationProjects(organization);

            return _connection.GetAndFlattenAllPages<Project>(url, new Dictionary<string, string>(), options);
        }

        /// <summary>
        /// Get all projects for the specified organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-organization-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="request">Used to filter the list of projects returned</param>
        public IObservable<Project> GetAllForOrganization(string organization, ProjectRequest request, CancellationToken cancellationToken = default)
        {
            return GetAllForOrganization(organization, request, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Get all projects for the specified organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-organization-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="request">Used to filter the list of projects returned</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Project> GetAllForOrganization(string organization, ProjectRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            var url = ApiUrls.OrganizationProjects(organization);

            return _connection.GetAndFlattenAllPages<Project>(url, request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Gets a single project for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#get-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The Id of the project</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        public IObservable<Project> Get(int id, CancellationToken cancellationToken = default)
        {
            return _client.Get(id, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Creates a project for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#create-a-repository-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newProject">The new project to create for the specified repository</param>
        public IObservable<Project> CreateForRepository(long repositoryId, NewProject newProject, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(newProject, nameof(newProject));

            return _client.CreateForRepository(repositoryId, newProject, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Creates a project for the specified organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#create-an-organization-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="newProject">The new project to create for the specified repository</param>
        public IObservable<Project> CreateForOrganization(string organization, NewProject newProject, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNull(newProject, nameof(newProject));

            return _client.CreateForOrganization(organization, newProject, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Updates a project for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#update-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="projectId">The Id of the project</param>
        /// <param name="projectUpdate">The modified project</param>
        public IObservable<Project> Update(int projectId, ProjectUpdate projectUpdate, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(projectUpdate, nameof(projectUpdate));

            return _client.Update(projectId, projectUpdate, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Deletes a project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#delete-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="projectId">The Id of the project</param>
        public IObservable<bool> Delete(int projectId, CancellationToken cancellationToken = default)
        {
            return _client.Delete(projectId, cancellationToken).ToObservable();
        }

        /// <summary>
        /// A client for GitHub's Project Cards API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/cards/">Repository Projects API documentation</a> for more information.
        /// </remarks>
        public IObservableProjectCardsClient Card { get; private set; }

        /// <summary>
        /// A client for GitHub's Project Columns API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/columns/">Repository Projects API documentation</a> for more information.
        /// </remarks>
        public IObservableProjectColumnsClient Column { get; private set; }
    }
}
