using System;
using System.Collections.Generic;
using System.Linq;

namespace Octokit.Tests.Integration.Helpers
{
    internal sealed class TeamContext : IDisposable
    {
        internal TeamContext(IConnection connection, Team team)
        {
            _connection = connection;
            Team = team;
            TeamId = team.Id;
            TeamName = team.Name;
            Invitations = new List<string>();
        }

        private readonly IConnection _connection;
        internal long TeamId { get; private set; }
        internal string TeamName { get; private set; }

        internal Team Team { get; private set; }
        internal List<string> Invitations { get; private set; }

        public void InviteMember(string login)
        {
            Invitations.Add(Helper.InviteMemberToTeam(_connection, TeamId, login));
        }

        public void Dispose()
        {
            if (Invitations.Any())
                Helper.DeleteInvitations(_connection, Invitations);

            Helper.DeleteTeam(_connection, Team);
        }
    }
}
