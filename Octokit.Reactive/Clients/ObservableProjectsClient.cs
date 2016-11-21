using Octokit.Reactive.Internal;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Threading.Tasks;

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
            Cards = new ObservableProjectCardsClient(client);
            Columns = new ObservableProjectColumnsClient(client);
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
            var url = ApiUrls.RepositoryProjects(repositoryId);

            return _connection.GetAndFlattenAllPages<Project>(url);
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
            var url = ApiUrls.OrganizationProjects(organization);

            return _connection.GetAndFlattenAllPages<Project>(url);
        }

        /// <summary>
        /// Gets a single project for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#get-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the project</param>
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
        /// <param name="newProject">The new project to create for this repository</param>
        public IObservable<Project> CreateForRepository(long repositoryId, NewProject newProject)
        {
            Ensure.ArgumentNotNull(newProject, "newRepositoryProject");

            return _client.CreateForRepository(repositoryId, newProject).ToObservable();
        }

        /// <summary>
        /// Creates a project for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#create-a-repository-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="newProject">The new project to create for this repository</param>
        public IObservable<Project> CreateForOrganization(string organization, NewProject newProject)
        {
            Ensure.ArgumentNotNull(newProject, "newRepositoryProject");

            return _client.CreateForOrganization(organization, newProject).ToObservable();
        }

        /// <summary>
        /// Updates a project for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#update-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The Id of the project</param>
        /// <param name="projectUpdate">The modified project</param>
        public IObservable<Project> Update(int id, ProjectUpdate projectUpdate)
        {
            Ensure.ArgumentNotNull(projectUpdate, "repositoryProjectUpdate");

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
        public IObservableProjectCardsClient Cards { get; private set; }

        /// <summary>
        /// A client for GitHub's Project Columns API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/columns/">Repository Projects API documentation</a> for more information.
        /// </remarks>
        public IObservableProjectColumnsClient Columns { get; private set; }
    }
}
