using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class ObservableCheckRunsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableCheckRunsClient(null));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckRunsClient(gitHubClient);

                var newCheckRun = new NewCheckRun("status", "123abc") { Status = CheckStatus.Queued };

                client.Create("fake", "repo", newCheckRun);

                gitHubClient.Check.Run.Received().Create("fake", "repo", newCheckRun);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckRunsClient(gitHubClient);

                var newCheckRun = new NewCheckRun("status", "123abc") { Status = CheckStatus.Queued };

                client.Create(1, newCheckRun);

                gitHubClient.Check.Run.Received().Create(1, newCheckRun);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckRunsClient(gitHubClient);

                var newCheckRun = new NewCheckRun("status", "123abc") { Status = CheckStatus.Queued };

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "repo", newCheckRun));
                Assert.Throws<ArgumentNullException>(() => client.Create("fake", null, newCheckRun));
                Assert.Throws<ArgumentNullException>(() => client.Create("fake", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.Create(1, null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCheckRunsClient(gitHubClient);

                var newCheckRun = new NewCheckRun("status", "123abc") { Status = CheckStatus.Queued };

                Assert.Throws<ArgumentException>(() => client.Create("", "repo", newCheckRun));
                Assert.Throws<ArgumentException>(() => client.Create("fake", "", newCheckRun));
            }
        }
    }
}