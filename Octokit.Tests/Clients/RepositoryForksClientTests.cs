using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class RepositoryForksClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.Forks.Get("fake", "repo");

                connection.Received().GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/forks"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Forks.Get(null, "name"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Forks.Get("owner", null));
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

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Forks.Create(null, "name", new NewRepositoryFork()));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Forks.Create("owner", null, new NewRepositoryFork()));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Forks.Create("owner", "name", null));
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