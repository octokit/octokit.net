using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Projects API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/projects/">Repository Projects API documentation</a> for more information.
    /// </remarks>
    public interface IRepositoryProjectsClient
    {
        /// <summary>
        /// Get all projects for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        Task<IReadOnlyList<Project>> GetAllForRepository(string owner, string name);

        /// <summary>
        /// Get all projects for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        Task<IReadOnlyList<Project>> GetAllForRepository(int repositoryId);

        /// <summary>
        /// Gets a single project for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#get-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The number of the project</param>
        Task<Project> Get(string owner, string name, int number);

        /// <summary>
        /// Gets a single project for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#get-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The number of the project</param>
        Task<Project> Get(int repositoryId, int number);

        /// <summary>
        /// Creates a project for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#create-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newRepositoryProject">The new project to create for this repository</param>
        Task<Project> Create(string owner, string name, NewProject newRepositoryProject);

        /// <summary>
        /// Creates a project for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#create-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newRepositoryProject">The new project to create for this repository</param>
        Task<Project> Create(int repositoryId, NewProject newRepositoryProject);

        /// <summary>
        /// Updates a project for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#update-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The number of the project</param>
        /// <param name="repositoryProjectUpdate">The modified project</param>
        Task<Project> Update(string owner, string name, int number, ProjectUpdate repositoryProjectUpdate);

        /// <summary>
        /// Updates a project for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#update-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The number of the project</param>
        /// <param name="repositoryProjectUpdate">The modified project</param>
        Task<Project> Update(int repositoryId, int number, ProjectUpdate repositoryProjectUpdate);

        /// <summary>
        /// Deletes a project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#delete-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The number of the project</param>
        Task<bool> Delete(string owner, string name, int number);

        /// <summary>
        /// Deletes a project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#delete-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The number of the project</param>
        Task<bool> Delete(int repositoryId, int number);

        /// <summary>
        /// Gets all columns for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-columns">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The number of the project</param>
        Task<IReadOnlyList<ProjectColumn>> GetAllColumns(string owner, string name, int number);

        /// <summary>
        /// Gets all columns for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-columns">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The number of the project</param>
        Task<IReadOnlyList<ProjectColumn>> GetAllColumns(int repositoryId, int number);

        /// <summary>
        /// Gets a single column for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#get-a-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="id">The id of the column</param>
        Task<ProjectColumn> GetColumn(string owner, string name, int id);

        /// <summary>
        /// Gets a single column for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#get-a-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the column</param>
        Task<ProjectColumn> GetColumn(int repositoryId, int id);

        /// <summary>
        /// Creates a column for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#create-a-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The number of the project</param>
        /// <param name="newRepositoryProjectColumn">The column to create</param>
        Task<ProjectColumn> CreateColumn(string owner, string name, int number, NewProjectColumn newRepositoryProjectColumn);

        /// <summary>
        /// Creates a column for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#create-a-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The number of the project</param>
        /// <param name="newRepositoryProjectColumn">The column to create</param>
        Task<ProjectColumn> CreateColumn(int repositoryId, int number, NewProjectColumn newRepositoryProjectColumn);

        /// <summary>
        /// Updates a column for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#create-a-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="id">The id of the column</param>
        /// 
        Task<ProjectColumn> UpdateColumn(string owner, string name, int id, ProjectColumnUpdate repositoryProjectColumnUpdate);

        /// <summary>
        /// Updates a column for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#create-a-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the column</param>
        Task<ProjectColumn> UpdateColumn(int repositoryId, int id, ProjectColumnUpdate repositoryProjectColumnUpdate);
    }
}
