using System;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
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

        public class TheGetMethod
        {
            [Fact]
            public void GetsFromClientIssueMilestone()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var statisticsClient = new ObservableStatisticsClient(gitHubClient);

                statisticsClient.GetContributors("username", "repositoryName");

                gitHubClient.Repository.Statistics.Received().GetContributors("username", "repositoryName");
            }

            [Fact]
            public async Task ThrowsIfGivenNullRepositoryName()
            {
                var statisticsClient = new ObservableStatisticsClient(Substitute.For<IGitHubClient>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => statisticsClient.GetContributors("owner", null).ToTask());
            }

            [Fact]
            public async Task ThrowsIfGivenNullOwnerName()
            {
                var statisticsClient = new ObservableStatisticsClient(Substitute.For<IGitHubClient>());
                await Assert.ThrowsAsync<ArgumentNullException>(() => statisticsClient.GetContributors(null, "repositoryName").ToTask());
            }
        }
    }
}