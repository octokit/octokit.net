using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Team Discussions API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/teams/discussions/">Team Discussions API documentation</a> for more information.
    /// </remarks>
    public class ObservableTeamDiscussionsClient : IObservableTeamDiscussionsClient
    {
        private static readonly string AcceptHeader = AcceptHeaders.Concat(AcceptHeaders.TeamDiscussionsApiPreview, AcceptHeaders.ReactionsPreview);

        readonly IConnection _connection;
        readonly ITeamDiscussionsClient _client;

        /// <summary>
        /// Initializes a new Organization Teams API client.
        /// </summary>
        /// <param name="client">An <see cref="IGitHubClient" /> used to make the requests</param>
        public ObservableTeamDiscussionsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));
            _connection = client.Connection;
            _client = client.Organization.Team.TeamDiscussion;
        }

        /// <summary>
        /// Get a specific discussion on a team's page.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/teams/discussions/#get-a-single-discussion
        /// </remarks>
        /// <param name="teamId">The team identifier.</param>
        /// <param name="number">The team discussion number.</param>
        /// <returns>The <see cref="TeamDiscussion"/> with the given identifier.</returns>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<TeamDiscussion> Get(int teamId, int number)
        {
            return _client.Get(teamId, number).ToObservable();
        }

        /// <summary>
        /// List all discussions on a team's page.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/teams/discussions/#list-discussions
        /// </remarks>
        /// <param name="teamId">The team identifier.</param>
        /// <returns>A list of discussions in the team.</returns>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<TeamDiscussion> GetAll(int teamId)
        {
            return GetAll(teamId, ApiOptions.None);
        }

        /// <summary>
        /// List all discussions on a team's page.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/teams/discussions/#list-discussions
        /// </remarks>
        /// <param name="teamId">The team identifier.</param>
        /// <param name="options">Options to change API behaviour.</param>
        /// <returns>A list of discussions in the team.</returns>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<TeamDiscussion> GetAll(int teamId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.TeamDiscussions(teamId);
            return _connection.GetAndFlattenAllPages<TeamDiscussion>(endpoint, null, AcceptHeader, options);
        }

        /// <summary>
        /// Creates a new discussion post on a team's page.
        /// </summary>
        /// <remarks>
        /// OAuth access tokens require the write:discussion scope.
        /// https://developer.github.com/v3/teams/discussions/#create-a-discussion
        /// </remarks>
        /// <param name="teamId">The team identifier.</param>
        /// <param name="newTeamDiscussion">New team discussion.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>Newly created <see cref="TeamDiscussion"/> object.</returns>
        public IObservable<TeamDiscussion> Create(int teamId, NewTeamDiscussion teamDiscussion)
        {
            Ensure.ArgumentNotNull(teamDiscussion, nameof(teamDiscussion));

            return _client.Create(teamId, teamDiscussion).ToObservable();
        }

        /// <summary>
        /// Edits the title and body text of a discussion post.
        /// Only the parameters you provide are updated.
        /// </summary>
        /// <remarks>
        /// OAuth access tokens require the write:discussion scope.
        /// https://developer.github.com/v3/teams/discussions/#edit-a-discussion
        /// </remarks>
        /// <param name="teamId">The team identifier.</param>
        /// <param name="number">The team discussion number.</param>
        /// <param name="updateTeamDiscussion">New values for the discussion.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>Updated <see cref="TeamDiscussion"/> object.</returns>
        public IObservable<TeamDiscussion> Update(int teamId, int number, UpdateTeamDiscussion teamDiscussion)
        {
            Ensure.ArgumentNotNull(teamDiscussion, nameof(teamDiscussion));

            return _client.Update(teamId, number, teamDiscussion).ToObservable();
        }

        /// <summary>
        /// Delete a discussion from a team's page.
        /// </summary>
        /// <remarks>
        /// OAuth access tokens require the write:discussion scope.
        /// https://developer.github.com/v3/teams/discussions/#delete-a-discussion
        /// </remarks>
        /// <param name="teamId">The team identifier.</param>
        /// <param name="number">The team discussion number.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<Unit> Delete(int teamId, int number)
        {
            return _client.Delete(teamId, number).ToObservable();
        }
    }
}
