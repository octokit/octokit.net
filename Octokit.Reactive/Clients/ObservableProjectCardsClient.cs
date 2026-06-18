using Octokit.Reactive.Internal;
using System;
using System.Threading;
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
        public IObservable<ProjectCard> GetAll(int columnId, CancellationToken cancellationToken = default)
        {
            return GetAll(columnId, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all cards for this project.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-projects-cards">API documentation</a> for more information.
        /// </remarks>
        /// <param name="columnId">The id of the column</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<ProjectCard> GetAll(int columnId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAll(columnId, new ProjectCardRequest(), options, cancellationToken);
        }

        /// <summary>
        /// Gets all cards.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-projects-cards">API documentation</a> for more information.
        /// </remarks>
        /// <param name="columnId">The id of the column</param>
        /// <param name="request">Used to filter the list of project cards returned</param>
        public IObservable<ProjectCard> GetAll(int columnId, ProjectCardRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAll(columnId, request, ApiOptions.None, cancellationToken);
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
        public IObservable<ProjectCard> GetAll(int columnId, ProjectCardRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            var url = ApiUrls.ProjectCards(columnId);

            return _connection.GetAndFlattenAllPages<ProjectCard>(url, request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Gets a single card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#get-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the card</param>
        public IObservable<ProjectCard> Get(long id, CancellationToken cancellationToken = default)
        {
            return _client.Get(id, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Creates a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#create-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="columnId">The id of the column</param>
        /// <param name="newProjectCard">The card to create</param>
        public IObservable<ProjectCard> Create(int columnId, NewProjectCard newProjectCard, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(newProjectCard, nameof(newProjectCard));

            return _client.Create(columnId, newProjectCard, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Updates a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#update-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the card</param>
        /// <param name="projectCardUpdate">New values to update the card with</param>
        public IObservable<ProjectCard> Update(long id, ProjectCardUpdate projectCardUpdate, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(projectCardUpdate, nameof(projectCardUpdate));

            return _client.Update(id, projectCardUpdate, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Deletes a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#delete-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the card</param>
        public IObservable<bool> Delete(long id, CancellationToken cancellationToken = default)
        {
            return _client.Delete(id, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Moves a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#move-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the card</param>
        /// <param name="position">The position to move the card</param>
        public IObservable<bool> Move(long id, ProjectCardMove position, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(position, nameof(position));

            return _client.Move(id, position, cancellationToken).ToObservable();
        }
    }
}
