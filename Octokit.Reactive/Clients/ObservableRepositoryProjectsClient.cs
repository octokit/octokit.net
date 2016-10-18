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
    public class ObservableRepositoryProjectsClient : IObservableRepositoryProjectsClient
    {
        readonly IRepositoryProjectsClient _client;
        readonly IConnection _connection;

        public ObservableRepositoryProjectsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Repository.Projects;
            _connection = client.Connection;
        }

        /// <summary>
        /// Get all projects for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public IObservable<Project> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var url = ApiUrls.Projects(owner, name);

            return _connection.GetAndFlattenAllPages<Project>(url);
        }

        /// <summary>
        /// Get all projects for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-projects">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        public IObservable<Project> GetAllForRepository(long repositoryId)
        {
            var url = ApiUrls.Projects(repositoryId);

            return _connection.GetAndFlattenAllPages<Project>(url);
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
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        public IObservable<Project> Get(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.Get(owner, name, number).ToObservable();
        }

        /// <summary>
        /// Gets a single project for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#get-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The number of the project</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        public IObservable<Project> Get(long repositoryId, int number)
        {
            return _client.Get(repositoryId, number).ToObservable();
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
        public IObservable<Project> Create(string owner, string name, NewProject newRepositoryProject)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newRepositoryProject, "newRepositoryProject");

            return _client.Create(owner, name, newRepositoryProject).ToObservable();
        }

        /// <summary>
        /// Creates a project for this repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#create-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newRepositoryProject">The new project to create for this repository</param>
        public IObservable<Project> Create(long repositoryId, NewProject newRepositoryProject)
        {
            Ensure.ArgumentNotNull(newRepositoryProject, "newRepositoryProject");

            return _client.Create(repositoryId, newRepositoryProject).ToObservable();
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
        public IObservable<Project> Update(string owner, string name, int number, ProjectUpdate repositoryProjectUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(repositoryProjectUpdate, "repositoryProjectUpdate");

            return _client.Update(owner, name, number, repositoryProjectUpdate).ToObservable();
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
        public IObservable<Project> Update(long repositoryId, int number, ProjectUpdate repositoryProjectUpdate)
        {
            Ensure.ArgumentNotNull(repositoryProjectUpdate, "repositoryProjectUpdate");

            return _client.Update(repositoryId, number, repositoryProjectUpdate).ToObservable();
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
        public IObservable<bool> Delete(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.Delete(owner, name, number).ToObservable();
        }

        /// <summary>
        /// Deletes a project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#delete-a-project">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The number of the project</param>
        public IObservable<bool> Delete(long repositoryId, int number)
        {
            return _client.Delete(repositoryId, number).ToObservable();
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
        public IObservable<ProjectColumn> GetAllColumns(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var url = ApiUrls.ProjectColumns(owner, name, number);

            return _connection.GetAndFlattenAllPages<ProjectColumn>(url);
        }

        /// <summary>
        /// Gets all columns for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-columns">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The number of the project</param>
        public IObservable<ProjectColumn> GetAllColumns(long repositoryId, int number)
        {
            var url = ApiUrls.ProjectColumns(repositoryId, number);

            return _connection.GetAndFlattenAllPages<ProjectColumn>(url);
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
        public IObservable<ProjectColumn> GetColumn(string owner, string name, int id)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.GetColumn(owner, name, id).ToObservable();
        }

        /// <summary>
        /// Gets a single column for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#get-a-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the column</param>
        public IObservable<ProjectColumn> GetColumn(long repositoryId, int id)
        {
            return _client.GetColumn(repositoryId, id).ToObservable();
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
        public IObservable<ProjectColumn> CreateColumn(string owner, string name, int number, NewProjectColumn newRepositoryProjectColumn)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newRepositoryProjectColumn, "newRepositoryProjectColumn");

            return _client.CreateColumn(owner, name, number, newRepositoryProjectColumn).ToObservable();
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
        public IObservable<ProjectColumn> CreateColumn(long repositoryId, int number, NewProjectColumn newRepositoryProjectColumn)
        {
            Ensure.ArgumentNotNull(newRepositoryProjectColumn, "newRepositoryProjectColumn");

            return _client.CreateColumn(repositoryId, number, newRepositoryProjectColumn).ToObservable();
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
        /// <param name="repositoryProjectColumnUpdate">New values to update the column with</param>
        public IObservable<ProjectColumn> UpdateColumn(string owner, string name, int id, ProjectColumnUpdate repositoryProjectColumnUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(repositoryProjectColumnUpdate, "repositoryProjectColumnUpdate");

            return _client.UpdateColumn(owner, name, id, repositoryProjectColumnUpdate).ToObservable();
        }

        /// <summary>
        /// Updates a column for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#create-a-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the column</param>
        /// <param name="repositoryProjectColumnUpdate">New values to update the column with</param>
        public IObservable<ProjectColumn> UpdateColumn(long repositoryId, int id, ProjectColumnUpdate repositoryProjectColumnUpdate)
        {
            Ensure.ArgumentNotNull(repositoryProjectColumnUpdate, "repositoryProjectColumnUpdate");

            return _client.UpdateColumn(repositoryId, id, repositoryProjectColumnUpdate).ToObservable();
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
        public IObservable<bool> DeleteColumn(string owner, string name, int id)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.DeleteColumn(owner, name, id).ToObservable();
        }

        /// <summary>
        /// Deletes a column.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#delete-a-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the column</param>
        public IObservable<bool> DeleteColumn(long repositoryId, int id)
        {
            return _client.DeleteColumn(repositoryId, id).ToObservable();
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
        public IObservable<bool> MoveColumn(string owner, string name, int id, ProjectColumnMove position)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(position, "position");

            return _client.MoveColumn(owner, name, id, position).ToObservable();
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
        public IObservable<bool> MoveColumn(long repositoryId, int id, ProjectColumnMove position)
        {
            Ensure.ArgumentNotNull(position, "position");

            return _client.MoveColumn(repositoryId, id, position).ToObservable();
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
        public IObservable<ProjectCard> GetAllCards(string owner, string name, int columnId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var url = ApiUrls.ProjectCards(owner, name, columnId);

            return _connection.GetAndFlattenAllPages<ProjectCard>(url);
        }

        /// <summary>
        /// Gets all cards for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-projects-cards">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="columnId">The id of the column</param>
        public IObservable<ProjectCard> GetAllCards(long repositoryId, int columnId)
        {
            var url = ApiUrls.ProjectCards(repositoryId, columnId);

            return _connection.GetAndFlattenAllPages<ProjectCard>(url);
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
        public IObservable<ProjectCard> GetCard(string owner, string name, int id)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.GetCard(owner, name, id).ToObservable();
        }

        /// <summary>
        /// Gets a single card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#get-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the card</param>
        public IObservable<ProjectCard> GetCard(long repositoryId, int id)
        {
            return _client.GetCard(repositoryId, id).ToObservable();
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
        public IObservable<ProjectCard> CreateCard(string owner, string name, int columnId, NewProjectCard newProjectCard)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newProjectCard, "newProjectCard");

            return _client.CreateCard(owner, name, columnId, newProjectCard).ToObservable();
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
        public IObservable<ProjectCard> CreateCard(long repositoryId, int columnId, NewProjectCard newProjectCard)
        {
            Ensure.ArgumentNotNull(newProjectCard, "newProjectCard");

            return _client.CreateCard(repositoryId, columnId, newProjectCard).ToObservable();
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
        /// <param name="projectCardUpdate">New values to update the card with</param>
        public IObservable<ProjectCard> UpdateCard(string owner, string name, int id, ProjectCardUpdate projectCardUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(projectCardUpdate, "projectCardUpdate");

            return _client.UpdateCard(owner, name, id, projectCardUpdate).ToObservable();
        }

        /// <summary>
        /// Updates a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#update-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the card</param>
        /// <param name="projectCardUpdate">New values to update the card with</param>
        public IObservable<ProjectCard> UpdateCard(long repositoryId, int id, ProjectCardUpdate projectCardUpdate)
        {
            Ensure.ArgumentNotNull(projectCardUpdate, "projectCardUpdate");

            return _client.UpdateCard(repositoryId, id, projectCardUpdate).ToObservable();
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
        public IObservable<bool> DeleteCard(string owner, string name, int id)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.DeleteCard(owner, name, id).ToObservable();
        }

        /// <summary>
        /// Deletes a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#delete-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the card</param>
        public IObservable<bool> DeleteCard(long repositoryId, int id)
        {
            return _client.DeleteCard(repositoryId, id).ToObservable();
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
        public IObservable<bool> MoveCard(string owner, string name, int id, ProjectCardMove position)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(position, "position");

            return _client.MoveCard(owner, name, id, position).ToObservable();
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
        public IObservable<bool> MoveCard(long repositoryId, int id, ProjectCardMove position)
        {
            Ensure.ArgumentNotNull(position, "position");

            return _client.MoveCard(repositoryId, id, position).ToObservable();
        }
    }
}
