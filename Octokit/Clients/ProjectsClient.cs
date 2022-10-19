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
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/projects")]
        public Task<IReadOnlyList<Project>> GetAllForRepository(string owner, string name)
        {
            return GetAllForRepository(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Get all projects for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/projects")]
        public Task<IReadOnlyList<Project>> GetAllForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Project>(ApiUrls.RepositoryProjects(owner, name), new Dictionary<string, string>(), options);
        }

        /// <summary>
        /// Get all projects for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter the list of projects returned</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/projects")]
        public Task<IReadOnlyList<Project>> GetAllForRepository(string owner, string name, ProjectRequest request)
        {
            return GetAllForRepository(owner, name, request, ApiOptions.None);
        }

        /// <summary>
        /// Get all projects for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter the list of projects returned</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/projects")]
        public Task<IReadOnlyList<Project>> GetAllForRepository(string owner, string name, ProjectRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Project>(ApiUrls.RepositoryProjects(owner, name), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Get all projects for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        [ManualRoute("GET", "/repositories/{id}/projects")]
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
        [ManualRoute("GET", "/repositories/{id}/projects")]
        public Task<IReadOnlyList<Project>> GetAllForRepository(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Project>(ApiUrls.RepositoryProjects(repositoryId), new Dictionary<string, string>(), options);
        }

        /// <summary>
        /// Get all projects for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter the list of projects returned</param>
        [ManualRoute("GET", "/repositories/{id}/projects")]
        public Task<IReadOnlyList<Project>> GetAllForRepository(long repositoryId, ProjectRequest request)
        {
            return GetAllForRepository(repositoryId, request, ApiOptions.None);
        }

        /// <summary>
        /// Get all projects for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter the list of projects returned</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{id}/projects")]
        public Task<IReadOnlyList<Project>> GetAllForRepository(long repositoryId, ProjectRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Project>(ApiUrls.RepositoryProjects(repositoryId), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Get all projects for the specified organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-organization-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        [ManualRoute("GET", "/orgs/{org}/projects")]
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
        /// <param name="organization">The name of the organization</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/orgs/{org}/projects")]
        public Task<IReadOnlyList<Project>> GetAllForOrganization(string organization, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNull(options, nameof(options));


            return ApiConnection.GetAll<Project>(ApiUrls.OrganizationProjects(organization), new Dictionary<string, string>(), options);
        }

        /// <summary>
        /// Get all projects for the specified organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-organization-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="request">Used to filter the list of projects returned</param>
        [ManualRoute("GET", "/orgs/{org}/projects")]
        public Task<IReadOnlyList<Project>> GetAllForOrganization(string organization, ProjectRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForOrganization(organization, request, ApiOptions.None);
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
        [ManualRoute("GET", "/orgs/{org}/projects")]
        public Task<IReadOnlyList<Project>> GetAllForOrganization(string organization, ProjectRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Project>(ApiUrls.OrganizationProjects(organization), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Gets a single project for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#get-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The Id of the project</param>
        [ManualRoute("GET", "/projects/{project_id}")]
        public Task<Project> Get(int id)
        {
            return ApiConnection.Get<Project>(ApiUrls.Project(id), null);
        }

        // NOTE: I think we're missing a Task<Project> CreateForRepository(owner, name, newProject)
        // Can we identify this programatically?

        /// <summary>
        /// Creates a project for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#create-a-repository-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newProject">The new project to create for this repository</param>
        [ManualRoute("POST", "/repositories/{id}/projects")]
        public Task<Project> CreateForRepository(long repositoryId, NewProject newProject)
        {
            Ensure.ArgumentNotNull(newProject, nameof(newProject));

            return ApiConnection.Post<Project>(ApiUrls.RepositoryProjects(repositoryId), newProject);
        }

        /// <summary>
        /// Creates a project for the specified organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#create-an-organization-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="newProject">The new project to create for this repository</param>
        [ManualRoute("POST", "/orgs/{org}/projects")]
        public Task<Project> CreateForOrganization(string organization, NewProject newProject)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));
            Ensure.ArgumentNotNull(newProject, nameof(newProject));

            return ApiConnection.Post<Project>(ApiUrls.OrganizationProjects(organization), newProject);
        }

        /// <summary>
        /// Updates a project for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#update-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The Id of the project</param>
        /// <param name="projectUpdate">The modified project</param>
        [ManualRoute("PATCH", "/project/{project_id}")]
        public Task<Project> Update(int id, ProjectUpdate projectUpdate)
        {
            Ensure.ArgumentNotNull(projectUpdate, nameof(projectUpdate));

            return ApiConnection.Patch<Project>(ApiUrls.Project(id), projectUpdate);
        }

        /// <summary>
        /// Deletes a project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#delete-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The Id of the project</param>
        [ManualRoute("DELETE", "/project/{project_id}")]
        public async Task<bool> Delete(int id)
        {
            var endpoint = ApiUrls.Project(id);

            try
            {
                var httpStatusCode = await Connection.Delete(endpoint, new object()).ConfigureAwait(false);
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
