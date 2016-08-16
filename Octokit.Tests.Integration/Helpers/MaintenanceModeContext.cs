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
            _initialState = enabled;

            // Ensure maintenance mode is in the desired state
            EnterpriseHelper.SetMaintenanceMode(_connection, _initialState);
        }

        private IConnection _connection;

        private bool _initialState;
        
        public void Dispose()
        {
            // Ensure maintenance mode is restored to desired state
            EnterpriseHelper.SetMaintenanceMode(_connection, _initialState);
        }
    }
}
