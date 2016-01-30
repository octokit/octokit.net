using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class RepositoryHooksClientTests
    {
        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.Hooks.GetAll("fake", "repo");

                connection.Received().GetAll<RepositoryHook>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/hooks"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hooks.GetAll(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hooks.GetAll("owner", null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.Hooks.Get("fake", "repo", 12345678);

                connection.Received().Get<RepositoryHook>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/hooks/12345678"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hooks.Get(null, "name", 123));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hooks.Get("owner", null, 123));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var hook = new NewRepositoryHook("name", new Dictionary<string, string> { { "config", "" } });

                client.Hooks.Create("fake", "repo", hook);

                connection.Received().Post<RepositoryHook>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/hooks"), hook);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                var config = new Dictionary<string, string> { { "config", "" } };
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hooks.Create(null, "name", new NewRepositoryHook("name", config)));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hooks.Create("owner", null, new NewRepositoryHook("name", config)));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hooks.Create("owner", "name", null));
            }

            [Fact]
            public void UsesTheSuppliedHook()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);
                var newRepositoryHook = new NewRepositoryHook("name", new Dictionary<string, string> { { "config", "" } });

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

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hooks.Edit(null, "name", 12345678, new EditRepositoryHook()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hooks.Edit("owner", null, 12345678, new EditRepositoryHook()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hooks.Edit("owner", "name", 12345678, null));
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

                connection.Received().Post(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/hooks/12345678/tests"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hooks.Test(null, "name", 12345678));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hooks.Test("owner", null, 12345678));
            }
        }

        public class ThePingMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoriesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hooks.Ping(null, "name", 12345678));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hooks.Ping("owner", null, 12345678));
            }

            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoriesClient(connection);

                client.Hooks.Ping("fake", "repo", 12345678);

                connection.Received().Post(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/hooks/12345678/pings"));
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

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hooks.Delete(null, "name", 12345678));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hooks.Delete("owner", null, 12345678));
            }
        }
    }
}
