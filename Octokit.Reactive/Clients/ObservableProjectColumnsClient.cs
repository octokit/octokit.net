using Octokit.Reactive.Internal;
using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableProjectColumnsClient : IObservableProjectColumnsClient
    {
        readonly IProjectColumnsClient _client;
        readonly IConnection _connection;

        public ObservableProjectColumnsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Repository.Projects.Columns;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets all columns for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/columns/#list-project-columns">API documentation</a> for more information.
        /// </remarks>
        /// <param name="projectId">The Id of the project</param>
        public IObservable<ProjectColumn> GetAll(int projectId)
        {
            var url = ApiUrls.ProjectColumns(projectId);

            return _connection.GetAndFlattenAllPages<ProjectColumn>(url);
        }

        /// <summary>
        /// Gets a single column for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/columns/#get-a-project-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the column</param>
        public IObservable<ProjectColumn> Get(int id)
        {
            return _client.Get(id).ToObservable();
        }

        /// <summary>
        /// Creates a column for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/columns/#create-a-project-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="projectId">The id of the project</param>
        /// <param name="newProjectColumn">The column to create</param>
        public IObservable<ProjectColumn> Create(int projectId, NewProjectColumn newProjectColumn)
        {
            Ensure.ArgumentNotNull(newProjectColumn, "newRepositoryProjectColumn");

            return _client.Create(projectId, newProjectColumn).ToObservable();
        }

        /// <summary>
        /// Updates a column for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/columns/#update-a-project-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the column</param>
        /// <param name="projectColumnUpdate">New values to update the column with</param>
        public IObservable<ProjectColumn> Update(int id, ProjectColumnUpdate projectColumnUpdate)
        {
            Ensure.ArgumentNotNull(projectColumnUpdate, "repositoryProjectColumnUpdate");

            return _client.Update(id, projectColumnUpdate).ToObservable();
        }

        /// <summary>
        /// Deletes a column.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/columns/#delete-a-project-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the column</param>
        public IObservable<bool> Delete(int id)
        {
            return _client.Delete(id).ToObservable();
        }

        /// <summary>
        /// Moves a column.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#move-a-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the column</param>
        /// <param name="position">The position to move the column</param>
        public IObservable<bool> Move(int id, ProjectColumnMove position)
        {
            Ensure.ArgumentNotNull(position, "position");

            return _client.Move(id, position).ToObservable();
        }
    }
}
