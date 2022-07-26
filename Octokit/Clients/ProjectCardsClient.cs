using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Project Cards API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/projects/cards/">Repository Projects API documentation</a> for more information.
    /// </remarks>
    public class ProjectCardsClient : ApiClient, IProjectCardsClient
    {
        public ProjectCardsClient(IApiConnection apiConnection) :
            base(apiConnection)
        {
        }

        /// <summary>
        /// Gets all cards.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-projects-cards">API documentation</a> for more information.
        /// </remarks>
        /// <param name="columnId">The id of the column</param>
        [ManualRoute("GET", "/projects/columns/{column_id}/cards")]
        public Task<IReadOnlyList<ProjectCard>> GetAll(int columnId)
        {
            return GetAll(columnId, ApiOptions.None);
        }

        /// <summary>
        /// Gets all cards.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-projects-cards">API documentation</a> for more information.
        /// </remarks>
        /// <param name="columnId">The id of the column</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/projects/columns/{column_id}/cards")]
        public Task<IReadOnlyList<ProjectCard>> GetAll(int columnId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAll(columnId, new ProjectCardRequest(), options);
        }

        /// <summary>
        /// Gets all cards.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-projects-cards">API documentation</a> for more information.
        /// </remarks>
        /// <param name="columnId">The id of the column</param>
        /// <param name="request">Used to filter the list of project cards returned</param>
        [ManualRoute("GET", "/projects/columns/{column_id}/cards")]
        public Task<IReadOnlyList<ProjectCard>> GetAll(int columnId, ProjectCardRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAll(columnId, request, ApiOptions.None);
        }

        /// <summary>
        /// Gets all cards.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-projects-cards">API documentation</a> for more information.
        /// </remarks>
        /// <param name="columnId">The id of the column</param>
        /// <param name="request">Used to filter the list of project cards returned</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/projects/columns/{column_id}/cards")]
        public Task<IReadOnlyList<ProjectCard>> GetAll(int columnId, ProjectCardRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<ProjectCard>(ApiUrls.ProjectCards(columnId), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Gets a single card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#get-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the card</param>
        [ManualRoute("GET", "/projects/columns/cards/{card_id}")]
        public Task<ProjectCard> Get(int id)
        {
            return ApiConnection.Get<ProjectCard>(ApiUrls.ProjectCard(id), null);
        }

        /// <summary>
        /// Creates a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#create-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="columnId">The id of the column</param>
        /// <param name="newProjectCard">The card to create</param>
        [ManualRoute("POST", "/projects/columns/{column_id}/cards")]
        public Task<ProjectCard> Create(int columnId, NewProjectCard newProjectCard)
        {
            Ensure.ArgumentNotNull(newProjectCard, nameof(newProjectCard));

            return ApiConnection.Post<ProjectCard>(ApiUrls.ProjectCards(columnId), newProjectCard);
        }

        /// <summary>
        /// Updates a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#update-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the card</param>
        /// <param name="projectCardUpdate">New values to update the card with</param>
        [ManualRoute("GET", "/projects/columns/cards/{card_id}")]
        public Task<ProjectCard> Update(int id, ProjectCardUpdate projectCardUpdate)
        {
            Ensure.ArgumentNotNull(projectCardUpdate, nameof(projectCardUpdate));

            return ApiConnection.Patch<ProjectCard>(ApiUrls.ProjectCard(id), projectCardUpdate);
        }

        /// <summary>
        /// Deletes a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#delete-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the card</param>
        [ManualRoute("DELETE", "/projects/columns/cards/{card_id}")]
        public async Task<bool> Delete(int id)
        {
            var endpoint = ApiUrls.ProjectCard(id);

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
        /// Moves a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#move-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the card</param>
        /// <param name="position">The position to move the card</param>
        [ManualRoute("POST", "/projects/columns/cards/{card_id}/moves")]
        public async Task<bool> Move(int id, ProjectCardMove position)
        {
            Ensure.ArgumentNotNull(position, nameof(position));

            var endpoint = ApiUrls.ProjectCardMove(id);
            try
            {
                var httpStatusCode = await Connection.Post(endpoint, position, null).ConfigureAwait(false);
                return httpStatusCode == HttpStatusCode.Created;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }
    }
}
