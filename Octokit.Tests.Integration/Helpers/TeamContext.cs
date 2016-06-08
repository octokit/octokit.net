using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        private IConnection _connection;
        internal int TeamId { get; private set; }
        internal string TeamName { get; private set; }

        internal Team Team { get; private set; }

        public void Dispose()
        {
            Helper.DeleteTeam(_connection, Team);
        }
    }
}
