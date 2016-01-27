using System;
using System.Security.Policy;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class CommitStatusClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CommitStatusClient(connection);

                client.GetAll("fake", "repo", "sha");

                connection.Received()
                    .GetAll<CommitStatus>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/sha/statuses"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new CommitStatusClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentException>(() =>
                    client.GetAll("", "name", "sha"));
                await Assert.ThrowsAsync<ArgumentException>(() =>
                    client.GetAll("owner", "", "sha"));
                await Assert.ThrowsAsync<ArgumentException>(() =>
                    client.GetAll("owner", "name", ""));
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    client.GetAll(null, "name", "sha"));
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    client.GetAll("owner", null, "sha"));
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    client.GetAll("owner", "name", null));
            }
        }

        public class TheGetCombinedMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CommitStatusClient(connection);

                client.GetCombined("fake", "repo", "sha");

                connection.Received()
                    .Get<CombinedCommitStatus>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/sha/status"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new CommitStatusClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentException>(() =>
                    client.GetCombined("", "name", "sha"));
                await Assert.ThrowsAsync<ArgumentException>(() =>
                    client.GetCombined("owner", "", "sha"));
                await Assert.ThrowsAsync<ArgumentException>(() =>
                    client.GetCombined("owner", "name", ""));
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    client.GetCombined(null, "name", "sha"));
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    client.GetCombined("owner", null, "sha"));
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    client.GetCombined("owner", "name", null));
            }
        }

        public class TheCreateMethodForUser
        {
            [Fact]
            public void PostsToTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CommitStatusClient(connection);

                client.Create("owner", "repo", "sha", new NewCommitStatus { State = CommitState.Success });

                connection.Received().Post<CommitStatus>(Arg.Is<Uri>(u =>
                    u.ToString() == "repos/owner/repo/statuses/sha"),
                    Arg.Is<NewCommitStatus>(s => s.State == CommitState.Success));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new CommitStatusClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentException>(() =>
                    client.Create("", "name", "sha", new NewCommitStatus()));
                await Assert.ThrowsAsync<ArgumentException>(() =>
                    client.Create("owner", "", "sha", new NewCommitStatus()));
                await Assert.ThrowsAsync<ArgumentException>(() =>
                    client.Create("owner", "name", "", new NewCommitStatus()));
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    client.Create(null, "name", "sha", new NewCommitStatus()));
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    client.Create("owner", null, "sha", new NewCommitStatus()));
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    client.Create("owner", "name", null, new NewCommitStatus()));
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    client.Create("owner", "name", "sha", null));
            }
        }

        public class TheConstructor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new CommitStatusClient(null));
            }
        }
    }
}
