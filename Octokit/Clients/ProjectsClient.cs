using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Projects API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/projects/">Repository Projects API documentation</a> for more information.
    /// </remarks>
    public class ProjectsClient : ApiClient, IProjectsClient
    {
        public ProjectsClient(IApiConnection apiConnection) :
            base(apiConnection)
        {
            Card = new ProjectCardsClient(apiConnection);
            Column = new ProjectColumnsClient(apiConnection);
        }

        /// <summary>
        /// Get all projects for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        public Task<IReadOnlyList<Project>> GetAllForRepository(long repositoryId)
        {
            return GetAllForRepository(repositoryId, ApiOptions.None);
        }

        /// <summary>
        /// Get all projects for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public Task<IReadOnlyList<Project>> GetAllForRepository(long repositoryId, ApiOptions options)
        {
            return ApiConnection.GetAll<Project>(ApiUrls.RepositoryProjects(repositoryId), new Dictionary<string, string>(), AcceptHeaders.ProjectsApiPreview, options);
        }

        /// <summary>
        /// Get all projects for the specified organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-organization-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="organization">The name of the organziation</param>
        public Task<IReadOnlyList<Project>> GetAllForOrganization(string organization)
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
        public Task<IReadOnlyList<Project>> GetAllForOrganization(string organization, ApiOptions options)
        {
            return ApiConnection.GetAll<Project>(ApiUrls.OrganizationProjects(organization), new Dictionary<string, string>(), AcceptHeaders.ProjectsApiPreview, options);
        }

        /// <summary>
        /// Gets a single project for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#get-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The Id of the project</param>
        public Task<Project> Get(int id)
        {
            return ApiConnection.Get<Project>(ApiUrls.Project(id), null, AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Creates a project for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#create-a-repository-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newProject">The new project to create for this repository</param>
        public Task<Project> CreateForRepository(long repositoryId, NewProject newProject)
        {
            Ensure.ArgumentNotNull(newProject, "newRepositoryProject");

            return ApiConnection.Post<Project>(ApiUrls.RepositoryProjects(repositoryId), newProject, AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Creates a project for the specified organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#create-an-organization-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="newProject">The new project to create for this repository</param>
        public Task<Project> CreateForOrganization(string organization, NewProject newProject)
        {
            Ensure.ArgumentNotNull(newProject, "newRepositoryProject");

            return ApiConnection.Post<Project>(ApiUrls.OrganizationProjects(organization), newProject, AcceptHeaders.ProjectsApiPreview);
        }
        
        /// <summary>
        /// Updates a project for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#update-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The Id of the project</param>
        /// <param name="projectUpdate">The modified project</param>
        public Task<Project> Update(int id, ProjectUpdate projectUpdate)
        {
            Ensure.ArgumentNotNull(projectUpdate, "repositoryProjectUpdate");

            return ApiConnection.Patch<Project>(ApiUrls.Project(id), projectUpdate, AcceptHeaders.ProjectsApiPreview);
        }
        
        /// <summary>
        /// Deletes a project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#delete-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The Id of the project</param>
        public async Task<bool> Delete(int id)
        {
            var endpoint = ApiUrls.Project(id);

            try
            {
                var httpStatusCode = await Connection.Delete(endpoint, new object(), AcceptHeaders.ProjectsApiPreview).ConfigureAwait(false);
                return httpStatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// A client for GitHub's Project Cards API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/cards/">Repository Projects API documentation</a> for more information.
        /// </remarks>
        public IProjectCardsClient Card { get; private set; }

        /// <summary>
        /// A client for GitHub's Project Columns API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/columns/">Repository Projects API documentation</a> for more information.
        /// </remarks>
        public IProjectColumnsClient Column { get; private set; }
    }
}
