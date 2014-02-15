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

                var client = Substitute.For<IApiConnection>();
                var statisticsClient = new StatisticsClient(client);

                statisticsClient.GetContributors("username","repositoryName");

                client.Received().GetQueuedOperation<IEnumerable<Contributor>>(expectedEndPoint,Args.CancellationToken);
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

                var client = Substitute.For<IApiConnection>();
                var statisticsClient = new StatisticsClient(client);

                statisticsClient.GetCommitActivityForTheLastYear("username", "repositoryName");

                client.Received().GetQueuedOperation<IEnumerable<WeeklyCommitActivity>>(expectedEndPoint, Args.CancellationToken);
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

                var client = Substitute.For<IApiConnection>();
                var statisticsClient = new StatisticsClient(client);

                statisticsClient.GetAdditionsAndDeletionsPerWeek("username", "repositoryName");

                client.Received().GetQueuedOperation<IEnumerable<int[]>>(expectedEndPoint, Args.CancellationToken);
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

                var client = Substitute.For<IApiConnection>();
                var statisticsClient = new StatisticsClient(client);

                statisticsClient.GetCommitCountsPerWeek("username", "repositoryName");

                client.Received().GetQueuedOperation<WeeklyCommitCounts>(expectedEndPoint, Args.CancellationToken);
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

        public class TheGetHourlyCommitCountsMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var expectedEndPoint = new Uri("/repos/username/repositoryName/stats/punch_card", UriKind.Relative);

                var client = Substitute.For<IApiConnection>();
                var statisticsClient = new StatisticsClient(client);

                statisticsClient.GetPunchCard("username", "repositoryName");

                client.Received().GetQueuedOperation<IEnumerable<int[]>>(expectedEndPoint, Args.CancellationToken);
            }

            [Fact]
            public async Task ThrowsIfGivenNullOwner()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await AssertEx.Throws<ArgumentNullException>(() => statisticsClient.GetPunchCard(null, "repositoryName"));
            }

            [Fact]
            public async Task ThrowsIfGivenNullRepositoryName()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await AssertEx.Throws<ArgumentNullException>(() => statisticsClient.GetPunchCard("owner", null));
            }
        }
    }
}