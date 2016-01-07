using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableCommitsClientTests
    {
        public class TheCtorMethod
        {
            [Fact]
            public void EnsuresArgumentIsNotNulll()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableCommitsClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ObservableCommitsClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", "").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, "").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", "name", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "name", "reference").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", "reference").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "name", "").ToTask());
            }

            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCommitsClient(gitHubClient);

                client.Get("owner", "name", "reference");

                gitHubClient.Git.Commit.Received(1).Get("owner", "name", "reference");
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ObservableCommitsClient(Substitute.For<IGitHubClient>());
                var newCommit = new NewCommit("message", "tree", new[] { "parent1", "parent2" });

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", newCommit).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, newCommit).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", newCommit).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", newCommit).ToTask());
            }

            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCommitsClient(gitHubClient);
                var newCommit = new NewCommit("message", "tree", new[] { "parent1", "parent2" });

                client.Create("owner", "name", newCommit);

                gitHubClient.Git.Commit.Received().Create("owner", "name", newCommit);
            }
        }
    }
}