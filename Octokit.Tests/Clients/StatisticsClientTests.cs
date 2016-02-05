using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Helpers;
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
            public async Task RetrievesContributorsForCorrectUrl()
            {
                var expectedEndPoint = new Uri("repos/username/repositoryName/stats/contributors", UriKind.Relative);
                var client = Substitute.For<IApiConnection>();
                IReadOnlyList<Contributor> contributors = new ReadOnlyCollection<Contributor>(new[] { new Contributor() });
                client.GetQueuedOperation<Contributor>(expectedEndPoint, Args.CancellationToken)
                    .Returns(Task.FromResult(contributors));
                var statisticsClient = new StatisticsClient(client);

                var result = await statisticsClient.GetContributors("username", "repositoryName");

                Assert.Equal(1, result.Count);
            }

            [Fact]
            public async Task ThrowsIfGivenNullOwner()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => statisticsClient.GetContributors(null, "repositoryName"));
            }

            [Fact]
            public async Task ThrowsIfGivenNullRepositoryName()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => statisticsClient.GetContributors("owner", null));
            }
        }

        public class TheGetCommitActivityForTheLastYearMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var expectedEndPoint = new Uri("repos/username/repositoryName/stats/commit_activity", UriKind.Relative);

                var data = new WeeklyCommitActivity(new[] { 1, 2 }, 100, 42);
                IReadOnlyList<WeeklyCommitActivity> response = new ReadOnlyCollection<WeeklyCommitActivity>(new[] { data });
                var client = Substitute.For<IApiConnection>();
                client.GetQueuedOperation<WeeklyCommitActivity>(expectedEndPoint, Args.CancellationToken)
                    .Returns(Task.FromResult(response));
                var statisticsClient = new StatisticsClient(client);

                var result = await statisticsClient.GetCommitActivity("username", "repositoryName");

                Assert.Equal(2, result.Activity[0].Days.Count);
                Assert.Equal(1, result.Activity[0].Days[0]);
                Assert.Equal(2, result.Activity[0].Days[1]);
                Assert.Equal(100, result.Activity[0].Total);
                Assert.Equal(42, result.Activity[0].Week);
            }

            [Fact]
            public async Task ThrowsIfGivenNullOwner()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => statisticsClient.GetCommitActivity(null, "repositoryName"));
            }

            [Fact]
            public async Task ThrowsIfGivenNullRepositoryName()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => statisticsClient.GetCommitActivity("owner", null));
            }
        }

        public class TheGetAdditionsAndDeletionsPerWeekMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var expectedEndPoint = new Uri("repos/username/repositoryName/stats/code_frequency", UriKind.Relative);

                long firstTimestamp = 159670861;
                long secondTimestamp = 0;
                IReadOnlyList<long[]> data = new ReadOnlyCollection<long[]>(new[]
                {
                    new[] { firstTimestamp, 10, 52 },
                    new[] { secondTimestamp, 0, 9 }
                });
                var client = Substitute.For<IApiConnection>();
                client.GetQueuedOperation<long[]>(expectedEndPoint, Args.CancellationToken)
                    .Returns(Task.FromResult(data));
                var statisticsClient = new StatisticsClient(client);

                var codeFrequency = await statisticsClient.GetCodeFrequency("username", "repositoryName");

                Assert.Equal(2, codeFrequency.AdditionsAndDeletionsByWeek.Count);
                Assert.Equal(firstTimestamp.FromUnixTime(), codeFrequency.AdditionsAndDeletionsByWeek[0].Timestamp);
                Assert.Equal(10, codeFrequency.AdditionsAndDeletionsByWeek[0].Additions);
                Assert.Equal(52, codeFrequency.AdditionsAndDeletionsByWeek[0].Deletions);
                Assert.Equal(secondTimestamp.FromUnixTime(), codeFrequency.AdditionsAndDeletionsByWeek[1].Timestamp);
                Assert.Equal(0, codeFrequency.AdditionsAndDeletionsByWeek[1].Additions);
                Assert.Equal(9, codeFrequency.AdditionsAndDeletionsByWeek[1].Deletions);
            }

            [Fact]
            public async Task ThrowsIfGivenNullOwner()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => statisticsClient.GetCodeFrequency(null, "repositoryName"));
            }

            [Fact]
            public async Task ThrowsIfGivenNullRepositoryName()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => statisticsClient.GetCodeFrequency("owner", null));
            }
        }

        public class TheGetWeeklyCommitCountsMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var expectedEndPoint = new Uri("repos/username/repositoryName/stats/participation", UriKind.Relative);

                var client = Substitute.For<IApiConnection>();
                var statisticsClient = new StatisticsClient(client);

                statisticsClient.GetParticipation("username", "repositoryName");

                client.Received().GetQueuedOperation<Participation>(expectedEndPoint, Args.CancellationToken);
            }

            [Fact]
            public async Task ThrowsIfGivenNullOwner()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => statisticsClient.GetParticipation(null, "repositoryName"));
            }

            [Fact]
            public async Task ThrowsIfGivenNullRepositoryName()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => statisticsClient.GetParticipation("owner", null));
            }
        }

        public class TheGetHourlyCommitCountsMethod
        {
            [Fact]
            public async Task RetrievesPunchCard()
            {
                var expectedEndPoint = new Uri("repos/username/repositoryName/stats/punch_card", UriKind.Relative);

                var client = Substitute.For<IApiConnection>();
                IReadOnlyList<int[]> data = new ReadOnlyCollection<int[]>(new[] { new[] { 2, 8, 42 } });
                client.GetQueuedOperation<int[]>(expectedEndPoint, Args.CancellationToken)
                    .Returns(Task.FromResult(data));
                var statisticsClient = new StatisticsClient(client);

                var result = await statisticsClient.GetPunchCard("username", "repositoryName");

                Assert.Equal(1, result.PunchPoints.Count);
                Assert.Equal(DayOfWeek.Tuesday, result.PunchPoints[0].DayOfWeek);
                Assert.Equal(8, result.PunchPoints[0].HourOfTheDay);
                Assert.Equal(42, result.PunchPoints[0].CommitCount);
            }

            [Fact]
            public async Task ThrowsIfGivenNullOwner()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => statisticsClient.GetPunchCard(null, "repositoryName"));
            }

            [Fact]
            public async Task ThrowsIfGivenNullRepositoryName()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => statisticsClient.GetPunchCard("owner", null));
            }
        }
    }
}
