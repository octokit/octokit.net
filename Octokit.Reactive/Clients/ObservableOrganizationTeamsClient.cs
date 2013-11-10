using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableOrganizationTeamsClient : IObservableOrganizationTeamsClient
    {
        readonly IConnection _connection;
        readonly ITeamsClient _client;

        /// <summary>
        /// Initializes a new Organization Teams API client.
        /// </summary>
        /// <param name="client">An <see cref="IGitHubClient" /> used to make the requests</param>
        public ObservableOrganizationTeamsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");
            _connection = client.Connection;
            _client = client.Organization.Team;
        }

        public IObservable<TeamItem> GetAllTeams(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");
            return _connection.GetAndFlattenAllPages<TeamItem>(ApiUrls.OrganizationTeams(org));
        }

        public IObservable<Team> CreateTeam(string org, NewTeam team)
        {
            return _client.CreateTeam(org, team).ToObservable();
        }

        public IObservable<Team> UpdateTeam(int id, UpdateTeam team)
        {
            return _client.UpdateTeam(id, team).ToObservable();
        }

        public IObservable<Unit> DeleteTeam(int id)
        {
            return _client.DeleteTeam(id).ToObservable();
        }
    }
}
