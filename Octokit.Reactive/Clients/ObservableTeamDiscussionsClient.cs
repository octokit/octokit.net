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
            _client = client.Organization.TeamDiscussions;
        }

        public IObservable<TeamDiscussion> Get(int teamId, int number)
        {
            return _client.Get(teamId, number).ToObservable();
        }

        public IObservable<TeamDiscussion> GetAll(int teamId)
        {
            return GetAll(teamId, ApiOptions.None);
        }

        public IObservable<TeamDiscussion> GetAll(int teamId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.TeamDiscussions(teamId);
            return _connection.GetAndFlattenAllPages<TeamDiscussion>(endpoint, null, AcceptHeader, options);
        }

        public IObservable<TeamDiscussion> Create(int teamId, NewTeamDiscussion teamDiscussion)
        {
            Ensure.ArgumentNotNull(teamDiscussion, nameof(teamDiscussion));

            return _client.Create(teamId, teamDiscussion).ToObservable();
        }

        public IObservable<TeamDiscussion> Update(int teamId, int number, UpdateTeamDiscussion teamDiscussion)
        {
            Ensure.ArgumentNotNull(teamDiscussion, nameof(teamDiscussion));

            return _client.Update(teamId, number, teamDiscussion).ToObservable();
        }

        public IObservable<Unit> Delete(int teamId, int number)
        {
            return _client.Delete(teamId, number).ToObservable();
        }
    }
}
