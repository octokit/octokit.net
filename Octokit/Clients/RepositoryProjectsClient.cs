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
    public class RepositoryProjectsClient : ApiClient, IRepositoryProjectsClient
    {
        public RepositoryProjectsClient(IApiConnection apiConnection) :
            base(apiConnection)
        {
        }

        /// <summary>
        /// Get all projects for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public Task<IReadOnlyList<Project>> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<Project>(ApiUrls.Projects(owner, name), AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Get all projects for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        public Task<IReadOnlyList<Project>> GetAllForRepository(long repositoryId)
        {
            return ApiConnection.GetAll<Project>(ApiUrls.Projects(repositoryId), AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Gets a single project for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#get-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The number of the project</param>
        public Task<Project> Get(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Get<Project>(ApiUrls.Project(owner, name, number), null, AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Gets a single project for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#get-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The number of the project</param>
        public Task<Project> Get(long repositoryId, int number)
        {
            return ApiConnection.Get<Project>(ApiUrls.Project(repositoryId, number), null, AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Creates a project for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#create-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newRepositoryProject">The new project to create for this repository</param>
        public Task<Project> Create(string owner, string name, NewProject newRepositoryProject)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newRepositoryProject, "newRepositoryProject");

            return ApiConnection.Post<Project>(ApiUrls.Projects(owner, name), newRepositoryProject, AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Creates a project for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#create-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newRepositoryProject">The new project to create for this repository</param>
        public Task<Project> Create(long repositoryId, NewProject newRepositoryProject)
        {
            Ensure.ArgumentNotNull(newRepositoryProject, "newRepositoryProject");

            return ApiConnection.Post<Project>(ApiUrls.Projects(repositoryId), newRepositoryProject, AcceptHeaders.ProjectsApiPreview);
        }

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
        public Task<Project> Update(string owner, string name, int number, ProjectUpdate repositoryProjectUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(repositoryProjectUpdate, "repositoryProjectUpdate");

            return ApiConnection.Patch<Project>(ApiUrls.Project(owner, name, number), repositoryProjectUpdate, AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Updates a project for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#update-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The number of the project</param>
        /// <param name="repositoryProjectUpdate">The modified project</param>
        public Task<Project> Update(long repositoryId, int number, ProjectUpdate repositoryProjectUpdate)
        {
            Ensure.ArgumentNotNull(repositoryProjectUpdate, "repositoryProjectUpdate");

            return ApiConnection.Patch<Project>(ApiUrls.Project(repositoryId, number), repositoryProjectUpdate, AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Deletes a project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#delete-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The number of the project</param>
        public async Task<bool> Delete(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = ApiUrls.Project(owner, name, number);
            try
            {
                var httpStatusCode = await Connection.Delete(endpoint, null, AcceptHeaders.ProjectsApiPreview).ConfigureAwait(false);
                return httpStatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes a project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#delete-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The number of the project</param>
        public async Task<bool> Delete(long repositoryId, int number)
        {
            var endpoint = ApiUrls.Project(repositoryId, number);
            try
            {
                var httpStatusCode = await Connection.Delete(endpoint, null, AcceptHeaders.ProjectsApiPreview).ConfigureAwait(false);
                return httpStatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets all columns for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-columns">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The number of the project</param>
        public Task<IReadOnlyList<ProjectColumn>> GetAllColumns(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<ProjectColumn>(ApiUrls.ProjectColumns(owner, name, number), AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Gets all columns for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-columns">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The number of the project</param>
        public Task<IReadOnlyList<ProjectColumn>> GetAllColumns(long repositoryId, int number)
        {
            return ApiConnection.GetAll<ProjectColumn>(ApiUrls.ProjectColumns(repositoryId, number), AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Gets a single column for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#get-a-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="id">The id of the column</param>
        public Task<ProjectColumn> GetColumn(string owner, string name, int id)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Get<ProjectColumn>(ApiUrls.ProjectColumn(owner, name, id), null, AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Gets a single column for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#get-a-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the column</param>
        public Task<ProjectColumn> GetColumn(long repositoryId, int id)
        {
            return ApiConnection.Get<ProjectColumn>(ApiUrls.ProjectColumn(repositoryId, id), null, AcceptHeaders.ProjectsApiPreview);
        }

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
        public Task<ProjectColumn> CreateColumn(string owner, string name, int number, NewProjectColumn newRepositoryProjectColumn)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newRepositoryProjectColumn, "newRepositoryProjectColumn");

            return ApiConnection.Post<ProjectColumn>(ApiUrls.ProjectColumns(owner, name, number), newRepositoryProjectColumn, AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Creates a column for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#create-a-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The number of the project</param>
        /// <param name="newRepositoryProjectColumn">The column to create</param>
        public Task<ProjectColumn> CreateColumn(long repositoryId, int number, NewProjectColumn newRepositoryProjectColumn)
        {
            Ensure.ArgumentNotNull(newRepositoryProjectColumn, "newRepositoryProjectColumn");

            return ApiConnection.Post<ProjectColumn>(ApiUrls.ProjectColumns(repositoryId, number), newRepositoryProjectColumn, AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Updates a column for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#create-a-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="id">The id of the column</param>
        public Task<ProjectColumn> UpdateColumn(string owner, string name, int id, ProjectColumnUpdate repositoryProjectColumnUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(repositoryProjectColumnUpdate, "repositoryProjectColumnUpdate");

            return ApiConnection.Patch<ProjectColumn>(ApiUrls.ProjectColumn(owner, name, id), repositoryProjectColumnUpdate, AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Updates a column for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#create-a-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the column</param>
        public Task<ProjectColumn> UpdateColumn(long repositoryId, int id, ProjectColumnUpdate repositoryProjectColumnUpdate)
        {
            Ensure.ArgumentNotNull(repositoryProjectColumnUpdate, "repositoryProjectColumnUpdate");

            return ApiConnection.Patch<ProjectColumn>(ApiUrls.ProjectColumn(repositoryId, id), repositoryProjectColumnUpdate, AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Deletes a column.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#delete-a-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="id">The id of the column</param>
        public async Task<bool> DeleteColumn(string owner, string name, int id)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = ApiUrls.ProjectColumn(owner, name, id);
            try
            {
                var httpStatusCode = await Connection.Delete(endpoint, null, AcceptHeaders.ProjectsApiPreview).ConfigureAwait(false);
                return httpStatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes a column.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#delete-a-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the column</param>
        public async Task<bool> DeleteColumn(long repositoryId, int id)
        {
            var endpoint = ApiUrls.ProjectColumn(repositoryId, id);
            try
            {
                var httpStatusCode = await Connection.Delete(endpoint, null, AcceptHeaders.ProjectsApiPreview).ConfigureAwait(false);
                return httpStatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Moves a column.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#move-a-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="id">The id of the column</param>        
        /// <param name="position">The position to move the column</param>
        public async Task<bool> MoveColumn(string owner, string name, int id, ProjectColumnMove position)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = ApiUrls.ProjectColumnMove(owner, name, id);
            try
            {
                var httpStatusCode = await Connection.Post(endpoint, position, AcceptHeaders.ProjectsApiPreview).ConfigureAwait(false);
                return httpStatusCode == HttpStatusCode.Created;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Moves a column.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#move-a-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the column</param>
        /// <param name="position">The position to move the column</param>
        public async Task<bool> MoveColumn(long repositoryId, int id, ProjectColumnMove position)
        {
            var endpoint = ApiUrls.ProjectColumnMove(repositoryId, id);
            try
            {
                var httpStatusCode = await Connection.Post(endpoint, position, AcceptHeaders.ProjectsApiPreview).ConfigureAwait(false);
                return httpStatusCode == HttpStatusCode.Created;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets all cards for a specific column.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-projects-cards">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="columnId">The id of the column</param>
        public Task<IReadOnlyList<ProjectCard>> GetAllCards(string owner, string name, int columnId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<ProjectCard>(ApiUrls.ProjectCards(owner, name, columnId), AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Gets all cards for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-projects-cards">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="columnId">The id of the column</param>
        public Task<IReadOnlyList<ProjectCard>> GetAllCards(long repositoryId, int columnId)
        {
            return ApiConnection.GetAll<ProjectCard>(ApiUrls.ProjectCards(repositoryId, columnId), AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Gets a single card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#get-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="id">The id of the card</param>
        public Task<ProjectCard> GetCard(string owner, string name, int id)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Get<ProjectCard>(ApiUrls.ProjectCard(owner, name, id), null, AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Gets a single card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#get-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the card</param>
        public Task<ProjectCard> GetCard(long repositoryId, int id)
        {
            return ApiConnection.Get<ProjectCard>(ApiUrls.ProjectCard(repositoryId, id), null, AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Creates a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#create-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="columnId">The id of the column</param>
        /// <param name="newProjectCard">The card to create</param>
        public Task<ProjectCard> CreateCard(string owner, string name, int columnId, NewProjectCard newProjectCard)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newProjectCard, "newProjectCard");

            return ApiConnection.Post<ProjectCard>(ApiUrls.ProjectCards(owner, name, columnId), newProjectCard, AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Creates a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#create-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="columnId">The id of the column</param>
        /// <param name="newProjectCard">The card to create</param>
        public Task<ProjectCard> CreateCard(long repositoryId, int columnId, NewProjectCard newProjectCard)
        {
            Ensure.ArgumentNotNull(newProjectCard, "newProjectCard");

            return ApiConnection.Post<ProjectCard>(ApiUrls.ProjectCards(repositoryId, columnId), newProjectCard, AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Updates a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#update-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="id">The id of the card</param>
        /// <param name="projectCardUpdate">The card to create</param>
        public Task<ProjectCard> UpdateCard(string owner, string name, int id, ProjectCardUpdate projectCardUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(projectCardUpdate, "projectCardUpdate");

            return ApiConnection.Patch<ProjectCard>(ApiUrls.ProjectCard(owner, name, id), projectCardUpdate, AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Updates a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#update-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the card</param>
        /// <param name="projectCardUpdate">The card to create</param>
        public Task<ProjectCard> UpdateCard(long repositoryId, int id, ProjectCardUpdate projectCardUpdate)
        {
            Ensure.ArgumentNotNull(projectCardUpdate, "projectCardUpdate");

            return ApiConnection.Patch<ProjectCard>(ApiUrls.ProjectCard(repositoryId, id), projectCardUpdate, AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Deletes a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#delete-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="id">The id of the card</param>
        public async Task<bool> DeleteCard(string owner, string name, int id)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = ApiUrls.ProjectCard(owner, name, id);
            try
            {
                var httpStatusCode = await Connection.Delete(endpoint, null, AcceptHeaders.ProjectsApiPreview).ConfigureAwait(false);
                return httpStatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#delete-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the card</param>
        public async Task<bool> DeleteCard(long repositoryId, int id)
        {
            var endpoint = ApiUrls.ProjectCard(repositoryId, id);
            try
            {
                var httpStatusCode = await Connection.Delete(endpoint, null, AcceptHeaders.ProjectsApiPreview).ConfigureAwait(false);
                return httpStatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Moves a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#move-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="id">The id of the card</param>
        /// <param name="position">The position to move the card</param>
        public async Task<bool> MoveCard(string owner, string name, int id, ProjectCardMove position)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = ApiUrls.ProjectCardMove(owner, name, id);
            try
            {
                var httpStatusCode = await Connection.Post(endpoint, position, AcceptHeaders.ProjectsApiPreview).ConfigureAwait(false);
                return httpStatusCode == HttpStatusCode.Created;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Moves a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#move-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the card</param>
        /// <param name="position">The position to move the card</param>
        public async Task<bool> MoveCard(long repositoryId, int id, ProjectCardMove position)
        {
            var endpoint = ApiUrls.ProjectCardMove(repositoryId, id);
            try
            {
                var httpStatusCode = await Connection.Post(endpoint, position, AcceptHeaders.ProjectsApiPreview).ConfigureAwait(false);
                return httpStatusCode == HttpStatusCode.Created;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }
    }
}
