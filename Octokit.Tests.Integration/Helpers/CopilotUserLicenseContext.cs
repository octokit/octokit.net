using System;

namespace Octokit.Tests.Integration.Helpers
{
    internal sealed class CopilotUserLicenseContext : IDisposable
    {
        internal CopilotUserLicenseContext(IConnection connection, string organization, string user)
        {
            _connection = connection;
            Organization = organization;
            UserLogin = user;
        }

        private readonly IConnection _connection;

        internal string Organization { get; }
        internal string UserLogin { get; private set; }

        public void Dispose()
        {
            CopilotHelper.RemoveUserLicense(_connection, Organization, UserLogin);
        }
    }
}
