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

        public class TheCreateMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var hook = new NewRepositoryHook();

                client.Hooks.Create("fake", "repo", hook);

                connection.Received().Post<RepositoryHook>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/hooks"), hook);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Hooks.Create(null, "name", new NewRepositoryHook()));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Hooks.Create("owner", null, new NewRepositoryHook()));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Hooks.Create("owner", "name", null));
            }

            [Fact]
            public void UsesTheSuppliedHook()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var newRepositoryHook = new NewRepositoryHook { Name = "aName" };

                client.Hooks.Create("owner", "repo", newRepositoryHook);

                connection.Received().Post<RepositoryHook>(Arg.Any<Uri>(), newRepositoryHook);
            }
        }

        public class TheEditMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var hook = new EditRepositoryHook();

                client.Hooks.Edit("fake", "repo", 12345678, hook);

                connection.Received().Patch<RepositoryHook>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/hooks/12345678"), hook);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Hooks.Edit(null, "name", 12345678, new EditRepositoryHook()));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Hooks.Edit("owner", null, 12345678, new EditRepositoryHook()));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Hooks.Edit("owner", "name", 12345678, null));
            }

            [Fact]
            public void UsesTheSuppliedHook()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var editRepositoryHook = new EditRepositoryHook() { Active = false };

                client.Hooks.Edit("owner", "repo", 12345678, editRepositoryHook);

                connection.Received().Patch<RepositoryHook>(Arg.Any<Uri>(), editRepositoryHook);
            }
        }

        public class TheTestMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.Hooks.Test("fake", "repo", 12345678);

                connection.Received().Post<TestRepositoryHook>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/hooks/12345678/tests"), Arg.Any<TestRepositoryHook>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Hooks.Test(null, "name", 12345678));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Hooks.Test("owner", null, 12345678));
            }

            [Fact]
            public void CallsPost()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.Hooks.Test("owner", "repo", 12345678);

                connection.Received().Post<TestRepositoryHook>(Arg.Any<Uri>(), Arg.Any<TestRepositoryHook>());
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.Hooks.Delete("fake", "repo", 12345678);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/hooks/12345678"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Hooks.Delete(null, "name", 12345678));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Hooks.Delete("owner", null, 12345678));
            }
        }
    }
}
