using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class StatisticsClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void DoesThrowOnBadArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new StatisticsClient(null));
            }
        }

        public class TheGetContributorsMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var expectedEndPoint = new Uri("/repos/username/repositoryName/stats/contributors", UriKind.Relative);

                var connection = Substitute.For<IConnection>();
                var client = Substitute.For<IApiConnection>();
                client.Connection.Returns(connection);
                var statisticsClient = new StatisticsClient(client);

                statisticsClient.GetContributors("username","repositoryName");

                connection.Received().GetAsync<IList<Contributor>>(expectedEndPoint);
            }

            [Fact]
            public async Task ThrowsIfGivenNullOwner()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await AssertEx.Throws<ArgumentNullException>(() => statisticsClient.GetContributors(null,"repositoryName"));
            }

            [Fact]
            public async Task ThrowsIfGivenNullRepositoryName()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await AssertEx.Throws<ArgumentNullException>(() => statisticsClient.GetContributors("owner", null));
            }
        }

        public class TheGetCommitActivityForTheLastYearMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var expectedEndPoint = new Uri("/repos/username/repositoryName/stats/commit_activity", UriKind.Relative);

                var connection = Substitute.For<IConnection>();
                var client = Substitute.For<IApiConnection>();
                client.Connection.Returns(connection);
                var statisticsClient = new StatisticsClient(client);

                statisticsClient.GetCommitActivityForTheLastYear("username", "repositoryName");

                connection.Received().GetAsync<IList<WeeklyCommitActivity>>(expectedEndPoint);
            }

            [Fact]
            public async Task ThrowsIfGivenNullOwner()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await AssertEx.Throws<ArgumentNullException>(() => statisticsClient.GetCommitActivityForTheLastYear(null, "repositoryName"));
            }

            [Fact]
            public async Task ThrowsIfGivenNullRepositoryName()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await AssertEx.Throws<ArgumentNullException>(() => statisticsClient.GetCommitActivityForTheLastYear("owner", null));
            }
        }

        public class TheGetAdditionsAndDeletionsPerWeekMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var expectedEndPoint = new Uri("/repos/username/repositoryName/stats/code_frequency", UriKind.Relative);

                var connection = Substitute.For<IConnection>();
                var client = Substitute.For<IApiConnection>();
                client.Connection.Returns(connection);
                var statisticsClient = new StatisticsClient(client);

                statisticsClient.GetAdditionsAndDeletionsPerWeek("username", "repositoryName");

                connection.Received().GetAsync<IList<int[]>>(expectedEndPoint);
            }

            [Fact]
            public async Task ThrowsIfGivenNullOwner()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await AssertEx.Throws<ArgumentNullException>(() => statisticsClient.GetAdditionsAndDeletionsPerWeek(null, "repositoryName"));
            }

            [Fact]
            public async Task ThrowsIfGivenNullRepositoryName()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await AssertEx.Throws<ArgumentNullException>(() => statisticsClient.GetAdditionsAndDeletionsPerWeek("owner", null));
            }
        }

        public class TheGetWeeklyCommitCountsMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var expectedEndPoint = new Uri("/repos/username/repositoryName/stats/participation", UriKind.Relative);

                var connection = Substitute.For<IConnection>();
                var client = Substitute.For<IApiConnection>();
                client.Connection.Returns(connection);
                var statisticsClient = new StatisticsClient(client);

                statisticsClient.GetCommitCountsPerWeek("username", "repositoryName");

                connection.Received().GetAsync<WeeklyCommitCounts>(expectedEndPoint);
            }

            [Fact]
            public async Task ThrowsIfGivenNullOwner()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await AssertEx.Throws<ArgumentNullException>(() => statisticsClient.GetCommitCountsPerWeek(null, "repositoryName"));
            }

            [Fact]
            public async Task ThrowsIfGivenNullRepositoryName()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await AssertEx.Throws<ArgumentNullException>(() => statisticsClient.GetCommitCountsPerWeek("owner", null));
            }
        }
    }
}