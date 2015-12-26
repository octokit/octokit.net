using System;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class EnterpriseAdminStatsClientTest
    {
        public class TheGetStatisticsMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseAdminStatsClient(connection);

                foreach (AdminStatsType type in Enum.GetValues(typeof(AdminStatsType)))
                {
                    client.GetStatistics(type);

                    connection.Received().Get<AdminStats>(Arg.Is<Uri>(u => u == "enterprise/stats/{0}".FormatUri(type.ToString().ToLowerInvariant())), null);
                }
            }
        }
    }
}