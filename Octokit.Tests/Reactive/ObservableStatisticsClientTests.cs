using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableStatisticsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableStatisticsClient(null));
            }
        }

        public class TheGetContributorsMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var statisticsClient = new ObservableStatisticsClient(gitHubClient);

                statisticsClient.GetContributors("owner", "name");

                gitHubClient.Repository.Statistics.Received().GetContributors("owner", "name");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var statisticsClient = new ObservableStatisticsClient(gitHubClient);

                statisticsClient.GetContributors(1);

                gitHubClient.Repository.Statistics.Received().GetContributors(1);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableStatisticsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetContributors("owner", null));
                Assert.Throws<ArgumentNullException>(() => client.GetContributors(null, "name"));

                Assert.Throws<ArgumentException>(() => client.GetContributors("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetContributors("owner", ""));
            }
        }

        public class TheGetCommitActivityMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var statisticsClient = new ObservableStatisticsClient(gitHubClient);

                statisticsClient.GetCommitActivity("owner", "name");

                gitHubClient.Repository.Statistics.Received().GetCommitActivity("owner", "name");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var statisticsClient = new ObservableStatisticsClient(gitHubClient);

                statisticsClient.GetCommitActivity(1);

                gitHubClient.Repository.Statistics.Received().GetCommitActivity(1);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableStatisticsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetCommitActivity("owner", null));
                Assert.Throws<ArgumentNullException>(() => client.GetCommitActivity(null, "name"));

                Assert.Throws<ArgumentException>(() => client.GetCommitActivity("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetCommitActivity("owner", ""));
            }
        }

        public class TheGetCodeFrequencyMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var statisticsClient = new ObservableStatisticsClient(gitHubClient);

                statisticsClient.GetCodeFrequency("owner", "name");

                gitHubClient.Repository.Statistics.Received().GetCodeFrequency("owner", "name");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var statisticsClient = new ObservableStatisticsClient(gitHubClient);

                statisticsClient.GetCodeFrequency(1);

                gitHubClient.Repository.Statistics.Received().GetCodeFrequency(1);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableStatisticsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetCodeFrequency("owner", null));
                Assert.Throws<ArgumentNullException>(() => client.GetCodeFrequency(null, "name"));

                Assert.Throws<ArgumentException>(() => client.GetCodeFrequency("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetCodeFrequency("owner", ""));
            }
        }

        public class TheGetParticipationMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var statisticsClient = new ObservableStatisticsClient(gitHubClient);

                statisticsClient.GetParticipation("owner", "name");

                gitHubClient.Repository.Statistics.Received().GetParticipation("owner", "name");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var statisticsClient = new ObservableStatisticsClient(gitHubClient);

                statisticsClient.GetParticipation(1);

                gitHubClient.Repository.Statistics.Received().GetParticipation(1);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableStatisticsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetParticipation("owner", null));
                Assert.Throws<ArgumentNullException>(() => client.GetParticipation(null, "name"));

                Assert.Throws<ArgumentException>(() => client.GetParticipation("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetParticipation("owner", ""));
            }
        }

        public class TheGetPunchCardMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var statisticsClient = new ObservableStatisticsClient(gitHubClient);

                statisticsClient.GetPunchCard("owner", "name");

                gitHubClient.Repository.Statistics.Received().GetPunchCard("owner", "name");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var statisticsClient = new ObservableStatisticsClient(gitHubClient);

                statisticsClient.GetPunchCard(1);

                gitHubClient.Repository.Statistics.Received().GetPunchCard(1);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableStatisticsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetPunchCard("owner", null));
                Assert.Throws<ArgumentNullException>(() => client.GetPunchCard(null, "name"));

                Assert.Throws<ArgumentException>(() => client.GetPunchCard("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetPunchCard("owner", ""));
            }
        }
    }
}
