using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit.Reactive;

namespace Octokit.Tests.Integration.Helpers
{
    internal sealed class MaintenanceModeContext : IDisposable
    {
        internal MaintenanceModeContext(IConnection connection, bool enabled)
        {
            _connection = connection;

            // Ensure maintenance mode is in the desired initial state
            EnterpriseHelper.SetMaintenanceMode(_connection, enabled);
        }

        private IConnection _connection;

        public void Dispose()
        {
            // Ensure maintenance mode is OFF
            EnterpriseHelper.SetMaintenanceMode(_connection, false);
        }
    }
}
