using System;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableCommitsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableCommitsClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCommitsClient(gitHubClient);

                client.Get("owner", "name", "reference");

                gitHubClient.Git.Commit.Received(1).Get("owner", "name", "reference");
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCommitsClient(gitHubClient);

                client.Get(1, "reference");

                gitHubClient.Git.Commit.Received(1).Get(1, "reference");
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableCommitsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Get(null, "name", ""));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", null, ""));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.Get(1, null));

                Assert.Throws<ArgumentException>(() => client.Get("", "name", "reference"));
                Assert.Throws<ArgumentException>(() => client.Get("owner", "", "reference"));
                Assert.Throws<ArgumentException>(() => client.Get("owner", "name", ""));

                Assert.Throws<ArgumentException>(() => client.Get(1, ""));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task PostsToTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCommitsClient(gitHubClient);

                var newCommit = new NewCommit("message", "tree", new[] { "parent1", "parent2" });

                client.Create("owner", "name", newCommit);

                gitHubClient.Git.Commit.Received().Create("owner", "name", newCommit);
            }

            [Fact]
            public async Task PostsToTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCommitsClient(gitHubClient);

                var newCommit = new NewCommit("message", "tree", new[] { "parent1", "parent2" });

                client.Create(1, newCommit);

                gitHubClient.Git.Commit.Received().Create(1, newCommit);
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ObservableCommitsClient(Substitute.For<IGitHubClient>());
                var newCommit = new NewCommit("message", "tree", new[] { "parent1", "parent2" });

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "name", newCommit));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", null, newCommit));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.Create(1, null));

                Assert.Throws<ArgumentException>(() => client.Create("", "name", newCommit));
                Assert.Throws<ArgumentException>(() => client.Create("owner", "", newCommit));
            }
        }
    }
}