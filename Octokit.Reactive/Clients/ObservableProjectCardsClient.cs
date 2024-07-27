using Octokit.Reactive.Internal;
using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Project Cards API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/projects/cards/">Repository Projects API documentation</a> for more information.
    /// </remarks>
    public class ObservableProjectCardsClient : IObservableProjectCardsClient
    {
        readonly IProjectCardsClient _client;
        readonly IConnection _connection;

        public ObservableProjectCardsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Repository.Project.Card;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets all cards for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-projects-cards">API documentation</a> for more information.
        /// </remarks>
        /// <param name="columnId">The id of the column</param>
        public IObservable<ProjectCard> GetAll(int columnId)
        {
            return GetAll(columnId, ApiOptions.None);
        }

        /// <summary>
        /// Gets all cards for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-projects-cards">API documentation</a> for more information.
        /// </remarks>
        /// <param name="columnId">The id of the column</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<ProjectCard> GetAll(int columnId, ApiOptions options)
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
        public IObservable<ProjectCard> GetAll(int columnId, ProjectCardRequest request)
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
        public IObservable<ProjectCard> GetAll(int columnId, ProjectCardRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            var url = ApiUrls.ProjectCards(columnId);

            return _connection.GetAndFlattenAllPages<ProjectCard>(url, request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Gets a single card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#get-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the card</param>
        public IObservable<ProjectCard> Get(long id)
        {
            return _client.Get(id).ToObservable();
        }

        /// <summary>
        /// Creates a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#create-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="columnId">The id of the column</param>
        /// <param name="newProjectCard">The card to create</param>
        public IObservable<ProjectCard> Create(int columnId, NewProjectCard newProjectCard)
        {
            Ensure.ArgumentNotNull(newProjectCard, nameof(newProjectCard));

            return _client.Create(columnId, newProjectCard).ToObservable();
        }

        /// <summary>
        /// Updates a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#update-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the card</param>
        /// <param name="projectCardUpdate">New values to update the card with</param>
        public IObservable<ProjectCard> Update(long id, ProjectCardUpdate projectCardUpdate)
        {
            Ensure.ArgumentNotNull(projectCardUpdate, nameof(projectCardUpdate));

            return _client.Update(id, projectCardUpdate).ToObservable();
        }

        /// <summary>
        /// Deletes a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#delete-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the card</param>
        public IObservable<bool> Delete(long id)
        {
            return _client.Delete(id).ToObservable();
        }

        /// <summary>
        /// Moves a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#move-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the card</param>
        /// <param name="position">The position to move the card</param>
        public IObservable<bool> Move(long id, ProjectCardMove position)
        {
            Ensure.ArgumentNotNull(position, nameof(position));

            return _client.Move(id, position).ToObservable();
        }
    }
}
