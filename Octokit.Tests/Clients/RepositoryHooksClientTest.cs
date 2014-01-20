using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class RepositoryHooksClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.Hooks.Get("fake", "repo");

                connection.Received().GetAll<RepositoryHook>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/hooks"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Hooks.Get(null, "name"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Hooks.Get("owner", null));
            }
        }

        public class TheGetByIdMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.Hooks.GetById("fake", "repo", 12345678);

                connection.Received().Get<RepositoryHook>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/hooks/12345678"), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Hooks.GetById(null, "name", 123));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Hooks.GetById("owner", null, 123));
            }
        }
    }
}
