using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Project Cards API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/projects/cards/">Repository Projects API documentation</a> for more information.
    /// </remarks>
    public interface IObservableProjectCardsClient
    {
        /// <summary>
        /// Gets all cards.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-projects-cards">API documentation</a> for more information.
        /// </remarks>
        /// <param name="columnId">The id of the column</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<ProjectCard> GetAll(int columnId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all cards.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-projects-cards">API documentation</a> for more information.
        /// </remarks>
        /// <param name="columnId">The id of the column</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<ProjectCard> GetAll(int columnId, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all cards.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-projects-cards">API documentation</a> for more information.
        /// </remarks>
        /// <param name="columnId">The id of the column</param>
        /// <param name="request">Used to filter the list of project cards returned</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<ProjectCard> GetAll(int columnId, ProjectCardRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all cards.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#list-projects-cards">API documentation</a> for more information.
        /// </remarks>
        /// <param name="columnId">The id of the column</param>
        /// <param name="request">Used to filter the list of project cards returned</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<ProjectCard> GetAll(int columnId, ProjectCardRequest request, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a single card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#get-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the card</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        IObservable<ProjectCard> Get(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#create-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="columnId">The id of the column</param>
        /// <param name="newProjectCard">The card to create</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<ProjectCard> Create(int columnId, NewProjectCard newProjectCard, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#update-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the card</param>
        /// <param name="projectCardUpdate">New values to update the card with</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<ProjectCard> Update(long id, ProjectCardUpdate projectCardUpdate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#delete-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the card</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<bool> Delete(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Moves a card.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/projects/#move-a-project-card">API documentation</a> for more information.
        /// </remarks>
        /// <param name="id">The id of the card</param>
        /// <param name="position">The position to move the card</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<bool> Move(long id, ProjectCardMove position, CancellationToken cancellationToken = default);
    }
}
