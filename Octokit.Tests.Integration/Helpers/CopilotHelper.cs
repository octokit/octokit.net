using System;

namespace Octokit.Tests.Integration.Helpers
{
    internal sealed class CopilotHelper
    {
        public static void RemoveUserLicense(IConnection connection, string organization, string userLogin)
        {
            var client = new GitHubClient(connection);
            client.Copilot.Licensing.Remove(organization, userLogin).Wait(TimeSpan.FromSeconds(15));
        }
    }
}
