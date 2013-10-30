using System;
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
                    .GetAll<CommitStatus>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/statuses/sha"), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new CommitStatusClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentException>(async () =>
                    await client.GetAll("", "name", "sha"));
                await AssertEx.Throws<ArgumentException>(async () =>
                    await client.GetAll("owner", "", "sha"));
                await AssertEx.Throws<ArgumentException>(async () =>
                    await client.GetAll("owner", "name", ""));
                await AssertEx.Throws<ArgumentNullException>(async () =>
                    await client.GetAll(null, "name", "sha"));
                await AssertEx.Throws<ArgumentNullException>(async () =>
                    await client.GetAll("owner", null, "sha"));
                await AssertEx.Throws<ArgumentNullException>(async () =>
                    await client.GetAll("owner", "name", null));
            }
        }

        public class TheCreateMethodForUser
        {
            [Fact]
            public void PostsToTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CommitStatusClient(connection);

                client.Create("owner", "repo", "sha", new NewCommitStatus {  State = CommitState.Success });

                connection.Received().Post<CommitStatus>(Arg.Is<Uri>(u => 
                    u.ToString() == "repos/owner/repo/statuses/sha"),
                    Arg.Is<NewCommitStatus>(s => s.State == CommitState.Success));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new CommitStatusClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentException>(async () =>
                    await client.Create("", "name", "sha", new NewCommitStatus()));
                await AssertEx.Throws<ArgumentException>(async () =>
                    await client.Create("owner", "", "sha", new NewCommitStatus()));
                await AssertEx.Throws<ArgumentException>(async () =>
                    await client.Create("owner", "name", "", new NewCommitStatus()));
                await AssertEx.Throws<ArgumentNullException>(async () =>
                    await client.Create(null, "name", "sha", new NewCommitStatus()));
                await AssertEx.Throws<ArgumentNullException>(async () =>
                    await client.Create("owner", null, "sha", new NewCommitStatus()));
                await AssertEx.Throws<ArgumentNullException>(async () =>
                    await client.Create("owner", "name", null, new NewCommitStatus()));
                await AssertEx.Throws<ArgumentNullException>(async () =>
                    await client.Create("owner", "name", "sha", null));
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
