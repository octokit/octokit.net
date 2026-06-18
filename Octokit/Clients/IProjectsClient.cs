using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Projects API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/projects/">Repository Projects API documentation</a> for more information.
    /// </remarks>
    public interface IProjectsClient
    {
        /// <summary>
        /// Get all projects for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<Project>> GetAllForRepository(string owner, string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all projects for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter the list of projects returned</param>
        Task<IReadOnlyList<Project>> GetAllForRepository(string owner, string name, ProjectRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all projects for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        Task<IReadOnlyList<Project>> GetAllForRepository(long repositoryId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all projects for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter the list of projects returned</param>
        Task<IReadOnlyList<Project>> GetAllForRepository(long repositoryId, ProjectRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all projects for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<Project>> GetAllForRepository(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default);

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
        Task<IReadOnlyList<Project>> GetAllForRepository(string owner, string name, ProjectRequest request, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all projects for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<Project>> GetAllForRepository(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all projects for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-repository-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter the list of projects returned</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<Project>> GetAllForRepository(long repositoryId, ProjectRequest request, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all projects for the specified organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-organization-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        Task<IReadOnlyList<Project>> GetAllForOrganization(string organization, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all projects for the specified organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-organization-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="request">Used to filter the list of projects returned</param>
        Task<IReadOnlyList<Project>> GetAllForOrganization(string organization, ProjectRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all projects for the specified organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-organization-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<Project>> GetAllForOrganization(string organization, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all projects for the specified organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#list-organization-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="request">Used to filter the list of projects returned</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<Project>> GetAllForOrganization(string organization, ProjectRequest request, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a single project for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#get-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="projectId">The Id of the project</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        Task<Project> Get(int projectId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a project for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#create-a-repository-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="newProject">The new project to create for this repository</param>
        Task<Project> CreateForRepository(long repositoryId, NewProject newProject, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a project for the specified organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/#create-an-organization-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <param name="newProject">The new project to create for this repository</param>
        Task<Project> CreateForOrganization(string organization, NewProject newProject, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates a project for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#update-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="projectId">The Id of the project</param>
        /// <param name="projectUpdate">The modified project</param>
        Task<Project> Update(int projectId, ProjectUpdate projectUpdate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#delete-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="projectId">The Id of the project</param>
        Task<bool> Delete(int projectId, CancellationToken cancellationToken = default);

        /// <summary>
        /// A client for GitHub's Project Cards API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/cards/">Repository Projects API documentation</a> for more information.
        /// </remarks>
        IProjectCardsClient Card { get; }

        /// <summary>
        /// A client for GitHub's Project Columns API.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/columns/">Repository Projects API documentation</a> for more information.
        /// </remarks>
        IProjectColumnsClient Column { get; }
    }
}
