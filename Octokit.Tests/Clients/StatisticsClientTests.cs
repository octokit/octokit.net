using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class StatisticsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new StatisticsClient(null));
            }
        }

        public class TheGetContributorsMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var expectedEndPoint = new Uri("repos/owner/name/stats/contributors", UriKind.Relative);
                IReadOnlyList<Contributor> contributors = new ReadOnlyCollection<Contributor>(new[] { new Contributor() });

                var connection = Substitute.For<IApiConnection>();
                connection.GetQueuedOperation<Contributor>(expectedEndPoint, Args.CancellationToken)
                    .Returns(Task.FromResult(contributors));

                var client = new StatisticsClient(connection);

                var result = await client.GetContributors("owner", "name");

                Assert.Single(result);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var expectedEndPoint = new Uri("repositories/1/stats/contributors", UriKind.Relative);
                IReadOnlyList<Contributor> contributors = new ReadOnlyCollection<Contributor>(new[] { new Contributor() });

                var connection = Substitute.For<IApiConnection>();
                connection.GetQueuedOperation<Contributor>(expectedEndPoint, Args.CancellationToken)
                    .Returns(Task.FromResult(contributors));

                var client = new StatisticsClient(connection);

                var result = await client.GetContributors(1);

                Assert.Single(result);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithCancellactionToken()
            {
                var expectedEndPoint = new Uri("repos/owner/name/stats/contributors", UriKind.Relative);
                IReadOnlyList<Contributor> contributors = new ReadOnlyCollection<Contributor>(new[] { new Contributor() });
                var cancellationToken = new CancellationToken(true);

                var connection = Substitute.For<IApiConnection>();

                connection.GetQueuedOperation<Contributor>(expectedEndPoint, cancellationToken)
                    .Returns(Task.FromResult(contributors));

                var client = new StatisticsClient(connection);

                var result = await client.GetContributors("owner", "name", cancellationToken);

                Assert.Single(result);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithCancellactionToken()
            {
                var expectedEndPoint = new Uri("repositories/1/stats/contributors", UriKind.Relative);
                IReadOnlyList<Contributor> contributors = new ReadOnlyCollection<Contributor>(new[] { new Contributor() });
                var cancellationToken = new CancellationToken(true);

                var connection = Substitute.For<IApiConnection>();

                connection.GetQueuedOperation<Contributor>(expectedEndPoint, cancellationToken)
                    .Returns(Task.FromResult(contributors));

                var client = new StatisticsClient(connection);

                var result = await client.GetContributors(1, cancellationToken);

                Assert.Single(result);
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new StatisticsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetContributors(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetContributors("owner", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetContributors("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetContributors("owner", ""));
            }
        }

        public class TheGetCommitActivityForTheLastYearMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var expectedEndPoint = new Uri("repos/owner/name/stats/commit_activity", UriKind.Relative);

                var data = new WeeklyCommitActivity(new[] { 1, 2 }, 100, 42);
                IReadOnlyList<WeeklyCommitActivity> response = new ReadOnlyCollection<WeeklyCommitActivity>(new[] { data });
                var client = Substitute.For<IApiConnection>();
                client.GetQueuedOperation<WeeklyCommitActivity>(expectedEndPoint, Args.CancellationToken)
                    .Returns(Task.FromResult(response));
                var statisticsClient = new StatisticsClient(client);

                var result = await statisticsClient.GetCommitActivity("owner", "name");

                Assert.Equal(2, result.Activity[0].Days.Count);
                Assert.Equal(1, result.Activity[0].Days[0]);
                Assert.Equal(2, result.Activity[0].Days[1]);
                Assert.Equal(100, result.Activity[0].Total);
                Assert.Equal(42, result.Activity[0].Week);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var expectedEndPoint = new Uri("repositories/1/stats/commit_activity", UriKind.Relative);

                var data = new WeeklyCommitActivity(new[] { 1, 2 }, 100, 42);
                IReadOnlyList<WeeklyCommitActivity> response = new ReadOnlyCollection<WeeklyCommitActivity>(new[] { data });
                var client = Substitute.For<IApiConnection>();
                client.GetQueuedOperation<WeeklyCommitActivity>(expectedEndPoint, Args.CancellationToken)
                    .Returns(Task.FromResult(response));
                var statisticsClient = new StatisticsClient(client);

                var result = await statisticsClient.GetCommitActivity(1);

                Assert.Equal(2, result.Activity[0].Days.Count);
                Assert.Equal(1, result.Activity[0].Days[0]);
                Assert.Equal(2, result.Activity[0].Days[1]);
                Assert.Equal(100, result.Activity[0].Total);
                Assert.Equal(42, result.Activity[0].Week);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithCancellationToken()
            {
                var expectedEndPoint = new Uri("repos/owner/name/stats/commit_activity", UriKind.Relative);
                var cancellationToken = new CancellationToken();

                var data = new WeeklyCommitActivity(new[] { 1, 2 }, 100, 42);
                IReadOnlyList<WeeklyCommitActivity> response = new ReadOnlyCollection<WeeklyCommitActivity>(new[] { data });
                var connection = Substitute.For<IApiConnection>();
                connection.GetQueuedOperation<WeeklyCommitActivity>(expectedEndPoint, cancellationToken)
                    .Returns(Task.FromResult(response));

                var client = new StatisticsClient(connection);

                var result = await client.GetCommitActivity("owner", "name", cancellationToken);

                Assert.Equal(2, result.Activity[0].Days.Count);
                Assert.Equal(1, result.Activity[0].Days[0]);
                Assert.Equal(2, result.Activity[0].Days[1]);
                Assert.Equal(100, result.Activity[0].Total);
                Assert.Equal(42, result.Activity[0].Week);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithCancellationToken()
            {
                var expectedEndPoint = new Uri("repositories/1/stats/commit_activity", UriKind.Relative);
                var cancellationToken = new CancellationToken();

                var data = new WeeklyCommitActivity(new[] { 1, 2 }, 100, 42);
                IReadOnlyList<WeeklyCommitActivity> response = new ReadOnlyCollection<WeeklyCommitActivity>(new[] { data });
                var connection = Substitute.For<IApiConnection>();
                connection.GetQueuedOperation<WeeklyCommitActivity>(expectedEndPoint, cancellationToken)
                    .Returns(Task.FromResult(response));

                var client = new StatisticsClient(connection);

                var result = await client.GetCommitActivity(1, cancellationToken);

                Assert.Equal(2, result.Activity[0].Days.Count);
                Assert.Equal(1, result.Activity[0].Days[0]);
                Assert.Equal(2, result.Activity[0].Days[1]);
                Assert.Equal(100, result.Activity[0].Total);
                Assert.Equal(42, result.Activity[0].Week);
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new StatisticsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetCommitActivity(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetCommitActivity("owner", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetCommitActivity("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetCommitActivity("owner", ""));
            }
        }

        public class TheGetAdditionsAndDeletionsPerWeekMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var expectedEndPoint = new Uri("repos/owner/name/stats/code_frequency", UriKind.Relative);

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

                var codeFrequency = await statisticsClient.GetCodeFrequency("owner", "name");

                Assert.Equal(2, codeFrequency.AdditionsAndDeletionsByWeek.Count);
                Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(firstTimestamp), codeFrequency.AdditionsAndDeletionsByWeek[0].Timestamp);
                Assert.Equal(10, codeFrequency.AdditionsAndDeletionsByWeek[0].Additions);
                Assert.Equal(52, codeFrequency.AdditionsAndDeletionsByWeek[0].Deletions);
                Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(secondTimestamp), codeFrequency.AdditionsAndDeletionsByWeek[1].Timestamp);
                Assert.Equal(0, codeFrequency.AdditionsAndDeletionsByWeek[1].Additions);
                Assert.Equal(9, codeFrequency.AdditionsAndDeletionsByWeek[1].Deletions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var expectedEndPoint = new Uri("repositories/1/stats/code_frequency", UriKind.Relative);

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

                var codeFrequency = await statisticsClient.GetCodeFrequency(1);

                Assert.Equal(2, codeFrequency.AdditionsAndDeletionsByWeek.Count);
                Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(firstTimestamp), codeFrequency.AdditionsAndDeletionsByWeek[0].Timestamp);
                Assert.Equal(10, codeFrequency.AdditionsAndDeletionsByWeek[0].Additions);
                Assert.Equal(52, codeFrequency.AdditionsAndDeletionsByWeek[0].Deletions);
                Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(secondTimestamp), codeFrequency.AdditionsAndDeletionsByWeek[1].Timestamp);
                Assert.Equal(0, codeFrequency.AdditionsAndDeletionsByWeek[1].Additions);
                Assert.Equal(9, codeFrequency.AdditionsAndDeletionsByWeek[1].Deletions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithCancellationToken()
            {
                var expectedEndPoint = new Uri("repos/owner/name/stats/code_frequency", UriKind.Relative);

                var cancellationToken = new CancellationToken();
                long firstTimestamp = 159670861;
                long secondTimestamp = 0;
                IReadOnlyList<long[]> data = new ReadOnlyCollection<long[]>(new[]
                {
                    new[] { firstTimestamp, 10, 52 },
                    new[] { secondTimestamp, 0, 9 }
                });

                var connection = Substitute.For<IApiConnection>();
                connection.GetQueuedOperation<long[]>(expectedEndPoint, cancellationToken)
                    .Returns(Task.FromResult(data));
                var client = new StatisticsClient(connection);

                var codeFrequency = await client.GetCodeFrequency("owner", "name", cancellationToken);

                Assert.Equal(2, codeFrequency.AdditionsAndDeletionsByWeek.Count);
                Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(firstTimestamp), codeFrequency.AdditionsAndDeletionsByWeek[0].Timestamp);
                Assert.Equal(10, codeFrequency.AdditionsAndDeletionsByWeek[0].Additions);
                Assert.Equal(52, codeFrequency.AdditionsAndDeletionsByWeek[0].Deletions);
                Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(secondTimestamp), codeFrequency.AdditionsAndDeletionsByWeek[1].Timestamp);
                Assert.Equal(0, codeFrequency.AdditionsAndDeletionsByWeek[1].Additions);
                Assert.Equal(9, codeFrequency.AdditionsAndDeletionsByWeek[1].Deletions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithCancellationToken()
            {
                var expectedEndPoint = new Uri("repositories/1/stats/code_frequency", UriKind.Relative);

                var cancellationToken = new CancellationToken();
                long firstTimestamp = 159670861;
                long secondTimestamp = 0;
                IReadOnlyList<long[]> data = new ReadOnlyCollection<long[]>(new[]
                {
                    new[] { firstTimestamp, 10, 52 },
                    new[] { secondTimestamp, 0, 9 }
                });

                var connection = Substitute.For<IApiConnection>();
                connection.GetQueuedOperation<long[]>(expectedEndPoint, cancellationToken)
                    .Returns(Task.FromResult(data));
                var client = new StatisticsClient(connection);

                var codeFrequency = await client.GetCodeFrequency(1, cancellationToken);

                Assert.Equal(2, codeFrequency.AdditionsAndDeletionsByWeek.Count);
                Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(firstTimestamp), codeFrequency.AdditionsAndDeletionsByWeek[0].Timestamp);
                Assert.Equal(10, codeFrequency.AdditionsAndDeletionsByWeek[0].Additions);
                Assert.Equal(52, codeFrequency.AdditionsAndDeletionsByWeek[0].Deletions);
                Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(secondTimestamp), codeFrequency.AdditionsAndDeletionsByWeek[1].Timestamp);
                Assert.Equal(0, codeFrequency.AdditionsAndDeletionsByWeek[1].Additions);
                Assert.Equal(9, codeFrequency.AdditionsAndDeletionsByWeek[1].Deletions);
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new StatisticsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetCodeFrequency(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetCodeFrequency("owner", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetCodeFrequency("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetCodeFrequency("owner", ""));
            }
        }

        public class TheGetWeeklyCommitCountsMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var expectedEndPoint = new Uri("repos/owner/name/stats/participation", UriKind.Relative);

                var client = Substitute.For<IApiConnection>();
                var statisticsClient = new StatisticsClient(client);

                statisticsClient.GetParticipation("owner", "name");

                client.Received().GetQueuedOperation<Participation>(expectedEndPoint, Args.CancellationToken);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var expectedEndPoint = new Uri("repositories/1/stats/participation", UriKind.Relative);

                var client = Substitute.For<IApiConnection>();
                var statisticsClient = new StatisticsClient(client);

                statisticsClient.GetParticipation(1);

                client.Received().GetQueuedOperation<Participation>(expectedEndPoint, Args.CancellationToken);
            }

            [Fact]
            public void RequestsCorrectUrlWithCancellationToken()
            {
                var expectedEndPoint = new Uri("repos/owner/name/stats/participation", UriKind.Relative);
                var cancellationToken = new CancellationToken();

                var client = Substitute.For<IApiConnection>();
                var statisticsClient = new StatisticsClient(client);

                statisticsClient.GetParticipation("owner", "name", cancellationToken);

                client.Received().GetQueuedOperation<Participation>(expectedEndPoint, cancellationToken);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryIdWithCancellationToken()
            {
                var expectedEndPoint = new Uri("repositories/1/stats/participation", UriKind.Relative);
                var cancellationToken = new CancellationToken();

                var client = Substitute.For<IApiConnection>();
                var statisticsClient = new StatisticsClient(client);

                statisticsClient.GetParticipation(1, cancellationToken);

                client.Received().GetQueuedOperation<Participation>(expectedEndPoint, cancellationToken);
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new StatisticsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetParticipation(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetParticipation("owner", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetParticipation("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetParticipation("owner", ""));
            }
        }

        public class TheGetHourlyCommitCountsMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var expectedEndPoint = new Uri("repos/owner/name/stats/punch_card", UriKind.Relative);

                var client = Substitute.For<IApiConnection>();
                IReadOnlyList<int[]> data = new ReadOnlyCollection<int[]>(new[] { new[] { 2, 8, 42 } });
                client.GetQueuedOperation<int[]>(expectedEndPoint, Args.CancellationToken)
                    .Returns(Task.FromResult(data));
                var statisticsClient = new StatisticsClient(client);

                var result = await statisticsClient.GetPunchCard("owner", "name");

                Assert.Single(result.PunchPoints);
                Assert.Equal(DayOfWeek.Tuesday, result.PunchPoints[0].DayOfWeek);
                Assert.Equal(8, result.PunchPoints[0].HourOfTheDay);
                Assert.Equal(42, result.PunchPoints[0].CommitCount);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var expectedEndPoint = new Uri("repositories/1/stats/punch_card", UriKind.Relative);

                var client = Substitute.For<IApiConnection>();
                IReadOnlyList<int[]> data = new ReadOnlyCollection<int[]>(new[] { new[] { 2, 8, 42 } });
                client.GetQueuedOperation<int[]>(expectedEndPoint, Args.CancellationToken)
                    .Returns(Task.FromResult(data));
                var statisticsClient = new StatisticsClient(client);

                var result = await statisticsClient.GetPunchCard(1);

                Assert.Single(result.PunchPoints);
                Assert.Equal(DayOfWeek.Tuesday, result.PunchPoints[0].DayOfWeek);
                Assert.Equal(8, result.PunchPoints[0].HourOfTheDay);
                Assert.Equal(42, result.PunchPoints[0].CommitCount);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithCancellationToken()
            {
                var expectedEndPoint = new Uri("repos/owner/name/stats/punch_card", UriKind.Relative);
                var cancellationToken = new CancellationToken();

                var connection = Substitute.For<IApiConnection>();
                IReadOnlyList<int[]> data = new ReadOnlyCollection<int[]>(new[] { new[] { 2, 8, 42 } });

                connection.GetQueuedOperation<int[]>(expectedEndPoint, cancellationToken)
                    .Returns(Task.FromResult(data));
                var client = new StatisticsClient(connection);

                var result = await client.GetPunchCard("owner", "name", cancellationToken);

                Assert.Single(result.PunchPoints);
                Assert.Equal(DayOfWeek.Tuesday, result.PunchPoints[0].DayOfWeek);
                Assert.Equal(8, result.PunchPoints[0].HourOfTheDay);
                Assert.Equal(42, result.PunchPoints[0].CommitCount);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithCancellationToken()
            {
                var expectedEndPoint = new Uri("repositories/1/stats/punch_card", UriKind.Relative);
                var cancellationToken = new CancellationToken();

                var connection = Substitute.For<IApiConnection>();
                IReadOnlyList<int[]> data = new ReadOnlyCollection<int[]>(new[] { new[] { 2, 8, 42 } });

                connection.GetQueuedOperation<int[]>(expectedEndPoint, cancellationToken)
                    .Returns(Task.FromResult(data));
                var client = new StatisticsClient(connection);

                var result = await client.GetPunchCard(1, cancellationToken);

                Assert.Single(result.PunchPoints);
                Assert.Equal(DayOfWeek.Tuesday, result.PunchPoints[0].DayOfWeek);
                Assert.Equal(8, result.PunchPoints[0].HourOfTheDay);
                Assert.Equal(42, result.PunchPoints[0].CommitCount);
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new StatisticsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetPunchCard(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetPunchCard("owner", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetPunchCard("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetPunchCard("owner", ""));
            }
        }
    }
}
