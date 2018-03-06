using Octokit.Reactive.Internal;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Threading.Tasks;
using System.Collections.Generic;

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
            Ensure.ArgumentNotNull(client, "client");

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
        /// <param name="repo">The name of the repository</param>
        public IObservable<Project> GetAllForRepository(string owner, string name)
        {
            return GetAllForRepository(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Get all projects for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        public IObservable<Project> GetAllForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            var url = ApiUrls.RepositoryProjects(owner, name);

            return _connection.GetAndFlattenAllPages<Project>(url, new Dictionary<string, string>(), AcceptHeaders.ProjectsApiPreview, options);
        }

        /// <summary>
        /// Get all projects for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="request">Used to filter the list of projects returned</param>
        public IObservable<Project> GetAllForRepository(string owner, string name, ProjectRequest request)
        {
            return GetAllForRepository(owner, name, request, ApiOptions.None);
        }

        /// <summary>
        /// Get all projects for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="request">Used to filter the list of projects returned</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Project> GetAllForRepository(string owner, string name, ProjectRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(request, "request");
            Ensure.ArgumentNotNull(options, "options");

            var url = ApiUrls.RepositoryProjects(owner, name);

            return _connection.GetAndFlattenAllPages<Project>(url, request.ToParametersDictionary(), AcceptHeaders.ProjectsApiPreview, options);
        }

        /// <summary>
        /// Get all projects for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        public IObservable<Project> GetAllForRepository(long repositoryId)
        {
            return GetAllForRepository(repositoryId, ApiOptions.None);
        }

        /// <summary>
        /// Get all projects for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Project> GetAllForRepository(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            var url = ApiUrls.RepositoryProjects(repositoryId);

            return _connection.GetAndFlattenAllPages<Project>(url, new Dictionary<string, string>(), AcceptHeaders.ProjectsApiPreview, options);
        }

        /// <summary>
        /// Get all projects for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter the list of projects returned</param>
        public IObservable<Project> GetAllForRepository(long repositoryId, ProjectRequest request)
        {
            return GetAllForRepository(repositoryId, request, ApiOptions.None);
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
        public IObservable<Project> GetAllForRepository(long repositoryId, ProjectRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, "request");
            Ensure.ArgumentNotNull(options, "options");

            var url = ApiUrls.RepositoryProjects(repositoryId);

            return _connection.GetAndFlattenAllPages<Project>(url, request.ToParametersDictionary(), AcceptHeaders.ProjectsApiPreview, options);
        }

        /// <summary>
        /// Get all projects for the specified organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-organization-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        public IObservable<Project> GetAllForOrganization(string organization)
        {
            return GetAllForOrganization(organization, ApiOptions.None);
        }

        /// <summary>
        /// Get all projects for the specified organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-organization-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="organization">The name of the organziation</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Project> GetAllForOrganization(string organization, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");
            Ensure.ArgumentNotNull(options, "options");

            var url = ApiUrls.OrganizationProjects(organization);

            return _connection.GetAndFlattenAllPages<Project>(url, new Dictionary<string, string>(), AcceptHeaders.ProjectsApiPreview, options);
        }

        /// <summary>
        /// Get all projects for the specified organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-organization-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="organization">The name of the organziation</param>
        /// <param name="request">Used to filter the list of projects returned</param>
        public IObservable<Project> GetAllForOrganization(string organization, ProjectRequest request)
        {
            return GetAllForOrganization(organization, request, ApiOptions.None);
        }

        /// <summary>
        /// Get all projects for the specified organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-organization-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="organization">The name of the organziation</param>
        /// <param name="request">Used to filter the list of projects returned</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Project> GetAllForOrganization(string organization, ProjectRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");
            Ensure.ArgumentNotNull(request, "request");
            Ensure.ArgumentNotNull(options, "options");

            var url = ApiUrls.OrganizationProjects(organization);

            return _connection.GetAndFlattenAllPages<Project>(url, request.ToParametersDictionary(), AcceptHeaders.ProjectsApiPreview, options);
        }

        /// <summary>
        /// Gets a single project for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#get-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The Id of the project</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        public IObservable<Project> Get(int id)
        {
            return _client.Get(id).ToObservable();
        }

        /// <summary>
        /// Creates a project for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#create-a-repository-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newProject">The new project to create for the specified repository</param>
        public IObservable<Project> CreateForRepository(long repositoryId, NewProject newProject)
        {
            Ensure.ArgumentNotNull(newProject, "newProject");

            return _client.CreateForRepository(repositoryId, newProject).ToObservable();
        }

        /// <summary>
        /// Creates a project for the specified organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#create-an-organization-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="newProject">The new project to create for the specified repository</param>
        public IObservable<Project> CreateForOrganization(string organization, NewProject newProject)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");
            Ensure.ArgumentNotNull(newProject, "newProject");

            return _client.CreateForOrganization(organization, newProject).ToObservable();
        }

        /// <summary>
        /// Updates a project for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#update-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The Id of the project</param>
        /// <param name="projectUpdate">The modified project</param>
        public IObservable<Project> Update(int id, ProjectUpdate projectUpdate)
        {
            Ensure.ArgumentNotNull(projectUpdate, "projectUpdate");

            return _client.Update(id, projectUpdate).ToObservable();
        }

        /// <summary>
        /// Deletes a project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#delete-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The Id of the project</param>
        public IObservable<bool> Delete(int id)
        {
            return _client.Delete(id).ToObservable();
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
