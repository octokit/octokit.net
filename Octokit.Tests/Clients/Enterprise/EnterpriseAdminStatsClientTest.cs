using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Tests.Helpers;
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