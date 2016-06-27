using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class ObservableMergingClientTests
    {
        public class TheCreateMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMergingClient(gitHubClient);

                var newMerge = new NewMerge("baseBranch", "shaToMerge")
                {
                    CommitMessage = "some mergingMessage"
                };
                client.Create("owner", "repo", newMerge);

                gitHubClient.Repository.Merging.Received(1).Create("owner", "repo", newMerge);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableMergingClient(gitHubClient);

                var newMerge = new NewMerge("baseBranch", "shaToMerge")
                {
                    CommitMessage = "some mergingMessage"
                };
                client.Create(1, newMerge);

                gitHubClient.Repository.Merging.Received(1).Create(1, newMerge);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableMergingClient(Substitute.For<IGitHubClient>());

                var newMerge = new NewMerge("baseBranch", "shaToMerge") { CommitMessage = "some mergingMessage" };

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "name", newMerge));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", null, newMerge));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.Create(1, null));

                Assert.Throws<ArgumentException>(() => client.Create("", "name", newMerge));
                Assert.Throws<ArgumentException>(() => client.Create("owner", "", newMerge));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableMergingClient(null));
            }
        }
    }
}
