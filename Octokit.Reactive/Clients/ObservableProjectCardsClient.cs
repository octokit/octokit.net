using Octokit.Reactive.Internal;
using System;
using System.Reactive.Threading.Tasks;
using System.Collections.Generic;

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
            Ensure.ArgumentNotNull(client, "client");

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
            Ensure.ArgumentNotNull(options, "options");

            var url = ApiUrls.ProjectCards(columnId);

            return _connection.GetAndFlattenAllPages<ProjectCard>(url, new Dictionary<string, string>(), AcceptHeaders.ProjectsApiPreview, options);
        }

        /// <summary>
        /// Gets a single card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#get-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the card</param>
        public IObservable<ProjectCard> Get(int id)
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
            Ensure.ArgumentNotNull(newProjectCard, "newProjectCard");

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
        public IObservable<ProjectCard> Update(int id, ProjectCardUpdate projectCardUpdate)
        {
            Ensure.ArgumentNotNull(projectCardUpdate, "projectCardUpdate");

            return _client.Update(id, projectCardUpdate).ToObservable();
        }

        /// <summary>
        /// Deletes a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#delete-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the card</param>
        public IObservable<bool> Delete(int id)
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
        public IObservable<bool> Move(int id, ProjectCardMove position)
        {
            Ensure.ArgumentNotNull(position, "position");

            return _client.Move(id, position).ToObservable();
        }
    }
}
