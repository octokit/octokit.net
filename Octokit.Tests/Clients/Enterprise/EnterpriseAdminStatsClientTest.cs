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

                foreach (AdminStatsType adminType in Enum.GetValues(typeof(AdminStatsType)))
                {
                    client.GetStatistics(adminType);

                    connection.Received().Get<AdminStats>(Arg.Is<Uri>(u => u.ToString() == String.Concat("enterprise/stats/", adminType.ToString().ToLowerInvariant())));
                }
            }
        }
    }
}