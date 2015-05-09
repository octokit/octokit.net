using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class RepositoryForksClientTests
    {
        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.Forks.GetAll("fake", "repo", null);

                connection.Received().GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/forks"));
            }

            [Fact]
            public void RequestsCorrectUrlWithRequestParameters()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.Forks.GetAll("fake", "repo", new RepositoryForksListRequest { Sort = Sort.Stargazers });

                connection.Received().GetAll<Repository>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/forks"),
                    Arg.Is<Dictionary<string, string>>(d => d["sort"] == "stargazers"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Forks.GetAll(null, "name", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Forks.GetAll("owner", null, null));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var newRepositoryFork = new NewRepositoryFork();

                client.Forks.Create("fake", "repo", newRepositoryFork);

                connection.Received().Post<Repository>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/forks"), newRepositoryFork);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Forks.Create(null, "name", new NewRepositoryFork()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Forks.Create("owner", null, new NewRepositoryFork()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Forks.Create("owner", "name", null));
            }

            [Fact]
            public void UsesTheSuppliedHook()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var newRepositoryFork = new NewRepositoryFork { Organization = "aName" };

                client.Forks.Create("owner", "repo", newRepositoryFork);

                connection.Received().Post<Repository>(Arg.Any<Uri>(), newRepositoryFork);
            }
        }
    }
}