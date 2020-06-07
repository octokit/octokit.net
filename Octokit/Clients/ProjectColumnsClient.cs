using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Project Columns API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/projects/columns/">Repository Projects API documentation</a> for more information.
    /// </remarks>
    public class ProjectColumnsClient : ApiClient, IProjectColumnsClient
    {
        public ProjectColumnsClient(IApiConnection apiConnection) :
            base(apiConnection)
        {
        }

        /// <summary>
        /// Gets all columns for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/columns/#list-project-columns">API documentation</a> for more information.
        /// </remarks>
        /// <param name="projectId">The Id of the project</param>
        [ManualRoute("GET", "/projects/{project_id}/columns")]
        public Task<IReadOnlyList<ProjectColumn>> GetAll(int projectId)
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
        [Preview("inertia")]
        [ManualRoute("GET", "/projects/{project_id}/columns")]
        public Task<IReadOnlyList<ProjectColumn>> GetAll(int projectId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<ProjectColumn>(ApiUrls.ProjectColumns(projectId), new Dictionary<string, string>(), AcceptHeaders.ProjectsApiPreview, options);
        }

        /// <summary>
        /// Gets a single column for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/columns/#get-a-project-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the column</param>
        [Preview("inertia")]
        [ManualRoute("GET", "/projects/columns/{column_id}")]
        public Task<ProjectColumn> Get(int id)
        {
            return ApiConnection.Get<ProjectColumn>(ApiUrls.ProjectColumn(id), null, AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Creates a column for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/columns/#create-a-project-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="projectId">The Id of the project</param>
        /// <param name="newProjectColumn">The column to create</param>
        [Preview("inertia")]
        [ManualRoute("POST", "/projects/{project_id}/columns")]
        public Task<ProjectColumn> Create(int projectId, NewProjectColumn newProjectColumn)
        {
            Ensure.ArgumentNotNull(newProjectColumn, nameof(newProjectColumn));

            return ApiConnection.Post<ProjectColumn>(ApiUrls.ProjectColumns(projectId), newProjectColumn, AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Updates a column for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/columns/#update-a-project-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the column</param>
        /// <param name="projectColumnUpdate">New values to update the column with</param>
        [Preview("inertia")]
        [ManualRoute("PATCH", "/projects/columns/{column_id}")]
        public Task<ProjectColumn> Update(int id, ProjectColumnUpdate projectColumnUpdate)
        {
            Ensure.ArgumentNotNull(projectColumnUpdate, nameof(projectColumnUpdate));

            return ApiConnection.Patch<ProjectColumn>(ApiUrls.ProjectColumn(id), projectColumnUpdate, AcceptHeaders.ProjectsApiPreview);
        }

        /// <summary>
        /// Deletes a column.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/projects/columns/#delete-a-project-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the column</param>
        [Preview("inertia")]
        [ManualRoute("DELETE", "/projects/columns/{column_id}")]
        public async Task<bool> Delete(int id)
        {
            var endpoint = ApiUrls.ProjectColumn(id);
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
        /// Moves a column.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#move-a-column">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the column</param>
        /// <param name="position">The position to move the column</param>
        [Preview("inertia")]
        [ManualRoute("POST", "/projects/columns/{column_id}/moves")]
        public async Task<bool> Move(int id, ProjectColumnMove position)
        {
            Ensure.ArgumentNotNull(position, nameof(position));

            var endpoint = ApiUrls.ProjectColumnMove(id);
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
