using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class CheckRunsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new CheckRunsClient(null));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                var newCheckRun = new NewCheckRun("status", "123abc") { Status = CheckStatus.Queued };

                await client.Create("fake", "repo", newCheckRun);

                connection.Received().Post<CheckRun>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/check-runs"),
                    newCheckRun,
                    "application/vnd.github.antiope-preview+json");
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                var newCheckRun = new NewCheckRun("status", "123abc") { Status = CheckStatus.Queued };

                await client.Create(1, newCheckRun);

                connection.Received().Post<CheckRun>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/check-runs"),
                    newCheckRun,
                    "application/vnd.github.antiope-preview+json");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                var newCheckRun = new NewCheckRun("status", "123abc") { Status = CheckStatus.Queued };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "repo", newCheckRun));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("fake", null, newCheckRun));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("fake", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new CheckRunsClient(connection);

                var newCheckRun = new NewCheckRun("status", "123abc") { Status = CheckStatus.Queued };

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "repo", newCheckRun));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("fake", "", newCheckRun));
            }
        }
    }
}