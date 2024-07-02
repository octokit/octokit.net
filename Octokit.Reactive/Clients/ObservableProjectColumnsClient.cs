using Octokit.Reactive.Internal;
using System;
using System.Reactive.Threading.Tasks;
using System.Collections.Generic;

namespace Octokit.Reactive
{
    public class ObservableProjectColumnsClient : IObservableProjectColumnsClient
    {
        readonly IProjectColumnsClient _client;
        readonly IConnection _connection;

        public ObservableProjectColumnsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Repository.Project.Column;
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
            return GetAll(projectId, ApiOptions.None);
        }

        /// <summary>
        /// Gets all columns for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/columns/#list-project-columns">API documentation</a> for more information.
        /// </remarks>
        /// <param name="projectId">The Id of the project</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<ProjectColumn> GetAll(int projectId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var url = ApiUrls.ProjectColumns(projectId);

            return _connection.GetAndFlattenAllPages<ProjectColumn>(url, new Dictionary<string, string>(), options);
        }

        /// <summary>
        /// Gets a single column for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/columns/#get-a-project-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="columnId">The id of the column</param>
        public IObservable<ProjectColumn> Get(int columnId)
        {
            return _client.Get(columnId).ToObservable();
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
            Ensure.ArgumentNotNull(newProjectColumn, nameof(newProjectColumn));

            return _client.Create(projectId, newProjectColumn).ToObservable();
        }

        /// <summary>
        /// Updates a column for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/columns/#update-a-project-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="columnId">The id of the column</param>
        /// <param name="projectColumnUpdate">New values to update the column with</param>
        public IObservable<ProjectColumn> Update(int columnId, ProjectColumnUpdate projectColumnUpdate)
        {
            Ensure.ArgumentNotNull(projectColumnUpdate, nameof(projectColumnUpdate));

            return _client.Update(columnId, projectColumnUpdate).ToObservable();
        }

        /// <summary>
        /// Deletes a column.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/columns/#delete-a-project-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="columnId">The id of the column</param>
        public IObservable<bool> Delete(int columnId)
        {
            return _client.Delete(columnId).ToObservable();
        }

        /// <summary>
        /// Moves a column.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#move-a-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="columnId">The id of the column</param>
        /// <param name="position">The position to move the column</param>
        public IObservable<bool> Move(int columnId, ProjectColumnMove position)
        {
            Ensure.ArgumentNotNull(position, nameof(position));

            return _client.Move(columnId, position).ToObservable();
        }
    }
}
