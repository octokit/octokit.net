using NSubstitute;
using Octokit.Reactive;
using System;
using System.Collections.Generic;
using Xunit;

namespace Octokit.Tests
{
    public class ObservableEnterpriseAdminStatsClientTests
    {
        public class TheGetStatisticsMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseAdminStatsClient(github);

                foreach (AdminStatsType type in Enum.GetValues(typeof(AdminStatsType)))
                {
                    var expectedUri = "enterprise/stats/{0}".FormatUri(type.ToString().ToLowerInvariant());
                    client.GetStatistics(type);

                    github.Enterprise.AdminStats.Received(1).GetStatistics(type);
                }
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservableEnterpriseAdminStatsClient(null));
            }
        }
    }
}
