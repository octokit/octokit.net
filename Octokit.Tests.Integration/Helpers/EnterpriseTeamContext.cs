using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Tests.Integration.Helpers
{
    internal sealed class EnterpriseTeamContext : IDisposable
    {
        internal EnterpriseTeamContext(Team team)
        {
            Team = team;
            TeamId = team.Id;
            TeamName = team.Name;
        }

        internal int TeamId { get; private set; }
        internal string TeamName { get; private set; }

        internal Team Team { get; private set; }

        public void Dispose()
        {
            EnterpriseHelper.DeleteTeam(Team);
        }
    }
}
