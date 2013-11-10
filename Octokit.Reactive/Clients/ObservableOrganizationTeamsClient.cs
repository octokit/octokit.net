using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableOrganizationTeamsClient : IObservableOrganizationTeamsClient
    {
        //readonly IConnection _connection;

        /// <summary>
        /// Initializes a new Organization Teams API client.
        /// </summary>
        /// <param name="client">An <see cref="IGitHubClient" /> used to make the requests</param>
        public ObservableOrganizationTeamsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");
            //_connection = client.Connection;
        }

        public IObservable<TeamItem> GetAllTeams(string org)
        {
            throw new NotImplementedException();
        }

        public IObservable<Team> CreateTeam(string org, NewTeam team)
        {
            throw new NotImplementedException();
        }

        public IObservable<Team> UpdateTeam(int id, UpdateTeam team)
        {
            throw new NotImplementedException();
        }

        public IObservable<Unit> DeleteTeam(int id)
        {
            throw new NotImplementedException();
        }
    }
}
