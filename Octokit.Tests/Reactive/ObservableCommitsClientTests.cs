using System;
using System.Collections.Generic;
using System.Reactive.Linq;
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

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "name", ""));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", null, ""));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", "name", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get("", "name", "reference"));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get("owner", "", "reference"));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get("owner", "name", ""));                
            }
 
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCommitsClient(gitHubClient);

                client.Get("owner", "name", "reference");

                gitHubClient.GitDatabase.Commit.Received(1).Get("owner", "name", "reference");
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ObservableCommitsClient(Substitute.For<IGitHubClient>());
                var newCommit = new NewCommit("message", "tree", new[] { "parent1", "parent2" });

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create(null, "name", newCommit));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", null, newCommit));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", "name", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.Create("", "name", newCommit));
                await AssertEx.Throws<ArgumentException>(async () => await client.Create("owner", "", newCommit));
            }
 
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableCommitsClient(gitHubClient);
                var newCommit = new NewCommit("message", "tree", new[] { "parent1", "parent2" });

                client.Create("owner", "name", newCommit);

                gitHubClient.GitDatabase.Commit.Received().Create("owner", "name", newCommit);
            }
        }
    }
}